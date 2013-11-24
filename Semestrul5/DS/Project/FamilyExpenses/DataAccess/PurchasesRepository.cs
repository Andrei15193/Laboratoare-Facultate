using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using FamilyExpenses.Model;

namespace FamilyExpenses.DataAccess
{
    internal class PurchasesRepository
        : IPurchasesRepository
    {
        #region IPurchasesRepository Members

        public int TotalCost(Person person)
        {
            throw new NotImplementedException();
        }

        public void Import()
        {
            SqlInterface.Export("Andrei", "Andrei.xml");
        }

        public void Export()
        {
            SqlInterface.Import("Andrei", "Andrei.xml", "Schema.xsd");
        }

        public IReadOnlyCollection<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Producer> GetProducers()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Shop> GetShops()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Address> GetAddresses()
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Purchase> GetPurchasesBetweenDates(Person person, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Purchase> GetPurchasesFrom(Person person, Producer producer)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Purchase> GetPurchasesFrom(Person person, Shop shop)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyCollection<Purchase> GetPurchases(Person person, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public void Add(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        public void Update(Purchase oldPurchase, Purchase newPurchase)
        {
            throw new NotImplementedException();
        }

        public void Delete(Purchase purchase)
        {
            throw new NotImplementedException();
        }

        #endregion

        private static class SqlInterface
        {
            public static void Import(string personName, string xmlFileName, string xsdFileName)
            {
                DataSet dataSet = new DataSet("familyExpenses");

                dataSet.ReadXmlSchema(xsdFileName);
                dataSet.ReadXml(xmlFileName);
                _dataAdapter.SelectCommand.Parameters["@personName"].Value = personName;
                foreach (string tableName in new[] {"Persons", "Incomes", "Addresses", "Shops", "Producers", "Products", "Purchases"})
                {
                    _dataAdapter.InsertCommand = _insertCommands[tableName];
                    _dataAdapter.UpdateCommand = _updateCommands[tableName];
                    _dataAdapter.DeleteCommand = _deleteCommands[tableName];
                    _dataAdapter.Update(dataSet.Tables[tableName]);
                }
            }

            public static void Export(string personName, string xmlFileName, string xsdFileName = null)
            {
                DataSet dataSet = new DataSet("familyExpenses");
                
                _dataAdapter.SelectCommand.Parameters["@personName"].Value = personName;
                _dataAdapter.FillSchema(dataSet, SchemaType.Mapped);
                _SetForeignKeysRelations(dataSet);
                _dataAdapter.Fill(dataSet);

                if (xsdFileName != null)
                    dataSet.WriteXmlSchema(xsdFileName);
                dataSet.WriteXml(xmlFileName);
            }

            private static void _SetForeignKeysRelations(DataSet personDataSet)
            {
                personDataSet.Relations.Add(new DataRelation("fkIncomesToPerson",
                                                             personDataSet.Tables["Persons"].Columns["name"],
                                                             personDataSet.Tables["Incomes"].Columns["person"])
                                                             {
                                                                 Nested = true
                                                             });
                personDataSet.Relations.Add(new DataRelation("fkShopsToAddress",
                                                             personDataSet.Tables["Addresses"].Columns["id"],
                                                             personDataSet.Tables["Shops"].Columns["address"])
                                                             {
                                                                 Nested = true
                                                             });
                personDataSet.Relations.Add(new DataRelation("fkProductsToProducer",
                                                             personDataSet.Tables["Producers"].Columns["name"],
                                                             personDataSet.Tables["Products"].Columns["producer"])
                                                             {
                                                                 Nested = true
                                                             });
                personDataSet.Relations.Add(new DataRelation("fkPurchasesToPerson",
                                                             personDataSet.Tables["Persons"].Columns["name"],
                                                             personDataSet.Tables["Purchases"].Columns["purchaser"])
                                                             {
                                                                 Nested = true
                                                             });
                personDataSet.Relations.Add(new DataRelation("fkPurchasesToProduct",
                                                             personDataSet.Tables["Products"].Columns["name"],
                                                             personDataSet.Tables["Purchases"].Columns["product"]));
                personDataSet.Relations.Add(new DataRelation("fkPurchasesToShop",
                                                             new[]
                                                         {
                                                             personDataSet.Tables["Shops"].Columns["name"],
                                                             personDataSet.Tables["Shops"].Columns["address"]
                                                         },
                                                             new[]
                                                         {
                                                             personDataSet.Tables["Purchases"].Columns["shop"],
                                                             personDataSet.Tables["Purchases"].Columns["shopAddress"]
                                                         }));

                personDataSet.Tables["Incomes"].Columns["person"].ColumnMapping = MappingType.Hidden;
                personDataSet.Tables["Shops"].Columns["address"].ColumnMapping = MappingType.Hidden;
                personDataSet.Tables["Products"].Columns["producer"].ColumnMapping = MappingType.Hidden;
                personDataSet.Tables["Purchases"].Columns["purchaser"].ColumnMapping = MappingType.Hidden;
            }

            private static SqlDataAdapter _CreateAdapter()
            {
                return new SqlDataAdapter
                {
                    MissingSchemaAction = MissingSchemaAction.Add,
                    FillLoadOption = LoadOption.Upsert,
                    SelectCommand = new SqlCommand
                    {
                        CommandType = CommandType.Text,
                        Connection = _sqlConnection,
                        CommandText = @"select Persons.*
                                        from Persons
                                        where name = @personName;

                                        select Incomes.*
                                            from Incomes
                                            where person = @personName

                                        select Addresses.*
                                            from Addresses
                                                inner join
                                                    (select shopAddress
                                                         from Purchases
                                                         where purchaser = @personName) as Purchases
                                                on Addresses.id = Purchases.shopAddress;

                                        select Shops.*
                                            from Shops
                                                inner join
                                                    (select shop, shopAddress
                                                         from Purchases
                                                         where purchaser = @personName) as Purchases
                                                on Shops.name = Purchases.shop
                                                    and Shops.address = Purchases.shopAddress;

                                        select Producers.*
                                            from Producers
                                                inner join
                                                    Products
                                                    inner join
                                                        (select product
                                                             from Purchases
                                                             where purchaser = @personName) as Purchases
                                                    on Products.name = Purchases.product
                                                on Producers.name = Products.producer;

                                        select Products.*
                                            from Products
                                                inner join
                                                    (select product
                                                         from Purchases
                                                         where purchaser = @personName) as Purchases
                                                on Products.name = Purchases.product;

                                        select Purchases.*
                                            from Purchases
                                            where purchaser = @personName;",
                        Parameters =
                        {
                            new SqlParameter("@personName", SqlDbType.VarChar, 100)
                        }
                    },
                    TableMappings =
                    {
                        new DataTableMapping("Table", "Persons"),
                        new DataTableMapping("Table1", "Incomes"),
                        new DataTableMapping("Table2", "Addresses"),
                        new DataTableMapping("Table3", "Shops"),
                        new DataTableMapping("Table4", "Producers"),
                        new DataTableMapping("Table5", "Products"),
                        new DataTableMapping("Table6", "Purchases"),
                        new DataTableMapping("Purchases", "Purchases"),
                        new DataTableMapping("Products", "Products"),
                        new DataTableMapping("Producers", "Producers"),
                        new DataTableMapping("Shops", "Shops"),
                        new DataTableMapping("Addresses", "Addresses"),
                        new DataTableMapping("Incomes", "Incomes"),
                        new DataTableMapping("Persons", "Persons"),
                    }
                };
            }

            private static IDictionary<string, SqlCommand> _GetDeleteCommands()
            {
                IDictionary<string, SqlCommand> deleteCommands = new Dictionary<string, SqlCommand>();

                deleteCommands.Add("Purchases",
                                   new SqlCommand(@"delete from Purchases
                                                    where price = @price 
                                                        and quantity = @quantity
                                                        and shop = @shop
                                                        and shopAddress = @shopAddress
                                                        and purchaser = @purchaser
                                                        and product = @product", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@price", SqlDbType.Int)
                                           {
                                               SourceColumn = "price",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@quantity", SqlDbType.Int)
                                           {
                                               SourceColumn = "quantity",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@shop", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "shop",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@shopAddress", SqlDbType.Int)
                                           {
                                               SourceColumn = "shopAddress",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@purchaser", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "purchaser",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@product", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "product",
                                               SourceVersion = DataRowVersion.Original
                                           }
                                       }
                                   });

                deleteCommands.Add("Products",
                                   new SqlCommand(@"delete from Products
                                                    where name = @name", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           }
                                       }
                                   });

                deleteCommands.Add("Producers",
                                   new SqlCommand(@"delete from Producers
                                                    where name = @name", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           }
                                       }
                                   });

                deleteCommands.Add("Shops",
                                   new SqlCommand(@"delete from Shops
                                                    where name = @name
                                                        and address = @address", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@address", SqlDbType.Int)
                                           {
                                               SourceColumn = "address",
                                               SourceVersion = DataRowVersion.Original
                                           }
                                       }
                                   });

                deleteCommands.Add("Addresses",
                                   new SqlCommand(@"delete from Addresses
                                                    where id = @id", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@id", SqlDbType.Int)
                                           {
                                               SourceColumn = "id",
                                               SourceVersion = DataRowVersion.Original
                                           }
                                       }
                                   });

                deleteCommands.Add("Incomes",
                                   new SqlCommand(@"delete from Incomes
                                                    where dateReceived = @dateReceived
                                                        and sum = @sum
                                                        and person = @person", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@dateReceived", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "dateReceived",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@sum", SqlDbType.Int)
                                           {
                                               SourceColumn = "sum",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@person", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "person",
                                               SourceVersion = DataRowVersion.Original
                                           }
                                       }
                                   });

                deleteCommands.Add("Persons",
                                   new SqlCommand(@"delete from Persons
                                                    where name = @name", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           }
                                       }
                                   });

                return deleteCommands;
            }

            private static IDictionary<string, SqlCommand> _GetUpdateCommands()
            {
                IDictionary<string, SqlCommand> updateCommands = new Dictionary<string, SqlCommand>();

                updateCommands.Add("Purchases",
                                   new SqlCommand(@"update Purchases
                                                        set price = @newPrice,
                                                            quantity = @newQuantity,
                                                            shop = @newShop,
                                                            shopAddress = @newShopAddress,
                                                            purchaser = @newPurchaser,
                                                            product = @newProduct,
                                                        where price = @oldPrice
                                                            and quantity = @oldQuantity
                                                            and shop = @oldShop
                                                            and shopAddress = @oldShopAddress
                                                            and purchaser = @oldPurchaser
                                                            and product = @oldProduct", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@oldPrice", SqlDbType.Int)
                                           {
                                               SourceColumn = "price",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldQuantity", SqlDbType.Int)
                                           {
                                               SourceColumn = "quantity",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldShop", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "shop",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldShopAddress", SqlDbType.Int)
                                           {
                                               SourceColumn = "shopAddress",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldPurchaser", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "purchaser",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldProduct", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "product",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@newPrice", SqlDbType.Int)
                                           {
                                               SourceColumn = "price",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newQuantity", SqlDbType.Int)
                                           {
                                               SourceColumn = "quantity",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newShop", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "shop",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newShopAddress", SqlDbType.Int)
                                           {
                                               SourceColumn = "shopAddress",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newPurchaser", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "purchaser",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newProduct", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "product",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                updateCommands.Add("Products",
                                   new SqlCommand(@"update Products
                                                        set name = @newName,
                                                            type = @type,
                                                            producer = @producer
                                                        where name = @oldName", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@oldName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@newName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@type", SqlDbType.Int)
                                           {
                                               SourceColumn = "type",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@producer", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "producer",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                updateCommands.Add("Producers",
                                   new SqlCommand(@"update Producers
                                                        set name = @newName,
                                                            country = @country
                                                        where name = @oldName", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@oldName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@newName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@country", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "country",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                updateCommands.Add("Shops",
                                   new SqlCommand(@"update Shops
                                                    set name = @newName,
                                                        address = @newAddress,
                                                        type = @type
                                                    where name = @oldName
                                                        and address = @oldAddress", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@oldName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldAddress", SqlDbType.Int)
                                           {
                                               SourceColumn = "address",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@newName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newAddress", SqlDbType.Int)
                                           {
                                               SourceColumn = "address",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@type", SqlDbType.Int)
                                           {
                                               SourceColumn = "type",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                updateCommands.Add("Addresses",
                                   new SqlCommand(@"update Addresses
                                                    set id = @newId,
                                                        street = @street,
                                                        city = @city,
                                                        county = @county,
                                                        country = @country
                                                    where id = @oldId", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@oldId", SqlDbType.Int)
                                           {
                                               SourceColumn = "id",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@newId", SqlDbType.Int)
                                           {
                                               SourceColumn = "id",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@street", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "street",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@city", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "city",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@county", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "county",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@country", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "country",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                updateCommands.Add("Incomes",
                                   new SqlCommand(@"update Incomes
                                                    set dateReceived = @newDateReceived
                                                        sum = @newSum
                                                        person = @newPerson
                                                    where dateReceived = @oldDateReceived
                                                        and sum = @oldSum
                                                        and person = @oldPerson", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@oldDateReceived", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "dateReceived",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldSum", SqlDbType.Int)
                                           {
                                               SourceColumn = "sum",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldPerson", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "person",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@newDateReceived", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "dateReceived",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newSum", SqlDbType.Int)
                                           {
                                               SourceColumn = "sum",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newPerson", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "person",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                updateCommands.Add("Persons",
                                   new SqlCommand(@"update Persons
                                                    set name = @newName,
                                                        preferedCurrency = @preferedCurrency
                                                    where name = @oldName", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@oldName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@newName", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@preferedCurrency", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "preferedCurrency",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                return updateCommands;
            }

            private static IDictionary<string, SqlCommand> _GetInsertCommands()
            {
                IDictionary<string, SqlCommand> insertCommands = new Dictionary<string, SqlCommand>();

                insertCommands.Add("Purchases",
                                   new SqlCommand(@"if not exists (select price
                                                                       from Purchases
                                                                        where price = @price
                                                                           and quantity = @quantity
                                                                           and shop = @shop
                                                                           and shopAddress = @shopAddress
                                                                           and purchaser = @purchaser
                                                                           and product = @product)
                                                        insert into Purchases(price, quantity, shop, shopAddress, purchaser, product)
                                                            values (@price, @quantity, @shop, @shopAddress, @purchaser, @product)", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@price", SqlDbType.Int)
                                           {
                                               SourceColumn = "price",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@quantity", SqlDbType.Int)
                                           {
                                               SourceColumn = "quantity",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@shop", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "shop",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@shopAddress", SqlDbType.Int)
                                           {
                                               SourceColumn = "shopAddress",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@purchaser", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "purchaser",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@product", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "product",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                insertCommands.Add("Products",
                                   new SqlCommand(@"if exists (select name
                                                                   from Products
                                                                   where name = @name)
                                                        update Products
                                                            set type = @type,
                                                                producer = @producer
                                                            where name = @name
                                                    else
                                                        insert into Products(name, type, producer)
                                                            values (@name, @type, @producer)", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Current,
                                           },
                                           new SqlParameter("@type", SqlDbType.Int)
                                           {
                                               SourceColumn = "type",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@producer", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "producer",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                insertCommands.Add("Producers",
                                   new SqlCommand(@"if exists(select name
                                                                  from Producers
                                                                  where name = @name)
                                                        update Producers
                                                            set country = @country
                                                            where name = @name
                                                    else
                                                        insert into Producers(name, country)
                                                            values (@name, @country)", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           { 
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@country", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "country",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                insertCommands.Add("Shops",
                                   new SqlCommand(@"if exists(select name
                                                                  from Shops
                                                                  where name = @name
                                                                      and address = @address)
                                                        update Shops
                                                            set type = @type
                                                            where name = @name
                                                                and address = @address
                                                    else
                                                        insert into Shops(name, address, type)
                                                            values (@name, @address, @type)", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn="name",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@address", SqlDbType.Int)
                                           {
                                               SourceColumn = "address",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@type", SqlDbType.Int)
                                           {
                                               SourceColumn = "type",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                insertCommands.Add("Addresses",
                                   new SqlCommand(@"if exists(select id
                                                                  from Addresses
                                                                  where id = @id)
                                                        update Addresses
                                                            set street = @street,
                                                                city = @city,
                                                                county = @county,
                                                                country = @country
                                                            where id = @id
                                                    else
                                                        insert into Addresses(id, street, city, county, country)
                                                            values (@id, @street, @city, @county, @country)", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@id", SqlDbType.Int)
                                           {
                                               SourceColumn = "id",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@street", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "street",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@city", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "city",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@county", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "county",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@country", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "country",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                       }
                                   });

                insertCommands.Add("Incomes",
                                   new SqlCommand(@"if not exists(select dateReceived
                                                                  from Incomes
                                                                  where dateReceived = @dateReceived
                                                                      and sum = @sum
                                                                      and person = @person)
                                                        insert into Incomes(dateReceived, sum, person)
                                                            values (@dateReceived, @sum, @person)", _sqlConnection)
                                   {
                                       Parameters =
                                       {
                                           new SqlParameter("@dateReceived", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "dateReceived",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@sum", SqlDbType.Int)
                                           {
                                               SourceColumn = "sum",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@person", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "person",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                       }
                                   });

                insertCommands.Add("Persons",
                                   new SqlCommand(@"if exists(select name
                                                                  from Persons
                                                                  where name = @name)
                                                        update Persons
                                                            set preferedCurrency= @preferedCurrency
                                                            where name = @name
                                                    else
                                                        insert into Persons(name, preferedCurrency)
                                                            values (@name, @preferedCurrency)", _sqlConnection)
                                   {
                                       CommandType = CommandType.Text,
                                       Parameters =
                                       {
                                           new SqlParameter("@name", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "name",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@preferedCurrency", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "preferedCurrency",
                                               SourceVersion = DataRowVersion.Current
                                           }
                                       }
                                   });

                return insertCommands;
            }

            private static string _SqlConnectionString
            {
                get
                {
                    return "Server=ANDREILAPTOP; Database=Local; Trusted_Connection=true;";
                }
            }

            private readonly static SqlConnection _sqlConnection = new SqlConnection(_SqlConnectionString);
            private readonly static SqlDataAdapter _dataAdapter = _CreateAdapter();
            private readonly static IReadOnlyDictionary<string, SqlCommand> _insertCommands = new ReadOnlyDictionary<string, SqlCommand>(_GetInsertCommands());
            private readonly static IReadOnlyDictionary<string, SqlCommand> _updateCommands = new ReadOnlyDictionary<string, SqlCommand>(_GetUpdateCommands());
            private readonly static IReadOnlyDictionary<string, SqlCommand> _deleteCommands = new ReadOnlyDictionary<string, SqlCommand>(_GetDeleteCommands());

        }
    }
}
