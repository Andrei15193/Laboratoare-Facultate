using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Partea1.Model;
using Partea1.Persistence;
using Partea1.Persistence.Xml;

namespace Partea1.ViewModels
{
    internal sealed class MainViewModel
        : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            _validateCommand = new RelayCommand(delegate
                {
                    IsSequenceValid = _ConstantStateMachine.IsValid(Sequence);
                });
            _getLongestPrefixCommand = new RelayCommand(delegate
                {
                    LongestPrefix = _ConstantStateMachine.GetLongestPrefix(Sequence);
                });
            _postAlphabetCommand = new RelayCommand(delegate
                {
                    DefinitionTitle = "Alphabet:";
                    Definition = new string(_ConstantStateMachine.Alphabet.ToArray());
                });
            _postAllStatesCommand = new RelayCommand(delegate
                {
                    DefinitionTitle = "All states:";
                    Definition = string.Join(", ", _ConstantStateMachine.States.Select(state => state.Name));
                });
            _postFinalStatesCommand = new RelayCommand(delegate
                {
                    DefinitionTitle = "Final states:";
                    Definition = string.Join(", ", _ConstantStateMachine.States.Where(state => state.IsFinalState).Select(state => state.Name));
                });
            _postTransitionsCommand = new RelayCommand(delegate
                {
                    DefinitionTitle = "Transitions:";
                    Definition = string.Join(", ",
                                             _ConstantStateMachine.Transitions
                                                                  .Select(transition => string.Format("{0} --> {1}",
                                                                                                      transition.Source.Name,
                                                                                                      transition.Destination.Name)));
                });
            _postAlphabetCommand.Execute(null);
        }

        public string StateMachineFileName
        {
            get
            {
                return _stateMachineFileName;
            }
            set
            {
                if (value != null)
                {
                    if (_stateMachineFileName != value)
                    {
                        _stateMachineFileName = value;
                        _constantStateMachine = null;
                        _OnPropertyChanged("StateMachineFileName");
                    }
                }
                else
                    throw new ArgumentNullException("SourceFileName");
            }
        }

        public string Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                if (value != null)
                {
                    if (_sequence != value)
                    {
                        _sequence = value;
                        _OnPropertyChanged("Sequence");
                    }
                }
                else
                    throw new ArgumentNullException("Sequence");
            }
        }

        public string LongestPrefix
        {
            get
            {
                return _longestPrefix;
            }
            set
            {
                if (value != null)
                {
                    if (_longestPrefix != value)
                    {
                        _longestPrefix = value;
                        System.Diagnostics.Debug.WriteLine("asdasdasd");
                        _OnPropertyChanged("LongestPrefix");
                    }
                }
                else
                    throw new ArgumentNullException("LongestPrefix");
            }
        }

        public string DefinitionTitle
        {
            get
            {
                return _definitionTitle;
            }
            set
            {
                if (value != null)
                {
                    if (_definitionTitle != value)
                    {
                        _definitionTitle = value;
                        _OnPropertyChanged("DefinitionTitle");
                    }
                }
                else
                    throw new ArgumentNullException("DefinitionTitle");
            }
        }

        public string Definition
        {
            get
            {
                return _definition;
            }
            set
            {
                if (value != null)
                {
                    if (_definition != value)
                    {
                        _definition = value;
                        _OnPropertyChanged("Definition");
                    }
                }
                else
                    throw new ArgumentNullException("Definition");
            }
        }

        public State<Char> InitialState
        {
            get
            {
                return _ConstantStateMachine.InitialState;
            }
        }

        public IReadOnlyList<State<char>> AllStates
        {
            get
            {
                return _ConstantStateMachine.States;
            }
        }

        public IReadOnlyList<State<char>> FinalStates
        {
            get
            {
                return new ReadOnlyCollection<State<char>>(_ConstantStateMachine.States.Where(state => state.IsFinalState).ToList());
            }
        }

        public IReadOnlyList<Transition<char>> Transitions
        {
            get
            {
                return _ConstantStateMachine.Transitions;
            }
        }

        public IReadOnlyList<char> Alphabet
        {
            get
            {
                return _ConstantStateMachine.Alphabet;
            }
        }

        public ICommand ValidateCommand
        {
            get
            {
                return _validateCommand;
            }
        }

        public ICommand GetLongestPrefixCommand
        {
            get
            {
                return _getLongestPrefixCommand;
            }
        }

        public ICommand PostAlphabetCommand
        {
            get
            {
                return _postAlphabetCommand;
            }
        }

        public ICommand PostAllStatesCommand
        {
            get
            {
                return _postAllStatesCommand;
            }
        }

        public ICommand PostFinalStatesCommand
        {
            get
            {
                return _postFinalStatesCommand;
            }
        }

        public ICommand PostTransitionsCommand
        {
            get
            {
                return _postTransitionsCommand;
            }
        }

        public bool IsSequenceValid
        {
            get
            {
                return _isSequenceValid;
            }
            set
            {
                if (value != _isSequenceValid)
                {
                    _isSequenceValid = value;
                    _OnPropertyChanged("IsSequenceValid");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void _OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            else
                throw new ArgumentNullException("propertyName");
        }

        private ConstantStateMachine _ConstantStateMachine
        {
            get
            {
                if (_constantStateMachine == null)
                    _constantStateMachine = _stateMachineRepository.Get(_stateMachineFileName);
                return _constantStateMachine;
            }
        }

        private bool _isSequenceValid = false;
        private string _sequence = string.Empty;
        private string _longestPrefix = string.Empty;
        private string _stateMachineFileName = "default.xml";
        private string _definitionTitle;
        private string _definition;
        private ConstantStateMachine _constantStateMachine = null;
        private readonly IConstantStateMachineRepository _stateMachineRepository = new XmlConstantStateMachineRepository();
        private readonly ICommand _validateCommand;
        private readonly ICommand _getLongestPrefixCommand;
        private readonly ICommand _postAlphabetCommand;
        private readonly ICommand _postAllStatesCommand;
        private readonly ICommand _postFinalStatesCommand;
        private readonly ICommand _postTransitionsCommand;
    }
}
