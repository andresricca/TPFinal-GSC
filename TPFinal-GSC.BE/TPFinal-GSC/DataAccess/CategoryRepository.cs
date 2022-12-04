using Microsoft.AspNetCore.Mvc.Rendering;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.DataAccess
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(LoansContext context) : base(context)
        {
        }

        public void AddRange(List<Category> categories)
        {
            dbSet.AddRange(categories);
        }

        public bool Exist(Category cat)
        {
            return dbSet.Any(x => x.Description == cat.Description);
        }
        public SelectList GetAllAsSelectList()
        {
            var categories = GetAll();
            return new SelectList(categories, "Id", "Description");
        }
    }
}
