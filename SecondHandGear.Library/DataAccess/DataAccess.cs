namespace SecondHandGear.Library.DataAccess;

using System.Collections.Generic;
using SecondHandGear.Library.Models;
public class DataAccess : IDataAccess
{
    private List<PersonModel> people = new ();
    public DataAccess()
    {
        people.Add(new PersonModel{ Id = 1, FirstName = "Foo", LastName = "Bar" });
        people.Add(new PersonModel{ Id = 2, FirstName = "Foo2", LastName = "Bar2" });
    }

    public List<PersonModel> GetPeople()
    {
        return people;
    }

    public PersonModel GetPeople(int id)
    {
        return people.FirstOrDefault(p => p.Id == id)!;
    }

    public PersonModel InsertPerson(string firstName,string lastName)
    {
        PersonModel model = new PersonModel{ Id = people.Max(x => x.Id) + 1, FirstName = firstName, LastName = lastName };
        people.Add(model);
        return model;
    }
}