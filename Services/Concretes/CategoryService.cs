using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concretes
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;

        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void CreateCategory(Category category)
        {
            _repositoryManager.Category.CreateCategory(category);
            _repositoryManager.Save();
        }

        public bool DeleteCategory(int id)
        {
            var model = _repositoryManager.Category.GetCategoryById(id, false);
            if (model != null)
            {
                _repositoryManager.Category.DeleteCategory(model);
                _repositoryManager.Save();
                return true;
            }
            return false;
        }

        public IEnumerable<Category> GetAllCategories(bool trackChanges)
        {
            return _repositoryManager.Category.FindAll(false).ToList();
        }

        public Category GetCategorybyId(int id, bool trackChanges)
        {
            return _repositoryManager.Category.GetCategoryById(id, trackChanges);
        }
    }
}
