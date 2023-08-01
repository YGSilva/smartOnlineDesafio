
using desafioDotNet.Utils;
using desafioDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using desafioDotNet.Context;
using System.Globalization;
using desafioDotNet.Repository.Interfaces;

namespace desafio.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase {

        private readonly RegisterContext _context;
        private readonly NormalizeFile _normalizeFile;
        private readonly IFileRepository _repository;

        public UploadController(RegisterContext context, NormalizeFile normalizeFile, IFileRepository repository) {
            _context = context;
            _normalizeFile = normalizeFile;
            _repository = repository;
        }

        [HttpPost("ProcessFile")]
        public IActionResult ProcessFile(IFormFile file) {
            if (file == null || file.Length == 0) {
                return BadRequest("Nenhum arquivo enviado.");
            }

            if (!Path.GetExtension(file.FileName).Equals(".txt", System.StringComparison.OrdinalIgnoreCase)) {
                return BadRequest("O arquivo deve ter a extensão .txt.");
            }

            using (var reader = new StreamReader(file.OpenReadStream())) {

                var content = reader.ReadToEnd();
                var registers = _normalizeFile.GetRegistration(content);

                foreach (var reg in registers) {
                    _context.Add(reg);
                    _context.SaveChanges();
                }

                return Ok(registers);
            }
        }

        [HttpGet("ListWithTotalBalance")]
        public async Task<ActionResult> ListWithTotalBalance() {
            var file = _repository.GetListWithTotalBalance();
            return file.Any()
                ? Ok(file)
                : BadRequest("NÃO EXISTE NADA NA BASE DE DADOS");
        }

        [HttpGet("OperationsWrong")]
        public async Task<ActionResult> OperationsWrong() {
            var file = _repository.GetOperationsWrong();
            return file.Any()
                ? Ok(file)
                : BadRequest("NÃO EXISTEM OPERAÇÕES QUE FALHARAM");
        }
    }
}