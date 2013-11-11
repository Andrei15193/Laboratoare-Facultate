using System;
using System.Collections.Generic;

namespace Partea1.Model
{
    public abstract class StateMachine<T>
    {
        public StateMachine(State<T> initialState)
        {
            if (initialState != null)
                _initialState = initialState;
            else
                throw new ArgumentNullException("initialState");
        }

        public abstract bool IsValid(T item);

        public State<T> InitialState
        {
            get
            {
                return _initialState;
            }
        }

        public abstract IReadOnlyList<State<T>> States
        {
            get;
        }

        public abstract IReadOnlyList<Transition<T>> Transitions
        {
            get;
        }

        public abstract IReadOnlyList<T> Alphabet
        {
            get;
        }

        private readonly State<T> _initialState;
    }
}
