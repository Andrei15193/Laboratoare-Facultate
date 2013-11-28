using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using FamilyExpenses.Model;

namespace FamilyExpenses.ViewModels
{
    internal sealed class MainViewModel
        : INotifyPropertyChanged
    {
        private static class Cache
        {
            public static Purchase GetPurchase(Person person, XElement purchaseElement, string xmlFileName)
            {
                if (person != null)
                    if (purchaseElement != null)
                        if (xmlFileName != null)
                        {
                            Shop shop = _GetShop(purchaseElement.Element("shop").Value,
                                                 purchaseElement.Element("shopAddress").Value,
                                                 xmlFileName);
                            Product product = _GetProduct(purchaseElement.Element("product").Value,
                                                          purchaseElement.Element("productProducer").Value,
                                                          xmlFileName);

                            return new Purchase(int.Parse(purchaseElement.Element("price").Value),
                                                int.Parse(purchaseElement.Element("quantity").Value),
                                                XmlConvert.ToDateTime(purchaseElement.Element("datePurchased").Value,
                                                                      "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz"),
                                                person,
                                                shop,
                                                product);
                        }
                        else
                            throw new ArgumentNullException("xmlFileName");
                    else
                        throw new ArgumentNullException("purchaseElement");
                else
                    throw new ArgumentNullException("person");
            }

            public static void Clear()
            {
                _producers.Clear();
                _addresses.Clear();
                _products.Clear();
                _shops.Clear();
            }

            internal static IReadOnlyList<Producer> AllProducers(string xmlFileName)
            {
                foreach (XElement producerElement in XDocument.Load(xmlFileName)
                                                              .Root
                                                              .Elements("Producers")
                                                              .Where(producerXElement => !_producers.ContainsKey(producerXElement.Element("name").Value)))
                {
                    string producerName = producerElement.Element("name").Value;

                    _producers.Add(producerName, new Producer(producerName, producerElement.Element("country").Value));
                }

                return _producers.Values.ToList();
            }

            internal static IReadOnlyList<Product> AllProducts(string xmlFileName)
            {
                foreach (XElement producerElement in XDocument.Load(xmlFileName)
                                                              .Root
                                                              .Elements("Producers"))
                    foreach (XElement productElement in producerElement.Elements("Products")
                                                                       .Where(productXElement => !_products.ContainsKey(productXElement.Element("name").Value + "\r\n" + producerElement.Element("name").Value)))
                    {
                        string productName = productElement.Element("name").Value;

                        _products.Add(productName + "\r\n" + producerElement.Element("name").Value, new Product(productName, (ProductType)int.Parse(productElement.Element("type").Value), _GetProducer(producerElement.Element("name").Value, xmlFileName)));
                    }

                return _products.Values.ToList();
            }

            internal static IReadOnlyList<Address> AllAddresses(string xmlFileName)
            {
                foreach (XElement addressElement in XDocument.Load(xmlFileName)
                                                              .Root
                                                              .Elements("Addresses")
                                                              .Where(addressXElement => !_addresses.ContainsKey(addressXElement.Element("id").Value)))
                    _addresses.Add(addressElement.Element("id").Value,
                                   new Address(addressElement.Element("street").Value,
                                               addressElement.Element("city").Value,
                                               addressElement.Element("county").Value,
                                               addressElement.Element("country").Value));

                return _addresses.Values.ToList();
            }

            internal static IReadOnlyList<Shop> AllShopst(string xmlFileName)
            {
                foreach (XElement addressElement in XDocument.Load(xmlFileName)
                                                              .Root
                                                              .Elements("Addresses"))
                    foreach (XElement shopElement in addressElement.Elements("Shops")
                                                                   .Where(shopXElement => !_shops.ContainsKey(shopXElement.Element("name").Value + "\r\n" + addressElement.Element("id").Value)))
                    {
                        string shopName = shopElement.Element("name").Value;

                        _shops.Add(shopName + "\r\n" + addressElement.Element("id").Value,
                                   new Shop(shopName,
                                            (ShopType)int.Parse(shopElement.Element("type").Value),
                                            _GetShopAddress(addressElement.Element("id").Value, xmlFileName)));
                    }

                return _shops.Values.ToList();
            }

            private static Product _GetProduct(string productName, string producerName, string xmlFileName)
            {
                Product product;

                if (!_products.TryGetValue(productName + "\r\n" + producerName, out product))
                {
                    Producer producer = _GetProducer(producerName, xmlFileName);
                    XElement productElement = XDocument.Load(xmlFileName)
                                                       .Root
                                                       .Elements("Producers")
                                                       .First(addressXElement => addressXElement.Element("name").Value == producerName)
                                                       .Elements("Products")
                                                       .First(prdocutXElement => prdocutXElement.Element("name").Value == productName);

                    product = new Product(productElement.Element("name").Value, (ProductType)int.Parse(productElement.Element("type").Value), producer);
                    _products.Add(productName + "\r\n" + producerName, product);
                }

                return product;
            }

            private static Producer _GetProducer(string producerName, string xmlFileName)
            {
                Producer producer;

                if (!_producers.TryGetValue(producerName, out producer))
                {
                    XElement producerElement = XDocument.Load(xmlFileName)
                                                        .Root
                                                        .Elements("Producers")
                                                        .First(addressXElement => addressXElement.Element("name").Value == producerName);

                    producer = new Producer(producerElement.Element("name").Value, producerElement.Element("country").Value);
                    _producers.Add(producer.Name, producer);
                }

                return producer;
            }

            private static Shop _GetShop(string shopName, string shopAddressId, string xmlFileName)
            {
                Shop shop;

                if (!_shops.TryGetValue(shopName + "\r\n" + shopAddressId, out shop))
                {
                    Address shopAddress = _GetShopAddress(shopAddressId, xmlFileName);
                    XElement shopElement = XDocument.Load(xmlFileName)
                                                    .Root
                                                    .Elements("Addresses")
                                                    .First(addressXElement => addressXElement.Element("id").Value == shopAddressId)
                                                    .Elements("Shops")
                                                    .First(shopXElement => shopXElement.Element("name").Value == shopName);

                    shop = new Shop(shopElement.Element("name").Value, (ShopType)int.Parse(shopElement.Element("type").Value), shopAddress);
                    _shops.Add(shopName + "\r\n" + shopAddressId, shop);
                }

                return shop;
            }

            private static Address _GetShopAddress(string id, string xmlFileName)
            {
                Address address;

                if (!_addresses.TryGetValue(id, out address))
                {
                    XElement addressElement = XDocument.Load(xmlFileName)
                                                       .Root
                                                       .Elements("Addresses")
                                                       .First(addressXElement => addressXElement.Element("id").Value == id);

                    address = new Address(addressElement.Element("street").Value,
                                          addressElement.Element("city").Value,
                                          addressElement.Element("county").Value,
                                          addressElement.Element("country").Value);
                    _addresses.Add(id, address);
                }

                return address;
            }

            private static readonly IDictionary<string, Producer> _producers = new SortedDictionary<string, Producer>();
            private static readonly IDictionary<string, Address> _addresses = new SortedDictionary<string, Address>();
            private static readonly IDictionary<string, Product> _products = new SortedDictionary<string, Product>();
            private static readonly IDictionary<string, Shop> _shops = new SortedDictionary<string, Shop>();
        }

        private static class SqlInterface
        {
            public static void Import(string personName, string xmlFileName, string xsdFileName)
            {
                DataSet dataSet = new DataSet("FamilyExpenses");

                dataSet.ReadXmlSchema(xsdFileName);
                dataSet.ReadXml(xmlFileName);
                _dataAdapter.SelectCommand.Parameters["@personName"].Value = personName;
                foreach (string tableName in new[] { "Persons", "Incomes", "Addresses", "Shops", "Producers", "Products", "Purchases" })
                {
                    _dataAdapter.InsertCommand = _insertCommands[tableName];
                    _dataAdapter.UpdateCommand = _updateCommands[tableName];
                    _dataAdapter.DeleteCommand = _deleteCommands[tableName];
                    _dataAdapter.Update(dataSet.Tables[tableName]);
                }
            }

            public static void Export(string personName, string xmlFileName, string xsdFileName = null)
            {
                DataSet dataSet = new DataSet("FamilyExpenses");

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
                                                        and product = @product
                                                        and productProducer = @productProducer
                                                        and datePurchased = @datePurchased", _sqlConnection)
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
                                           },
                                           new SqlParameter("@datePurchased", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "datePurchased",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@productProducer", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "productProducer",
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
                                                            productProducer = @newProductProducer,
                                                            datePurchased = @newDatePurchased,
                                                        where price = @oldPrice
                                                            and quantity = @oldQuantity
                                                            and shop = @oldShop
                                                            and shopAddress = @oldShopAddress
                                                            and purchaser = @oldPurchaser
                                                            and product = @oldProduct
                                                            and productProducer = @oldProductProducer
                                                            and datePurchased = @oldDatePurchased", _sqlConnection)
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
                                           new SqlParameter("@oldDatePurchased", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "datePurchased",
                                               SourceVersion = DataRowVersion.Original
                                           },
                                           new SqlParameter("@oldProductProducer", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "productProducer",
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
                                           },
                                           new SqlParameter("@newDatePurchased", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "datePurchased",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@newProductProducer", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "productProducer",
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
                                                                           and product = @product
                                                                           and productProducer = @productProducer
                                                                           and datePurchased = @datePurchased)
                                                        insert into Purchases(price, quantity, shop, shopAddress, purchaser, product, datePurchased, productProducer)
                                                            values (@price, @quantity, @shop, @shopAddress, @purchaser, @product, @datePurchased, @productProducer)", _sqlConnection)
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
                                           },
                                           new SqlParameter("@datePurchased", SqlDbType.DateTime)
                                           {
                                               SourceColumn = "datePurchased",
                                               SourceVersion = DataRowVersion.Current
                                           },
                                           new SqlParameter("@productProducer", SqlDbType.VarChar, 100)
                                           {
                                               SourceColumn = "productProducer",
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

        public MainViewModel()
        {
            _importFromSqlCommand = new RelayCommand(delegate
            {
                SqlInterface.Export(LoggedinPerson.Name, _xmlFileName);
                _LoadNodesInfo();
                _LoadPurchases();
            });
            _exportToSqlCommand = new RelayCommand(delegate
            {
                SqlInterface.Import(LoggedinPerson.Name, _xmlFileName, "schema.xsd");
                _LoadNodesInfo();
                _LoadPurchases();
            });
            _addProducerCommand = new RelayCommand(parameter =>
            {
                Tuple<string, string> producerInfo = parameter as Tuple<string, string>;

                if (producerInfo != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.Load(_xmlFileName);
                    XmlElement producerElement = xmlDocument.CreateElement("Producers");
                    XmlElement nameElement = xmlDocument.CreateElement("name");
                    XmlElement countryElement = xmlDocument.CreateElement("country");
                    XmlNode rootElement = xmlDocument.ChildNodes.Item(xmlDocument.ChildNodes.Count - 1);
                    nameElement.InnerText = producerInfo.Item1;
                    countryElement.InnerText = producerInfo.Item2;
                    producerElement.AppendChild(nameElement);
                    producerElement.AppendChild(countryElement);
                    rootElement.AppendChild(producerElement);
                    xmlDocument.Save(_xmlFileName);
                    _OnPropertyChanged("AllProducers");
                }
            });
            _addProductCommand = new RelayCommand(parameter =>
            {
                Tuple<string, ProductType, Producer> productInfo = parameter as Tuple<string, ProductType, Producer>;

                if (productInfo != null)
                {
                    XDocument xDocument = XDocument.Load(_xmlFileName);
                    XElement producerElement = xDocument.Root
                                                        .Elements("Producers")
                                                        .FirstOrDefault(producerXElement => producerXElement.Element("name").Value == productInfo.Item3.Name);

                    producerElement.Add(new XElement("Products", new XElement("name")
                    {
                        Value = productInfo.Item1
                    }, new XElement("type")
                    {
                        Value = ((int)productInfo.Item2).ToString()
                    }));
                    xDocument.Save(_xmlFileName);
                    _OnPropertyChanged("AllProducts");
                }
            });
            _addAddressCommand = new RelayCommand(parameter =>
            {
                Tuple<string, string, string, string> addressInfo = parameter as Tuple<string, string, string, string>;

                if (addressInfo != null)
                {
                    XDocument xDocument = XDocument.Load(_xmlFileName);
                    XElement rootElement = xDocument.Root;

                    rootElement.Add(new XElement("Addresses", new XElement("id")
                    {
                        Value = (xDocument.Root
                                          .Elements("Addresses")
                                          .Select(addressXElement => int.Parse(addressXElement.Element("id").Value))
                                          .OrderByDescending(idValue => idValue)
                                          .FirstOrDefault() + 1).ToString()
                    }, new XElement("street")
                    {
                        Value = addressInfo.Item1
                    }, new XElement("city")
                    {
                        Value = addressInfo.Item2
                    }, new XElement("county")
                    {
                        Value = addressInfo.Item3
                    }, new XElement("country")
                    {
                        Value = addressInfo.Item4
                    }));
                    xDocument.Save(_xmlFileName);
                    _OnPropertyChanged("AllAddresses");
                }
            });
            _addShopCommand = new RelayCommand(parameter =>
            {
                Tuple<string, ShopType, Address> shopInfo = parameter as Tuple<string, ShopType, Address>;

                if (shopInfo != null)
                {
                    XDocument xDocument = XDocument.Load(_xmlFileName);
                    XElement rootElement = xDocument.Root;
                    XElement addressElement = rootElement.Elements("Addresses")
                                                         .Where(addressXElement => addressXElement.Element("street").Value == shopInfo.Item3.Street
                                                                                   && addressXElement.Element("city").Value == shopInfo.Item3.City
                                                                                   && addressXElement.Element("county").Value == shopInfo.Item3.County
                                                                                   && addressXElement.Element("country").Value == shopInfo.Item3.Country)
                                                         .First();

                    addressElement.Add(new XElement("Shops",
                                       new XElement("name")
                                       {
                                           Value = shopInfo.Item1
                                       },
                                       new XElement("type")
                                       {
                                           Value = ((int)shopInfo.Item2).ToString()
                                       }));
                    xDocument.Save(_xmlFileName);
                    _OnPropertyChanged("AllShops");
                }
            });
            _addPurchaseCommand = new RelayCommand(parameter =>
            {
                Tuple<int, int, DateTime, Product, Shop> purchaseInfo = parameter as Tuple<int, int, DateTime, Product, Shop>;

                XDocument xDocument = XDocument.Load(_xmlFileName);

                xDocument.Root
                         .Element("Persons")
                         .Add(new XElement("Purchases",
                              new XElement("price")
                              {
                                  Value = purchaseInfo.Item1.ToString()
                              },
                              new XElement("quantity")
                              {
                                  Value = purchaseInfo.Item2.ToString()
                              },
                              new XElement("shop")
                              {
                                  Value = purchaseInfo.Item5.Name
                              },
                              new XElement("shopAddress")
                              {
                                  Value = xDocument.Root.Elements("Addresses")
                                                        .Where(addressXElement => addressXElement.Element("street").Value == purchaseInfo.Item5.Address.Street
                                                                                  && addressXElement.Element("city").Value == purchaseInfo.Item5.Address.City
                                                                                  && addressXElement.Element("county").Value == purchaseInfo.Item5.Address.County
                                                                                  && addressXElement.Element("country").Value == purchaseInfo.Item5.Address.Country)
                                                        .First()
                                                        .Element("id")
                                                        .Value
                              },
                              new XElement("product")
                              {
                                  Value = purchaseInfo.Item4.Name
                              },
                              new XElement("productProducer")
                              {
                                  Value = purchaseInfo.Item4.Producer.Name
                              },
                              new XElement("datePurchased")
                              {
                                  Value = XmlConvert.ToString(purchaseInfo.Item3, "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz")
                              }));

                xDocument.Save(_xmlFileName);
                _LoadNodesInfo();
                _LoadPurchases();
            });
            _modifyPurchaseCommand = new RelayCommand(parameter =>
            {
                Tuple<int, int, DateTime, Product, Shop> newPurchaseInfo = parameter as Tuple<int, int, DateTime, Product, Shop>;

                XDocument xDocument = XDocument.Load(_xmlFileName);

                XElement purachseElement = xDocument.Root
                                                    .Element("Persons")
                                                    .Elements("Purchases")
                                                    .Where(purchaseXElement => purchaseXElement.Element("price").Value == SelectedPurchase.Price.ToString()
                                                                               && purchaseXElement.Element("quantity").Value == SelectedPurchase.Quantity.ToString()
                                                                               && purchaseXElement.Element("shop").Value == SelectedPurchase.Shop.Name
                                                                               && purchaseXElement.Element("product").Value == SelectedPurchase.Product.Name
                                                                               && purchaseXElement.Element("productProducer").Value == SelectedPurchase.Product.Producer.Name
                                                                               && XmlConvert.ToDateTime(purchaseXElement.Element("datePurchased").Value, "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz") == SelectedPurchase.PurchaseDate
                                                                               && purchaseXElement.Element("shopAddress").Value == xDocument.Root
                                                                                                                                            .Elements("Addresses")
                                                                                                                                            .Where(addressXElement => addressXElement.Element("street").Value == SelectedPurchase.Shop.Address.Street
                                                                                                                                                                      && addressXElement.Element("city").Value == SelectedPurchase.Shop.Address.City
                                                                                                                                                                      && addressXElement.Element("county").Value == SelectedPurchase.Shop.Address.County
                                                                                                                                                                      && addressXElement.Element("country").Value == SelectedPurchase.Shop.Address.Country)
                                                                                                                                            .First()
                                                                                                                                            .Element("id")
                                                                                                                                            .Value)
                                                    .First();

                purachseElement.Element("price").Value = newPurchaseInfo.Item1.ToString();
                purachseElement.Element("quantity").Value = newPurchaseInfo.Item2.ToString();
                purachseElement.Element("shop").Value = newPurchaseInfo.Item5.Name;
                purachseElement.Element("product").Value = newPurchaseInfo.Item4.Name;
                purachseElement.Element("productProducer").Value = newPurchaseInfo.Item4.Producer.Name;
                purachseElement.Element("datePurchased").Value = XmlConvert.ToString(newPurchaseInfo.Item3, "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz");
                purachseElement.Element("shopAddress").Value = xDocument.Root
                                                                        .Elements("Addresses")
                                                                        .Where(addressXElement => addressXElement.Element("street").Value == newPurchaseInfo.Item5.Address.Street
                                                                                                  && addressXElement.Element("city").Value == newPurchaseInfo.Item5.Address.City
                                                                                                  && addressXElement.Element("county").Value == newPurchaseInfo.Item5.Address.County
                                                                                                  && addressXElement.Element("country").Value == newPurchaseInfo.Item5.Address.Country)
                                                                        .First()
                                                                        .Element("id")
                                                                        .Value;
                xDocument.Save(_xmlFileName);
                _LoadPurchases();
            });
            _removePurchase = new RelayCommand(parameter =>
            {
                if (SelectedPurchase != null)
                {
                    XDocument xDocument = XDocument.Load(_xmlFileName);

                    xDocument.Root
                             .Element("Persons")
                             .Elements("Purchases")
                             .Where(purchaseXElement => purchaseXElement.Element("price").Value == SelectedPurchase.Price.ToString()
                                                        && purchaseXElement.Element("quantity").Value == SelectedPurchase.Quantity.ToString()
                                                        && purchaseXElement.Element("shop").Value == SelectedPurchase.Shop.Name
                                                        && purchaseXElement.Element("product").Value == SelectedPurchase.Product.Name
                                                        && purchaseXElement.Element("productProducer").Value == SelectedPurchase.Product.Producer.Name
                                                        && XmlConvert.ToDateTime(purchaseXElement.Element("datePurchased").Value, "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz") == SelectedPurchase.PurchaseDate
                                                        && purchaseXElement.Element("shopAddress").Value == xDocument.Root
                                                                                                                     .Elements("Addresses")
                                                                                                                     .Where(addressXElement => addressXElement.Element("street").Value == SelectedPurchase.Shop.Address.Street
                                                                                                                                               && addressXElement.Element("city").Value == SelectedPurchase.Shop.Address.City
                                                                                                                                               && addressXElement.Element("county").Value == SelectedPurchase.Shop.Address.County
                                                                                                                                               && addressXElement.Element("country").Value == SelectedPurchase.Shop.Address.Country)
                                                                                                                     .First()
                                                                                                                     .Element("id")
                                                                                                                     .Value)
                             .Remove();
                    xDocument.Save(_xmlFileName);
                    _LoadNodesInfo();
                    _LoadPurchases();
                }
            });
            _applyFilterCommand = new RelayCommand(parameter =>
            {
                _selectorTreeNode = parameter as SelectorTreeNode;
                _LoadPurchases();
            });
            _loginCommand = new RelayCommand(parameter =>
            {
                string personName = parameter as string;

                if (personName != null)
                    if (File.Exists(personName + ".xml"))
                    {
                        _xmlFileName = personName + ".xml";
                        _loginCommand.CanExecuteCommand = false;
                        _logoutCommand.CanExecuteCommand = true;
                        _LoadPerson();
                        _LoadNodesInfo();
                    }
            });
            _logoutCommand = new RelayCommand(delegate
            {
                _filterTreeNode.Children.Clear();
                _monthsTreeNode.Children.Clear();
                _shopsTreeNode.Children.Clear();
                _filterTreeNode.Children.Add(_monthsTreeNode);
                _filterTreeNode.Children.Add(_shopsTreeNode);
                LoggedinPerson = null;
                _loginCommand.CanExecuteCommand = true;
                _logoutCommand.CanExecuteCommand = false;
                _purchases.Clear();
                Cache.Clear();
            }, false);
            _createPersonCommand = new RelayCommand(parameter =>
            {
                Tuple<string, string> personInfo = parameter as Tuple<string, string>;

                if (personInfo != null)
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    XmlNode rootXmlNode = xmlDocument.CreateElement("FamilyExpenses");
                    XmlNode personsXmlNode = xmlDocument.CreateElement("Persons");
                    XmlNode personNameXmlNode = xmlDocument.CreateElement("name");
                    XmlNode personPreferedCurrencyXmlNode = xmlDocument.CreateElement("preferedCurrency");

                    xmlDocument.Schemas.Add(null, "Schema.xsd");
                    xmlDocument.AppendChild(xmlDocument.CreateXmlDeclaration("1.0", null, "yes"));
                    personNameXmlNode.InnerText = personInfo.Item1;
                    personPreferedCurrencyXmlNode.InnerText = personInfo.Item2;
                    personsXmlNode.AppendChild(personNameXmlNode);
                    personsXmlNode.AppendChild(personPreferedCurrencyXmlNode);
                    rootXmlNode.AppendChild(personsXmlNode);
                    xmlDocument.AppendChild(rootXmlNode);
                    xmlDocument.Validate(null);
                    xmlDocument.Save(personInfo.Item1 + ".xml");
                    _loginCommand.Execute(personInfo.Item1);
                }
            });
            _filterTreeNode.Children.Add(_monthsTreeNode);
            _filterTreeNode.Children.Add(_shopsTreeNode);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public Person LoggedinPerson
        {
            get
            {
                return _loggedinPerson;
            }
            set
            {
                _loggedinPerson = value;
                _OnPropertyChanged("PersonName");
                _OnPropertyChanged("IsPersonLoggedIn");
            }
        }

        public bool IsPersonLoggedIn
        {
            get
            {
                return (_loggedinPerson != null);
            }
        }

        public string PersonName
        {
            get
            {
                if (_loggedinPerson != null)
                    return _loggedinPerson.Name;
                else
                    return string.Empty;
            }
        }

        public TreeNode FilterRootTreeNode
        {
            get
            {
                return _filterTreeNode;
            }
        }

        public IReadOnlyList<string> CurrencyValues
        {
            get
            {
                return Enum.GetNames(typeof(Currency)).AsEnumerable().ToList();
            }
        }

        public IReadOnlyList<string> ProductTypes
        {
            get
            {
                return Enum.GetNames(typeof(ProductType)).AsEnumerable().ToList();
            }
        }

        public IReadOnlyList<string> ShopTypes
        {
            get
            {
                return Enum.GetNames(typeof(ShopType)).AsEnumerable().ToList();
            }
        }

        public IReadOnlyList<Producer> AllProducers
        {
            get
            {
                return Cache.AllProducers(_xmlFileName);
            }
        }

        public IReadOnlyList<Product> AllProducts
        {
            get
            {
                return Cache.AllProducts(_xmlFileName);
            }
        }

        public IReadOnlyList<Address> AllAddresses
        {
            get
            {
                return Cache.AllAddresses(_xmlFileName);
            }
        }

        public IReadOnlyList<Shop> AllShops
        {
            get
            {
                return Cache.AllShopst(_xmlFileName);
            }
        }

        public ObservableCollection<Purchase> Purchases
        {
            get
            {
                return _purchases;
            }
        }

        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand;
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand;
            }
        }

        public ICommand CreatePersonCommand
        {
            get
            {
                return _createPersonCommand;
            }
        }

        public ICommand AddProductCommand
        {
            get
            {
                return _addProductCommand;
            }
        }

        public ICommand AddProducerCommand
        {
            get
            {
                return _addProducerCommand;
            }
        }

        public ICommand AddAddressCommand
        {
            get
            {
                return _addAddressCommand;
            }
        }

        public ICommand AddShopCommand
        {
            get
            {
                return _addShopCommand;
            }
        }

        public ICommand AddPurchaseCommand
        {
            get
            {
                return _addPurchaseCommand;
            }
        }

        public ICommand ModifyPurchaseCommand
        {
            get
            {
                return _modifyPurchaseCommand;
            }
        }

        public ICommand RemovePurchaseCommand
        {
            get
            {
                return _removePurchase;
            }
        }

        public ICommand ApplyFliterCommand
        {
            get
            {
                return _applyFilterCommand;
            }
        }

        public ICommand ImportFromSqlCommand
        {
            get
            {
                return _importFromSqlCommand;
            }
        }

        public ICommand ExportToSqlCommand
        {
            get
            {
                return _exportToSqlCommand;
            }
        }

        internal Purchase SelectedPurchase
        {
            get;
            set;
        }

        private void _LoadPerson()
        {
            XElement personNode = XDocument.Load(_xmlFileName).Root.Element("Persons");

            LoggedinPerson = new Person(personNode.Element("name").Value, (Currency)Enum.Parse(typeof(Currency), personNode.Element("preferedCurrency").Value));
        }

        private void _LoadNodesInfo()
        {
            _shopsTreeNode.Children.Clear();
            foreach (int monthNumber in XDocument.Load(_xmlFileName)
                                                 .Root
                                                 .Element("Persons")
                                                 .Descendants("Purchases")
                                                 .Select(purchase => XmlConvert.ToDateTime(purchase.Element("datePurchased")
                                                                                                   .Value,
                                                                                           "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz")
                                                                               .Month)
                                                 .Distinct()
                                                 .OrderBy(month => month))
            {
                string monthName = CultureInfo.GetCultureInfo("en-GB").DateTimeFormat.GetMonthName(monthNumber);

                if (_monthsTreeNode.Children.FirstOrDefault(treeNode => treeNode.Header == monthName) == null)
                    _monthsTreeNode.Children.Add(new SelectorTreeNode(monthName,
                                                                      delegate
                                                                      {
                                                                          return XDocument.Load(_xmlFileName)
                                                                                          .Root
                                                                                          .Element("Persons")
                                                                                          .Descendants("Purchases")
                                                                                          .Where(purchase => XmlConvert.ToDateTime(purchase.Element("datePurchased")
                                                                                                                                           .Value,
                                                                                                                                   "yyyy-MM-dd\\THH:mm:ss.FFFFFFFzzz")
                                                                                                                       .Month == monthNumber)
                                                                                          .Select(purchaseElement => Cache.GetPurchase(_loggedinPerson, purchaseElement, _xmlFileName))
                                                                                          .OrderBy(purchase => purchase.PurchaseDate);
                                                                      }));
            }
            foreach (string shop in XDocument.Load(_xmlFileName)
                                             .Root
                                             .Element("Persons")
                                             .Descendants("Purchases")
                                             .Select(purchase => purchase.Element("shop").Value)
                                             .Distinct()
                                             .OrderBy(shopName => shopName))
            {
                TreeNode shopTreeNode = new SelectorTreeNode(shop, delegate
                {
                    return XDocument.Load(_xmlFileName)
                                    .Root
                                    .Element("Persons")
                                    .Descendants("Purchases")
                                    .Where(purchase => purchase.Element("shop").Value == shop)
                                    .Select(purchaseElement => Cache.GetPurchase(_loggedinPerson, purchaseElement, _xmlFileName))
                                    .OrderBy(purchase => purchase.PurchaseDate);
                });

                foreach (string product in XDocument.Load(_xmlFileName)
                                                    .Root
                                                    .Element("Persons")
                                                    .Descendants("Purchases")
                                                    .Where(purchase => purchase.Element("shop").Value == shop)
                                                    .Select(purchase => purchase.Element("product").Value)
                                                    .Distinct()
                                                    .OrderBy(productName => productName))
                    shopTreeNode.Children.Add(new SelectorTreeNode(product, delegate
                    {
                        return XDocument.Load(_xmlFileName)
                                   .Root
                                   .Element("Persons")
                                   .Descendants("Purchases")
                                   .Where(purchase => purchase.Element("shop").Value == shop && purchase.Element("product").Value == product)
                                   .Select(purchaseElement => Cache.GetPurchase(_loggedinPerson, purchaseElement, _xmlFileName))
                                   .OrderBy(purchase => purchase.PurchaseDate);
                    }));

                _shopsTreeNode.Children.Add(shopTreeNode);
            }
        }

        private void _LoadPurchases()
        {
            if (_selectorTreeNode != null)
            {
                _purchases.Clear();

                foreach (Purchase purchase in _selectorTreeNode.GetPurchases())
                    _purchases.Add(purchase);
            }
        }

        private void _OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private static readonly XmlWriterSettings _xmlSettings = new XmlWriterSettings
        {
            CloseOutput = true,
            ConformanceLevel = ConformanceLevel.Document,
            Indent = true,
            IndentChars = new string(' ', 4),
            OmitXmlDeclaration = false,
            WriteEndDocumentOnClose = true
        };
        private string _xmlFileName;
        private Person _loggedinPerson;
        private SelectorTreeNode _selectorTreeNode = null;
        private readonly RelayCommand _createPersonCommand;
        private readonly RelayCommand _loginCommand;
        private readonly RelayCommand _logoutCommand;
        private readonly RelayCommand _applyFilterCommand;
        private readonly RelayCommand _addProducerCommand;
        private readonly RelayCommand _addProductCommand;
        private readonly RelayCommand _addAddressCommand;
        private readonly RelayCommand _addShopCommand;
        private readonly RelayCommand _addPurchaseCommand;
        private readonly RelayCommand _modifyPurchaseCommand;
        private readonly RelayCommand _removePurchase;
        private readonly RelayCommand _importFromSqlCommand;
        private readonly RelayCommand _exportToSqlCommand;
        private readonly TreeNode _monthsTreeNode = new SelectorTreeNode("Months", () => new Purchase[0]);
        private readonly TreeNode _shopsTreeNode = new SelectorTreeNode("Shops", () => new Purchase[0]);
        private readonly TreeNode _filterTreeNode = new TreeNode("Filter");
        private readonly ObservableCollection<Purchase> _purchases = new ObservableCollection<Purchase>();
    }
}
