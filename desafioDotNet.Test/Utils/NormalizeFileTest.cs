using desafioDotNet.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace desafioDotNet.Test.Utils {
    public class NormalizeFileTest {

        [Fact]
        public void CpfHappyFlow() {
            // Arrange
            var cpf = "12345678909";

            // Act
            var result = NormalizeFile.IsCpfValid(cpf);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CpfWithSizeDifferentFromEleven() {
            // Arrange
            var cpf = "123456789123";

            // Act
            var result = NormalizeFile.IsCpfValid(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void CpfWithEqualCharacters() {
            // Arrange
            var cpf = "11111111111"; 

            // Act
            var result = NormalizeFile.IsCpfValid(cpf);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void DateHappyFlow() {
            // Arrange
            var dateString = "20230801"; // Format: yyyyMMdd

            // Act
            var result = NormalizeFile.DateConverter(dateString);
            var data = new DateTime(2023, 08, 01);
            var expected = TimeZoneInfo.ConvertTimeToUtc(data);

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DateMonthInvalid() {
            // Arrange
            var dateString = "20237001"; // Format: yyyyMMdd

            // Act
            var result = NormalizeFile.DateConverter(dateString);
            var expected = new DateTime();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void DataDayInvalid() {
            // Arrange
            var dateString = "20230070"; // Format: yyyyMMdd

            // Act
            var result = NormalizeFile.DateConverter(dateString);
            var expected = new DateTime();

            // Assert
            Assert.Equal(expected, result);
        }

    }
}
