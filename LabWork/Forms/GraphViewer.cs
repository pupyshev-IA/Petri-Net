namespace LabWork
{
    public partial class GraphViewer : Form
    {
        private string _currentStageText = "Этап: {0}";
        private int _currentStage = 0;

        public GraphViewer()
        {
            InitializeComponent();

            SetLabelStageText();
        }

        /*_________________________________________________________________________________________*/
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [System.Runtime.InteropServices.DllImport("user32.dll")]

        public static extern bool ReleaseCapture();

        private void panelMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        /*_________________________________________________________________________________________*/

        private void btnClose_Click(object sender, EventArgs e) => 
            Application.Exit();

        private void btnBack_Click(object sender, EventArgs e)
        {

        }

        private void btnForward_Click(object sender, EventArgs e)
        {

        }

        private void SetLabelStageText() => 
            labelStage.Text = string.Format(_currentStageText, _currentStage);
    }
}
