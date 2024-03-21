
using AppStoreDataAccess.Data;
using AppStoreDataAccess.Repository.IRepository;
using AppStoreModels;

namespace AppStoreDataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {

        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }



        public void Update(Category obj)
        {
            _db.categories.Update(obj);
        }
    }
}
