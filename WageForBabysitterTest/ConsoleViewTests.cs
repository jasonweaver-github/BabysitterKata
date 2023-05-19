using WageForBabysitter;

namespace WageForBabysitterTests
{

    //    The babysitter
    //- starts no earlier than 5:00PM
    //- leaves no later than 4:00AM
    //- gets paid $12/hour from start-time to bedtime
    //- gets paid $8/hour from bedtime to midnight
    //- gets paid $16/hour from midnight to end of job
    //- gets paid for full hours(no fractional hours)


    //Feature:
    //As a babysitter
    //In order to get paid for 1 night of work
    //I want to calculate my nightly charge


    public class ConsoleViewTests
    {

        ConsoleView consoleView = new ConsoleView();

        [Fact]
        public void WhenSomethingHappens_ThenInputIsNotNull()
        {          
            //Act
            var startTimeInput = consoleView.StartTimeInput;
            var endTimeInput = consoleView.EndTimeInput;
            var bedTimeInput = consoleView.BedTimeInput;
            //Assert
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