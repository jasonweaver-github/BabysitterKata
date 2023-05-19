using WageForBabysitter;

namespace WageForBabysitterTests
{
    public class ValidateDataTests
    {

        [Fact]
        public void WhenPassedValidTimeInputs_ThenReturnsValidDateTimeValues()
        {
            // Arrange
            string startTimeInput = "5:00PM";
            string bedTimeInput = "9:00PM";
            string endTimeInput = "2:00AM";

            // Act            
            var result = ValidateData.ParseAndValidate(startTimeInput, bedTimeInput, endTimeInput);

            // Assert
            Assert.Equal(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 17, 0, 0), result.Item1);
            Assert.Equal(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0), result.Item2);
            Assert.Equal(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 2, 0, 0), result.Item3);
        }

        [Fact]
        public void WhenPassedInvalidTimeInputs_ThenThrowsException()
        {
            // Arrange
            string startTimeInput = "abcd"; // Invalid format
            string bedTimeInput = "88888";
            string endTimeInput = "$%^&$";

            // Act & Assert
            Assert.Throws<Exception>(() => ValidateData.ParseAndValidate(startTimeInput, bedTimeInput, endTimeInput));
        }
    }
}
