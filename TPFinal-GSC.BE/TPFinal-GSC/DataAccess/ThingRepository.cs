using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess
{
    public class ThingRepository : GenericRepository<Thing>, IThingRepository
    {
        public ThingRepository(LoansContext context) : base(context)
        {
        }

        public List<Thing> GetAll()
        {
            return dbSet.Include(x => x.Category).ToList();
        }

        public Thing GetById(int id)
        {
            return dbSet.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public bool ExistsDescription(string desc)
        {
            return dbSet.Any(x => x.Description == desc);
        }
    }
}
