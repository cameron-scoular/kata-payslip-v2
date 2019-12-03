
using System;
using System.Collections.Generic;
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class MonthlyPayslipInformationCalculator : IPayslipInformationCalculator
    {
        
        public List<TaxRateBracket> TaxRateBracketList;


        public MonthlyPayslipInformationCalculator(List<TaxRateBracket> taxRateBracketList)
        {
            TaxRateBracketList = taxRateBracketList;
        }

        public PayslipInformation CreatePayslipInformation(UserInputInformation userInputInformation)
        {
            
            PayslipInformation payslipInformation = new PayslipInformation();

            payslipInformation.Fullname = $"{userInputInformation.Name} {userInputInformation.Surname}";
            
            payslipInformation.GrossIncome = CalculateGrossIncome(userInputInformation.Salary); 

            payslipInformation.IncomeTax = CalculateIncomeTax(userInputInformation.Salary); 

            payslipInformation.NetIncome = CalculateNetIncome(payslipInformation.GrossIncome, payslipInformation.IncomeTax);

            payslipInformation.Super = CalculateSuperAmount(userInputInformation.SuperRate, payslipInformation.GrossIncome);

            payslipInformation.PaymentStartDate = userInputInformation.PaymentStartDate;

            payslipInformation.PaymentEndDate = userInputInformation.PaymentEndDate;

            return payslipInformation;

        }
        
        public uint CalculateSuperAmount(uint superRate, uint grossIncome)
        {
            return checked((uint)Math.Round(grossIncome * (superRate / 100.0)));
        }

        public uint CalculateNetIncome(uint grossIncome, uint incomeTax)
        {
            return grossIncome - incomeTax;
        }

        public uint CalculateGrossIncome(uint salary)
        {
            return Convert.ToUInt32(Math.Round(salary / 12.0));
        }

        public uint CalculateIncomeTax(uint salary)
        {
            foreach (var bracket in TaxRateBracketList)
            {
                if(bracket.IsInBracket(salary))
                {
                    var residual = checked((uint) Math.Round((salary - bracket.LowerBound) * bracket.Rate));
                    return checked((uint) Math.Round((bracket.FixedAmount + residual) / 12.0));
                }
            }

            throw new Exception();
        }

        
        
    }
}