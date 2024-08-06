using System.Runtime.InteropServices;


namespace Console2Desk
{
    public class MessagesBoxImplementation
    {
        private Form _form;
        private System.Windows.Forms.Timer _timer;
        private bool _isTimerActive;

        public bool IsTimerActive => _isTimerActive;

        public MessagesBoxImplementation(Form form, System.Windows.Forms.Timer timer)
        {
            _form = form;
            _timer = timer;
            _isTimerActive = true;
        }

        public DialogResult ShowMessage(string message, string caption, MessageBoxButtons buttons)
        {
            try
            {
                PauseTimer(); // Pause the timer
                return MessageBox.Show(_form, message, caption, buttons, GetMessageBoxIcon(caption));
                
            }
            finally
            {
                ResumeTimer(); // Resume the timer
            }
        }

        private void PauseTimer()
        {
            _isTimerActive = false;
            _timer.Stop();
        }

        private void ResumeTimer()
        {
            _isTimerActive = true;
            _timer.Start();
        }

        private void BringFormToFront()
        {
            _form.Invoke((Action)(() =>
            {
                _form.BringToFront();
                SetWindowPos(_form.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
            }));
        }

        private MessageBoxIcon GetMessageBoxIcon(string caption)
        {
            // Map caption or other criteria to MessageBoxIcon
            switch (caption)
            {
                case "Information":
                    return MessageBoxIcon.Information;
                case "Display Resolution Information":
                    return MessageBoxIcon.Information;
                case "Warning":
                    return MessageBoxIcon.Warning;
                case "Error":
                    return MessageBoxIcon.Error;
                default:
                    return MessageBoxIcon.None;
            }
        }

        // Import functions from User32.dll
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW;
    }
}
