using System;

namespace kata_payslip_v2.DataObjects
{
    public class PayslipInformation
    {
        public string Fullname { get; set; }
        
        public DateTime PaymentStartDate { get; set; }

        public DateTime PaymentEndDate { get; set; }

        public uint GrossIncome { get; set; }

        public uint IncomeTax { get; set; }

        public uint NetIncome { get; set; }
        
        public uint Super { get; set; }
    }
}