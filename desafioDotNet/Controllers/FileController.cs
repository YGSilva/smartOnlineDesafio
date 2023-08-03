
using desafioDotNet.Utils;
using desafioDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using desafioDotNet.Context;
using System.Globalization;
using desafioDotNet.Repository.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace desafioDotNet.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase {

        private readonly RegisterContext _context;
        private readonly NormalizeFile _normalizeFile;
        private readonly IFileRepository _repository;

        public FileController(RegisterContext context, NormalizeFile normalizeFile, IFileRepository repository) {
            _context = context;
            _normalizeFile = normalizeFile;
            _repository = repository;
        }

        /// <summary>
        /// Upload do arquivo CNAB.txt
        /// </summary>
        /// <param name="file"> Arquivo com os dados </param>
        /// <returns> Dados criados na Base </returns>
        /// <response code="201"> Sucesso </response>
        [HttpPost("ProcessFile")]
        [SwaggerOperation(Summary = "Processa o arquivo de registro.", Description = "Este endpoint recebe um arquivo de texto (.txt) contendo registros, processa e insere os registros na base de dados.")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<RegisterModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult ProcessFile(IFormFile file) {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo enviado.");

            if (!Path.GetExtension(file.FileName).Equals(".txt", System.StringComparison.OrdinalIgnoreCase))
                return BadRequest("O arquivo deve ter a extensão .txt.");

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

        /// <summary>
        /// Obtém uma lista com o total de saldo em conta agrupado por tipo de operação e loja com excessão das operações que falharam.
        /// </summary>
        /// <returns> Lista agrupada e com saldo total </returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Dados não encontrado na base </response>
        [HttpGet("ListWithTotalBalance")]
        [SwaggerOperation(Summary = "Obtém uma lista com o total de saldo em conta agrupado por tipo de operação e loja com excessão das operações que falharam.", Description = "Este endpoint retorna uma lista com o total de saldo em conta agrupado por tipo de operação e loja com excessão das operações que falharam..")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegisterModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> ListWithTotalBalance() {
            var file = _repository.GetListWithTotalBalance();
            return file.Any()
                ? Ok(file)
                : NotFound("NÃO EXISTE NADA NA BASE DE DADOS");
        }

        /// <summary>
        /// Obtém uma lista com as operações que falharam.
        /// </summary>
        /// <returns> Lista com os dados das operações falhas </returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Dados não encontrado na base </response>
        [HttpGet("OperationsWrong")]
        [SwaggerOperation(Summary = "Obtém uma lista com as operações que falharam.", Description = "Este endpoint retorna uma lista com as operações que falharam, agrupadas por tipo de operação e loja.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegisterModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<ActionResult> OperationsWrong() {
            var file = _repository.GetOperationsWrong();
            return file.Any()
                ? Ok(file)
                : NotFound("NÃO EXISTEM OPERAÇÕES QUE FALHARAM");
        }

        /// <summary>
        /// Exclui todos os dados da base
        /// </summary>
        /// <response code="204"> No content </response>
        [SwaggerOperation(Summary = "Exclui todos os dados da base.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RegisterModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        [HttpDelete]
        public IActionResult DeleteAllData() {
            _repository.DeleteAllData();
            return NoContent();
        }
    }
}