using System;
using System.IO.Ports;
using System.Linq;
using MacroboardDriver.Messaging.Content;

namespace MacroboardDriver.Messaging
{
    public class Message<T> where T : IMessageContent
    {
        public static readonly ushort Preamble = 47806;
        public static readonly byte[] PreambleBytes = BitConverter.GetBytes(Preamble);
        
        /// <summary>
        /// Intended Action of the message.
        /// </summary>
        public Action Action;
        
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
                    (byte) this.Action,
                    this.Body?.Length ?? 0
                };
            }
        }

        /// <summary>
        /// Data of the message
        /// </summary>
        public T Body;
        
        public static Message<T> ReadMessage<T>(SerialPort port) where T : IMessageContent, new()
        {
            while (true)
            {
              Message<T> message = new Message<T>();
                          
              // Preamble check
              
              byte[] preambleCheck = new byte[2];
              port.Read(preambleCheck, 0, 2);
              int check = BitConverter.ToUInt16(preambleCheck);
              if (check != Preamble)
              {
                  continue;
              }
              
              
              message.Action = (Action) port.ReadByte();
              message.Length = (byte) port.ReadByte();
              byte[] data = new byte[message.Length];
              port.Read(data, 0, message.Length);
              message.Body = new T();
              message.Body.FromData(data);
              Console.WriteLine("Action: {0} - Length: {1} - Data: {2}", message.Action, message.Length, BitConverter.ToString(data).Replace("-", " "));
              
              return message;  
            }
            
        }
        
        public static void WriteMessage<T>(SerialPort port, Message<T> message) where T : IMessageContent
        {
            byte[] data = message.Header;
            if (message.Body != null)
            {
                if (message.Body.Length != 0)
                {
                    data = data.Concat(message.Body.Data()).ToArray();
                }
                
            }
            port.Write(PreambleBytes, 0, PreambleBytes.Length);
            port.Write(data, 0, data.Length);
        }
    }
}