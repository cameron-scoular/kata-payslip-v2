using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;
using Microsoft.VisualBasic.FileIO;

namespace kata_payslip_v2
{
    public class CsvFileInputHandler : IInputHandler
    {
        
        private string FilePath { get; }
        
        private TextFieldParser FileFieldParser;

        private Dictionary<string, int> MonthIntegerDictionary;

        public CsvFileInputHandler(Dictionary<string, int> monthIntegerDictionary, string filePath)
        {
            MonthIntegerDictionary = monthIntegerDictionary;
            FilePath = filePath;

            FileFieldParser = new TextFieldParser(filePath);
            FileFieldParser.TextFieldType = FieldType.Delimited;
            FileFieldParser.SetDelimiters(",");
            FileFieldParser.ReadFields();
        }
        
        public UserInputInformation? GetNextUserInputInformation()
        {

            if (FileFieldParser.EndOfData)
            {
                FileFieldParser.Close();
                return null;
            }

            var fields = FileFieldParser.ReadFields();

            var userInputInformation = ParseCsvFields(fields);

            return userInputInformation;

        }

        private UserInputInformation ParseCsvFields(string[] rowFields)
        {
            var userInputInformation = new UserInputInformation();
            
            userInputInformation.Name = rowFields[0];
            userInputInformation.Surname = rowFields[1];
            userInputInformation.Salary = ParseSalary(rowFields[2]);
            userInputInformation.SuperRate = ParseSuperRate(rowFields[3]);

            var paymentPeriod = ParsePaymentPeriod(rowFields[4]);
            userInputInformation.PaymentStartDate = paymentPeriod.PaymentStartDate;
            userInputInformation.PaymentEndDate = paymentPeriod.PaymentEndDate;
            
            return userInputInformation;
        }

        private uint ParseSalary(string inputString)
        {
            return uint.Parse(inputString);
        }

        private uint ParseSuperRate(string inputString)
        {
            return uint.Parse(inputString.Replace("%", ""));
        }

        private PaymentPeriod ParsePaymentPeriod(string inputString)
        {
            
            var paymentPeriod = new PaymentPeriod();
            
            var dateInputs = inputString.Split(" ");
            dateInputs.ToList().Remove("-");
            
            paymentPeriod.PaymentStartDate = ParseDateTime(dateInputs[0], dateInputs[1]);
            paymentPeriod.PaymentEndDate = ParseDateTime(dateInputs[3], dateInputs[4]);

            
            return paymentPeriod;
        }

        private DateTime ParseDateTime(string dayString, string monthString)
        {
            var monthInteger = MonthIntegerDictionary[FirstCharToUpperCase(monthString)];
            var dayInteger = uint.Parse(dayString);
            
            return new DateTime(1, monthInteger, checked((int) dayInteger));
        }

        private string FirstCharToUpperCase(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        
    }
}