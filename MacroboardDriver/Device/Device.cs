
using System;
using System.IO.Ports;
using MacroboardDriver.Device.Modules;
using MacroboardDriver.Messaging;
using MacroboardDriver.Messaging.Content;
using MacroboardDriver.Model;
using Action = MacroboardDriver.Messaging.Action;

namespace MacroboardDriver.Device
{
    public class Device : Capabilities<Capability>
    {
        /// <summary>
        /// The unique device identifier set by the developer
        /// </summary>
        public int Uid { get; set; }
        
        /// <summary>
        /// Custom set name for the device
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The sliders the device has
        /// </summary>
        public Slider[] Sliders { get; set; }

        /// <summary>
        /// The buttons the device has
        /// </summary>
        public Button[] Buttons { get; set; }

        /// <summary>
        /// The serial port
        /// </summary>
        public SerialPort SerialPort { get; set; }

        public void WriteMessage<T>(Message<T> message) where T : IMessageContent
        {
            Message<T>.WriteMessage(this.SerialPort, message);
        }
        
        public Message<V> WriteMessageWithResponse<T, V>(Message<T> message) where T : IMessageContent where V : IMessageContent, new()
        {
            WriteMessage(message);
            return ReadMessage<V>();
        }

        public Message<T> ReadMessage<T>() where T : IMessageContent, new()
        {
            return Message<T>.ReadMessage<T>(this.SerialPort);
        }
        
        
        public void InitHandshake()
        {
            Message<MessageEmptyBody> dummyMessage = new Message<MessageEmptyBody>();
            
            // Reset device
            dummyMessage.Action = Action.RESET;
            Message<MessageEmptyBody> response = WriteMessageWithResponse<MessageEmptyBody, MessageEmptyBody>(dummyMessage);

            if (response.Action != Action.OK)
            {
                throw new InvalidDeviceResponseException("Response after device reset should be NOK");
            }
            
            // Welcome the device
            dummyMessage.Action = Action.HELLO;
            Message<MessageInfoBody> infoResponse = WriteMessageWithResponse<MessageEmptyBody, MessageInfoBody>(dummyMessage);

            if (infoResponse.Action != Action.INFO)
            {
                throw new InvalidDeviceResponseException("Response after HELLO should be INFO, not: " + infoResponse.Action);
            }
            
            this.SetCapability(Capability.Sliders, infoResponse.Body.Sliders);
            this.SetCapability(Capability.Buttons, infoResponse.Body.Buttons);
            this.Uid = infoResponse.Body.Uid;


        }
        
        public static Device InstantiateDeviceFromPort(string port, int baudRate)
        {

            Device device = new Device();
            device.SetCapability(Capability.PortString, port);
            device.SetCapability(Capability.BaudRate, baudRate);

            SerialPort serialPort = new SerialPort(port, baudRate);
            try
            {
                
                serialPort.Open();
            }
            catch (UnauthorizedAccessException e)
            {
                serialPort.Close();
                return null;
            }

            device.SerialPort = serialPort;
            
            device.InitHandshake();
            
            return device;
        }
    }
}