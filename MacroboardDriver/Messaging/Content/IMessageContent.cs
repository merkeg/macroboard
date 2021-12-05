namespace MacroboardDriver.Messaging.Content
{
    public interface IMessageContent
    {
        /// <summary>
        /// Get the lenght of the body
        /// </summary>
        public byte Length { get; }

        /// <summary>
        /// Convert the body to an byte array
        /// </summary>
        /// <returns></returns>
        public byte[] Data();

        /// <summary>
        /// Parse the data and put it in the current object
        /// </summary>
        /// <param name="data"></param>
        public void FromData(byte[] data);
    }
}