using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess.Interfaces
{
    public interface IPersonRepository : IGenericRepository<Person>
    {
        public bool ExistsName(string name);
    }
}
