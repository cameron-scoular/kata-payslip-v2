using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class ConsoleUserInterface : IUserInterface
    {

        private Dictionary<string, int> MonthIntegerDictionary;

        public ConsoleUserInterface(Dictionary<string, int> monthIntegerDictionary)
        {
            MonthIntegerDictionary = monthIntegerDictionary;
        }

        public void WriteMessage(string message)
        {
            Console.WriteLine($"\r\n{message}\r\n");
        }

        public void WritePayslipInformation(PayslipInformation payslipInformation)
        {
            Console.WriteLine("Your payslip has been generated!");

            Console.WriteLine("Name: " + payslipInformation.Fullname);
            
            // Reverse dictionary lookups for month string associated with month integer
            var startMonthString =  MonthIntegerDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentStartDate.Month).Key;
            var endMonthString =  MonthIntegerDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentEndDate.Month).Key;

            startMonthString = FirstCharToUpperCase(startMonthString);
            endMonthString = FirstCharToUpperCase(endMonthString);

            Console.WriteLine("Pay Period: {0} {1} - {2} {3}", payslipInformation.PaymentStartDate.Day, startMonthString, payslipInformation.PaymentEndDate.Day, endMonthString);

            Console.WriteLine("Gross Income: " + payslipInformation.GrossIncome);
            
            Console.WriteLine("Income Tax: " + payslipInformation.IncomeTax);
            
            Console.WriteLine("Net Income: " + payslipInformation.NetIncome);
            
            Console.WriteLine("Super: " + payslipInformation.Super + "\r\n");
        }

        public uint PromptUnsignedInteger(string message)
        {
            Console.WriteLine(message);

            var userInput = Console.ReadLine();
            uint parsedInput;

            // Ensuring input is an unsigned integer
            while (!uint.TryParse(userInput, out parsedInput))
            {
                Console.WriteLine("The value you entered is not valid, please enter an unsigned integer:");
                userInput = Console.ReadLine();
            }

            return parsedInput;
        }

        public string PromptString(string message, int maxLength)
        {
            Console.WriteLine(message);
            string userInput = Console.ReadLine();

            // Ensuring input is nonempty, less than max length, and uses alphabet characters only before proceeding
            while (InputIsNotValidAlphabeticalString(maxLength, userInput))
            {
                Console.WriteLine(
                    "The string you entered is not valid, please enter a valid string without any numbers or special characters:");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static bool InputIsNotValidAlphabeticalString(int maxLength, string userInput)
        {
            return userInput == string.Empty || userInput.Length > maxLength || !Regex.IsMatch(userInput, @"^[a-zA-Z]+$");
        }

        public DateTime PromptDateTime(string message)
        {
            while (true)
            {
                Console.WriteLine(message);

                var inputString = Console.ReadLine();
                var userInputArray = inputString.ToLower().Split(" ");

                try
                {
                    var dayString = userInputArray[0];
                    var monthString = userInputArray[1];

                    var monthInteger = MonthIntegerDictionary[monthString];
                    var dayInteger = uint.Parse(dayString);
                    
                    return new DateTime(1, monthInteger, checked((int) dayInteger));

                }
                catch (Exception e)
                {
                    if (e is FormatException)
                    {
                        Console.WriteLine("The date given is not in the correct format (e.g. 17 March)");
                    }
                    else if (e is KeyNotFoundException)
                    {
                        Console.WriteLine("The given month is not valid");
                    }
                    else if (e is ArgumentOutOfRangeException || e is IndexOutOfRangeException)
                        Console.WriteLine("The date given is not a valid date");
                }
            }
        }

        private string FirstCharToUpperCase(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}