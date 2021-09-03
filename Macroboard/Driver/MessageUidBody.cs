using System;

namespace Macroboard.Driver
{
    /// <inheritdoc />
    public class MessageUidBody : IMessageBody
    {
        /// <summary>
        /// Uid of the device
        /// </summary>
        public int Uid;


        /// <inheritdoc />
        public byte Length => 4;


        /// <inheritdoc />
        public byte[] ToData()
        {
            return BitConverter.GetBytes(Uid);
        }

        /// <inheritdoc />
        public void FromData(byte[] data)
        {
            this.Uid = BitConverter.ToInt32(data);
        }
    }
}