using System;
using kata_payslip_v2.DataObjects;

namespace kata_payslip_v2.Interfaces
{
    public interface IUserInterface : IOutputHandler
    {
        void WriteMessage(string message);
        
        uint PromptUnsignedInteger(string message);

        string PromptString(string message, int maxLength);

        DateTime PromptDateTime(string message);

    }
}