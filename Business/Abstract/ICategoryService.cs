using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService
    {

        Category GetById(int categoryId);
        List<Category> GetList();
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
