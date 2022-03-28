using SecondHandGear.Library.Models;

namespace SecondHandGear.Library.DataAccess
{
    public interface IDataAccess
    {
        public List<PersonModel> GetPeople();
        public PersonModel InsertPerson(string firstName,string lastName);
    }
}