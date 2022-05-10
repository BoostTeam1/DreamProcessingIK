using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public void Add(Category category)
        {
            _categoryDal.Add(category);
        }

        public void Delete(Category category)
        {
            _categoryDal.Delete(category);
        }

        public Category GetById(int categoryId)
        {
            return _categoryDal.Get(filter: x => x.Id == categoryId).Result;
        }

        public List<Category> GetList()
        {
            return _categoryDal.GetList().Result.ToList();
        }

        public void Update(Category category)
        {
            _categoryDal.Update(category);
        }
    }
}
