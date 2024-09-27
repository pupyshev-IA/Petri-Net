using LabWork.Abstractions;
using LabWork.Domain;

namespace LabWork
{
    public partial class GraphViewer : Form
    {
        private const string InputSeparator = ";  ";
        private const string WhiteSpace = "   ";

        private string _currentStageText = "Этап: {0}";
        private int _currentStage = 1;

        private IGraphBuilder _graphBuilder;
        private ICollection<int>? _currentTokenSequence;
        private GraphInfo? _graphInfo;

        public GraphViewer(IGraphBuilder graphBuilder)
        {
            InitializeComponent();

            _graphBuilder = graphBuilder;

            panelView.Visible = false; // Prevents the panel from redrawing when the form is first launched
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

        private void panelView_Paint(object sender, PaintEventArgs e)
        {
            _graphInfo = _graphBuilder.BuildPetriGraph(panelView, _currentTokenSequence.ToList());

            Graphics graphics = e.Graphics;
            _graphBuilder.VisualizePetriGraph(_graphInfo, panelView, graphics);
        }

        private void btnCreateGraph_Click(object sender, EventArgs e)
        {
            if (!ValidateInputData())
            {
                CreateErrorMessage(title: "Ошибка ввода", message: "Введены некорректные данные");
                return;
            }

            _currentTokenSequence = InitializeTokenSequence();
            _currentStage = 1;
            SetValueForStageLabel();
            UpdateButtonsStatus();

            panelView.Visible = true;
            panelView.Invalidate();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _currentStage--;
            SetValueForStageLabel();
            UpdateButtonsStatus();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
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
            lblTokensMaxCount.Text = lblTokensMaxCount.Text + WhiteSpace + AppConstants.TokensMaxCountPerPlace;
        }

        private void UpdateButtonsStatus()
        {
            btnBack.Enabled = _currentStage > 1;
            btnForward.Enabled = _currentStage < AppConstants.NumberOfFirings;
        }

        private bool ValidateInputData()
        {
            var tokenCountSequence = mTextBoxInputData.Text.Split(InputSeparator);
            foreach (var tokenCount in tokenCountSequence)
            {
                if (string.IsNullOrWhiteSpace(tokenCount) || int.Parse(tokenCount) > AppConstants.TokensMaxCountPerPlace)
                    return false;
            }

            return true;
        }

        private ICollection<int> InitializeTokenSequence() =>
            mTextBoxInputData.Text.Split(InputSeparator)
            .Select(int.Parse)
            .ToList();

        private void CreateErrorMessage(string title, string message) =>
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
