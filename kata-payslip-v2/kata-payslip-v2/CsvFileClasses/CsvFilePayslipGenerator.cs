using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class CsvFilePayslipGenerator : IPayslipGenerator
    {
        private IInputHandler FileInputHandler;
        private IOutputHandler FileOutputHandler;
        private IPayslipInformationFactory _monthlyPayslipInformationFactory;

        public CsvFilePayslipGenerator(IInputHandler fileInputHandler, IOutputHandler fileOutputHandler, IPayslipInformationFactory monthlyPayslipInformationFactory)
        {
            FileInputHandler = fileInputHandler;
            FileOutputHandler = fileOutputHandler;
            _monthlyPayslipInformationFactory = monthlyPayslipInformationFactory;
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

                var payslipInformation = _monthlyPayslipInformationFactory.CreatePayslipInformationObject(userInputInformation);
                
                FileOutputHandler.WritePayslipInformation(payslipInformation);
                
            }

        }
    }
}