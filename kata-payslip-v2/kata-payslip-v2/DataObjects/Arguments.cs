namespace kata_payslip_v2.DataObjects
{
    public class Arguments
    {

        public string InputFilePath;
        public string OutputFilePath;
        public PayslipInputType PayslipInputType;
        
        public Arguments(PayslipInputType inputType, string inputFilePath, string outputFilePath)
        {
            PayslipInputType = inputType;
            InputFilePath = inputFilePath;
            OutputFilePath = outputFilePath;
        }
        
    }
}