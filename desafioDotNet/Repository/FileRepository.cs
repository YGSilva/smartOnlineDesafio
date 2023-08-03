using desafioDotNet.Context;
using desafioDotNet.Models;
using desafioDotNet.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace desafioDotNet.Repository {
    public class FileRepository : BaseRepository, IFileRepository {

        private readonly RegisterContext _context;

        public FileRepository(RegisterContext context) : base(context){
            _context = context;
        }

        public IEnumerable<RegisterModel> GetListWithTotalBalance() {
            DateTime value = new DateTime();

            var operations = _context
                .Register
                .Where(x => !x.Cpf.Contains("CPF INVALIDO!") && x.Data != value)
                .GroupBy(x => new { x.Tipo, x.NomeLoja })
                .Select(g => new RegisterModel {
                    Tipo = g.Key.Tipo,
                    NomeLoja = g.Key.NomeLoja,
                    Valor = g.Sum(x => x.Valor)
                })
                .OrderBy(x => x.NomeLoja)
                .ThenBy(x => x.Tipo)
                .ToList();

            return operations;
        }

        public IEnumerable<RegisterModel> GetOperationsWrong() {
            DateTime value = new DateTime();

            var operations = _context
                .Register
                .Where(x => x.Cpf.Contains("CPF INVALIDO!") || x.Data == value)
                .ToList();

            return operations;
        }

    }
}
