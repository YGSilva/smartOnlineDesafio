using desafioDotNet.Context;
using desafioDotNet.Repository.Interfaces;

namespace desafioDotNet.Repository {
    public class BaseRepository : IBaseRepository {

        private readonly RegisterContext _context;

        public BaseRepository(RegisterContext context) {
            _context = context;
        }
        public void Add<T>(T entity) where T : class {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class {
            throw new NotImplementedException();
        }

        public bool SaveChanges() {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class {
            throw new NotImplementedException();
        }
    }
}
