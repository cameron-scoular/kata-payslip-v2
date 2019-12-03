using System.Collections.Generic;
using System.IO;
using System.Linq;
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;
using Microsoft.VisualBasic;

namespace kata_payslip_v2
{
    public class CsvFileOutputHandler : IOutputHandler
    {

        public readonly Dictionary<string, int> MontIntegerDictionary;
        public readonly string OutputFilePath;
        public readonly string OutputHeaderString;

        
        public CsvFileOutputHandler(Dictionary<string, int> monthIntegerDictionary, string outputFilePath, string outputHeaderString)
        {
            MontIntegerDictionary = monthIntegerDictionary;
            OutputFilePath = outputFilePath;
            OutputHeaderString = outputHeaderString;
        }

        public void WritePayslipInformation(PayslipInformation payslipInformation)
        {
            if (!File.Exists(OutputFilePath))
            {
                File.WriteAllText(OutputFilePath, OutputHeaderString);
            }
            
            using (var file = new StreamWriter(OutputFilePath, true))
            {
                var startMonthInteger = payslipInformation.PaymentStartDate.Month;
                var endMonthInteger = payslipInformation.PaymentEndDate.Month;
                
                var startMonthString =  MontIntegerDictionary.FirstOrDefault(x => x.Value == startMonthInteger).Key;
                var endMonthString =  MontIntegerDictionary.FirstOrDefault(x => x.Value == endMonthInteger).Key;

                startMonthString = FirstCharToUpperCase(startMonthString);
                endMonthString = FirstCharToUpperCase(endMonthString);

                var payPeriod = $"{payslipInformation.PaymentStartDate.Day} {startMonthString} - {payslipInformation.PaymentEndDate.Day} {endMonthString}";
                
                
                var newPayslipRow = $"{payslipInformation.Fullname}, {payPeriod}, {payslipInformation.GrossIncome}, " +
                                    $"{payslipInformation.IncomeTax}, {payslipInformation.NetIncome}, {payslipInformation.Super}";
                
                file.WriteLine(newPayslipRow);
            }
            
        }
        
        private string FirstCharToUpperCase(string input)
        {
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
    }
}