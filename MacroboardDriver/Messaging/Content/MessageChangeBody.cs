using System;
using MacroboardDriver.Messaging.Content;

namespace MacroboardDriver.Messaging.Content
{
    /// <inheritdoc />
    public class MessageChangeBody : IMessageContent
    {
        /// <summary>
        /// entity type
        /// </summary>
        public byte entityType;

        /// <summary>
        /// entity id
        /// </summary>
        public byte entityId;
        
        /// <summary>
        /// value
        /// </summary>
        public byte value;

        /// <inheritdoc />
        public byte Length => 3;


        /// <inheritdoc />
        public byte[] Data()
        {
            byte[] data = new byte[Length];

            data[0] = entityType;
            data[1] = entityId;
            data[2] = value;
            return data;
        }

        /// <inheritdoc />
        public void FromData(byte[] data)
        {
            this.entityType = data[0];
            this.entityId = data[1];
            this.value = data[2];
        }
    }
}