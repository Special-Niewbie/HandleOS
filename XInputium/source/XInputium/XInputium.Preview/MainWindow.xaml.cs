﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using XInputium.Preview.Data.Poco;
using XInputium.ModifierFunctions;
using XInputium.XInput;
using System.Windows.Media.Imaging;

namespace XInputium.Preview;

/// <summary>
/// Interaction logic for MainWindow.xaml.
/// </summary>
public partial class MainWindow : Window
{
    private MediaPlayer mediaPlayer;
    private string audioFilePath = @"C:\Program Files\Console2Desk\XinputTest\AudioTest.mp3";
    private bool isAudioPlaying = false;

    private bool isLeftAudioPlaying = false;
    private bool isRightAudioPlaying = false;


    #region Fields

    private static readonly ModifierFunctionPoco[] s_ModifierFunctions;

    #endregion Fields


    #region Constructors

    static MainWindow()
    {
        // Create the modifier functions that we will use in the UI.
        s_ModifierFunctions = new ModifierFunctionPoco[]
        {
            new ModifierFunctionPoco("None", null),
            new("Linear", NonLinearFunctions.Linear),
            new("Quadratic EaseIn", NonLinearFunctions.QuadraticEaseIn),
            new("Quadratic EaseOut", NonLinearFunctions.QuadraticEaseOut),
            new("Quadratic EaseInOut", NonLinearFunctions.QuadraticEaseInOut),
            new("Cubic EaseIn", NonLinearFunctions.CubicEaseIn),
            new("Cubic EaseOut", NonLinearFunctions.CubicEaseOut),
            new("Cubic EaseInOut", NonLinearFunctions.CubicEaseInOut),
            new("Quartic EaseIn", NonLinearFunctions.QuarticEaseIn),
            new("Quartic EaseOut", NonLinearFunctions.QuarticEaseOut),
            new("Quartic EaseInOut", NonLinearFunctions.QuarticEaseInOut),
            new("Quintic EaseIn", NonLinearFunctions.QuinticEaseIn),
            new("Quintic EaseOut", NonLinearFunctions.QuinticEaseOut),
            new("Quintic EaseInOut", NonLinearFunctions.QuinticEaseInOut),
            new("Sine EaseIn", NonLinearFunctions.SineEaseIn),
            new("Sine EaseOut", NonLinearFunctions.SineEaseOut),
            new("Sine EaseInOut", NonLinearFunctions.SineEaseInOut),
            new("Circular EaseIn", NonLinearFunctions.CircularEaseIn),
            new("Circular EaseOut", NonLinearFunctions.CircularEaseOut),
            new("Circular EaseInOut", NonLinearFunctions.CircularEaseInOut),
            new("Exponential EaseIn", NonLinearFunctions.ExponentialEaseIn),
            new("Exponential EaseOut", NonLinearFunctions.ExponentialEaseOut),
            new("Exponential EaseInOut", NonLinearFunctions.ExponentialEaseInOut),
            new("Bézier", NonLinearFunctions.Bezier),
            new("Reverse", CommonModifierFunctions.Reverse),
            new("Negate", CommonModifierFunctions.Negate),
            new("Scale 200%", CommonModifierFunctions.Scale(2f)),
            new("Scale 50%", CommonModifierFunctions.Scale(0.5f)),
            new("Boolean", CommonModifierFunctions.Boolean()),
            new("Quantize 1/1", CommonModifierFunctions.Quantize(1f)),
            new("Quantize 1/2", CommonModifierFunctions.Quantize(0.5f)),
            new("Quantize 1/4", CommonModifierFunctions.Quantize(0.25f)),
            new("Quantize 1/8", CommonModifierFunctions.Quantize(0.125f)),
            new("Zero", CommonModifierFunctions.Zero),
        };
    }


    public MainWindow()
    {
        InitializeComponent();

        // Register window event handlers
        SourceInitialized += (_, _) => CompositionTarget.Rendering += (_, _) => OnRendering();
        Loaded += MainWindow_Loaded;

        // Initialize input objects.
        DeviceManager = new();
        DeviceManager.DeviceConnected += DeviceManager_DeviceConnected;
        Gamepad = new(null);
        Gamepad.StateChanged += (_, _) => OnGamepadStateChanged();

        imageRightAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));
        imageLeftAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));

        // Inizializza il MediaPlayer
        mediaPlayer = new MediaPlayer();
        mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

        // Imposta i valori iniziali degli slider
        sliderLeft.Value = 100;
        sliderRight.Value = 100;

        // Imposta il volume iniziale
        SetInitialVolume();

        // Aggiorna il volume per riflettere i valori iniziali degli slider
        sliderLeft_ValueChanged(sliderLeft, null);
        sliderRight_ValueChanged(sliderRight, null);
    }

    #endregion Constructors


    #region Properties

    public static IReadOnlyList<ModifierFunctionPoco> ModifierFunctions => s_ModifierFunctions;


    #region Gamepad dependency property

    public XGamepad Gamepad
    {
        get => (XGamepad)GetValue(GamepadProperty);
        private set => SetValue(s_GamepadPropertyKey, value);
    }

    private static readonly DependencyPropertyKey s_GamepadPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(Gamepad),
            typeof(XGamepad), typeof(MainWindow),
            new PropertyMetadata(null));

    public static readonly DependencyProperty GamepadProperty
        = s_GamepadPropertyKey.DependencyProperty;

    #endregion


    #region DeviceManager dependency property

    public XInputDeviceManager DeviceManager
    {
        get => (XInputDeviceManager)GetValue(DeviceManagerProperty);
        private set => SetValue(s_DeviceManagerPropertyKey, value);
    }

    private static readonly DependencyPropertyKey s_DeviceManagerPropertyKey
        = DependencyProperty.RegisterReadOnly(nameof(DeviceManager),
            typeof(XInputDeviceManager), typeof(MainWindow),
            new PropertyMetadata(null));

    public static readonly DependencyProperty DeviceManagerProperty
        = s_DeviceManagerPropertyKey.DependencyProperty;

    #endregion

    #endregion Properties


    #region Methods

    private void SetGamepadDefaultConfiguration()
    {
        // Setup triggers.
        Gamepad.LeftTrigger.InnerDeadZone = 0.15f;
        Gamepad.LeftTrigger.ModifierFunction = NonLinearFunctions.QuadraticEaseIn;
        Gamepad.RightTrigger.CopyConfigurationFrom(Gamepad.LeftTrigger);

        // Setup joysticks.
        Gamepad.LeftJoystick.InnerDeadZone = 0.2f;
        Gamepad.LeftJoystick.RadiusModifierFunction = NonLinearFunctions.QuadraticEaseIn;
        Gamepad.LeftJoystick.SmoothingSamplePeriod = TimeSpan.FromMilliseconds(100d);
        Gamepad.LeftJoystick.SmoothingFactor = 0.75f;
        Gamepad.RightJoystick.CopyConfigurationFrom(Gamepad.LeftJoystick);
    }


    [Conditional("DEBUG")]
    private void InitializeDebug()
    {
        // Code in this method is for simple testing purposes during debug.

        // This could be used to simulate a typical 'jump' action button, where
        // the user makes a character jump higher by holding the button for longer,
        // but with a time limit that makes the character jump its highest if the
        // user doesn't release the button after too long.
        double maxJumpHoldTime = 750;  // Button hold time for the highest jump, in milliseconds. 
        Gamepad.RegisterActivationInputEvent(
            () => Gamepad.Buttons.A.IsPressed,
            TimeSpan.Zero, TimeSpan.Zero,
            TimeSpan.FromMilliseconds(maxJumpHoldTime),
            ActivationInputEventTriggerMode.OnDeactivation,
            (s, e) =>
            {
                float jumpForce = (float)(e.PreviousStateDuration.TotalMilliseconds / maxJumpHoldTime);
                jumpForce = NonLinearFunctions.QuadraticEaseOut(jumpForce);
                Debug.WriteLine($"Jump force: {jumpForce:P0};");
            });


    }



    protected virtual void OnRendering()
    {
        DeviceManager.Update();
    }


    protected virtual void OnGamepadStateChanged()
    {
        // Set the controller motors's speed based on the values of its triggers.
        Gamepad.LeftMotorSpeed = Gamepad.LeftTrigger.Value;
        Gamepad.RightMotorSpeed = Gamepad.RightTrigger.Value;
    }

    #endregion Methods


    #region Event handlers

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
        SetGamepadDefaultConfiguration();
        InitializeDebug();
    }


    private void DeviceManager_DeviceConnected(object? sender, XInputDeviceEventArgs e)
    {
        if (!Gamepad.IsConnected)
        {
            Gamepad.Device = DeviceManager.ConnectedDevices.FirstOrDefault();
        }
    }


    #endregion Event handlers

    private void backButton_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void SetInitialVolume()
    {
        mediaPlayer.Volume = Math.Max(sliderLeft.Value, sliderRight.Value);
    }

    private void MediaPlayer_MediaEnded(object sender, EventArgs e)
    {
        // Se l'audio termina, reimposta lo stato di riproduzione
        isLeftAudioPlaying = false;
        isRightAudioPlaying = false;

        // Reimposta le icone degli altoparlanti a "Speaker.png"
        imageLeftAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));
        imageRightAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));
    }

    private void leftAudio_Click(object sender, RoutedEventArgs e)
    {
        // Controlla se l'audio di destra è in riproduzione
        if (isRightAudioPlaying)
        {
            // Interrompi l'audio di destra
            mediaPlayer.Stop();
            isRightAudioPlaying = false;
            // Cambia l'icona di destra a "Speaker.png"
            imageRightAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));
        }

        // Controlla se l'audio è in riproduzione
        if (!isLeftAudioPlaying)
        {
            // Avvia la riproduzione dell'audio sinistro
            mediaPlayer.Open(new Uri(audioFilePath));
            mediaPlayer.Balance = -1; // Indirizza l'audio solo alla cassa sinistra
            mediaPlayer.Volume = sliderLeft.Value; // Imposta il volume basato sul valore dello slider
            mediaPlayer.Play();
            isLeftAudioPlaying = true;
            isRightAudioPlaying = false; // Assicura che l'audio di destra sia disabilitato

            // Cambia l'icona a "SpeakerON.png"
            imageLeftAudio.Source = new BitmapImage(new Uri("SpeakerON.png", UriKind.Relative));
        }
        else
        {
            // Interrompi la riproduzione dell'audio sinistro
            mediaPlayer.Stop();
            isLeftAudioPlaying = false;

            // Cambia l'icona a "Speaker.png"
            imageLeftAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));
        }

    }
    private void rightAudio_Click(object sender, RoutedEventArgs e)
    {
        // Controlla se l'audio di sinistra è in riproduzione
        if (isLeftAudioPlaying)
        {
            // Interrompi l'audio di sinistra
            mediaPlayer.Stop();
            isLeftAudioPlaying = false;
            // Cambia l'icona di sinistra a "Speaker.png"
            imageLeftAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));
        }

        // Controlla se l'audio è in riproduzione
        if (!isRightAudioPlaying)
        {
            // Avvia la riproduzione dell'audio destro
            mediaPlayer.Open(new Uri(audioFilePath));
            mediaPlayer.Balance = 1; // Indirizza l'audio solo alla cassa destra
            mediaPlayer.Volume = sliderRight.Value; // Imposta il volume basato sul valore dello slider
            mediaPlayer.Play();
            isRightAudioPlaying = true;
            isLeftAudioPlaying = false; // Assicura che l'audio di sinistra sia disabilitato

            // Cambia l'icona a "SpeakerON.png"
            imageRightAudio.Source = new BitmapImage(new Uri("SpeakerON.png", UriKind.Relative));
        }
        else
        {
            // Interrompi la riproduzione dell'audio destro
            mediaPlayer.Stop();
            isRightAudioPlaying = false;

            // Cambia l'icona a "Speaker.png"
            imageRightAudio.Source = new BitmapImage(new Uri("SpeakerOFF.png", UriKind.Relative));
        }

    }

    private void sliderLeft_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (mediaPlayer != null && mediaPlayer.Balance == -1) // Assicurati che l'audio sia indirizzato alla cassa sinistra
        {
            mediaPlayer.Volume = sliderLeft.Value / 100.0;
        }
    }

    private void sliderRight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        if (mediaPlayer != null && mediaPlayer.Balance == 1) // Assicurati che l'audio sia indirizzato alla cassa destra
        {
            mediaPlayer.Volume = sliderRight.Value / 100.0;
        }
    }
}
