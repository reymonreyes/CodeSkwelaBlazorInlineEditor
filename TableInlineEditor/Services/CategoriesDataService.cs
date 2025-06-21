using TableInlineEditor.Services.Models;

namespace TableInlineEditor.Services
{
    public interface ICategoriesDataService
    {
        List<Category> GetAll();
    }

    public class CategoriesDataService : ICategoriesDataService
    {
        public List<Category> GetAll()
        {
            var result = new List<Category>();

            using (var context = new ApplicationDbContext())
            {
                result = context.Categories.ToList();
            }

            return result;
        }
    }
}
