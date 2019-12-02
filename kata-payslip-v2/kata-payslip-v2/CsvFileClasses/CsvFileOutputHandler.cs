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
                var startMonth =  MontIntegerDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentStartDate.Month).Key;
                var endMonth =  MontIntegerDictionary.FirstOrDefault(x => x.Value == payslipInformation.PaymentEndDate.Month).Key;

                var payPeriod = $"{payslipInformation.PaymentStartDate.Day} {startMonth} - {payslipInformation.PaymentEndDate.Day} {endMonth}";
                
                
                var newPayslipRow = $"{payslipInformation.Fullname}, {payPeriod}, {payslipInformation.GrossIncome}, " +
                                    $"{payslipInformation.IncomeTax}, {payslipInformation.NetIncome}, {payslipInformation.Super}";
                
                file.WriteLine(newPayslipRow);
            }
            
        }
    }
}