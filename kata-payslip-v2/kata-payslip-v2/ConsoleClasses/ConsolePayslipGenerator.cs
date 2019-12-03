
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class ConsolePayslipGenerator : IPayslipGenerator
    {
        private readonly IUserInterface UserInterface;
        private readonly IInputHandler InputHandler;
        private readonly IPayslipInformationCalculator PayslipInformationCalculator;

        public ConsolePayslipGenerator(IUserInterface userInterface, IInputHandler inputHandler, IPayslipInformationCalculator payslipInformationCalculator)
        {
            UserInterface = userInterface;
            InputHandler = inputHandler;
            PayslipInformationCalculator = payslipInformationCalculator;
        }

        public void GeneratePayslip()
        {

            var userInputInformation = InputHandler.GetNextUserInputInformation();

            var payslipInformation = PayslipInformationCalculator.CreatePayslipInformation(userInputInformation);
            
            UserInterface.WriteMessage("Your payslip has been generated!");
            UserInterface.WritePayslipInformation(payslipInformation);
            
        }

        
        
    }
}