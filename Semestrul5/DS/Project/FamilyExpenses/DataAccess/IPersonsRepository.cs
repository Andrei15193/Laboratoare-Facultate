using System.Collections.Generic;
using FamilyExpenses.Model;

namespace FamilyExpenses.DataAccess
{
    public interface IPersonsRepository
    {
        IReadOnlyCollection<Person> AllPersons();

        bool Exists(string name);

        void Add(Person person);

        void Update(Person oldPerson, Person newPerson);

        void Delete(Person person);

        void Add(Person person, Income income);

        void Remove(Person person, Income income);
    }
}
