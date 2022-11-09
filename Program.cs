using System.Text;

namespace Curiosidades
{
    class Curiosidades
    {
        private void DisplayIntro()
        {
            Console.WriteLine("");
            Console.WriteLine("Coisas curiosas que talvez seja interessante você saber.");
            Console.WriteLine("");
        }

        private bool ValidateDate(string InputDate, out DateTime ReturnDate)
        {
            string DateString = InputDate.Replace(",", "/");

            return (DateTime.TryParse(DateString, out ReturnDate));
        }

        private DateTime PromptForADate(string Prompt)
        {
            bool Success = false;
            string LineInput = String.Empty;
            DateTime TodaysDate = DateTime.MinValue;

            while (!Success)
            {
                Console.Write(Prompt);
                LineInput = Console.ReadLine().Trim().ToLower();

                Success = ValidateDate(LineInput, out TodaysDate);

                if (!Success)
                {
                    Console.WriteLine("*** Data Inválida.  Tente novamente.");
                    Console.WriteLine("");
                }
            }

            return TodaysDate;
        }

        private void CalculateDateDiff(DateTime TodaysDate, DateTime BirthDate, Double Factor, out int AgeInYears, out int AgeInMonths, out int AgeInDays)
        {
            TimeSpan TimeDiff = TodaysDate.Subtract(BirthDate);
            Double NumberOfDays = TimeDiff.Days * Factor;
            DateTime FactorDate = BirthDate.AddDays(NumberOfDays);

            AgeInMonths = FactorDate.Month - BirthDate.Month;
            AgeInYears = FactorDate.Year - BirthDate.Year;

            if (FactorDate.Day < BirthDate.Day)
            {
                AgeInMonths--;
            }

            if (AgeInMonths < 0)
            {
                AgeInYears--;
                AgeInMonths += 12;
            }

            AgeInDays = (FactorDate - BirthDate.AddMonths((AgeInYears * 12) + AgeInMonths)).Days;

        }

        private void WriteColumnOutput(string Message, int Years, int Months, int Days)
        {

            Console.WriteLine("{0,-25} {1,-10:N0} {2,-10:N0} {3,-10:N0}", Message, Years, Months, Days);

        }

        private void DisplayOutput(DateTime TodaysDate, DateTime BirthDate)
        {
            Console.WriteLine("");

            if (TodaysDate.Year < 1582)
            {
                Console.WriteLine("Não estou preparado para dar uma data antes de MDLXXXII.");
                return;
            }

            Console.Write(" {0} ", BirthDate.ToString("d"));

            string DateVerb = "";
            if (BirthDate.CompareTo(TodaysDate) < 0)
            {
                DateVerb = "foi uma ";
            }
            else if (BirthDate.CompareTo(TodaysDate) == 0)
            {
                DateVerb = "é uma ";
            }
            else
            {
                DateVerb = "será uma ";
            }
            Console.Write("{0}", DateVerb);

            if (BirthDate.DayOfWeek.ToString().Equals("Sexta-Feira") && BirthDate.Day == 13)
            {
                Console.WriteLine("{0} CUIDADO!", BirthDate.DayOfWeek.ToString());
            }
            else
            {
                Console.WriteLine("{0}", BirthDate.DayOfWeek.ToString());
            }

            if (BirthDate.Month == TodaysDate.Month && BirthDate.Day == TodaysDate.Day)
            {
                Console.WriteLine("");
                Console.Write("***Feliz Aniversário***");
            }

            Console.WriteLine("");

            if (DateVerb.Trim().Equals("foi uma"))
            {

                Console.WriteLine("{0,-24} {1,-10} {2,-10} {3,-10}", " ", "Anos", "Meses", "Dias");

                int TheYears = 0, TheMonths = 0, TheDays = 0;
                int FlexYears = 0, FlexMonths = 0, FlexDays = 0;

                CalculateDateDiff(TodaysDate, BirthDate, 1, out TheYears, out TheMonths, out TheDays);
                WriteColumnOutput("Sua idade se vc nasceu em", TheYears, TheMonths, TheDays);

                FlexYears = TheYears;
                FlexMonths = TheMonths;
                FlexDays = TheDays;
                CalculateDateDiff(TodaysDate, BirthDate, .35, out FlexYears, out FlexMonths, out FlexDays);
                WriteColumnOutput("Você dormiu durante", FlexYears, FlexMonths, FlexDays);

                FlexYears = TheYears;
                FlexMonths = TheMonths;
                FlexDays = TheDays;
                CalculateDateDiff(TodaysDate, BirthDate, .17, out FlexYears, out FlexMonths, out FlexDays);
                WriteColumnOutput("Você comeu durante", FlexYears, FlexMonths, FlexDays);

                FlexYears = TheYears;
                FlexMonths = TheMonths;
                FlexDays = TheDays;
                CalculateDateDiff(TodaysDate, BirthDate, .23, out FlexYears, out FlexMonths, out FlexDays);
                string FlexPhrase = "Você brincou durante";
                if (TheYears > 3)
                    FlexPhrase = "Você brincou/estudou";
                if (TheYears > 9)
                    FlexPhrase = "Você trabalhou/brincou";
                WriteColumnOutput(FlexPhrase, FlexYears, FlexMonths, FlexDays);

                FlexYears = TheYears;
                FlexMonths = TheMonths;
                FlexDays = TheDays;
                CalculateDateDiff(TodaysDate, BirthDate, .25, out FlexYears, out FlexMonths, out FlexDays);
                WriteColumnOutput("Você relaxou durante", FlexYears, FlexMonths, FlexDays);

                Console.WriteLine("");
                Console.WriteLine("* Talvez você se aposente em {0} *".PadLeft(38), BirthDate.Year + 65);
            }
        }

        public void PlayTheGame()
        {
            DateTime TodaysDate = DateTime.MinValue;
            DateTime BirthDate = DateTime.MinValue;

            DisplayIntro();

            TodaysDate = PromptForADate("Digite a data de hoje no formato: Mês,Dia,Ano ");
            BirthDate = PromptForADate("Qual a sua data de nascimento?");

            DisplayOutput(TodaysDate, BirthDate);

        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            new Curiosidades().PlayTheGame();

        }
    }
}
