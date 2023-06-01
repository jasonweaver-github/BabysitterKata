using WageForBabysitter;

namespace WageForBabysitterTests
{

    public class ConsoleViewTests
    {

        ConsoleView consoleView = new ConsoleView();

        [Fact]
        public void WhenSomethingHappens_ThenInputIsNotNull()
        {          
            // Arrange/Act
            var startTimeInput = consoleView.StartTimeInput;
            var endTimeInput = consoleView.EndTimeInput;
            var bedTimeInput = consoleView.BedTimeInput;

            // Assert
            Assert.NotNull(startTimeInput);
            Assert.NotNull(endTimeInput);
            Assert.NotNull(bedTimeInput);
        }

        [Fact]
        public void WhenUsersInputData_ThenProgramCapturesCorrectValue()
        {
            // Arrange
            var consoleInput = new StringReader("9:00PM");
            Console.SetIn(consoleInput);

            // Act
            consoleView.StartTimeInput = Console.ReadLine();

            // Assert
            string capturedInputStart = consoleView.StartTimeInput;
            string expectedInput = "9:00PM";
            Assert.Equal(expectedInput, capturedInputStart);
        }

        [Fact]
        public void WhenTheTotalWageIsCalculated_ThenDisplayTheAmountToTheUser()
        {
            // Arrange
            var output = new StringWriter();
            Console.SetOut(output);

            int totalPayment = 35;

            string expectedMessage = "The babysitter gets paid $35 for one night of work.";

            // Act
            consoleView.DisplayWage(totalPayment);
            string actualMessage = output.ToString().Trim();

            // Assert
            Assert.Equal(expectedMessage, actualMessage);
        }
    }
}