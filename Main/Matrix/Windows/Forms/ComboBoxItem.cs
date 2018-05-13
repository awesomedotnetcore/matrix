namespace Matrix.Windows.Forms
{
    public class ComboBoxItem
    {
        public object Display { get; set; }

        public object Value { get; set; }

        public ComboBoxItem()
        {
        }

        public ComboBoxItem(object display, object value)
        {
            Display = display;
            Value = value;
        }
    }
}