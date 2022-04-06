using SecondHand.Library.Models;

namespace SecondHand.Library.DataAccess
{
    public interface IDataAccess
    {
        public List<PersonModel> GetPeople();
        public PersonModel GetPeople(Guid id);
        public PersonModel InsertPerson(string firstName,string lastName);
        public PersonModel UpdatePerson(Guid id,string firstName,string lastName);
        public bool DeletePerson(Guid id);
    }
}