using System;
using kata_payslip_v2.DataObjects;

namespace kata_payslip_v2
{
    public static class ArgumentParser
    {
        public static Arguments ParseArguments(string[] argumentStrings)
        {

            if (argumentStrings.Length == 0)
            {
                return new Arguments(PayslipInputType.ConsoleInput, null, null);
            }

            if (argumentStrings.Length != 2)
            {
                throw new ArgumentException();
            }
            
            return new Arguments(PayslipInputType.CsvFileInput, argumentStrings[0], argumentStrings[1]);
            
        }
    }
}