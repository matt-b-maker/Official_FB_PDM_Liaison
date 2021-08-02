using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FishBowl_PDM_BOM_Import_Addin_Official_
{
    public partial class LaminateForm : Form
    {
        int lamCount = 1;
        Point lowestMenu = new Point();
        Point lowestValue = new Point();
        public List<Laminate> laminates = new List<Laminate>();

        public LaminateForm()
        {
            InitializeComponent();
            lowestMenu = new Point(LamBox1.Location.X, LamBox1.Location.Y);
            lowestValue = new Point(ValBox1.Location.X, ValBox1.Location.Y);
        }

        private void LaminateValidateButton_Click(object sender, EventArgs e)
        {
            var comboBoxes = Controls.OfType<ComboBox>();

            foreach (var box in comboBoxes)
            {
                laminates.Add(new Laminate(box.SelectedItem.ToString()));
            }

            var lamCountBoxes = Controls.OfType<NumericUpDown>();

            for (int i = 0; i < lamCountBoxes.Count<NumericUpDown>(); i++)
            {
                laminates[i].Quantity = int.Parse(lamCountBoxes.ElementAt<NumericUpDown>(i).Value.ToString());
            }
        }

        private void AddLam_Click(object sender, EventArgs e)
        {
            var comboBoxes = Controls.OfType<ComboBox>();
            var lamCountBoxes = Controls.OfType<NumericUpDown>();

            bool allFilledOut = true;

            foreach (var box in comboBoxes)
            {
                if (box.SelectedIndex == -1)
                {
                    allFilledOut = false;
                    break;
                } 
            }

            foreach (var countBox in lamCountBoxes)
            {
                if (countBox.Value == 0)
                {
                    allFilledOut = false;
                }
            }

            if (lamCount <= 5 && allFilledOut)
            {
                lamCount++;

                ComboBox cmbbx = new ComboBox();
                cmbbx.Name = "LamBox" + lamCount;
                cmbbx.Width = 144;
                cmbbx.Height = 23;
                cmbbx.Location = new Point(lowestMenu.X, lowestMenu.Y + 33);

                for (int i = 0; i < LamBox1.Items.Count; i++)
                {
                    cmbbx.Items.Add(LamBox1.Items[i]);
                }

                lowestMenu.X = cmbbx.Location.X;
                lowestMenu.Y = cmbbx.Location.Y;
                cmbbx.Visible = true;
                cmbbx.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbbx.SelectedIndexChanged += (sender, e) => { CheckForDuplicates(cmbbx); };
                this.Controls.Add(cmbbx);

                NumericUpDown upDown = new NumericUpDown();
                upDown.Name = "ValBox" + lamCount;
                upDown.Width = 45;
                upDown.Height = 23;
                upDown.Location = new Point(lowestValue.X, lowestValue.Y + 33);
                lowestValue.X = upDown.Location.X;
                lowestValue.Y = upDown.Location.Y;
                upDown.Visible = true;
                this.Controls.Add(upDown);

                AddLam.Location = new Point(AddLam.Location.X, AddLam.Location.Y + 33);
                LaminateValidateButton.Location = new Point(LaminateValidateButton.Location.X, LaminateValidateButton.Location.Y + 33);
            }
            else if (lamCount > 5)
            {
                MessageBox.Show("You probably don't need to add more laminate to this Lucky Putt. Double check your references.");
            }
            else
            {
                MessageBox.Show("Each line needs a laminate type and quantity to continue.");
            }
        }

        private void CheckForDuplicates(ComboBox sender)
        {
            int count = 0;

            var comboBoxes = Controls.OfType<ComboBox>();

            if (sender.SelectedIndex != -1)
            {
                foreach (var box in comboBoxes)
                {
                    if (box.SelectedItem.ToString() == sender.SelectedItem.ToString())
                    {
                        count++;
                        if (count == 2)
                        {
                            MessageBox.Show("This item has already been selected in another box. Select a new one.");
                            sender.SelectedIndex = -1;
                            break;
                        }
                    }
                }
            }
        }

        private void LaminateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var window = MessageBox.Show(
                "This is a Lucky Putt ramp and it's very likely that there needs to be laminate on the BOM. Are you sure you want to close this window?","You sure?",
                MessageBoxButtons.YesNo);

            e.Cancel = (window == DialogResult.No);
        }
    }
}
