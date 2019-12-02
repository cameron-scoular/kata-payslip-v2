
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class MonthlyPayslipInformationFactory : IPayslipInformationFactory
    {
        public IPayslipCalculatorUtility PayslipCalculatorUtility;

        public MonthlyPayslipInformationFactory(IPayslipCalculatorUtility payslipCalculatorUtility)
        {
            PayslipCalculatorUtility = payslipCalculatorUtility;
        }

        public PayslipInformation CreatePayslipInformationObject(UserInputInformation userInputInformation)
        {
            
            PayslipInformation payslipInformation = new PayslipInformation();

            payslipInformation.Fullname = $"{userInputInformation.Name} {userInputInformation.Surname}";
            
            payslipInformation.GrossIncome = PayslipCalculatorUtility.CalculateGrossIncome(userInputInformation.Salary); 

            payslipInformation.IncomeTax = PayslipCalculatorUtility.CalculateIncomeTax(userInputInformation.Salary); 

            payslipInformation.NetIncome = PayslipCalculatorUtility.CalculateNetIncome(payslipInformation.GrossIncome, payslipInformation.IncomeTax);

            payslipInformation.Super = PayslipCalculatorUtility.CalculateSuperAmount(userInputInformation.SuperRate, payslipInformation.GrossIncome);

            payslipInformation.PaymentStartDate = userInputInformation.PaymentStartDate;

            payslipInformation.PaymentEndDate = userInputInformation.PaymentEndDate;

            return payslipInformation;

        }

        
        
    }
}