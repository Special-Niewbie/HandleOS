using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace Console2Desk
{
    public class CheckForUpdatesJson
    {
        public async Task CheckForUpdatesAsync(MessagesBoxImplementation messagesBoxImplementation)
        {
            var localFilePath = @"C:\Program Files\Console2Desk\sources\local_news_file.json";
            var url = "https://raw.githubusercontent.com/Special-Niewbie/HandleOS/main/news";

            try
            {
                using (var client = new HttpClient())
                {
                    // Ottieni il contenuto del file remoto
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode(); // Assicura che la risposta sia stata correttamente ricevuta
                    var remoteContent = await response.Content.ReadAsStringAsync();

                    // Ottieni il contenuto del file locale
                    string localContent = string.Empty;
                    if (File.Exists(localFilePath))
                    {
                        localContent = File.ReadAllText(localFilePath);
                    }

                    // Confronta il contenuto dei due file
                    if (remoteContent != localContent)
                    {
                        // Se il contenuto è diverso, aggiorna il file locale
                        File.WriteAllText(localFilePath, remoteContent);
                        // Riproduci un suono di sistema
                        // Riproduci un suono personalizzato
                        using (var player = new SoundPlayer(@"C:\Windows\Media\Alarm02.wav"))
                        {
                            player.Play(); // Riproduce il suono
                        }
                        // Mostra un messaggio di sistema
                        messagesBoxImplementation.ShowMessage("There is HandleOS NEWS, please check the News Box area!!", "News", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show($"Errore durante l'aggiornamento delle notizie: {ex.Message}");
            }
        }
    }
}
