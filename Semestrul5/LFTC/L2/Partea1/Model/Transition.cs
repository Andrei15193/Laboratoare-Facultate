using System;

namespace Partea1.Model
{
    public sealed class Transition<T>
    {
        public Transition(Func<T, bool> condition, State<T> source, State<T> destination)
        {
            if (source != null)
                if (destination != null)
                {
                    _condition = condition;
                    _source = source;
                    _destination = destination;
                }
                else
                    throw new ArgumentNullException("destination");
            else
                throw new ArgumentNullException("source");
        }

        public bool CanTransit(T item)
        {
            return (_condition == null || _condition(item));
        }

        public State<T> Source
        {
            get
            {
                return _source;
            }
        }

        public State<T> Destination
        {
            get
            {
                return _destination;
            }
        }

        private readonly Func<T, bool> _condition;
        private readonly State<T> _source;
        private readonly State<T> _destination;
    }
}
