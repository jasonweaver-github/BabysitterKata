namespace WageForBabysitter
{
    internal class Program
    {
        static void Main()
        {
            bool shouldRestart = true;

            // If either the total is successfully given or an exception is thrown, the application will restart from the beginning.
            while (shouldRestart)
            {
                try
                {
                    ConsoleView consoleView = new ConsoleView();
                    BabySitter babySitter = new BabySitter();

                    (consoleView.StartTimeParsed, consoleView.BedTimeParsed, consoleView.EndTimeParsed) = consoleView.InputData();

                    babySitter.DateToIntConverter(consoleView.StartTimeParsed, consoleView.BedTimeParsed, consoleView.EndTimeParsed);

                    babySitter.TotalPayment = babySitter.CalculatePayment(babySitter.StartTime, babySitter.BedTime, babySitter.EndTime);

                    consoleView.DisplayWage(babySitter.TotalPayment);
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }
    }
}