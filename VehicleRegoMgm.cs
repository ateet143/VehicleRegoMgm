using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
//Created by Atit

namespace VehicleRegoMgm
{
    public partial class VehicleRegoMgm : Form
    {
        public VehicleRegoMgm()
        {
            InitializeComponent();
        }

        List<string> vehicleRegos = new List<string>() 
        {"1CDH-090","1GHT-047","1ABC-101","1DGR-348","1FHR-898"};
        
        private void DisplayRegoList()
        {
            ListBoxVehicle.Items.Clear();
            vehicleRegos.Sort();
            foreach(var rego in vehicleRegos)
            {
                ListBoxVehicle.Items.Add(rego);
            }
        }
        private void VehicleRegoMgm_Load(object sender, EventArgs e)
        {
            DisplayRegoList();
           
        }

        #region Validating Rego Plate
        private void TextBoxInput_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidRego(TextBoxInput.Text, out errorMsg))
            {
                // Cancel the event  the text to be corrected by the user
                e.Cancel = true;
                //TextBoxInput.Select(0, TextBoxInput.Text.Length);
                TextBoxInput.Clear();
                MessageBox.Show(errorMsg);
            }
        }
        public bool ValidRego(string textBoxInput, out string errorMessage)
        {
            if (!string.IsNullOrWhiteSpace(textBoxInput))
            {
                if (Regex.Match(textBoxInput, @"^[1-9][a-zA-Z]{3}[-][0-9]{3}$").Success)
                {
                    errorMessage = "good";
                    return true;
                }
                errorMessage = "Vehicle plate should be in Format eg.0aaa-000";
                return false;

            }
            errorMessage = "Please input vehicle plate number";
            return true;
        }
        
#endregion
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            vehicleRegos.Clear();
            DisplayRegoList();
        }

        private void ButtonEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                if (vehicleRegos.Contains(TextBoxInput.Text.ToUpper()))
                {
                    MessageBox.Show("Duplicate entry");
                }
                else
                {
                    vehicleRegos.Add(TextBoxInput.Text.ToUpper());
                    DisplayRegoList();
                }
                
            }
            else
            {
                MessageBox.Show("Input plate number");
            }
           
        }

        private void ButtonBinarySearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                vehicleRegos.Sort();
                if (vehicleRegos.BinarySearch(TextBoxInput.Text) >= 0)
                    MessageBox.Show("Found");
                else
                    MessageBox.Show("Not Found");
                TextBoxInput.Clear();
            }
            else
                MessageBox.Show("Type the vehicle plate");
        }
             

        private void ButtonLinearSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                bool found = false;
                for (int i = 0; i < vehicleRegos.Count; i++)
                {
                    if (TextBoxInput.Text.Equals(vehicleRegos[i]))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    MessageBox.Show("Found");
                else
                    MessageBox.Show("Not Found");
            }
            else
            {
                MessageBox.Show("Type the vehicle plate");
            }
            TextBoxInput.Clear();
           
           
        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if(ListBoxVehicle.SelectedIndex != -1)
            {
                ListBoxVehicle.SetSelected(ListBoxVehicle.SelectedIndex, true);
                vehicleRegos.RemoveAt(ListBoxVehicle.SelectedIndex);
                DisplayRegoList();
            }
            else
            {
                MessageBox.Show("Vehicle plate not selected");
                
            }
        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (ListBoxVehicle.SelectedIndex != -1)
            {
             
                if (!vehicleRegos.Contains(TextBoxInput.Text.ToUpper()))
                {
                    vehicleRegos[ListBoxVehicle.SelectedIndex] = TextBoxInput.Text.ToUpper();
                    TextBoxInput.Clear();
                    DisplayRegoList();
                }
                else
                {
                    MessageBox.Show("Vehicle already exist");
                    TextBoxInput.Clear();
                }
              
            }
            else
            {
                MessageBox.Show("Vehicle plate not selected");

            }
        }

        private void ButtonTag_Click(object sender, EventArgs e)
        {
            if (ListBoxVehicle.SelectedIndex != -1)
            {
                ListBoxVehicle.SetSelected(ListBoxVehicle.SelectedIndex, true);
                vehicleRegos[ListBoxVehicle.SelectedIndex] = "Z" + ListBoxVehicle.SelectedItem.ToString();
                TextBoxInput.Clear();
                DisplayRegoList();
            }
            else
            {
                MessageBox.Show("Vehicle plate not selected");

            }
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            string fileName = "Rainbow.bin";
            OpenFileDialog OpenBinary = new OpenFileDialog();
            DialogResult sr = OpenBinary.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = OpenBinary.FileName;
            }
            try
            {
                vehicleRegos.Clear();
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    while (stream.Position < stream.Length)
                    {
                        vehicleRegos.Add((string)binaryFormatter.Deserialize(stream));
                    }
                }
                DisplayRegoList();
            }
            catch (IOException)
            {
                MessageBox.Show("cannot open file");
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            string fileName = "Rainbow.bin";
            SaveFileDialog saveBinary = new SaveFileDialog();
            DialogResult sr = saveBinary.ShowDialog();
            if (sr == DialogResult.Cancel)
            {
                saveBinary.FileName = fileName;
            }
            if (sr == DialogResult.OK)
            {
                fileName = saveBinary.FileName;
            }
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    BinaryFormatter binFormatter = new BinaryFormatter();
                    foreach (var item in vehicleRegos)
                    {
                        binFormatter.Serialize(stream, item);
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("cannot save file");
            }
        }
    }
}
