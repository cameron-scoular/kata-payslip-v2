namespace kata_payslip_v2.DataObjects
{
    public class Arguments
    {

        public string InputFilePath;
        public string OutputFilePath;
        public PayslipInputType PayslipInputType;
        public PayslipOutputType PayslipOutputType;
        
        public Arguments(PayslipInputType inputType, PayslipOutputType outputType, string inputFilePath, string outputFilePath)
        {
            PayslipInputType = inputType;
            PayslipOutputType = outputType;
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }
        
    }
}