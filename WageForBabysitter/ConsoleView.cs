using static WageForBabysitter.ValidateData;

namespace WageForBabysitter
{
    public class ConsoleView
    {
        private string _startTimeInput = "No Input";
        private string _bedTimeInput = "No Input";
        private string _endTimeInput = "No Input";

        public string StartTimeInput
        {
            get { return _startTimeInput; }
            set { _startTimeInput = value; }
        }

        public string BedTimeInput
        {
            get { return _bedTimeInput; }
            set { _bedTimeInput = value; }
        }

        public string EndTimeInput
        {
            get { return _endTimeInput; }
            set { _endTimeInput = value; }
        }

        public DateTime StartTimeParsed { get; set; }
        public DateTime BedTimeParsed { get; set; }
        public DateTime EndTimeParsed { get; set; }

        // This method prompts the user to enter the start time, bedtime, and end time.
        public (DateTime, DateTime, DateTime) InputData()
        {

            Console.WriteLine("Enter the start time (e.g., 5:00PM):");
            ConsoleView view = new ConsoleView();
            view.StartTimeInput = Console.ReadLine();

            Console.WriteLine("Enter the bedtime (e.g., 9:00PM):");
            view.BedTimeInput = Console.ReadLine();

            Console.WriteLine("Enter the end time (e.g., 4:00AM):");
            view.EndTimeInput = Console.ReadLine();

            (StartTimeParsed, BedTimeParsed, EndTimeParsed) = ParseAndValidate(view.StartTimeInput, view.BedTimeInput, view.EndTimeInput);

            return (StartTimeParsed, BedTimeParsed, EndTimeParsed);
        }

        // This final method displays the total wage to the user.
        public void DisplayWage(int totalPayment)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine($"The babysitter gets paid ${totalPayment} for one night of work.");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
