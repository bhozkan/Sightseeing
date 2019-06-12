using DataAccessLayer;
using Entitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class CategoryRepository
    {

        protected readonly SightseeingContext context;

        public CategoryRepository(SightseeingContext ctx)
        {
            context = ctx;
        }

        public List<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category FindByCategoryId(int id)
        {
            return context.Categories.Find(id);
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
        }

        public void DeleteCategory(int id)
        {
           Category deletedCat = FindByCategoryId(id);
           context.Categories.Remove(deletedCat);
        }

        public void  UpdateCategory(Category updatedCategory)
        {
            var categoryOld = FindByCategoryId(updatedCategory.CategoryId);
            
        }


    }
}
