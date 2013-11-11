using System;
using System.Collections.Generic;
using System.Linq;

namespace Partea1.Model
{
    public sealed class State<T>
    {
        public State(string name, bool isFinalState, Func<T, bool> transitionToSelfCondition, IEnumerable<Transition<T>> transitions)
        {
            if (transitions != null)
            {
                _name = name ?? string.Empty;
                _transitions = new List<Transition<T>>();
                if (transitionToSelfCondition != null)
                    _transitions.Add(new Transition<T>(transitionToSelfCondition, this, this));
                _transitions.AddRange(transitions.ToList());
                _isFinalState = isFinalState;
            }
        }

        public State(string name, bool isFinalState, Func<T, bool> transitionToSelfCondition = null, params Transition<T>[] transitions)
            : this(name, isFinalState, transitionToSelfCondition, (IEnumerable<Transition<T>>)transitions)
        {
        }

        public State<T> Transit(T item)
        {
            State<T> destination = null;

            using (IEnumerator<Transition<T>> transition = _transitions.GetEnumerator())
                while (transition.MoveNext() && destination == null)
                    if (transition.Current.CanTransit(item))
                        destination = transition.Current.Destination;

            return destination;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public bool IsFinalState
        {
            get
            {
                return _isFinalState;
            }
        }

        public IList<Transition<T>> Transitions
        {
            get
            {
                return _transitions;
            }
        }

        private readonly bool _isFinalState;
        private readonly string _name;
        private readonly List<Transition<T>> _transitions;
    }
}
