namespace WageForBabysitter
{
    public class BabySitter
    {

        public int StartTime { get; set; }
        public int StartTimeTest { get; set; }
        public int BedTime { get; set; }
        public int BedTimeTest { get; set; }
        public int EndTime { get; set; }
        public int EndTimeTest { get; set; }
        public int Midnight { get; set; } = 24;
        public int EarliestArrivalTime { get; set; } = 17;
        public int LatestDepartureTime { get; set; } = 4;
        public int NextDay { get; set; } = 24;
        public int TotalPayment { get; set; }
        public int StartTimeToBedTimeNumOfHours { get; set; }
        public int BedTimeToMidnightNumOfHours { get; set; }
        public int MidnightToEndTimeNumOfHours { get; set; }
        public int StartTimeToBedTimeRate { get; set; } = 12;
        public int BedTimeToMidnightRate { get; set; } = 8;
        public int MidnightToEndTimeRate { get; set; } = 16;

        // This method converts the DateTimes to ints by extracting the hour from each DateTime.
        public void DateToIntConverter(DateTime startTime, DateTime bedTime, DateTime endTime)
        {
            StartTime = startTime.Hour;
            BedTime = bedTime.Hour;
            EndTime = endTime.Hour;
        }

        // This method calculates the total payment for the babysitter based on the number of hours worked and the rates for each time period.
        public int CalculatePayment(int StartTime, int BedTime, int EndTime)
        {
            // When StartTime, BedTime, or EndTime occur after midnight, 24 hours is added to the time.
            if (StartTime <= LatestDepartureTime)
            {
                StartTime += NextDay;
            }

            if (BedTime <= LatestDepartureTime)
            {
                BedTime += NextDay;
            }

            if (EndTime <= LatestDepartureTime)
            {
                EndTime += NextDay;
            }

            // These 3 lines are for unit testing purposes only.
            StartTimeTest = StartTime; 
            BedTimeTest = BedTime;
            EndTimeTest = EndTime;

            // This block of code is validating agaisnt invalid time ranges and throwing an error when any occur.
            if (StartTime < EarliestArrivalTime && StartTime > LatestDepartureTime)
            {
                throw new Exception("Invalid time range. The babysitter must arrive no earlier than 5:00PM.");
            }

            if (EndTime > LatestDepartureTime && EndTime < EarliestArrivalTime)
            {
                throw new Exception("Invalid time range. The babysitter must leave no later than 4:00AM.");
            }
            
            if (StartTime > EndTime)
            {
                throw new Exception("Invalid time range. The babysitter's end time must be later than their arrival time.");
            }

            // Order of events: StartTime => BedTime => Midnight => EndTime
            if (StartTime <= BedTime && BedTime <= Midnight && Midnight <= EndTime)
            {
                StartTimeToBedTimeNumOfHours = BedTime - StartTime;
                BedTimeToMidnightNumOfHours = Midnight - BedTime;
                MidnightToEndTimeNumOfHours = EndTime - Midnight;
            }
            // Order of events: StartTime => Midnight => BedTime => EndTime
            else if (StartTime <= Midnight && Midnight <= BedTime && BedTime <= EndTime)
            {
                StartTimeToBedTimeNumOfHours = Midnight - StartTime;
                BedTimeToMidnightNumOfHours = 0;
                MidnightToEndTimeNumOfHours = EndTime - Midnight;
            }
            // Order of events: StartTime => BedTime => EndTime => Midnight
            else if (StartTime <= BedTime && BedTime <= EndTime && EndTime <= Midnight)
            {
                StartTimeToBedTimeNumOfHours = BedTime - StartTime;
                BedTimeToMidnightNumOfHours = EndTime - BedTime;
                MidnightToEndTimeNumOfHours = 0;
            }
            // Order of events: StartTime => EndTime => BedTime/Midnight
            else if (StartTime <= EndTime && EndTime <= BedTime && EndTime <= Midnight)
            {
                StartTimeToBedTimeNumOfHours = EndTime - StartTime;
                BedTimeToMidnightNumOfHours = 0;
                MidnightToEndTimeNumOfHours = 0;
            }
            // Order of events: StartTime => Midnight => EndTime => BedTime
            else if (StartTime <= Midnight && Midnight <= EndTime && EndTime <= BedTime)
            {
                StartTimeToBedTimeNumOfHours = Midnight - StartTime;
                BedTimeToMidnightNumOfHours = 0;
                MidnightToEndTimeNumOfHours = EndTime - Midnight;
            }
            // Order of events: BedTime => StartTime => Midnight => EndTime
            else if (BedTime <= StartTime && StartTime <= Midnight && Midnight <= EndTime)
            {
                StartTimeToBedTimeNumOfHours = 0;
                BedTimeToMidnightNumOfHours = Midnight - StartTime;
                MidnightToEndTimeNumOfHours = EndTime - Midnight;
            }
            // Order of events: BedTime => Midnight => StartTime => EndTime
            else if (BedTime <= Midnight && Midnight <= StartTime && StartTime <= EndTime)
            {
                StartTimeToBedTimeNumOfHours = 0;
                BedTimeToMidnightNumOfHours = 0;
                MidnightToEndTimeNumOfHours = EndTime - StartTime;
            }
            // Order of events: Midnight => StartTime => EndTime
            else if (Midnight <= StartTime)
            {
                StartTimeToBedTimeNumOfHours = 0;
                BedTimeToMidnightNumOfHours = 0;
                MidnightToEndTimeNumOfHours = EndTime - StartTime;
            }
            // In case any scenario occurs that is not accounted for above, an error is thrown.
            else
            {
                throw new Exception("Unexpected scenario.");
            }           

            // All of the hours worked are multiplied by their respective rates and added together to get the total payment.
            TotalPayment = (StartTimeToBedTimeNumOfHours * StartTimeToBedTimeRate) + (BedTimeToMidnightNumOfHours * BedTimeToMidnightRate) + (MidnightToEndTimeNumOfHours * MidnightToEndTimeRate);

            return TotalPayment;
        }
    }
}
