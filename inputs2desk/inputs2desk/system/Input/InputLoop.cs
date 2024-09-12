using XInputium;

namespace inputs2desk.system.Input;

/// <summary>
/// Implements a thread safe input loop.
/// </summary>
/// <remarks>
/// <see cref="InputLoop"/> iterates at a specified frequency and calls
/// a provided callback <see cref="Action"/> on every iteration. This
/// callback is called in a thread safe manner -- it is never called
/// concurrently, and it's always called in the same thread.
/// <see cref="InputLoop"/> uses it's own dedicated thread.
/// </remarks>
public sealed class InputLoop : IDisposable
{


    private readonly Action<TimeSpan> _onUpdate;
    private readonly InputLoopWatch _watch;
    private readonly PreciseTimer _timer;
    private bool _disposed, _disposing;


    /// <summary>
    /// Initializes a new instance of the <see cref="InputLoop"/> class,
    /// that uses the specified callback for iterations and the specified
    /// number of iterations per second.
    /// </summary>
    /// <param name="onUpdate">Callback that is invoked
    /// on every iteration of the input loop.</param>
    /// <param name="frequency">Optional. Number of desired iterations
    /// per second. The default is 60.</param>
    /// <param name="watch">Optional. The <see cref="InputLoopWatch"/> instance
    /// used to measure time, or <see langword="null"/> to use the default
    /// implementation. The default value is <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="onUpdate"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException"><paramref name="frequency"/> is
    /// <see cref="double.NaN"/>, <see cref="double.NegativeInfinity"/> or
    /// <see cref="double.PositiveInfinity"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="frequency"/> is equal to or less than 0.</exception>
    public InputLoop(Action<TimeSpan> onUpdate, double frequency = 60, InputLoopWatch? watch = null)
    {
        ArgumentNullException.ThrowIfNull(onUpdate);
        if (double.IsNaN(frequency) || double.IsInfinity(frequency))
            throw new ArgumentException(
                $"'{frequency}' is not a valid value for the '{nameof(frequency)}' parameter",
                nameof(frequency));
        if (frequency <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(frequency), "Frequency must be greater than zero.");
        }

        _onUpdate = onUpdate;
        _watch = watch ?? InputLoopWatch.GetDefault();
        _timer = new(OnTick, 1000d / frequency);
    }


    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed || _disposing)
            return;
        _disposing = true;

        _timer.Dispose();

        _disposed = true;
        _disposing = false;
    }


    public void Start()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _timer.Start();
    }


    public void Stop()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        _timer.Stop();
    }


    private void OnTick()
    {
        _onUpdate(_watch.GetTime());
    }


}
