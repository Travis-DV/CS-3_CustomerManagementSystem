using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validator
{
    static class Validator
    {
        public static int OneOrTwo(string message1, string message2, string message3 = "")
        {
            string questionString = $@"1. {message1}
2. {message2}
3. Cancel";
            if (message3 != "")
            {
                questionString = $@"{message3}
1. {message1}
2. {message2}
3. Cancel";
            }

            string? input = null;
            int output;
            while (input == null || !int.TryParse(input, out output) || output > 3 || output < 1)
            {
                Console.Clear();
                Console.WriteLine(questionString);
                input = Console.ReadLine();
            }

            return output;
        }

        public static string CheckString(string message)
        {
            string? CheckString = null;

            while (CheckString == null || !CheckString.Any(Char.IsLetterOrDigit))
            {
                Console.Clear();
                Console.Write($"{message} (string): ");
                CheckString = Console.ReadLine();
            }
            return CheckString;
        }

        public static int CheckInt(string message, int Max, int Min = 0)
        {
            string? input = null;
            int output = -1;

            while (input == null || !int.TryParse(input, out output) || output < Min || output > Max)
            {
                Console.Clear();
                Console.Write($"{message} (int): ");
                input = Console.ReadLine();
            }

            return output;
        }

        public static DateOnly GetDate()
        {
            int CurrentOrCustom = Validator.OneOrTwo("Set Date Time to Current", "Custom Date Time");

            DateOnly OutTime = DateOnly.FromDateTime(DateTime.Now);
            switch (CurrentOrCustom)
            {
                case 1:
                    break;
                case 2:
                    string? outTime = null;

                    while (outTime == null || !DateOnly.TryParse(outTime, out OutTime))
                    {
                        Console.Clear();
                        Console.Write("Give Date and Time (MM-DD-YYYY HH:MM:SS): ");
                        outTime = Console.ReadLine();
                    }
                    break;
            }

            return OutTime;
        }

        public static DateTime GetDateTime()
        {
            int CurrentOrCustom = Validator.OneOrTwo("Set Date Time to Current", "Custom Date Time");

            DateTime OutTime = DateTime.Now;
            switch (CurrentOrCustom)
            {
                case 1:
                    OutTime = DateTime.Now;
                    break;
                case 2:
                    string? outTime = null;

                    while (outTime == null || !DateTime.TryParse(outTime, out OutTime))
                    {
                        Console.Clear();
                        Console.Write("Give Date and Time (MM-DD-YYYY HH:MM:SS): ");
                        outTime = Console.ReadLine();
                    }
                    break;
            }

            return OutTime;
        }

        public static double CheckFloat(string message, double Min = 0)
        {
            string? input = null;
            double output = -1;

            while (input == null || !double.TryParse(input, out output) || output < Min)
            {
                Console.Clear();
                Console.Write($"{message} (double): ");
                input = Console.ReadLine();
            }

            return output;
        }

        public static String GetEmail()
        {
            string? email = null;

            while (email == null || !email.Contains("@") || !email.Contains("."))
            {
                email = Validator.CheckString("Email");
            }

            return email;
        }
    }
}
