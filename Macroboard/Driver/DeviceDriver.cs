using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Macroboard.Models.Devices;
using Newtonsoft.Json;

namespace Macroboard.Driver
{
    public class DeviceDriver: IDeviceDriver
    {

        private Dictionary<int, FullDeviceInfo> _devices = new();
        private int _ids = 0;

        /// <inheritdoc />
        public async Task<FullDeviceInfo> ConnectToDevice(string port)
        {
            SerialPort serialPort = new SerialPort(port, Hardware.BaudRate);

            try
            {
                serialPort.Open();
            }
            catch (UnauthorizedAccessException e)
            {
                return null;
            }

            Message<MessageUidBody> uidRequest = new Message<MessageUidBody>();
            uidRequest.MessageAction = MessageAction.Uid;
            this.WriteMessage(serialPort, uidRequest);
            
            Message<MessageUidBody> uidResponse = this.ReadMessage<MessageUidBody>(serialPort);
            Console.WriteLine(uidResponse.Body.Uid.ToString("X"));

            Message<MessageCapabilitiesBody> capsRequest = new Message<MessageCapabilitiesBody>();
            capsRequest.MessageAction = MessageAction.Capabilities;
            this.WriteMessage(serialPort, capsRequest);
            
            Message<MessageCapabilitiesBody> capsResponse = this.ReadMessage<MessageCapabilitiesBody>(serialPort);
            Console.WriteLine("Sliders: {0} - Buttons: {1}", capsResponse.Body.Sliders, capsResponse.Body.Buttons);

            FullDeviceInfo deviceInfo = new FullDeviceInfo()
            {
                Id = _ids++,
                Uid = uidResponse.Body.Uid,
                Port = port,
                SerialPort = serialPort,
                Capabilities = new DeviceCapabilities()
                {
                    Sliders = capsResponse.Body.Sliders,
                    Buttons = capsResponse.Body.Buttons
                }
            };

            List<DeviceSliderItem> sliderItems = new List<DeviceSliderItem>();
            for (int i = 0; i < deviceInfo.Capabilities.Sliders; i++)
            {
                sliderItems.Add(new DeviceSliderItem()
                {
                    Id = i,
                    Value = 0
                });
            }
            deviceInfo.Sliders = sliderItems.ToArray();
            
            List<DeviceButtonItem> buttonItems = new List<DeviceButtonItem>();
            for (int i = 0; i < deviceInfo.Capabilities.Buttons; i++)
            {
                buttonItems.Add(new DeviceButtonItem()
                {
                    Id = i,
                    Value = false
                });
            }
            deviceInfo.Buttons = buttonItems.ToArray();
            
            
            this._devices.Add(deviceInfo.Id, deviceInfo);
            return deviceInfo;
        }

        /// <inheritdoc />
        public FullDeviceInfo[] ListDevices()
        {
            return this._devices.Values.ToArray();
        }

        /// <inheritdoc />
        public string[] GetComPorts(bool hideUsed = true)
        {
            return SerialPort.GetPortNames();
        }

        /// <inheritdoc />
        public Message<T> ReadMessage<T>(SerialPort port) where T : IMessageBody, new()
        {
            Message<T> message = new Message<T>();
            message.MessageAction = (MessageAction) port.ReadByte();
            message.Length = (byte) port.ReadByte();
            byte[] data = new byte[message.Length];
            port.Read(data, 0, message.Length);
            message.Body = new T();
            message.Body.FromData(data);
            Console.WriteLine("Action: {0} - Length: {1} - Data: {2}", message.MessageAction, message.Length, BitConverter.ToString(data).Replace("-", " "));
            
            return message;
        }

        /// <inheritdoc />
        public void WriteMessage<T>(SerialPort port, Message<T> message) where T : IMessageBody
        {
            byte[] data = message.Header;
            if (message.Body != null)
            {
                data = data.Concat(message.Body.Data()).ToArray();
            }
            port.Write(message.Header, 0, data.Length);
        }
    }
}