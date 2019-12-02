using System;
using System.Collections.Generic;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class MonthlyPayslipCalculatorUtility : IPayslipCalculatorUtility
    {

        public List<TaxRateBracket> TaxRateBracketList;

        public MonthlyPayslipCalculatorUtility(List<TaxRateBracket> taxRateBrackets)
        {
            TaxRateBracketList = taxRateBrackets;
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