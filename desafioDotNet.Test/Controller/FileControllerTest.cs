using desafioDotNet.Context;
using desafioDotNet.Repository.Interfaces;
using desafioDotNet.Utils;
using desafioDotNet.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using desafioDotNet.Models;
using Moq;

namespace desafioDotNet.Test.Controller {
        public class FileControllerTest {

        private readonly RegisterContext _context;
        private readonly NormalizeFile _normalize = Substitute.For<NormalizeFile>();
        private readonly IFileRepository _repository = Substitute.For<IFileRepository>();
        private readonly FileController _controller;

        public FileControllerTest() {
            _controller = new FileController(_context, _normalize, _repository);
        }

        [Fact]
        public async Task FileIsNull() {
            // Arrange
            IFormFile file = null;

            // Act
            var result = _controller.ProcessFile(file);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            BadRequestObjectResult badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("Nenhum arquivo enviado.", badRequestResult.Value.ToString());

        }

        [Fact]
        public async Task FileIsNotTxt() {
            // Arrange
            IFormFile file = createTXT();

            // Act
            var result = _controller.ProcessFile(file);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            BadRequestObjectResult badRequestResult = (BadRequestObjectResult)result;
            Assert.Equal("O arquivo deve ter a extensão .txt.", badRequestResult.Value);
        }

        [Fact]
        public async Task ListWithTotalBalanceNotFound() {
            // Arrange
            var data = new List<RegisterModel>{
                new RegisterModel {}
            };

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(repo => repo.GetListWithTotalBalance()).Returns(data);
            var _controllerSubs = new FileController(_context, _normalize, mockRepository.Object);

            // Act
            var result = await _controller.ListWithTotalBalance();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            NotFoundObjectResult notFoundObjectResult = (NotFoundObjectResult)result;
            Assert.Equal("NÃO EXISTE NADA NA BASE DE DADOS", notFoundObjectResult.Value);
        }

        [Fact]
        public async Task OperationsWrongNotFound() {
            // Arrange
            var data = new List<RegisterModel>{
                new RegisterModel {}
            };

            var mockRepository = new Mock<IFileRepository>();
            mockRepository.Setup(repo => repo.GetOperationsWrong()).Returns(data);
            var _controllerSubs = new FileController(_context, _normalize, mockRepository.Object);

            // Act
            var result = await _controller.OperationsWrong();

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            NotFoundObjectResult notFoundObjectResult = (NotFoundObjectResult)result;
            Assert.Equal("NÃO EXISTEM OPERAÇÕES QUE FALHARAM", notFoundObjectResult.Value);
        }

        public IFormFile createTXT() {
            string filePath = "arquivo_de_teste.txt";

            using (StreamWriter writer = new StreamWriter(filePath)) {
                writer.WriteLine("4202070300000503573486375407818342****2231JOSÉ ALENCAR FSUPERMERCADO ARAUJ");
                writer.WriteLine("2202301020000015200232702980569723****9987CARLOS HENRIQUPADARIA 3 CORAÇÕES");
                writer.WriteLine("2202301020000001500348637540781344****1222JOSÉ ALENCAR FSUPERMERCADO ARAUJ");
                writer.WriteLine("2202301020000002600232702980562677****8778CARLOS HENRIQUPADARIA 3 CORAÇÕES");
                writer.WriteLine("3202301020000011500348637540783777****1313JOSÉ ALENCAR FSUPERMERCADO ARAUJ");
            }

            byte[] fileBytes = File.ReadAllBytes(filePath);

            using (MemoryStream memoryStream = new MemoryStream(fileBytes)) {
                IFormFile file = new FormFile(memoryStream, 0, memoryStream.Length, "arquivo_de_teste", "arquivo_de_teste.pdf");
                return file;
            }
        }

    }
}
