/*
Console2Desk

Copyright (C) 2023 Special-Niewbie Softwares

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation version 3.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

namespace Console2Desk.FormWifi
{
    // Enum per identificare la modalità del sistema
    public enum SystemMode
    {
        Desktop,
        Console
    }

    // Classe principale che orchestera tutte le operazioni WiFi
    public class WifiOrchestrator
    {
        private readonly WifiAnimationManager _animationManager;
        private readonly WifiMenuManager _menuManager;
        private readonly Form1 _parentForm;
        private readonly System.Windows.Forms.Timer _topMostTimer;
        private bool _isMenuOpen;

        public WifiOrchestrator(
            Form1 parentForm,
            PictureBox wifiPictureBox,
            System.Windows.Forms.Timer topMostTimer,
            MessagesBoxImplementation messagesBox)
        {
            _parentForm = parentForm;
            _topMostTimer = topMostTimer;
            _animationManager = new WifiAnimationManager(wifiPictureBox);
            _menuManager = new WifiMenuManager(messagesBox);
        }

        public void HandleWifiButtonClick()
        {
            SystemMode currentMode = _menuManager.GetSystemMode();

            switch (currentMode)
            {
                case SystemMode.Desktop:
                    HandleDesktopMode();
                    //HandleCustomMode();
                    break;
                case SystemMode.Console:
                    HandleCustomMode();
                    break;
            }
        }

        private void HandleDesktopMode()
        {
            if (_menuManager.ShowWifiMenu())
            {
                _animationManager.StartAnimation();
            }
        }

        private void HandleCustomMode()
        {
            _isMenuOpen = true;
            var formWifiMenu = new FormWifi(_parentForm, _topMostTimer);

            // Configura la posizione del form
            SetupCustomFormPosition(formWifiMenu);

            // Configura gli eventi
            formWifiMenu.FormClosed += (s, args) =>
            {
                _isMenuOpen = false;
                _parentForm.BringToFront();
            };

            formWifiMenu.ShowDialog();
        }

        private void SetupCustomFormPosition(Form formWifiMenu)
        {
            Point formLocation = new Point(
                _parentForm.Location.X + _parentForm.Width - formWifiMenu.Width - 10,
                _parentForm.Location.Y + _parentForm.Height - formWifiMenu.Height - 10
            );

            formWifiMenu.StartPosition = FormStartPosition.Manual;
            formWifiMenu.Location = formLocation;
        }

        public bool IsMenuOpen => _isMenuOpen;
    }
}
