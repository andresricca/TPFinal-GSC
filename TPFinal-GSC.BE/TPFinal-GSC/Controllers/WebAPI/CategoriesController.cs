using Microsoft.AspNetCore.Mvc;
using TPFinal_GSC.DataAccess.Interfaces;
using TPFinal_GSC.Entities;

namespace TPFinal_GSC.Controllers.WebAPI
{
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        public CategoriesController(IUnitOfWork uom)
        {
            this.uow = uom;
        }

        [HttpPost]
        [Route("createDefaults")]
        public IActionResult CreateDefaultCategories()
        {
            var categories = new List<Category> {
                 new Category {
                    Description = "Books"
                },
                 new Category {
                    Description = "Tools"
                },
                 new Category {
                     Description = "Games"
                },
                 new Category {
                    Description = "Others"
                }
            };

            var missingCategories = categories.Where(cat => !uow.CategoryRepository.Exist(cat)).ToList();
            uow.CategoryRepository.AddRange(missingCategories);
            uow.Complete();

            return Ok(GetCategories());
        }

        [HttpGet]
        public List<Category> GetCategories()
        {
            return uow.CategoryRepository.GetAll();
        }
    }
}
