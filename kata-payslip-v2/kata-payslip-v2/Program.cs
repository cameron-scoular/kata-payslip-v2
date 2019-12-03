using System;
using System.Collections.Generic;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    class Program
    {
        
        public static Dictionary<string, int> MonthIntegerDictionary = new Dictionary<string, int>()
        {
            { "january", 1},
            {"february", 2},
            {"march", 3},
            {"april", 4},
            {"may", 5},
            {"june", 6},
            {"july", 7},
            {"august", 8},
            {"september", 9},
            {"october", 10},
            {"november", 11},
            {"december", 12}
        };
        
        public static List<TaxRateBracket> TaxRateBracketList = new List<TaxRateBracket>()
        {
            new TaxRateBracket(0, 18200, 0, 0),
            new TaxRateBracket(18201, 37000, 0.19, 0),
            new TaxRateBracket(37001, 87000, 0.325, 3572),
            new TaxRateBracket(87001, 180000, 0.37, 19822),
            new TaxRateBracket(180001, int.MaxValue, 0.45, 54232)
        };
        
        public static string outputHeaderString = "name, pay period, gross income, income tax, net income, super\r\n";
        
        public static int maxInputStringLength = 50;
        
        static void Main(string[] args)
        {
            var arguments = ArgumentParser.ParseArguments(args);
            
            var payslipGeneratorFactory = new PayslipGeneratorFactory(MonthIntegerDictionary, maxInputStringLength, TaxRateBracketList, outputHeaderString);

            IPayslipGenerator payslipGenerator = payslipGeneratorFactory.CreatePayslipGenerator(arguments);

            payslipGenerator.GeneratePayslip();
        }

        
    }
}