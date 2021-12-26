
using System;
using System.IO.Ports;
using System.Threading;
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
        public uint Uid { get; set; }
        
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

        public delegate void EntityChange(ModuleType type, byte entityId, byte value);

        /// <summary>
        /// Entity change event
        /// </summary>
        public event EntityChange EntityChangeEvent;

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

        public void ResetDevice()
        {
            Message<MessageEmptyBody> resetMessage = new Message<MessageEmptyBody>();
            resetMessage.Action = Action.RESET;
            
            Message<MessageEmptyBody> resetResponse = WriteMessageWithResponse<MessageEmptyBody, MessageEmptyBody>(resetMessage);
            if (resetResponse.Action != Action.OK)
            {
                throw new InvalidDeviceResponseException("Response after device reset should be OK");
            }
        }

        public Message<MessageInfoBody> HelloDevice()
        {
            Message<MessageEmptyBody> helloMessage = new Message<MessageEmptyBody>();
            helloMessage.Action = Action.HELLO;
            
            Message<MessageInfoBody> helloResponse = WriteMessageWithResponse<MessageEmptyBody, MessageInfoBody>(helloMessage);
            
            if (helloResponse.Action != Action.INFO)
            {
                throw new InvalidDeviceResponseException("Response after HELLO should be INFO, not: " + helloResponse.Action);
            }

            return helloResponse;
        }

        public void StartMessageLoop()
        {
            Message<MessageEmptyBody> startMessage = new Message<MessageEmptyBody>();
            startMessage.Action = Action.START;
            Message<MessageEmptyBody> startResponse = WriteMessageWithResponse<MessageEmptyBody, MessageEmptyBody>(startMessage);
            if (startResponse.Action != Action.OK)
            {
                throw new InvalidDeviceResponseException("Response after device start should be OK");
            }

            Thread t = new Thread(MessageLoop);
            t.Name = "Message loop thread for device " + Uid;
            
            t.Start();

        }

        private void MessageLoop()
        {
            Console.WriteLine("Message loop thread started for device " + Uid);
            while (true)
            {
                Message<MessageChangeBody> message = ReadMessage<MessageChangeBody>();
                EntityChangeEvent?.Invoke((ModuleType) message.Body.entityType, message.Body.entityId, message.Body.value);
            }
        }
        
        public void InitHandshake()
        {
            
            ResetDevice();
            Message<MessageInfoBody> infoMessage = HelloDevice();
            
            this.SetCapability(Capability.Sliders, infoMessage.Body.Sliders);
            this.SetCapability(Capability.Buttons, infoMessage.Body.Buttons);
            this.Uid = infoMessage.Body.Uid;
            
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