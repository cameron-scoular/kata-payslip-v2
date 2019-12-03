using System.Collections.Generic;
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class PayslipGeneratorFactory
    {
        private Dictionary<string, int> MonthIntegerDictionary;
        private int MaxInputStringLength;
        private List<TaxRateBracket> TaxRateBracketList;
        private string OutputHeaderString;
        
        public PayslipGeneratorFactory(Dictionary<string, int> monthIntegerDictionary, int maxInputStringLength, List<TaxRateBracket> taxRateBrackets, string outputHeaderString)
        {
            MonthIntegerDictionary = monthIntegerDictionary;
            MaxInputStringLength = maxInputStringLength;
            TaxRateBracketList = taxRateBrackets;
            OutputHeaderString = outputHeaderString;
        }


        public IPayslipGenerator CreatePayslipGenerator(Arguments arguments)
        {
            IInputHandler inputHandler = null;
            IOutputHandler outputHandler = null;
            
            var userInterface = new ConsoleUserInterface(MonthIntegerDictionary);
            var payslipInformationCalculator = new MonthlyPayslipInformationCalculator(TaxRateBracketList);

            switch (arguments.PayslipInputType)
            {
                case PayslipInputType.ConsoleInput:
                    inputHandler = new MonthlyConsoleInputHandler(userInterface, MaxInputStringLength);
                    break;
                case PayslipInputType.CsvFileInput:
                    inputHandler = new CsvFileInputHandler(MonthIntegerDictionary, arguments.InputFilePath);
                    break;
            }

            switch (arguments.PayslipOutputType)
            {
                case PayslipOutputType.ConsoleOutput:
                    outputHandler = userInterface;
                    break;
                case PayslipOutputType.CsvFileOutput:
                    outputHandler = new CsvFileOutputHandler(MonthIntegerDictionary, arguments.OutputFilePath, OutputHeaderString);
                    break;
            }

            return new PayslipGenerator(inputHandler, outputHandler, payslipInformationCalculator);
        }
        
        
    }
}