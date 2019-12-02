using kata_payslip_v2.DataObjects;

namespace kata_payslip_v2.Interfaces
{
    public interface IOutputHandler
    {
        void WritePayslipInformation(PayslipInformation payslipInformation);

    }
}