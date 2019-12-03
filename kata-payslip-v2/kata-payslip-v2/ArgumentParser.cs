using System;
using System.Linq;
using kata_payslip_v2.DataObjects;

namespace kata_payslip_v2
{
    public static class ArgumentParser
    {
        public static Arguments ParseArguments(string[] argumentStrings)
        {

            PayslipInputType inputType;
            PayslipOutputType outputType;
            string outputFilePath = null;
            string inputFilePath = null;

            if (argumentStrings.Length == 0)
            {
                return new Arguments(PayslipInputType.ConsoleInput, PayslipOutputType.ConsoleOutput, null, null);
            }

            inputType = argumentStrings.Contains("csvInput") ? PayslipInputType.CsvFileInput : PayslipInputType.ConsoleInput;

            outputType = argumentStrings.Contains("csvOutput") ? PayslipOutputType.CsvFileOutput : PayslipOutputType.ConsoleOutput;

            if (outputType == PayslipOutputType.CsvFileOutput)
            {
                outputFilePath = argumentStrings[^1];
                if (inputType == PayslipInputType.CsvFileInput)
                {
                    inputFilePath = argumentStrings[^2];
                }
            }
            else
            {
                if (inputType == PayslipInputType.CsvFileInput)
                {
                    inputFilePath = argumentStrings[^1];
                }
            }
            
            return new Arguments(inputType, outputType, inputFilePath, outputFilePath);
            
        }
    }
}