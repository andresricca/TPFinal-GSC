using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess
{
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(LoansContext context) : base(context)
        {
        }

        public bool ExistsName(string name)
        {
            return dbSet.Any(x => x.Name == name);
        }
    }
}
