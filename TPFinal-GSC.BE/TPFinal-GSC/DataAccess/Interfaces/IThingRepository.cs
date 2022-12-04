using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess.Interfaces
{
    public interface IThingRepository : IGenericRepository<Thing>
    {
        public bool ExistsDescription(string desc);
    }
}
