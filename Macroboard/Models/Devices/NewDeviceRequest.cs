namespace Macroboard.Models.Devices
{
    public class NewDeviceRequest
    {
        /// <summary>
        /// Custom name for the device
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Serial port name to communicate with the device
        /// </summary>
        public string Port { get; set; }
    }
}