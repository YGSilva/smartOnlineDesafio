using desafioDotNet.Models;

namespace desafioDotNet.Repository.Interfaces {
    public interface IFileRepository : IBaseRepository {

        IEnumerable<RegisterModel> GetListWithTotalBalance();
        IEnumerable<RegisterModel> GetOperationsWrong();
        void DeleteAllData();
    }
}