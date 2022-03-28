namespace SecondHandGear.Library.DataAccess;

using System.Collections.Generic;
using SecondHandGear.Library.Models;
public class DataAccess : IDataAccess
{
    private List<PersonModel> people = new ();
    public DataAccess()
    {
        people.Add(new PersonModel{ Id = 1, FirstName = "Tim", LastName = "Corey" });
        people.Add(new PersonModel{ Id = 1, FirstName = "Sue", LastName = "Strom" });
    }

    public List<PersonModel> GetPeople()
    {
        return people;
    }

    public PersonModel InsertPerson(string firstName,string lastName)
    {
        PersonModel model = new PersonModel{ Id = people.Max(x => x.Id) + 1, FirstName = firstName, LastName = lastName };
        people.Add(model);
        return model;
    }
}