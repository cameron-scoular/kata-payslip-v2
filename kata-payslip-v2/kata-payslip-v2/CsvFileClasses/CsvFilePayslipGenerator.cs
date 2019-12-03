using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class CsvFilePayslipGenerator : IPayslipGenerator
    {
        private IInputHandler FileInputHandler;
        private IOutputHandler FileOutputHandler;
        private IPayslipInformationCalculator _monthlyPayslipInformationCalculator;

        public CsvFilePayslipGenerator(IInputHandler fileInputHandler, IOutputHandler fileOutputHandler, IPayslipInformationCalculator monthlyPayslipInformationCalculator)
        {
            FileInputHandler = fileInputHandler;
            FileOutputHandler = fileOutputHandler;
            _monthlyPayslipInformationCalculator = monthlyPayslipInformationCalculator;
        }
        
        public void GeneratePayslip()
        {

            while (true)
            {
                var userInputInformation = FileInputHandler.GetNextUserInputInformation();

                if (userInputInformation == null)
                {
                    break;
                }

                var payslipInformation = _monthlyPayslipInformationCalculator.CreatePayslipInformation(userInputInformation);
                
                FileOutputHandler.WritePayslipInformation(payslipInformation);
                
            }

        }
    }
}