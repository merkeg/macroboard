namespace MacroboardDriver.Messaging
{
    /// <summary>
    /// An device or host action
    /// </summary>
    public enum Action: byte
    {
        /// <summary>
        /// Action to acknowledge the information sent
        /// </summary>
        OK = 0x0,
        
        /// <summary>
        /// Action to reset device
        /// </summary>
        RESET = 0x1,
        
        /// <summary>
        /// Action to start device loop
        /// </summary>
        START = 0x2,
        
        /// <summary>
        /// Action to welcome the device
        /// </summary>
        HELLO = 0x3,
        
        /// <summary>
        /// Action to send information about the device
        /// </summary>
        INFO = 0x4,
        
        /// <summary>
        /// Action to set the slider accuracy (wip)
        /// </summary>
        ACCURACY = 0x5,
        
        /// <summary>
        /// Action to get the value from an entity
        /// </summary>
        GET = 0x10,
        
        /// <summary>
        /// Action to notify the driver about the change of a device
        /// </summary>
        CHANGE = 0x11,
        
        /// <summary>
        /// Action to send if something is not ok
        /// </summary>
        NOK = 0xFF,
    }
}