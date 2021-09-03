namespace Macroboard.Driver
{
    /// <summary>
    /// An device or host action
    /// </summary>
    public enum MessageAction: byte
    {
        /// <summary>
        /// Action to acknowledge the information sent
        /// </summary>
        Ok = 0x0,
        
        /// <summary>
        /// Action to gather the capabilities about the device
        /// </summary>
        Capabilities = 0x1,
        
        /// <summary>
        /// Action to get the uid of the device
        /// </summary>
        Uid = 0x2,
        
        /// <summary>
        /// Action to inform the host about the change of data
        /// </summary>
        Change = 0x3,
        
        /// <summary>
        /// Action to request the value of data
        /// Syntax is as follows:
        /// {hardware type}{hardware id}
        /// </summary>
        Get = 0x6,
    }
}