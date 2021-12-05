using System;

namespace MacroboardDriver.Model
{
    public class InvalidDeviceResponseException : Exception
    {
        public InvalidDeviceResponseException()
        {
            
        }

        public InvalidDeviceResponseException(string message) : base(message)
        {
            
        }
    }
}