using kata_payslip_v2.DataObjects;

namespace kata_payslip_v2.Interfaces
{
    public interface IPayslipInformationFactory
    {
        PayslipInformation CreatePayslipInformationObject(UserInputInformation userInputInformation);
    }
}