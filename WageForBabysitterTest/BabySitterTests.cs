using WageForBabysitter;

namespace WageForBabysitterTests
{
    public class BabySitterTests
    {

        BabySitter babySitter = new BabySitter();

        [Fact]
        public void WhenValidDateIsSubmitted_ThenExtractHourAndConvertToInt()
        {
            // Arrange
            DateTime startTime = new DateTime(2023, 1, 1, 20, 0, 0); // 8:00 PM
            DateTime bedTime = new DateTime(2023, 1, 1, 22, 0, 0); // 10:00 PM
            DateTime endTime = new DateTime(2023, 1, 2, 2, 0, 0); // 2:00 AM

            // Act
            babySitter.DateToIntConverter(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(20, babySitter.StartTime);
            Assert.Equal(22, babySitter.BedTime);
            Assert.Equal(2, babySitter.EndTime);
        }

        [Fact]
        public void WhenStartTimeIsAfterMidnightAndBefore4_ThenAdd24Hours()
        {
            // Arrange
            int startTime = 2;
            int bedTime = 21;
            int endTime = 3;
            int expectedStartTime = 26;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTime, babySitter.StartTimeTest);
        }

        [Fact]
        public void WhenBedTimeIsAfterMidnightAndBefore4_ThenAdd24Hours()
        {
            // Arrange
            int startTime = 2;
            int bedTime = 1;
            int endTime = 3;
            var expected = "Invalid time range. Bedtime must be earlier than midnight.";
            //int expectedBedTime = 25;

            // Act/Assert
            var exception = Assert.Throws<Exception>(() => babySitter.CalculatePayment(startTime, bedTime, endTime));
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public void WhenEndTimeIsAfterMidnightAndBefore4_ThenAdd24Hours()
        {
            // Arrange
            int startTime = 2;
            int bedTime = 21;
            int endTime = 3;
            int expectedEndTime = 27;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedEndTime, babySitter.EndTimeTest);
        }

        [Fact]
        public void WhenStartTimeIsAfter4AndBefore17_ThenThrowException()
        {
            // Arrange
            var expected = "Invalid time range. The babysitter must arrive no earlier than 5:00PM.";
            int earliestArrival = babySitter.EarliestArrivalTime;
            int latestDeparture = babySitter.LatestDepartureTime;
            int startTime = babySitter.StartTime = 8; // 8:00 AM
            int bedTime = babySitter.BedTime = 21;
            int endTime = babySitter.EndTime = 22;

            // Act
            if (startTime > latestDeparture && startTime < earliestArrival)
            {
                // Assert
                var exception = Assert.Throws<Exception>(() => babySitter.CalculatePayment(startTime, bedTime, endTime));
                Assert.Equal(expected, exception.Message);
            }
        }

        [Fact]
        public void WhenEndTimeIsAfter4andBefore17_ThenThrowException()
        {
            // Arrange
            var expected = "Invalid time range. The babysitter must leave no later than 4:00AM.";
            int earliestArrival = babySitter.EarliestArrivalTime;
            int latestDeparture = babySitter.LatestDepartureTime;
            int startTime = babySitter.StartTime = 19;
            int bedTime = babySitter.BedTime = 21;
            int endTime = babySitter.EndTime = 8; // 8:00 AM

            // Act
            if (endTime > latestDeparture && endTime < earliestArrival)
            {
                // Assert
                var exception = Assert.Throws<Exception>(() => babySitter.CalculatePayment(startTime, bedTime, endTime));
                Assert.Equal(expected, exception.Message);
            }
        }

        [Fact]
        public void WhenStartTimeIsGreaterThanEndTime_ThenThrowException()
        {
            // Arrange
            var expected = "Invalid time range. The babysitter's end time must be later than their arrival time.";
            int startTime = babySitter.StartTime = 20; // 8:00 PM
            int bedTime = babySitter.BedTime = 21;
            int endTime = babySitter.EndTime = 19; // 7:00 PM

            // Act
            if (startTime > endTime)
            {
                // Assert
                var exception = Assert.Throws<Exception>(() => babySitter.CalculatePayment(startTime, bedTime, endTime));
                Assert.Equal(expected, exception.Message);
            }
        }

        [Fact]
        public void WhenBedTimeIsEqualToOrGreaterThanMidnight_ThenThrowException()
        {
            // Arrange
            var expected = "Invalid time range. Bedtime must be earlier than midnight.";
            int startTime = babySitter.StartTime = 20; // 8:00 PM
            int bedTime = babySitter.BedTime = 25; // 1:00 AM
            int midnight = 24; // 12:00 AM
            int endTime = babySitter.EndTime = 26; // 2:00 AM

            // Act
            if (bedTime > midnight)
            {
                // Assert
                var exception = Assert.Throws<Exception>(() => babySitter.CalculatePayment(startTime, bedTime, endTime));
                Assert.Equal(expected, exception.Message);
            }
        }

        [Fact]
        public void WhenOrderOfEventsIsStartTimeBedTimeMidnightEndTime_ThenCalculateNumOfHoursCorrectly()
        {
            //Order of events: StartTime -> BedTime -> Midnight -> EndTime
            // Arrange
            int startTime = 18;
            int bedTime = 21;
            int midnight = 24;
            int endTime = 26;

            int expectedStartTimeToBedTimeNumOfHours = bedTime - startTime;
            int expectedBedTimeToMidnightNumOfHours = midnight - bedTime;
            int expectedMidnightToEndTimeNumOfHours = endTime - midnight;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
            Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
            Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        }

        // Note: This scenario for this test is deprecated because it's no longer possible for bedtime to be after midnight.
        //[Fact]
        //public void WhenOrderOfEventsIsStartTimeMidnightBedTimeEndTime_ThenCalculateNumOfHoursCorrectly()
        //{
        //    //Order of events: StartTime -> Midnight -> BedTime -> EndTime
        //    // Arrange
        //    int startTime = 20;
        //    int midnight = 24;
        //    int bedTime = 25;
        //    int endTime = 26;

        //    int expectedStartTimeToBedTimeNumOfHours = midnight - startTime;
        //    int expectedBedTimeToMidnightNumOfHours = 0;
        //    int expectedMidnightToEndTimeNumOfHours = endTime - midnight;

        //    // Act
        //    babySitter.CalculatePayment(startTime, bedTime, endTime);

        //    // Assert
        //    Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
        //    Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
        //    Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        //}

        [Fact]
        public void WhenOrderOfEventsIsStartTimeBedTimeEndTimeMidnight_ThenCalculateNumOfHoursCorrectly()
        {
            //Order of events: StartTime -> BedTime -> EndTime -> Midnight
            // Arrange
            int startTime = 20;
            int bedTime = 21;                       
            int endTime = 22;
            int midnight = 24;

            int expectedStartTimeToBedTimeNumOfHours = bedTime - startTime;
            int expectedBedTimeToMidnightNumOfHours = endTime -  bedTime;
            int expectedMidnightToEndTimeNumOfHours = 0;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
            Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
            Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        }

        [Fact]
        public void WhenOrderOfEventsIsStartTimeEndTimeBedTimeMidnight_ThenCalculateNumOfHoursCorrectly()
        {
            //Order of events: StartTime -> EndTime -> BedTime/Midnight
            // Arrange
            int startTime = 19;
            int endTime = 20;
            int bedTime = 22;
            int midnight = 24;
            
            int expectedStartTimeToBedTimeNumOfHours = endTime - startTime;
            int expectedBedTimeToMidnightNumOfHours = 0;
            int expectedMidnightToEndTimeNumOfHours = 0;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
            Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
            Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        }

        // Note: This scenario for this test is deprecated because it's no longer possible for bedtime to be after midnight.
        //[Fact]
        //public void WhenOrderOfEventsIsStartTimeMidnightEndTimeBedTime_ThenCalculateNumOfHoursCorrectly()
        //{
        //    //Order of events: StartTime -> Midnight -> EndTime -> BedTime
        //    // Arrange
        //    int startTime = 19;
        //    int midnight = 24;
        //    int endTime = 26;
        //    int bedTime = 27;

        //    int expectedStartTimeToBedTimeNumOfHours = midnight - startTime;
        //    int expectedBedTimeToMidnightNumOfHours = 0;
        //    int expectedMidnightToEndTimeNumOfHours = endTime - midnight;

        //    // Act
        //    babySitter.CalculatePayment(startTime, bedTime, endTime);

        //    // Assert
        //    Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
        //    Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
        //    Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        //}

        [Fact]
        public void WhenOrderOfEventsIsBedTimeStartTimeMidnightEndTime_ThenCalculateNumOfHoursCorrectly()
        {
            //Order of events: BedTime -> StartTime -> Midnight -> EndTime
            // Arrange
            int bedTime = 19;
            int startTime = 22;           
            int midnight = 24;
            int endTime = 26;

            int expectedStartTimeToBedTimeNumOfHours = 0;
            int expectedBedTimeToMidnightNumOfHours = midnight - startTime;
            int expectedMidnightToEndTimeNumOfHours = endTime - midnight;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
            Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
            Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        }

        [Fact]
        public void WhenOrderOfEventsIsBedTimeStartTimeEndTimeMidnight_ThenCalculateNumOfHoursCorrectly()
        {
            //Order of events: BedTime -> StartTime -> EndTime -> Midnight
            // Arrange            
            int bedTime = 18;          
            int startTime = 19;
            int endTime = 22;
            int midnight = 24;

            int expectedStartTimeToBedTimeNumOfHours = 0;
            int expectedBedTimeToMidnightNumOfHours = endTime - startTime;
            int expectedMidnightToEndTimeNumOfHours = 0;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
            Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
            Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        }

        [Fact]
        public void WhenOrderOfEventsIsBedTimeMidnightStartTimeEndTime_ThenCalculateNumOfHoursCorrectly()
        {
            //Order of events: BedTime -> Midnight -> StartTime -> EndTime
            // Arrange            
            int bedTime = 23;
            int midnight = 24;
            int startTime = 26;
            int endTime = 28;

            int expectedStartTimeToBedTimeNumOfHours = 0;
            int expectedBedTimeToMidnightNumOfHours = 0;
            int expectedMidnightToEndTimeNumOfHours = endTime - startTime;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
            Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
            Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        }

        [Fact]
        public void WhenOrderOfEventsIsMidnightStartTimeEndTime_ThenCalculateNumOfHoursCorrectly()
        {
            //Order of events: Midnight -> StartTime -> EndTime
            // Arrange
            var babySitter = new BabySitter();
            int bedTime = 20;
            int midnight = 24;
            int startTime = 25;                     
            int endTime = 27;

            int expectedStartTimeToBedTimeNumOfHours = 0;
            int expectedBedTimeToMidnightNumOfHours = 0;
            int expectedMidnightToEndTimeNumOfHours = endTime - startTime;

            // Act
            babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedStartTimeToBedTimeNumOfHours, babySitter.StartTimeToBedTimeNumOfHours);
            Assert.Equal(expectedBedTimeToMidnightNumOfHours, babySitter.BedTimeToMidnightNumOfHours);
            Assert.Equal(expectedMidnightToEndTimeNumOfHours, babySitter.MidnightToEndTimeNumOfHours);
        }

        [Fact]
        public void WhenHoursAreTotaled_ThenCalculatePayment()
        {
            // Arrange
            int startTime = 19;
            int bedTime = 22;          
            int endTime = 28;

            int expectedTotalPayment = (3 * 12) + (2 * 8) + (4 * 16);

            // Act
            int actualTotalPayment = babySitter.CalculatePayment(startTime, bedTime, endTime);

            // Assert
            Assert.Equal(expectedTotalPayment, actualTotalPayment);
        }

    }
}
