using Microsoft.AspNetCore.Mvc.Rendering;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        public bool Exist(Category cat);
        public void AddRange(List<Category> categories);
        public SelectList GetAllAsSelectList();
    }
}
