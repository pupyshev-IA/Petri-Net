using LabWork.Domain;
using LabWork.Service;

namespace LabWork
{
    public partial class GraphViewer : Form
    {
        private const string SplitSeparator = ";  ";
        private const string WhiteSpace = "   ";
        private string _currentStageText = "Этап: {0}";
        private int _currentStage = 1;

        public GraphViewer()
        {
            InitializeComponent();

            SetValuesForInfoLabels();
            SetValueForStageLabel();
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

        private void btnCreateGraph_Click(object sender, EventArgs e)
        {
            if (!ValidateInputData())
            {
                CreateErrorMessage(title: "Ошибка ввода", message: "Введены некорректные данные");
                return;
            }

            GraphBuilder.BuildGraph();
            UpdateButtonsStatus();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (_currentStage <= 1)
                return;

            _currentStage--;
            SetValueForStageLabel();
            UpdateButtonsStatus();

            tlpOutput.Controls.Add(new Button());
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (_currentStage > AppConstants.NumberOfFirings)
                return;

            _currentStage++;
            SetValueForStageLabel();
            UpdateButtonsStatus();
        }

        private void btnClose_Click(object sender, EventArgs e) =>
            Application.Exit();

        private void SetValueForStageLabel() =>
            lblStage.Text = string.Format(_currentStageText, _currentStage);

        private void SetValuesForInfoLabels()
        {
            lblNumberOfFirings.Text = lblNumberOfFirings.Text + WhiteSpace + AppConstants.NumberOfFirings;
            lblPlacesMaxCount.Text = lblPlacesMaxCount.Text + WhiteSpace + AppConstants.PlacesMaxCount;
            lblTransitionsMaxCount.Text = lblTransitionsMaxCount.Text + WhiteSpace + AppConstants.TransitionsMaxCount;
            lblTokensMaxCount.Text = lblTokensMaxCount.Text + WhiteSpace + AppConstants.TokensMaxCount;
        }

        private void UpdateButtonsStatus()
        {
            btnBack.Enabled = _currentStage > 1;
            btnForward.Enabled = _currentStage < AppConstants.NumberOfFirings;
        }

        private bool ValidateInputData()
        {
            var tokenCountSequence = mTextBoxInputData.Text.Split(SplitSeparator);
            foreach (var tokenCount in tokenCountSequence)
            {
                if (string.IsNullOrWhiteSpace(tokenCount) || int.Parse(tokenCount) > AppConstants.TokensMaxCount)
                    return false;
            }

            return true;
        }

        private void CreateErrorMessage(string title, string message) =>
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
