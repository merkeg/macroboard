namespace Macroboard.Models.Devices
{
    public class DeviceInfo
    {

        /// <summary>
        /// The unique device identifier set by the developer
        /// </summary>
        public int Uid { get; set; }

        /// <summary>
        /// The identifier of the device, determined by the order of operation the devices got added
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The custom set name for the device
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Port the Device is connected to
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// The sliders of the device
        /// </summary>
        public DeviceSliderItem[] Sliders { get; set; }
        
        /// <summary>
        /// The buttons of the device
        /// </summary>
        public DeviceButtonItem[] Buttons { get; set; }
        
        
        /// <summary>
        /// Capabilities of the device
        /// </summary>
        public DeviceCapabilities Capabilities { get; set; }
    }

    public class DeviceCapabilities
    {
        /// <summary>
        /// The amount of sliders the device has
        /// </summary>
        public byte Sliders { get; set; }

        /// <summary>
        /// The amount of buttons the device has
        /// </summary>
        public byte Buttons { get; set; }
    }

    public class DeviceSliderItem
    {
        /// <summary>
        /// The identifier of the slider
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The current value of the slider.
        /// </summary>
        public int Value { get; set; }
    }
    
    public class DeviceButtonItem
    {
        /// <summary>
        /// The identifier of the button
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The current value of the button.
        /// </summary>
        public bool Value { get; set; }
    }
}