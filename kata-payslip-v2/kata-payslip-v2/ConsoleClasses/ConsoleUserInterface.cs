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
            Console.WriteLine("Name: " + payslipInformation.Fullname);
            
            
            string startMonth =  MonthIntegerDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentStartDate.Month).Key;
            string endMonth =  MonthIntegerDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentEndDate.Month).Key;

            Console.WriteLine("Pay Period: {0} {1} - {2} {3}", payslipInformation.PaymentStartDate.Day, startMonth, payslipInformation.PaymentEndDate.Day, endMonth);

            Console.WriteLine("Gross Income: " + payslipInformation.GrossIncome);
            
            Console.WriteLine("Income Tax: " + payslipInformation.IncomeTax);
            
            Console.WriteLine("Net Income: " + payslipInformation.NetIncome);
            
            Console.WriteLine("Super: " + payslipInformation.Super + "\r\n");
        }

        public uint PromptUnsignedInteger(string message)
        {
            Console.WriteLine(message);

            string userInput = Console.ReadLine();
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
            while (userInput == string.Empty || userInput.Length > maxLength ||
                   !Regex.IsMatch(userInput, @"^[a-zA-Z]+$"))
            {
                Console.WriteLine(
                    "The string you entered is not valid, please enter a valid string without any numbers or special characters:");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        public DateTime PromptDateTime(string message)
        {
            while (true)
            {
                Console.WriteLine(message);

                var userInputArray = Console.ReadLine().ToLower().Split(" ");

                try
                {
                    var dayString = userInputArray[0];
                    var monthString = userInputArray[1];
                    
                    var monthInteger = MonthIntegerDictionary[FirstCharToUpperCase(monthString)];
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