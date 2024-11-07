using LabWork.Abstractions;
using LabWork.Domain;
using LabWork.Service;

namespace LabWork
{
    public partial class GraphViewer : Form
    {
        private const string InputSeparator = ";  ";
        private const string WhiteSpace = "   ";

        private Action<Graphics> _action;
        private string _currentStageText = "����: {0}";
        private int _currentStage = 0;

        private IGraphBuilder _graphBuilder;
        private ICollection<int>? _currentTokenSequence;
        private List<GraphInfo> _stages;

        public GraphViewer(IGraphBuilder graphBuilder)
        {
            InitializeComponent();

            _graphBuilder = graphBuilder;
            _action = InitializeNewPetriNet;

            panelView.Visible = false; // Prevents the panel from redrawing when the form is first launched.
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

        private void panelView_Paint(object sender, PaintEventArgs e) =>
            _action(e.Graphics);

        private void btnCreateGraph_Click(object sender, EventArgs e)
        {
            panelView.Visible = true;
            _action = InitializeNewPetriNet;

            panelView.Invalidate();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            _currentStage--;
            _action = ShowCurrentStage;

            panelView.Invalidate();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            _currentStage++;
            _action = ShowCurrentStage;

            panelView.Invalidate();
        }

        private void btnClose_Click(object sender, EventArgs e) =>
            Application.Exit();

        private void InitializeNewPetriNet(Graphics graphics)
        {
            if (!ValidateInputData())
            {
                MessageBox.Show("������� ������������ ������", "������ �����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _currentStage = 0;
            _currentTokenSequence = InitializeTokenSequence();
            var graphInfo = _graphBuilder.BuildPetriGraph(panelView, _currentTokenSequence.ToList());
            _stages = PetriNetEngine.Simulate(graphInfo, _graphBuilder);
            
            ShowCurrentStage(graphics);
        }

        private void ShowCurrentStage(Graphics graphics)
        {
            _graphBuilder.VisualizePetriGraph(_stages[_currentStage], panelView, graphics);

            SetValueForStageLabel();
            UpdateButtonsStatus();
        }

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
            btnBack.Enabled = _currentStage > 0;
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
    }
}
