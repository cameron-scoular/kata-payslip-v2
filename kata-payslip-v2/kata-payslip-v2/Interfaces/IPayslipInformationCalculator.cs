using kata_payslip_v2.DataObjects;

namespace kata_payslip_v2.Interfaces
{
    public interface IPayslipInformationCalculator
    {
        PayslipInformation CreatePayslipInformation(UserInputInformation userInputInformation);
    }
}