using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using kata_payslip_v2.DataObjects;
using kata_payslip_v2.Interfaces;

namespace kata_payslip_v2
{
    public class MonthlyConsoleInputHandler : IInputHandler
    {
        private readonly IUserInterface UserInterface;
        private readonly int MaxInputStringLength;
        private bool UserHasAlreadyProvidedInput;
        
        public MonthlyConsoleInputHandler(IUserInterface userInterface, int maxStringLength)
        {
            UserInterface = userInterface;
            MaxInputStringLength = maxStringLength;
            UserHasAlreadyProvidedInput = false;

        }

        public UserInputInformation GetNextUserInputInformation()
        {

            if (UserHasAlreadyProvidedInput)
            {
                return null;
            }
            
            var userInputInformation = new UserInputInformation();

            userInputInformation.Name = PromptEmployeeName();
            userInputInformation.Surname = PromptEmployeeSurname();
            userInputInformation.Salary = PromptEmployeeSalary();
            userInputInformation.SuperRate = PromptEmployeeSuperRate();

            var paymentPeriod = PromptPaymentPeriod();
            
            userInputInformation.PaymentStartDate = paymentPeriod.PaymentStartDate;
            userInputInformation.PaymentEndDate = paymentPeriod.PaymentEndDate;

            UserHasAlreadyProvidedInput = true;

            return userInputInformation;
        }

        public string PromptEmployeeName()
        {
            return UserInterface.PromptString("Please input your name: ", MaxInputStringLength);
        }

        public string PromptEmployeeSurname()
        {
            return UserInterface.PromptString("Please input your surname: ", MaxInputStringLength);
        }

        public uint PromptEmployeeSalary()
        {
            return UserInterface.PromptUnsignedInteger("Please enter your annual salary: ");
        }

        public uint PromptEmployeeSuperRate()
        {
            return UserInterface.PromptUnsignedInteger("Please enter your super rate: ");
        }

        public PaymentPeriod PromptPaymentPeriod()
        {
            var paymentPeriod = new PaymentPeriod();

            var paymentPeriodIsValid = false;
            while (!paymentPeriodIsValid)
            {
                paymentPeriod.PaymentStartDate =
                    UserInterface.PromptDateTime("Please enter your payment start date (e.g. '17 March'):");

                paymentPeriod.PaymentEndDate = UserInterface.PromptDateTime("Please enter your payment end date:");

                var paymentPeriodSpan = paymentPeriod.PaymentEndDate.Subtract(paymentPeriod.PaymentStartDate);

                if (TimeSpanIsMonthly(paymentPeriodSpan.Days)) 
                {
                    paymentPeriodIsValid = true;
                }
                else
                {
                    UserInterface.WriteMessage("The given payment period is not monthly, please enter a monthly payment period");
                }
            }

            return paymentPeriod;

        }

        private bool TimeSpanIsMonthly(int numberOfDays)
        {
            return numberOfDays >= 26 && numberOfDays <= 31;
        }
    }
}