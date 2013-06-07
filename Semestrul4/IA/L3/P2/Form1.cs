using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IALab302
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            expert = GetSystemExpert();
            foreach (Conclusion conclusion in expert.Conclusions)
                conclusionsComboBox.Items.Add(conclusion);
            conclusionsComboBox.SelectedIndex = 0;
        }

        private void getEfficiencyButton_Click(object sender, EventArgs e)
        {
            IEnumerable<Fact> factsToAnswer = expert.Facts.Where((fact) => fact.IsValid == null);
            while (!expert.HasConclusion && factsToAnswer.Count() > 0)
            {
                Fact factToAnswer = factsToAnswer.First();
                if (new Form2(factToAnswer.Text).ShowDialog() == DialogResult.OK)
                    factToAnswer.IsValid = true;
                else
                    factToAnswer.IsValid = false;
            }
            if (expert.HasConclusion)
                resultTextBox.Text = string.Format("Urmatoarea concluzie este valida: {0}", expert.Conclusion.Text);
            else
                resultTextBox.Text = "Nu s-au putut trage concluzii de pe urma raspunsurilor!";
            expert.Reset();
        }

        private void validateEfficiencyButton_Click(object sender, EventArgs e)
        {
            bool canContinue = false;
            Form2 factToValidate;
            Conclusion conclusionToValidate = conclusionsComboBox.SelectedItem as Conclusion;
            IEnumerator<Rule> ruleEnumerator = conclusionToValidate.Rules.GetEnumerator();
            if (ruleEnumerator.MoveNext())
                do
                {
                    IEnumerable<Fact> facts = expert.GetFacts(ruleEnumerator.Current);
                    factToValidate = new Form2(string.Join("\r\n", facts.Select((fact) => fact.Text)), true);
                    canContinue = (factToValidate.ShowDialog() != DialogResult.OK);
                } while (canContinue && ruleEnumerator.MoveNext());
            if (!canContinue)
                resultTextBox.Text = "Concluzia este valida!";
            else
                resultTextBox.Text = "Concluzia nu este valida deoarece faptele necesare pentru aceasta nu sunt valide!";
        }

        private SystemExpert expert;

        private static SystemExpert GetSystemExpert()
        {
            Conclusion furnalCuEficientaMare = new Conclusion("Furnal este utilizat cu o efictienta mare.");
            Conclusion furnalCuEficientaMedie = new Conclusion("Furnal este utilizat cu o efictienta medie.");
            Conclusion furnalCuEficientaMica = new Conclusion("Furnal este utilizat cu o efictienta mica.");
            Fact temperaturaMaiMicaDecat60 = new Fact("Temeperatura este mai mica decat 60 de grade.");
            Fact temperaturaIntre60Si80 = new Fact("Temperatura este intre 60 si 80 de grade.");
            Fact temperaturaMaiMareDecat80 = new Fact("Temperatura este mai mare decat 80 de grade.");
            Fact numarDeUnitatiMaiMicDecat5 = new Fact("Numarul unitatilor de minereu din furnal este mai mic decat 5.");
            Fact numarDeUnitatiMaiMareDecat5 = new Fact("Numarul unitatilor de minereu din furnal este mai mare decat 5.");
            Fact furnalulFunctioneaza = new Fact("Furnalul este pus in functiune.");
            Rule regula1 = new Rule();
            regula1.Add(furnalCuEficientaMedie);
            regula1.Add(furnalulFunctioneaza);
            regula1.Add(numarDeUnitatiMaiMicDecat5);
            regula1.Add(temperaturaMaiMicaDecat60);

            Rule regula2 = new Rule();
            regula2.Add(furnalCuEficientaMare);
            regula2.Add(furnalulFunctioneaza);
            regula2.Add(temperaturaMaiMicaDecat60);
            regula2.Add(numarDeUnitatiMaiMareDecat5);

            Rule regula3 = new Rule();
            regula3.Add(furnalCuEficientaMare);
            regula3.Add(furnalulFunctioneaza);
            regula3.Add(temperaturaMaiMareDecat80);

            Rule regula4 = new Rule();
            regula4.Add(furnalCuEficientaMare);
            regula4.Add(furnalulFunctioneaza);
            regula4.Add(temperaturaIntre60Si80);

            Rule regula5 = new Rule();
            regula5.Add(furnalCuEficientaMica);
            regula5.Add(furnalulFunctioneaza);
            regula5.Add(temperaturaIntre60Si80);

            SystemExpert expertSystem = new SystemExpert(new Conclusion[] { furnalCuEficientaMare, furnalCuEficientaMedie, furnalCuEficientaMica });
            return expertSystem;
        }
    }
}
