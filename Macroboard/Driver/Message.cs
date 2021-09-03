namespace Macroboard.Driver
{
    public class Message<T> where T : IMessageBody
    {
        /// <summary>
        /// Intended Action of the message.
        /// </summary>
        public MessageAction MessageAction;
        
        /// <summary>
        /// Length of the body.
        /// </summary>
        public byte Length;

        /// <summary>
        /// Byte-array converted header
        /// </summary>
        public byte[] Header
        {
            get
            {
                return new[]
                {
                    (byte) this.MessageAction,
                    this.Body?.Length ?? 0
                };
            }
        }

        /// <summary>
        /// Data of the message
        /// </summary>
        public T Body;
    }

    
}