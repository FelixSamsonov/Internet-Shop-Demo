using InternetShopAspNetCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetShopAspNetCoreMvc.Service.Interfaces
{
    public interface ICategoryService
    {
        Task <Category> CreateCategoryAsync(Category category);
        Task <Category> UpdateCategoryAsync(Category category);
        Task <bool> DeleteCategoryAsync(int id);
        Task <IEnumerable<Category>> GetAllCategoryAsync();
        Task <Category> GetByIdCategoryAsync(int id);
    }
}
