namespace kata_payslip_v2
{
    public class TaxRateBracket
    {
        public readonly uint LowerBound; 
        public readonly uint UpperBound;
        public readonly double Rate;
        public readonly uint FixedAmount;

        public TaxRateBracket(uint lowerBound, uint upperBound, double rate, uint fixedAmount)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
            Rate = rate;
            FixedAmount = fixedAmount;
        }

        public bool IsInBracket(uint amount)
        {
            if (amount >= LowerBound && amount <= UpperBound)
            {
                return true;
            }

            return false;
        }
    }
}