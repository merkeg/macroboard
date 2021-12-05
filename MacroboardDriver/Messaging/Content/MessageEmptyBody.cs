using System;
using MacroboardDriver.Messaging.Content;

namespace MacroboardDriver.Messaging.Content
{
    /// <inheritdoc />
    public class MessageEmptyBody: IMessageContent
    {
        

        /// <inheritdoc />
        public byte Length => 0;

        /// <inheritdoc />
        public byte[] Data()
        {
            return Array.Empty<byte>();
        }

        /// <inheritdoc />
        public void FromData(byte[] data)
        {
            
        }
    }
}