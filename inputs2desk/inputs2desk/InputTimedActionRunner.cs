
namespace inputs2desk
{
    public class InputTimedActionRunner
    {


        private readonly List<InputTimedAction> _actions = [];


        public void Update(TimeSpan time)
        {
            for (int i = _actions.Count - 1; i >= 0; i--)
            {
                if (!_actions[i].Update(time))
                {
                    _actions.RemoveAt(i);
                }
            }
        }


        public InputTimedActionRunner AddAction(InputTimedAction action)
        {
            ArgumentNullException.ThrowIfNull(action);

            _actions.Add(action);
            return this;
        }


        public InputTimedActionRunner Wait(Action action, TimeSpan duration)
        {
            ArgumentNullException.ThrowIfNull(action);

            _actions.Add(new InputTimedAction(null, action, duration));
            return this;
        }


        public InputTimedActionRunner DoAndWait(Action immediateAction, Action afterWaitAction, TimeSpan duration)
        {
            ArgumentNullException.ThrowIfNull(immediateAction);
            ArgumentNullException.ThrowIfNull(afterWaitAction);

            _actions.Add(new InputTimedAction(immediateAction, afterWaitAction, duration));
            return this;
        }


    }
}
