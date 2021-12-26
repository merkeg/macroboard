using System;
using MacroboardDriver.Messaging.Content;

namespace MacroboardDriver.Messaging.Content
{
    /// <inheritdoc />
    public class MessageInfoBody : IMessageContent
    {
        /// <summary>
        /// Uid of the device
        /// </summary>
        public uint Uid;

        /// <summary>
        /// Sliders of the device
        /// </summary>
        public byte Sliders;
        
        /// <summary>
        /// Buttons of the device
        /// </summary>
        public byte Buttons;

        /// <inheritdoc />
        public byte Length => 6;


        /// <inheritdoc />
        public byte[] Data()
        {
            byte[] data = BitConverter.GetBytes(Uid);
            Array.Resize(ref data, Length);
            
            data[4] = this.Sliders;
            data[5] = this.Buttons;
            
            return data;

        }

        /// <inheritdoc />
        public void FromData(byte[] data)
        {
            this.Uid = BitConverter.ToUInt32(data);
            this.Sliders = data[4];
            this.Buttons = data[5];
        }
    }
}