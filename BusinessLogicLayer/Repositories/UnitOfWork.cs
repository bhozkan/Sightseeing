using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class UnitOfWork
    {
        SightseeingContext _context = new SightseeingContext();

        public CategoryRepository _categoryRepository;
        public ActivityRepository _activityRepository;

        public UnitOfWork()
        {
            _categoryRepository = new CategoryRepository(_context);
            _activityRepository = new ActivityRepository(_context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
