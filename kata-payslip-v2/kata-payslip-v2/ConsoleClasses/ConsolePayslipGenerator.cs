
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class ConsolePayslipGenerator : IPayslipGenerator
    {
        private IUserInterface UserInterface;
        private IInputHandler InputHandler;
        private IPayslipInformationFactory _monthlyPayslipInformationFactory;

        public ConsolePayslipGenerator(IUserInterface userInterface, IInputHandler inputHandler, IPayslipInformationFactory monthlyPayslipInformationFactory)
        {
            UserInterface = userInterface;
            InputHandler = inputHandler;
            _monthlyPayslipInformationFactory = monthlyPayslipInformationFactory;
        }

        public void GeneratePayslip()
        {

            var userInputInformation = InputHandler.GetNextUserInputInformation();

            var payslipInformation = _monthlyPayslipInformationFactory.CreatePayslipInformationObject(userInputInformation);
            
            UserInterface.WriteMessage("Your payslip has been generated!");
            UserInterface.WritePayslipInformation(payslipInformation);
            
        }

        
        
    }
}