using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console2Desk
{
    internal class timerGifTweakParadise
    {
        public static void StartTimerForPictureTweakParadise(PictureBox pictureBoxTweakParadise)
        {
            // Avvia la GIF di apertura
            pictureBoxTweakParadise.Image = Properties.Resources.TweakParadiseToOpen;

            // Timer per fermare la GIF e visualizzare l'icona statica
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 950; // Durata in millisecondi per completare la GIF

            timer.Tick += (s, _) =>
            {
                // Mostra l'icona statica di apertura
                pictureBoxTweakParadise.Image = Properties.Resources.TweakParadiseIconOpened;

                // Ferma il timer
                timer.Stop();

                // Rilascia le risorse del timer
                timer.Dispose();
            };
            timer.Start();
        }

        public static void StartTimerReverseForPictureTweakParadise(PictureBox pictureBoxTweakParadise)
        {
            // Avvia la GIF di chiusura
            pictureBoxTweakParadise.Image = Properties.Resources.TweakParadiseToClose;

            // Timer per fermare la GIF e visualizzare l'icona statica
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1050; // Durata in millisecondi per completare la GIF

            timer.Tick += (s, _) =>
            {
                // Mostra l'icona statica di chiusura
                pictureBoxTweakParadise.Image = Properties.Resources.TweakParadiseIconClosed;

                // Ferma il timer
                timer.Stop();

                // Rilascia le risorse del timer
                timer.Dispose();
            };
            timer.Start();
        }
    }
}
