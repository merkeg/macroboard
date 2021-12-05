using MacroboardDriver.Device;
using MacroboardDriver.Device.Modules;

namespace MacroboardDriver.Model
{
    public class DeviceInfo
    {
        /// <summary>
        /// The unique device identifier set by the developer
        /// </summary>
        public int Uid { get; set; }
        
        /// <summary>
        /// The custom set name for the device
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Port the Device is connected to
        /// </summary>
        public string Port { get; set; }
        
        /// <summary>
        /// Device capabilities
        /// </summary>
        public Capabilities<Capability> Capabilities { get; set; }
        
        /// <summary>
        /// Device Buttons
        /// </summary>
        public Button[] Buttons { get; set; }
        
        /// <summary>
        /// Device Sliders
        /// </summary>
        public Slider[] Sliders { get; set; }
    }
}