namespace kata_payslip_v2.Interfaces
{
    public interface IPayslipCalculatorUtility
    {
        uint CalculateIncomeTax(uint salary);
        uint CalculateGrossIncome(uint salary);
        uint CalculateSuperAmount(uint grossIncome, uint superRate);
        uint CalculateNetIncome(uint salary, uint incomeTax);
    }
}