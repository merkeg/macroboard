namespace MacroboardDriver.Device.Modules
{
    public class Button
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