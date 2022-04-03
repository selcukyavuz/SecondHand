using SecondHandGear.Library.Models;

namespace SecondHandGear.Library.DataAccess
{
    public interface IDataAccess
    {
        public List<PersonModel> GetPeople();
        public PersonModel GetPeople(int id);
        public PersonModel InsertPerson(string firstName,string lastName);
        public PersonModel UpdatePerson(int id,string firstName,string lastName);
        public bool DeletePerson(int id);
    }
}