using System;
using System.Collections.Generic;

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

            //GeneratePayslipFromCsvFileInput("../../../SampleCsvFiles/sample_input.csv", "../../../SampleCsvFiles/output.csv");
            GeneratePayslipFromConsoleInput();
        }

        public static void GeneratePayslipFromConsoleInput()
        {
            var userInterface = new ConsoleUserInterface(MonthIntegerDictionary);
            var consoleInputHandler = new ConsoleInputHandler(userInterface, maxInputStringLength);
            
            var monthlyPayslipCalculatorUtility = new MonthlyPayslipCalculatorUtility(TaxRateBracketList);
            var monthlyPayslipInformationFactory = new MonthlyPayslipInformationFactory(monthlyPayslipCalculatorUtility);
            
            var consolePayslipGenerator = new ConsolePayslipGenerator(userInterface, consoleInputHandler, monthlyPayslipInformationFactory);
            
            consolePayslipGenerator.GeneratePayslip();
        }

        public static void GeneratePayslipFromCsvFileInput(string inputFilePath, string outputFilePath)
        {
            var inputHandler = new CsvFileInputHandler(MonthIntegerDictionary, inputFilePath);
            var outputHandler = new CsvFileOutputHandler(MonthIntegerDictionary, outputFilePath);
            
            var monthlyPayslipCalculatorUtility = new MonthlyPayslipCalculatorUtility(TaxRateBracketList);
            var monthlyPayslipInformationFactory = new MonthlyPayslipInformationFactory(monthlyPayslipCalculatorUtility);
            
            var csvFilePayslipGenerator = new CsvFilePayslipGenerator(inputHandler, outputHandler, monthlyPayslipInformationFactory);
            
            csvFilePayslipGenerator.GeneratePayslip();
        }
    }
}