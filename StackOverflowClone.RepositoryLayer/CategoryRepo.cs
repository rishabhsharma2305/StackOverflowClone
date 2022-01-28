using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowClone.DomainModels;
using StackOverflowClone.ViewModel;

namespace StackOverflowClone.RepositoryLayer
{
    public class CategoryRepo
    {
        public List<Category> categoriesList;
        public ContextClass contextClass;
        public CategoryRepo()
        {
            categoriesList = new List<Category>();
            contextClass = new ContextClass();
        }
        public List<CategoryViewModel> GetCategories()
        {
            categoriesList = contextClass.Categories.ToList();
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Category, CategoryViewModel>());
            var mapper = config.CreateMapper();
            List<CategoryViewModel> cvm = mapper.Map<List<Category>, List<CategoryViewModel>>(categoriesList);
            return cvm;
        }
    }
}
