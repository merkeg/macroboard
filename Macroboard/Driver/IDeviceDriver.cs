using System.Collections.Generic;
using System.IO.Ports;
using System.Threading.Tasks;
using Macroboard.Models.Devices;

namespace Macroboard.Driver
{
    /// <summary>
    /// Device driver 
    /// </summary>
    public interface IDeviceDriver
    {

        /// <summary>
        /// Try to connect to a device
        /// </summary>
        /// <param name="port">Port to communicate with</param>
        /// <returns>The device info object, if it was successful.</returns>
        public Task<FullDeviceInfo> ConnectToDevice(string port);

        /// <summary>
        /// Lists all connected devices
        /// </summary>
        /// <returns></returns>
        public FullDeviceInfo[] ListDevices();
        
        /// <summary>
        /// Lists all available com ports
        /// </summary>
        /// <returns></returns>
        public string[] GetComPorts(bool hideUsed = true);

        /// <summary>
        /// Reads a message from the specified serial port
        /// </summary>
        /// <returns></returns>
        public Message<T> ReadMessage<T>(SerialPort port) where T : IMessageBody, new();

        /// <summary>
        /// Write a message to the specified serial port
        /// </summary>
        /// <param name="port">Serial port to send the message</param>
        /// <param name="message">The message</param>
        public void WriteMessage<T>(SerialPort port, Message<T> message) where T : IMessageBody;
        
    }
}