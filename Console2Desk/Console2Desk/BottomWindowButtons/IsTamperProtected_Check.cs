using System.Management.Automation;

namespace Console2Desk.BottomWindowButtons
{
    internal class IsTamperProtected_Check
    {
        // Metodo per controllare lo stato di 'IsTamperProtected'
        public static bool CheckIsTamperProtected()
        {
            using (PowerShell powerShell = PowerShell.Create())
            {
                try
                {
                    // Modifica la policy di esecuzione di PowerShell
                    powerShell.AddScript("Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass");
                    powerShell.Invoke();

                    // Esegui il comando PowerShell per controllare lo stato di IsTamperProtected
                    powerShell.AddScript("Get-MpComputerStatus | Select-Object -ExpandProperty IsTamperProtected");
                    var result = powerShell.Invoke();

                    // Verifica se ci sono errori
                    if (powerShell.Streams.Error.Count > 0)
                    {
                        foreach (var error in powerShell.Streams.Error)
                        {
                            // Gestisci gli errori (eventualmente mostra un messaggio o logga l'errore)
                        }
                        return false;
                    }

                    // Controlla se il risultato contiene dati
                    if (result != null && result.Count > 0)
                    {
                        string tamperProtectionStatus = result[0].BaseObject.ToString();

                        // Restituisce True se la protezione Tamper è attiva, False altrimenti
                        return tamperProtectionStatus.Equals("True", StringComparison.OrdinalIgnoreCase);
                    }
                    else
                    {
                        // Nessun risultato ottenuto
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    // Gestisci l'errore (eventualmente mostra un messaggio o logga l'errore)
                    return false;
                }
            }
        }
    }
}


