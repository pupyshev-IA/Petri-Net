namespace LabWork.Forms
{
    public partial class DetailedInfo : Form
    {
        private const int MaxHeight = 800;

        public DetailedInfo(string text)
        {
            InitializeComponent();

            label.Text = text;

            int formHeight = Math.Min(label.Height, MaxHeight);

            panel.Height = label.Height;
            panel.Width = label.Width;

            this.Height = formHeight;
            this.Width = panel.Width;
        }
    }
}
