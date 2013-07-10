using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IALab406
{
    public class RobotChromosome : IChromosome<RobotChromosome>
    {
        public RobotChromosome(int numberOfInputs, int numberOfExtraValues, params double[] leafValues)
        {
            _numberOfInputs = numberOfInputs;
            _extraValues = new double[numberOfExtraValues];
            for (int i = 0; i < leafValues.Length && i < numberOfExtraValues; i++)
                _extraValues[i] = leafValues[i];
            for (int i = leafValues.Length; i < numberOfExtraValues; i++)
                _extraValues[i] = Globals.Instance.Random.NextDouble();
            _function = _GenerateFunction(Operand.Variable, numberOfInputs);
            _Normalize();
        }

        public RobotChromosome(int numberOfInputs)
            : this(numberOfInputs, 0)
        {
        }

        public RobotChromosome(int numberOfInputs, params double[] leafValues)
            : this(numberOfInputs, leafValues.Length, leafValues)
        {
        }

        public RobotChromosome Mutate()
        {
            Random random = Globals.Instance.Random;
            int selectedIndex = random.Next(_function.Count);
            object[] function = _function.ToArray();
            object gene = function[selectedIndex];
            if (gene is Tuple<Operand, int>)
            {
                Tuple<Operand, int> operand = gene as Tuple<Operand, int>;
                if (operand.Item1 == Operand.Variable)
                {
                    function[selectedIndex] = new Tuple<Operand, int>(Operand.Variable, random.Next(_numberOfInputs));
                    return new RobotChromosome()
                    {
                        _numberOfInputs = this._numberOfInputs,
                        _extraValues = this._extraValues,
                        _function = new LinkedList<object>(function)
                    }._Normalize();
                }
                else
                {
                    object last = function[function.Length - 1];
                    function[selectedIndex] = new Tuple<Operand, int>(Operand.Constant, random.Next(_extraValues.Length));
                    function[function.Length - 1] = _GetBinary((Operation)random.Next(4));
                    return new RobotChromosome()
                    {
                        _numberOfInputs = this._numberOfInputs,
                        _extraValues = this._extraValues,
                        _function = new LinkedList<object>(function.Concat(new object[] { gene, last }))
                    }._Normalize();
                }
            }
            else
                if (gene is Tuple<Operand, Func<double, double, double>>)
                {
                    function[selectedIndex] = _GetBinary(Operation.Addition);
                    return new RobotChromosome()
                    {
                        _numberOfInputs = this._numberOfInputs,
                        _extraValues = this._extraValues,
                        _function = new LinkedList<object>(function)
                    }._Normalize();
                }
                else
                    return new RobotChromosome()
                    {
                        _numberOfInputs = this._numberOfInputs,
                        _extraValues = this._extraValues,
                        _function = new LinkedList<object>(function)
                    }._Normalize();
            //else
            //    else
            //    {
            //        function[selectedIndex] = _GetUnary((Operation)(random.Next(2) + 4));
            //        return new RobotChromosome()
            //        {
            //            _numberOfInputs = this._numberOfInputs,
            //            _extraValues = this._extraValues,
            //            _function = new LinkedList<object>(function)
            //        };
            //    }
        }

        public RobotChromosome CrossOver(RobotChromosome chromosome)
        {
            Random random = Globals.Instance.Random;
            int startIndex = random.Next(_function.Count);
            var personalSubExpression = _GetSubExpression(startIndex);
            var partnerSubExpression = _GetSubExpression(random.Next(chromosome._function.Count));
            return new RobotChromosome()
            {
                _numberOfInputs = this._numberOfInputs,
                _extraValues = this._extraValues,
                _function = new LinkedList<object>(this._function.Take(startIndex).Concat(partnerSubExpression).Concat(_function.Skip(startIndex + personalSubExpression.Count())))
            }._Normalize();
        }

        public double Apply(double[] input)
        {
            if (_numberOfInputs >= input.Length)
            {
                bool previousOperand = false;
                Stack<object> operations = new Stack<object>();
                Stack<double> operands = new Stack<double>();
                foreach (var item in _function)
                    if (item is Tuple<Operation, Func<double, double, double>>)
                    {
                        operations.Push(item);
                        previousOperand = false;
                    }
                    else
                    {
                        if (item is Tuple<Operation, Func<double, double>>)
                            operations.Push(item);
                        else
                        {
                            Tuple<Operand, int> operand = item as Tuple<Operand, int>;
                            double result;
                            if (operand.Item1 == Operand.Constant)
                                result = _extraValues[operand.Item2];
                            else
                                result = input[operand.Item2];
                            if (previousOperand)
                            {
                                while (operations.Count > 0 && operations.Peek() is Tuple<Operation, Func<double, double>>)
                                    result = (operations.Pop() as Tuple<Operation, Func<double, double>>).Item2(result);
                                while (operations.Count > 0 && operands.Count > 0)
                                    if (operations.Peek() is Tuple<Operation, Func<double, double, double>>)
                                        result = (operations.Pop() as Tuple<Operation, Func<double, double, double>>).Item2(operands.Pop(), result);
                                    else
                                        result = (operations.Pop() as Tuple<Operation, Func<double, double>>).Item2(result);
                                while (operations.Count > 0 && operations.Peek() is Tuple<Operation, Func<double, double>>)
                                    result = (operations.Pop() as Tuple<Operation, Func<double, double>>).Item2(result);
                            }
                            operands.Push(result);
                        }
                        previousOperand = true;
                    }
                return operands.Pop();
            }
            else
                throw new InvalidOperationException("The provided input is not sufficient to apply the storred function.");
        }

        public override string ToString()
        {
            return ToString(true);
        }

        public string ToString(bool isInfixed)
        {
            bool isPreviousUnary = false;
            StringBuilder representationBuilder = new StringBuilder();
            IEnumerable<object> function = _function;
            if (isInfixed)
                function = _ToInfixedRepresentation(_function);
            foreach (object item in function)
                if (item is Tuple<Operation, Func<double, double>>)
                {
                    if (isPreviousUnary)
                        representationBuilder.Append(' ');
                    isPreviousUnary = true;
                    representationBuilder.Append(_ToOperator((item as Tuple<Operation, Func<double, double>>).Item1));
                }
                else
                {
                    isPreviousUnary = false;
                    if (item is Tuple<Operation, Func<double, double, double>>)
                        representationBuilder.Append(_ToOperator((item as Tuple<Operation, Func<double, double, double>>).Item1));
                    else
                    {
                        Tuple<Operand, int> input = item as Tuple<Operand, int>;
                        switch (input.Item1)
                        {
                            case Operand.Constant:
                                representationBuilder.AppendFormat(" {0} ", _extraValues[input.Item2].ToString());
                                break;
                            case Operand.Paranthesis:
                                representationBuilder.Append(_paranthesis[input.Item2]);
                                break;
                            default:
                                representationBuilder.AppendFormat(" x{0} ", input.Item2.ToString());
                                break;
                        }
                    }
                }
            return representationBuilder.ToString();
        }

        private RobotChromosome()
        {
        }

        private string _ToOperator(Operation operation)
        {
            switch (operation)
            {
                case Operation.Addition:
                    return "+";
                case Operation.Substraction:
                    return "-";
                case Operation.Multiplication:
                    return "*";
                case Operation.Division:
                    return "/";
                case Operation.Sine:
                    return "Sin";
                case Operation.Cosine:
                    return "Cos";
                default:
                    return "<unknown_operation>";
            }
        }

        private RobotChromosome _Normalize()
        {
            Random random = Globals.Instance.Random;
            bool[] membership = new bool[_extraValues.Length];
            for (int i = 0; i < membership.Length; i++)
                membership[i] = false;
            foreach (var operand in _function.OfType<Tuple<Operand, int>>().Where((operand) => operand.Item1 == Operand.Constant))
                membership[operand.Item2] = true;
            int nonMembers = membership.Count((isMembership) => !isMembership);
            if (nonMembers > 0)
            {
                int index = -1;
                object last = _function.Last.Value;
                _function.Last.Value = _GetBinary((Operation)random.Next(4));
                for (int i = 1; i < nonMembers; i++)
                {
                    do index++; while (membership[index]);
                    _function.AddLast(new Tuple<Operand, int>(Operand.Constant, index));
                    _function.AddLast(_GetBinary((Operation)random.Next(4)));
                }
                do index++; while (membership[index]);
                _function.AddLast(new Tuple<Operand, int>(Operand.Constant, index));
                _function.AddLast(last);
            }
            return this;
        }

        private IEnumerable<object> _ToInfixedRepresentation(IEnumerable<object> preFixed)
        {
            int operands = 0;
            bool previousOperand = false;
            Stack<object> operations = new Stack<object>();
            foreach (var item in preFixed)
                if (item is Tuple<Operation, Func<double, double, double>>)
                {
                    yield return new Tuple<Operand, int>(Operand.Paranthesis, 0);
                    operations.Push(item);
                    previousOperand = false;
                }
                else
                {
                    if (item is Tuple<Operation, Func<double, double>>)
                        yield return item;
                    else
                    {
                        yield return item;
                        if (previousOperand)
                        {
                            for (int i = 0; i < operands; i++)
                                yield return new Tuple<Operand, int>(Operand.Paranthesis, 1);
                            operands = 1;
                        }
                        else
                            operands++;
                        if (operations.Count > 0)
                            yield return operations.Pop();
                    }
                    previousOperand = true;
                }
        }

        private IEnumerable<object> _GetSubExpression(int position)
        {
            int inputNeeded = 1, inputProvided = 0;
            var remainderEnumerator = _function.Skip(position).GetEnumerator();
            LinkedList<object> subExpression = new LinkedList<object>();
            while (remainderEnumerator.MoveNext() && inputNeeded != inputProvided)
            {
                if (remainderEnumerator.Current is Tuple<Operand, int>)
                    inputProvided++;
                else
                    if (remainderEnumerator.Current is Tuple<Operation, Func<double, double, double>>)
                        inputNeeded++;
                subExpression.AddLast(remainderEnumerator.Current);
            }
            return subExpression;
        }

        private LinkedList<object> _GenerateFunction(Operand operandType, int inputsToSet)
        {
            Random random = Globals.Instance.Random;
            LinkedList<object> function = new LinkedList<object>();
            int left = 0;
            int totalInputs = inputsToSet;
            int numberOfFunctions = Enum.GetValues((typeof(Operation))).Length;
            int offset = random.Next(totalInputs);
            left = 0;
            while (totalInputs > left)
            {
                Operation operation = (Operation)random.Next(numberOfFunctions);
                var unaryFunction = _GetUnary(operation);
                if (unaryFunction == null)
                {
                    if (left == 0)
                        left = 2;
                    else
                        left++;
                    function.AddLast(_GetBinary(operation));
                }
                else
                    function.AddLast(unaryFunction);
            }
            for (int i = 0; i < totalInputs; i++)
                function.AddLast(new Tuple<Operand, int>(operandType, (i + offset) % totalInputs));
            for (int i = totalInputs; i < left; i++)
                function.AddLast(new Tuple<Operand, int>(operandType, random.Next(totalInputs)));
            return function;
        }

        private Tuple<Operation, Func<double, double, double>> _GetBinary(Operation operation)
        {
            switch (operation)
            {
                case Operation.Addition:
                    return new Tuple<Operation, Func<double, double, double>>(operation, _Addition);
                case Operation.Substraction:
                    return new Tuple<Operation, Func<double, double, double>>(operation, _Substraction);
                case Operation.Multiplication:
                    return new Tuple<Operation, Func<double, double, double>>(operation, _Multiplication);
                case Operation.Division:
                    return new Tuple<Operation, Func<double, double, double>>(operation, _Division);
                default:
                    return null;
            }
        }

        private Tuple<Operation, Func<double, double>> _GetUnary(Operation operation)
        {
            switch (operation)
            {
                case Operation.Sine:
                    return new Tuple<Operation, Func<double, double>>(operation, _Sine);
                case Operation.Cosine:
                    return new Tuple<Operation, Func<double, double>>(operation, _Cosine);
                default:
                    return null;
            }
        }

        private double _Addition(double arg1, double arg2)
        {
            return arg1 + arg2;
        }

        private double _Substraction(double arg1, double arg2)
        {
            return arg1 - arg2;
        }

        private double _Multiplication(double arg1, double arg2)
        {
            return arg1 * arg2;
        }

        private double _Division(double arg1, double arg2)
        {
            if (arg2 != 0)
                return arg1 / arg2;
            else
                return 0;
        }

        private double _Sine(double arg)
        {
            return Math.Sin(arg);
        }

        private double _Cosine(double arg)
        {
            return Math.Cos(arg);
        }

        private double[] _extraValues;
        private LinkedList<object> _function;
        private int _numberOfInputs;
        private static readonly char[] _paranthesis = new char[] { '(', ')' };

        private enum Operation
        {
            Addition,
            Substraction,
            Multiplication,
            Division,
            Sine,
            Cosine
        }

        private enum Operand
        {
            Constant,
            Variable,
            Paranthesis
        }
    }
}
