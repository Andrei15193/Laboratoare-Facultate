using System;
using System.Windows.Forms;
namespace L3
{
	public partial class Form1
		: Form
	{
		public Form1()
		{
			InitializeComponent();

			_setElementNumericUpDown.Minimum = int.MinValue;
			_setElementNumericUpDown.Maximum = int.MaxValue;

			foreach (object setOperationValue in Enum.GetValues(typeof(SetOperation)))
				_setOperationComboBox.Items.Add(setOperationValue);
			_setOperationComboBox.SelectedIndex = 0;

			_currentSetTextBox.Text = _currentSet.ToString();
		}

		private void _SetOperationExecuteButtonClick(object sender, EventArgs e)
		{
			Func<Set, Set, Set> operation;

			switch ((SetOperation)_setOperationComboBox.Items[_setOperationComboBox.SelectedIndex])
			{
				case SetOperation.Reuniune:
					operation = (first, second) => (first + second);
					break;
				case SetOperation.Diferenta:
					operation = (first, second) => (first - second);
					break;
				case SetOperation.Intersectie:
				default:
					operation = (first, second) => (first * second);
					break;
			}

			_setOperationResultTextBox.Text = operation((Set)_setsListBox1.SelectedItem, (Set)_setsListBox2.SelectedItem).ToString();
		}
		private void _AddSetButtonClick(object sender, EventArgs e)
		{
			_setsListBox1.Items.Add(_currentSet);
			_setsListBox2.Items.Add(_currentSet);
			_currentSet = new Set();
			_currentSetTextBox.Text = _currentSet.ToString();
		}
		private void _AddElementToSetButtonClick(object sender, EventArgs e)
		{
			_currentSet.Add((int)_setElementNumericUpDown.Value);
			_currentSetTextBox.Text = _currentSet.ToString();
		}
		private void _SetsListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			_setOperationExecuteButton.Enabled = _setOperationComboBox.Enabled = (_setsListBox1.SelectedIndex >= 0 && _setsListBox2.SelectedIndex >= 0);
		}

		private Set _currentSet = new Set();
	}
}