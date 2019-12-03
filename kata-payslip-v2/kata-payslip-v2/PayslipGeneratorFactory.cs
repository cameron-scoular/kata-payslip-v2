using System.Collections.Generic;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class PayslipGeneratorFactory
    {
        private Dictionary<string, int> MonthIntegerDictionary;
        private int MaxInputStringLength;
        private List<TaxRateBracket> TaxRateBracketList;
        
        public PayslipGeneratorFactory(Dictionary<string, int> monthIntegerDictionary, int maxInputStringLength, List<TaxRateBracket> taxRateBrackets)
        {
            MonthIntegerDictionary = monthIntegerDictionary;
            MaxInputStringLength = maxInputStringLength;
            TaxRateBracketList = taxRateBrackets;
        }
        
        public IPayslipGenerator CreateConsolePayslipGenerator()
        {
            var userInterface = new ConsoleUserInterface(MonthIntegerDictionary);
            var consoleInputHandler = new MonthlyConsoleInputHandler(userInterface, MaxInputStringLength);
            
            var monthlyPayslipInformationCalculator = new MonthlyPayslipInformationCalculator(TaxRateBracketList);
            
            return new ConsolePayslipGenerator(userInterface, consoleInputHandler, monthlyPayslipInformationCalculator);
        }

        public IPayslipGenerator CreateCsvFilePayslipGenerator(string inputFilePath, string outputFilePath)
        {
            var inputHandler = new CsvFileInputHandler(MonthIntegerDictionary, inputFilePath);
            var outputHandler = new CsvFileOutputHandler(MonthIntegerDictionary, outputFilePath);
            
            var monthlyPayslipInformationCalculator = new MonthlyPayslipInformationCalculator(TaxRateBracketList);
            
            return new CsvFilePayslipGenerator(inputHandler, outputHandler, monthlyPayslipInformationCalculator);

        }
        
    }
}