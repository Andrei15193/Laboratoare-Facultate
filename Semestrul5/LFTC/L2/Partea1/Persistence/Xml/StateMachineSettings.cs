namespace Partea1.Persistence.Xml
{
    public struct StateMachineSettings
    {
        public StateSettings InitialState
        {
            get;
            set;
        }

        public StateSettings[] FinalStates
        {
            get;
            set;
        }

        public TranstionSettings[] Transitions
        {
            get;
            set;
        }
    }
}
