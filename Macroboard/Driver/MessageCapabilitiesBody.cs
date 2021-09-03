namespace Macroboard.Driver
{
    /// <inheritdoc />
    public class MessageCapabilitiesBody: IMessageBody
    {

        /// <summary>
        /// Sliders of the device
        /// </summary>
        public byte Sliders;
        
        /// <summary>
        /// Buttons of the device
        /// </summary>
        public byte Buttons;

        /// <inheritdoc />
        public byte Length => 2;

        /// <inheritdoc />
        public byte[] ToData()
        {
            return new[]
            {
                Sliders,
                Buttons
            };
        }

        /// <inheritdoc />
        public void FromData(byte[] data)
        {
            this.Sliders = data[0];
            this.Buttons = data[1];
        }
    }
}