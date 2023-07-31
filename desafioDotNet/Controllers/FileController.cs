
using desafioDotNet.Utils;
using desafioDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using desafioDotNet.Context;
using System.Globalization;

namespace desafio.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UploadController : ControllerBase {

        private readonly RegisterContext _context;
        private readonly NormalizeFile _normalizeFile;

        public UploadController(RegisterContext context, NormalizeFile normalizeFile) {
            _context = context;
            _normalizeFile = normalizeFile;
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

       
    }
}