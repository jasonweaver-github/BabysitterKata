namespace WageForBabysitter
{
    public class ValidateData
    {
        public static (DateTime, DateTime, DateTime) ParseAndValidate(string startTimeInput, string bedTimeInput, string endTimeInput)
        {
            DateTime startTime = DateTime.Now;
            DateTime bedTime = DateTime.Now;
            DateTime endTime = DateTime.Now;

            // This section is converting the strings to DateTimes as well as validating the user input. If the user input is invalid, an exception is thrown.
            if (!DateTime.TryParse(startTimeInput, out startTime) ||
                !DateTime.TryParse(bedTimeInput, out bedTime) ||
                !DateTime.TryParse(endTimeInput, out endTime))
            {
                throw new Exception("Invalid time format. Please provide times in a valid format, e.g., 5:00PM.");
            }

            return (startTime, bedTime, endTime);
        }
    }
}
