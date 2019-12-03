using System.Collections.Generic;
using System.IO;
using System.Linq;
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class CsvFileOutputHandler : IOutputHandler
    {

        public readonly Dictionary<string, int> MontIntegerDictionary;
        public readonly string OutputFilePath;

        
        public CsvFileOutputHandler(Dictionary<string, int> monthIntegerDictionary, string outputFilePath)
        {
            MontIntegerDictionary = monthIntegerDictionary;
            OutputFilePath = outputFilePath;
            CreateOutputFile();
        }

        public void CreateOutputFile()
        {
            var headerString = "name, pay period, gross income, income tax, net income, super\r\n";
            File.WriteAllText(OutputFilePath, headerString);
        }
        
        public void WritePayslipInformation(PayslipInformation payslipInformation)
        {
            
            using (var file = new System.IO.StreamWriter(OutputFilePath, true))
            {
                var startMonthInteger = payslipInformation.PaymentStartDate.Month;
                var endMonthInteger = payslipInformation.PaymentEndDate.Month;
                var startMonthString =  MontIntegerDictionary.FirstOrDefault(x => x.Value == startMonthInteger).Key;
                var endMonthString =  MontIntegerDictionary.FirstOrDefault(x => x.Value == endMonthInteger).Key;

                var payPeriod = $"{payslipInformation.PaymentStartDate.Day} {startMonthString} - {payslipInformation.PaymentEndDate.Day} {endMonthString}";
                
                
                var newPayslipRow = $"{payslipInformation.Fullname}, {payPeriod}, {payslipInformation.GrossIncome}, " +
                                    $"{payslipInformation.IncomeTax}, {payslipInformation.NetIncome}, {payslipInformation.Super}";
                
                file.WriteLine(newPayslipRow);
            }
            
        }
    }
}