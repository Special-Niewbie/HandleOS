using inputs2desk.system.Input;
using System.Diagnostics;
using System.IO.Pipes;
using XInputium;
using XInputium.ModifierFunctions;
using XInputium.XInput;
using Osklib;

namespace inputs2desk
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer processCheckTimer;
        private InputLoop inputLoop;
        private Thread fileWriteThread;
        private string tempFilePath;
        XGamepad gamepad = new();
        XInputDeviceManager deviceManager = new();
        private int _connectedControllers = 0;

        private readonly object lockObject = new object();
        private readonly object updateLock = new object();

        // Variabile per gestire il tempo di vibrazione
        private InputTimedActionRunner actionRunner;

        public Form1()
        {
            InitializeComponent();
            // Nascondi la finestra
            this.Load += (sender, e) => { this.Hide(); };

            // Inizializza gamepad e deviceManager
            
            deviceManager.DeviceStateChanged += (_, e) =>
            {
                if (e.Device.IsConnected)
                    gamepad.Device = e.Device;
            };

            
            // Imposta il timer per il controllo del processo
            processCheckTimer = new System.Windows.Forms.Timer();
            processCheckTimer.Interval = 1500;
            processCheckTimer.Tick += ProcessCheckTimer_Tick;
            processCheckTimer.Start();

            // Inizializza il gamepad
            InitializeGamepad();

            // Percorso del file temporaneo
            tempFilePath = Path.Combine(Path.GetTempPath(), "controller_status.txt");

            // Avvia il loop di aggiornamento dello stato dei controller
            StartStatusUpdate();

            // Inizializza l'input loop
            inputLoop = new InputLoop(OnInputUpdate);
            inputLoop.Start();

            // Inizializzazione di actionRunner
            actionRunner = new InputTimedActionRunner();

            // Continua l'esecuzione dell'applicazione
            Application.Run();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // Metodo per aggiornare lo stato dei controller e scriverlo nel file
        private void StartStatusUpdate()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        int connectedControllers = CountConnectedControllers();
                        string filePath = Path.Combine(Path.GetTempPath(), "controller_status.txt");

                        // Scrivi lo stato del controller in un file temporaneo
                        lock (lockObject)
                        {
                            File.WriteAllText(filePath, connectedControllers.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        //LogException("Errore durante l'aggiornamento dello stato dei controller: " + ex.Message);
                    }

                    Thread.Sleep(1500); // Intervallo di 1.5 secondi per l'aggiornamento
                }
            });
        }

        private void OnInputUpdate(TimeSpan time)
        {
            try
            {
                deviceManager.Update();
                // Aggiorna actionRunner nel ciclo di aggiornamento
                actionRunner.Update(time);
                _connectedControllers = deviceManager.ConnectedDevices.Count;

            }
            catch (Exception ex)
            {
                //LogException("Errore durante l'aggiornamento dell'input: " + ex.Message);
            }
        }

        private int CountConnectedControllers()
        {
            return _connectedControllers;
        }

        private void InitializeGamepad()
        {
            //XInputDevice device = new(XInputUserIndex.One);
            // Configura il gamepad come nel tuo codice precedente
            var mouseRightJoystick = gamepad.RightJoystick;
            var mouseLeftJoystick = gamepad.LeftJoystick;
            var leftMouseButton = gamepad.Buttons.RB;            
            var middleMouseButton = gamepad.Buttons.RS;
            var rightMouseButton = gamepad.RightTrigger.ToDigitalButton(0.5f);
            var leftTriggerDigital = gamepad.LeftTrigger.ToDigitalButton(0.5f);
            var leftLBButtonTab = gamepad.Buttons.LB;

            mouseLeftJoystick.InnerDeadZone = 0.2f;
            mouseLeftJoystick.RadiusModifierFunction = NonLinearFunctions.QuadraticEaseIn;

            mouseRightJoystick.InnerDeadZone = 0.2f;
            mouseRightJoystick.RadiusModifierFunction = NonLinearFunctions.QuadraticEaseIn;

            double moveSpeed = 0.015;
            mouseLeftJoystick.Updated += (sender, e) =>
            {
                if (mouseLeftJoystick.IsPushed)
                {
                    Mouse.MoveBy(
                        mouseLeftJoystick.X * moveSpeed * gamepad.FrameTime.TotalSeconds,
                        -mouseLeftJoystick.Y * moveSpeed * gamepad.FrameTime.TotalSeconds);
                }
            };
            mouseRightJoystick.Updated += (sender, e) =>
            {
                if (mouseRightJoystick.IsPushed)
                {
                    Mouse.MoveBy(
                        mouseRightJoystick.X * moveSpeed * gamepad.FrameTime.TotalSeconds,
                        -mouseRightJoystick.Y * moveSpeed * gamepad.FrameTime.TotalSeconds);
                }
            };

            leftMouseButton.Pressed += (sender, e) => Mouse.PressButton(MouseButton.Left);
            leftMouseButton.Released += (sender, e) => Mouse.ReleaseButton(MouseButton.Left);
            rightMouseButton.Pressed += (sender, e) => Mouse.PressButton(MouseButton.Right);
            rightMouseButton.Released += (sender, e) => Mouse.ReleaseButton(MouseButton.Right);
            middleMouseButton.Pressed += (sender, e) => Mouse.PressButton(MouseButton.Middle);
            middleMouseButton.Released += (sender, e) => Mouse.ReleaseButton(MouseButton.Middle);


            leftTriggerDigital.Pressed += (s, e) =>
            {
                Keyboard.PressKey(KeyboardVirtualKey.Shift);
                Keyboard.TapKey(KeyboardVirtualKey.Tab);
                Keyboard.ReleaseKey(KeyboardVirtualKey.Shift);
            };
            leftLBButtonTab.Pressed += (s, e) => Keyboard.TapKey(KeyboardVirtualKey.Tab);



            gamepad.Buttons.Back.Pressed += (sender, e) => Keyboard.TapKey(KeyboardVirtualKey.Escape);
            gamepad.Buttons.A.Pressed += (sender, e) =>
            {
                Keyboard.PressKey(KeyboardVirtualKey.Enter);
                
                Keyboard.ReleaseKey(KeyboardVirtualKey.Enter);
            };
            gamepad.Buttons.Y.Pressed += (_, _) =>
            {
                // Verifica se la tastiera su schermo è visibile
                if (OnScreenKeyboard.IsOpened())
                {
                    // Nascondi la tastiera se è visibile
                    OnScreenKeyboard.Close();
                }
                else
                {
                    // Mostra la tastiera se non è visibile
                    OnScreenKeyboard.Show();
                }

                // Durata della vibrazione
                gamepad.LeftMotorSpeed = 0.2f;
                gamepad.RightMotorSpeed = 0.8f;
                // Usa actionRunner per fermare entrambi i motori dopo 200 ms
                actionRunner.Wait(() =>
                {
                    gamepad.LeftMotorSpeed = 0;
                    gamepad.RightMotorSpeed = 0;
                }, TimeSpan.FromMilliseconds(200));

            };
            gamepad.Buttons.X.Pressed += (_, _) =>
            {

                // Simula la pressione dei tasti Control, Windows e O
                Keyboard.PressKey(KeyboardVirtualKey.Control);          // Premere Control
                Keyboard.PressKey(KeyboardVirtualKey.RightWindows);     // Premere il tasto Windows destro
                Keyboard.PressKey(KeyboardVirtualKey.O);                // Premere O

                // Rilascia i tasti
                Keyboard.ReleaseKey(KeyboardVirtualKey.Control);        // Rilascia Control
                Keyboard.ReleaseKey(KeyboardVirtualKey.RightWindows);   // Rilascia il tasto Windows destro
                Keyboard.ReleaseKey(KeyboardVirtualKey.O);              // Rilascia O

                // Durata della vibrazione
                gamepad.LeftMotorSpeed = 0.2f;
                gamepad.RightMotorSpeed = 0.8f;
                // Usa actionRunner per fermare entrambi i motori dopo 200 ms
                actionRunner.Wait(() =>
                {
                    gamepad.LeftMotorSpeed = 0;
                    gamepad.RightMotorSpeed = 0;
                }, TimeSpan.FromMilliseconds(200));

            };

            gamepad.Buttons.DPadUp.Pressed += (sender, e) => Keyboard.TapKey(KeyboardVirtualKey.Up);
            gamepad.Buttons.DPadDown.Pressed += (sender, e) => Keyboard.TapKey(KeyboardVirtualKey.Down);
            gamepad.Buttons.DPadLeft.Pressed += (sender, e) => Keyboard.TapKey(KeyboardVirtualKey.Left);
            gamepad.Buttons.DPadRight.Pressed += (sender, e) => Keyboard.TapKey(KeyboardVirtualKey.Right);

            
        }
        // Evento che controlla se Console2Desk.exe è in esecuzione
        private void ProcessCheckTimer_Tick(object sender, EventArgs e)
        {
            // Controlla se Console2Desk.exe è in esecuzione
            if (!IsProcessRunning("Console2Desk"))
            {
                // Ferma il timer per evitare che continui a mostrare il messaggio
                processCheckTimer.Stop();

                // Mostra il messaggio di errore una sola volta
                // For Debug MessageBox.Show("Console2Desk.exe non è in esecuzione. L'applicazione verrà chiusa.", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Esci dall'applicazione
                Environment.Exit(0);
            }
        }

        // Metodo per verificare se il processo è in esecuzione
        private bool IsProcessRunning(string processName)
        {
            // Cerca i processi con il nome fornito
            Process[] processes = Process.GetProcessesByName(processName);

            // Restituisce true se almeno un processo corrisponde al nome, false altrimenti
            return processes.Length > 0;
        }
        #region Just keep it. Don't use it, as this implementation generate double press in Windows Menu navigation
        //TimeSpan buttonRepeatDelay = TimeSpan.FromMilliseconds(350);
        //TimeSpan buttonRepeatInterval = TimeSpan.FromMilliseconds(100);
        /*gamepad.RegisterButtonRepeatEvent(XButtons.DPadUp, buttonRepeatDelay, buttonRepeatInterval,
            (_, e) => Keyboard.TapKey(KeyboardVirtualKey.Up));
        gamepad.RegisterButtonRepeatEvent(XButtons.DPadDown, buttonRepeatDelay, buttonRepeatInterval,
            (_, e) => Keyboard.TapKey(KeyboardVirtualKey.Down));
        gamepad.RegisterButtonRepeatEvent(XButtons.DPadLeft, buttonRepeatDelay, buttonRepeatInterval,
            (_, e) => Keyboard.TapKey(KeyboardVirtualKey.Left));
        gamepad.RegisterButtonRepeatEvent(XButtons.DPadRight, buttonRepeatDelay, buttonRepeatInterval,
            (_, e) => Keyboard.TapKey(KeyboardVirtualKey.Right));*/
        #endregion
    }
}

