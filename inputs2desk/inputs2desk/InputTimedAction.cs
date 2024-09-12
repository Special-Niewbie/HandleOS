
namespace inputs2desk
{
    public sealed class InputTimedAction
    {


        private readonly TimeSpan _startTime;
        private TimeSpan _remainingTime;


        public InputTimedAction(Action? startAction, Action? endAction, TimeSpan duration)
        {
            _startTime = duration;
            _remainingTime = _startTime;

            OnStart = startAction;
            OnEnd = endAction;
        }


        private Action? OnStart { get; }


        private Action? OnEnd { get; }


        public bool Update(TimeSpan time)
        {
            if (_startTime == _remainingTime)
            {
                OnStart?.Invoke();
                if (OnEnd is null)
                {
                    return false;
                }
            }

            _remainingTime -= time;
            if (_remainingTime <= TimeSpan.Zero)
            {
                OnEnd?.Invoke();
                return false;
            }

            return OnStart is not null || OnEnd is not null;
        }


    }
}
