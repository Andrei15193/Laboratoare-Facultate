using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Partea1.Model;

namespace Partea1.Persistence.Xml
{
    public class XmlConstantStateMachineRepository
        : IConstantStateMachineRepository
    {
        static private ConstantStateMachine _CreateConstantStateMachine(StateMachineSettings stateMachineSettings)
        {
            ISet<char> alphabet = new SortedSet<char>();
            State<char> initialState = new State<char>(stateMachineSettings.InitialState.Name, stateMachineSettings.FinalStates.Select(finalStates => finalStates.Name).Contains(stateMachineSettings.InitialState.Name));
            IDictionary<string, State<char>> states = new SortedDictionary<string, State<char>>();

            states.Add(initialState.Name, initialState);

            foreach (TranstionSettings transitionSetting in stateMachineSettings.Transitions)
            {
                State<char> sourceState;
                State<char> destinationState;

                alphabet.Add(transitionSetting.TranstitingCharacter);
                if (!states.TryGetValue(transitionSetting.DestinationState, out destinationState))
                {
                    destinationState = new State<char>(transitionSetting.DestinationState, stateMachineSettings.FinalStates.Select(finalStates => finalStates.Name).Contains(transitionSetting.DestinationState));
                    states.Add(destinationState.Name, destinationState);
                }

                if (!states.TryGetValue(transitionSetting.SourceState, out sourceState))
                {
                    sourceState = new State<char>(transitionSetting.SourceState, stateMachineSettings.FinalStates.Select(finalStates => finalStates.Name).Contains(transitionSetting.SourceState));
                    states.Add(sourceState.Name, sourceState);
                }

                sourceState.Transitions.Add(new Transition<char>(item => (item == transitionSetting.TranstitingCharacter), sourceState, destinationState));
            }

            return new ConstantStateMachine(initialState, states.Values, alphabet);
        }

        public ConstantStateMachine Get(string fileName)
        {
            if (fileName != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(StateMachineSettings));

                using (Stream stream = File.OpenRead(fileName))
                    return _CreateConstantStateMachine((StateMachineSettings)serializer.Deserialize(stream));
            }
            else
                throw new ArgumentNullException("fileName");
        }
    }
}
