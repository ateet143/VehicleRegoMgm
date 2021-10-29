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

        List<string> vehicleRegos = new List<string>();
        string currentFile;

        private void DisplayRegoList()
        {
            ListBoxVehicle.Items.Clear();
            vehicleRegos.Sort();
            foreach (var rego in vehicleRegos)
            {
                ListBoxVehicle.Items.Add(rego);
            }
        }
        private void VehicleRegoMgm_Load(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Rainbow.bin");
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Rainbow.bin")))
            {
                vehicleRegos.Clear();
                BinaryToText(fileName,vehicleRegos);
                toolStripStatusLabel1.Text = fileName + " is opened";

            }
            else
            {   
                vehicleRegos.Add("1RTU-567");
                vehicleRegos.Add("1CDH-979");
                vehicleRegos.Add("1CDA-999");
                vehicleRegos.Add("1GHT-047");
                vehicleRegos.Add("1QWE-345");
                TexttoBinary(fileName, vehicleRegos);
                toolStripStatusLabel1.Text = fileName + " is Created";
            }
            DisplayRegoList();
            currentFile = fileName;

        }

        #region Validating Rego Plate
        private void TextBoxInput_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!ValidRego(TextBoxInput.Text, out errorMsg))
            {
                // Cancel the event  the text to be corrected by the user
                e.Cancel = true;
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
                    errorMessage = "";
                    return true;
                }
                errorMessage = "ERROR! Enter in the format: 1aaa-111";
                return false;

            }
            errorMessage = "Type Vehicle Plate Number";
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
                toolStripStatusLabel1.Text = "ERROR! Type Valid Vehicle Plate Number";
            }

        }

        private void ButtonBinarySearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                vehicleRegos.Sort();
                if (vehicleRegos.BinarySearch(TextBoxInput.Text.ToUpper()) >= 0)
                    toolStripStatusLabel1.Text = "Vehicle is Parked";
                else
                    toolStripStatusLabel1.Text = "Vehicle not Parked";
                TextBoxInput.Clear();
            }
            else
                toolStripStatusLabel1.Text = "Type the vehicle plate";
        }


        private void ButtonLinearSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                bool found = false;
                for (int i = 0; i < vehicleRegos.Count; i++)
                {
                    if (TextBoxInput.Text.ToUpper().Equals(vehicleRegos[i]))
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                    toolStripStatusLabel1.Text = "Vehicle is Parked";
                else
                    toolStripStatusLabel1.Text = "Vehicle not Parked";
            }
            else
            {
                MessageBox.Show("Type the vehicle plate");
            }
            TextBoxInput.Clear();


        }

        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if (ListBoxVehicle.SelectedIndex != -1 || !string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                if (ListBoxVehicle.SelectedIndex != -1)
                {
                    ListBoxVehicle.SetSelected(ListBoxVehicle.SelectedIndex, true);
                    vehicleRegos.RemoveAt(ListBoxVehicle.SelectedIndex);
                    toolStripStatusLabel1.Text = "Plate Removed";
                    DisplayRegoList();
                }

                if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
                {

                    int a = vehicleRegos.IndexOf(TextBoxInput.Text.ToUpper());
                    if (a >= 0)
                    {
                        vehicleRegos.RemoveAt(a);
                        toolStripStatusLabel1.Text = "Plate Removed";
                        DisplayRegoList();
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Plate Not Found";
                    }

                }
            }
            else
                toolStripStatusLabel1.Text = "SELECT or TYPE Plate Number";






        }

        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (ListBoxVehicle.SelectedIndex != -1 && !string.IsNullOrEmpty(TextBoxInput.Text))
            {

                if (!vehicleRegos.Contains(TextBoxInput.Text.ToUpper()))
                {
                    vehicleRegos[ListBoxVehicle.SelectedIndex] = TextBoxInput.Text.ToUpper();
                    TextBoxInput.Clear();
                    DisplayRegoList();
                }
                else
                {
                    toolStripStatusLabel1.Text = "Vehicle already exist";
                    TextBoxInput.Clear();
                }

            }
            else
            {
                toolStripStatusLabel1.Text = "Select Plate Number from List and  Type to Edit";

            }
        }

        private void ButtonTag_Click(object sender, EventArgs e)
        {
            if (ListBoxVehicle.SelectedIndex != -1)
            {
                if (ListBoxVehicle.SelectedItem.ToString().IndexOf('Z') != 0)
                {
                    ListBoxVehicle.SetSelected(ListBoxVehicle.SelectedIndex, true);
                    vehicleRegos[ListBoxVehicle.SelectedIndex] = "Z" + ListBoxVehicle.SelectedItem.ToString();
                    toolStripStatusLabel1.Text = "Plate number TAGGED";
                    TextBoxInput.Clear();
                    DisplayRegoList();
                }
                else
                {
                    toolStripStatusLabel1.Text = "Already Tagged";
                }

            }
            else
            {
                MessageBox.Show("Vehicle plate not selected");

            }
        }

        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Rainbow.bin");
            OpenFileDialog OpenBinary = new OpenFileDialog();
            OpenBinary.FileName = "Rainbow.bin";  //set the default filename as Rainbow.bin 
            DialogResult sr = OpenBinary.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = OpenBinary.FileName;
                currentFile = fileName;
                toolStripStatusLabel1.Text = fileName + " is opened.";
            }
                vehicleRegos.Clear();
                vehicleRegos = BinaryToText(fileName,vehicleRegos);
                DisplayRegoList();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Rainbow.bin");
            SaveFileDialog saveBinary = new SaveFileDialog();
            saveBinary.InitialDirectory = fileName;
            saveBinary.Filter = "binary files (*.bin)|*.bin|All files (*.*)|*.*";  //setting so that user can save as binary files
            saveBinary.DefaultExt = "bin";  //save the file as bin extension if all files is choosed.
            saveBinary.FileName = "Rainbow.bin";  //set default filename
            DialogResult sr = saveBinary.ShowDialog();
            if (sr == DialogResult.Cancel)
            {
                saveBinary.FileName = fileName;
                currentFile = fileName;

            }
            if (sr == DialogResult.OK)
            {
                fileName = saveBinary.FileName;
                currentFile = fileName;
                toolStripStatusLabel1.Text = "File saved as: " + fileName;
            }
            TexttoBinary(fileName, vehicleRegos);
           
        }

        private void VehicleRegoMgm_FormClosing(object sender, FormClosingEventArgs e)
        {

            List<string> currentList = new List<string>();
            currentList = BinaryToText(currentFile, currentList);
            if (!compareList(vehicleRegos, currentList))
            {
                if (MessageBox.Show("Do you want to save changes to your File?", "Vehicle Management Application",
                          MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    // Cancel the Closing event from closing the form.
                    e.Cancel = true;
                    // Call method to save file...
                }
            }
        }
        public bool compareList(List<string> a, List<string> b)
        {
            a.Sort();
            b.Sort();
            if (a.Count != b.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Count; i++)
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;

                }
            }

            return true;
        }

        public List<string> BinaryToText(string fileName, List<string> tempList)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    while (stream.Position < stream.Length)
                    {
                        tempList.Add((string)binaryFormatter.Deserialize(stream));

                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("cannot open file");
            }

            return tempList;
        }
        public void TexttoBinary(string fileName, List<string> tempList)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Create))
                {
                    BinaryFormatter binFormatter = new BinaryFormatter();
                    foreach (var item in tempList)
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
