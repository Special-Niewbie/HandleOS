using System.Diagnostics;

namespace inputs2desk.system.Input;

internal sealed class PreciseTimer : IDisposable
{


    private readonly Action _callback;
    private double _interval;
    private int _isRunning;
    private bool _disposed;


    public PreciseTimer(Action callback, double interval)
    {
        if (callback == null)
            throw new ArgumentNullException(nameof(callback));
        if (interval <= 0)
            throw new ArgumentOutOfRangeException(nameof(interval), "Interval must be greater than zero.");

        _callback = callback;
        _interval = interval;
    }


    public double Interval
    {
        get => _interval;
        set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Interval must be greater than zero.");

            _interval = value;
        }
    }


    public bool IsRunning => _isRunning == 1;


    public void Dispose()
    {
        if (_disposed)
            return;

        Stop();

        _disposed = true;
    }


    public void Start()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (Interlocked.Exchange(ref _isRunning, 1) == 0)
        {
            Thread workerThread = new(RunLoop)
            {
                IsBackground = true
            };
            workerThread.Start();
        }
    }


    public void Stop()
    {
        ObjectDisposedException.ThrowIf(_disposed, this);

        if (Interlocked.Exchange(ref _isRunning, 0) == 1)
        {

        }
    }


    private void RunLoop()
    {
        try
        {
            int sleepMargin = Environment.ProcessorCount > 1 ? 16 : 0;

            Stopwatch stopwatch = Stopwatch.StartNew();
            while (_isRunning == 1)
            {
                double remaining = GetRemainingTime(stopwatch);
                if (remaining == 0d)
                {
                    stopwatch.Restart();
                    OnTick();
                    continue;
                }

                if (remaining >= sleepMargin * 2)
                {
                    Thread.Sleep((int)Math.Floor(remaining - sleepMargin));
                    continue;
                }

                Thread.SpinWait((int)(remaining * 100));
            }
        }
        finally
        {
            Interlocked.Exchange(ref _isRunning, 0);
        }
    }


    private void OnTick()
    {
        if (_isRunning == 0)
        {
            return;
        }

        _callback();
    }


    private double GetRemainingTime(Stopwatch stopwatch)
    {
        return Math.Max(_interval - stopwatch.Elapsed.TotalMilliseconds, 0d);
    }


}
