using System;
using System.Collections.Generic;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    class Program
    {
        
        public static Dictionary<string, int> MonthIntegerDictionary = new Dictionary<string, int>()
        {
            { "January", 1},
            {"February", 2},
            {"March", 3},
            {"April", 4},
            {"May", 5},
            {"June", 6},
            {"July", 7},
            {"August", 8},
            {"September", 9},
            {"October", 10},
            {"November", 11},
            {"December", 12}
        };
        
        public static List<TaxRateBracket> TaxRateBracketList = new List<TaxRateBracket>()
        {
            new TaxRateBracket(0, 18200, 0, 0),
            new TaxRateBracket(18201, 37000, 0.19, 0),
            new TaxRateBracket(37001, 87000, 0.325, 3572),
            new TaxRateBracket(87001, 180000, 0.37, 19822),
            new TaxRateBracket(180001, int.MaxValue, 0.45, 54232)
        };
        
        public static int maxInputStringLength = 50;
        
        static void Main(string[] args)
        {
            var arguments = ArgumentParser.ParseArguments(args);
            
            var payslipGeneratorFactory = new PayslipGeneratorFactory(MonthIntegerDictionary, maxInputStringLength, TaxRateBracketList);

            IPayslipGenerator payslipGenerator;

            switch (arguments.PayslipInputType)
            {
                case PayslipInputType.CsvFileInput:
                    payslipGenerator = payslipGeneratorFactory.CreateCsvFilePayslipGenerator(arguments.InputFilePath, arguments.OutputFilePath);
                    break;
                case PayslipInputType.ConsoleInput:
                    payslipGenerator = payslipGeneratorFactory.CreateConsolePayslipGenerator();
                    break;
                default:
                    payslipGenerator = payslipGeneratorFactory.CreateConsolePayslipGenerator();
                    break;
            }

            payslipGenerator.GeneratePayslip();
        }

        
    }
}