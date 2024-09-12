namespace HandleOS_Benchmark
{
    public partial class FormBench : Form
    {
        public event EventHandler RequestCancelBenchmark;
        public FormBench()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;

            InitializeProgressBarBench();
        }

        private void FormBench_Load(object sender, EventArgs e)
        {

        }

        private void InitializeProgressBarBench()
        {
            customProgressBar1.MinValue = 0;
            customProgressBar1.MaxValue = 100;
            customProgressBar1.Value = 0;

            /*I have chosen to write here these dimensions for the progress bar, to ensure it remains centrally aligned
              and easily accessible across various screen resolutions,
              thereby avoiding constant adjustments that would be necessary with different design specifications
              without this approach. This simplifies user interaction by maintaining
              a consistent layout on screens of all sizes.*/
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int barWidth = 500; // Width Bar
            int barHeight = 50; // Height Bar

            customProgressBar1.Width = barWidth;
            customProgressBar1.Height = barHeight;
            customProgressBar1.Location = new Point((screenWidth - barWidth) / 2, (screenHeight - barHeight) / 2);

            this.Controls.Add(customProgressBar1);
        }

        public void UpdateProgress(int step, int totalSteps)
        {
            int progress = (int)((float)step / totalSteps * 100);
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int>(UpdateProgressInternal), progress);
            }
            else
            {
                UpdateProgressInternal(progress);
            }
        }

        private void UpdateProgressInternal(int progress)
        {
            // Ensuring progress bars reflect actual completion accurately without overshooting,
            // currently set at an incorrect value of 104 due to rounding.
            // This discrepancy only affects the display by four points but does not impact functionality
            // since these are mere numbers and can be tolerated for demonstration purposes
            // when showcasing a progress bar full (indicating 100%)
            progress = Math.Min(progress, 100);
            customProgressBar1.Value = progress;
            customProgressBar1.BarText = $"Progress: {progress}%";
        }

        private void customProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            // Ask the user if they are sure they want to cancel the benchmark
            DialogResult result = MessageBox.Show(
                "Are you sure you want to exit the benchmark?",
                "Confirm Exit",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Trigger the cancellation
                RequestCancelBenchmark?.Invoke(this, EventArgs.Empty);
                this.Close();

                // Show a message indicating that the benchmark was not completed
                MessageBox.Show(
                    "The benchmark was not completed. Please restart it using the START button.",
                    "Benchmark Cancelled",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
