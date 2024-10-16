using System.Management.Automation;

namespace Console2Desk.BottomWindowButtons
{
    internal class RealTimeProtectionEnabled_Check
    {
        // Metodo asincrono che restituisce un Task
        public static async Task CheckAndSetRealTimeProtectionAsync(PictureBox pictureBoxRealTime_ON, PictureBox pictureBoxRealTime_OFF)
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                try
                {
                    // Modifica la policy di esecuzione di PowerShell in modo asincrono
                    powerShell.AddScript("Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass");
                    await Task.Run(() => powerShell.Invoke());

                    // Importa il modulo 'ConfigDefender' ed esegui il comando in modo asincrono
                    powerShell.AddScript("Import-Module ConfigDefender; Get-MpComputerStatus | Select-Object -ExpandProperty RealTimeProtectionEnabled");
                    var result = await Task.Run(() => powerShell.Invoke());

                    // Verifica se ci sono errori
                    if (powerShell.Streams.Error.Count > 0)
                    {
                        foreach (var error in powerShell.Streams.Error)
                        {
                            MessageBox.Show("Errore PowerShell: " + error.ToString());
                        }
                    }

                    // Controlla lo stato di Real-Time Protection
                    if (result != null && result.Count > 0)
                    {
                        string realTimeProtectionStatus = result[0].BaseObject.ToString();

                        //MessageBox.Show("Stato di RealTimeProtection: " + realTimeProtectionStatus);

                        // Cambia l'icona in base allo stato
                        if (realTimeProtectionStatus.Equals("True", StringComparison.OrdinalIgnoreCase))
                        {
                            pictureBoxRealTime_ON.Visible = true;
                            pictureBoxRealTime_OFF.Visible = false;
                        }
                        else if (realTimeProtectionStatus.Equals("False", StringComparison.OrdinalIgnoreCase))
                        {
                            pictureBoxRealTime_ON.Visible = false;
                            pictureBoxRealTime_OFF.Visible = true;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Errore: Nessun risultato ottenuto da PowerShell.");
                        pictureBoxRealTime_ON.Visible = false;
                        pictureBoxRealTime_OFF.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Errore durante l'esecuzione di PowerShell: " + ex.Message);
                    pictureBoxRealTime_ON.Visible = false;
                    pictureBoxRealTime_OFF.Visible = true;
                }
            }
        }
    }
}