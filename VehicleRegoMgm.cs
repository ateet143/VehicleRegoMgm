﻿using System;
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
using System.Diagnostics;
//Created by Atit
//Date: 02/11/2021
//Vehicle plate management system
//Version 1.4
//This application will record the vehicle entered and leave the building, have different features such as load and write file, add, modify , remove and search data 

namespace VehicleRegoMgm
{
    public partial class VehicleRegoMgm : Form
    {
        public VehicleRegoMgm()
        {
            InitializeComponent();
        }

        private void VehicleRegoMgm_Load(object sender, EventArgs e)
        {
            //Q4: Load/Open file into the list
            if (File.Exists(fileName))
            {
                vehicleRegos.Clear();
                BinaryToText(fileName, vehicleRegos);
                toolStripStatusLabel1.Text = fileName + " is opened";
            }
            else
            {
                TexttoBinary(fileName, vehicleRegos);
                toolStripStatusLabel1.Text = fileName + " is Created";
            }
            DisplayRegoList();
            currentFile = fileName;
        }

        #region GLOBAL VARIABLES
        //Q2: The global data structure List<> of string type
        List<string> vehicleRegos = new List<string>();
        //Global variable for the file
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Demo_00.bin");
        //Setting global variable as filename, can be used for saving or renaming file while saving
        string currentFile;
        #endregion

        #region Validating Rego Plate
        private void TextBoxInput_Validating(object sender, CancelEventArgs e)
        {
            if (!ValidRego(TextBoxInput.Text, out string errorMsg))
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

        #region ADD, MODIFY, REMOVE, TAG AND RESET
        private void DisplayRegoList()
        {
            ListBoxVehicle.Items.Clear();
            vehicleRegos.Sort();
            foreach (var rego in vehicleRegos)
            {
                ListBoxVehicle.Items.Add(rego);
            }
        }
        //Reset all the data in list with user confirmation.
        private void ButtonReset_Click(object sender, EventArgs e)
        {
            DialogResult resetTask = MessageBox.Show("Do you want to Continue?",
                "It will permanently delete the data.", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (resetTask == DialogResult.Yes)
            {
                vehicleRegos.Clear();
                toolStripStatusLabel1.Text = "All Data Cleared";
                DisplayRegoList();
            }
            else
            {
                toolStripStatusLabel1.Text = "User has cancelled the operation";
            }
        }

        //Q4: Enter new data with error trapping if null or whitespace
        private void ButtonEnter_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                if (vehicleRegos.Contains(TextBoxInput.Text.ToUpper()))
                {
                    MessageBox.Show("Duplicate entry");
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                }
                else
                {
                    vehicleRegos.Add(TextBoxInput.Text.ToUpper());
                    toolStripStatusLabel1.Text = "Vehicle Number Added";
                    vehicleRegos.Sort();
                    DisplayRegoList();
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "ERROR! Type Valid Vehicle Plate Number";
                TextBoxInput.Clear();
                TextBoxInput.Focus();
            }
        }

        //Q5: Leave data,data is removed with error trapping notification
        private void ButtonExit_Click(object sender, EventArgs e)
        {
            if (ListBoxVehicle.SelectedIndex != -1 || !string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                if (ListBoxVehicle.SelectedIndex != -1)
                {
                    ListBoxVehicle.SetSelected(ListBoxVehicle.SelectedIndex, true);
                    vehicleRegos.RemoveAt(ListBoxVehicle.SelectedIndex);
                    toolStripStatusLabel1.Text = "Plate Removed";
                    vehicleRegos.Sort();
                    DisplayRegoList();
                }

                if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
                {
                    int a = vehicleRegos.IndexOf(TextBoxInput.Text.ToUpper());
                    Trace.WriteLine("Index of number:" + a);
                    if (a >= 0)
                    {
                        vehicleRegos.RemoveAt(a);
                        toolStripStatusLabel1.Text = "Plate is Removed";
                        TextBoxInput.Clear();
                        TextBoxInput.Focus();
                        DisplayRegoList();

                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Plate Not Found";
                        TextBoxInput.Clear();
                        TextBoxInput.Focus();
                    }

                }
            }
            else
            {
                Trace.WriteLine("No double click or textboox content detected");
                toolStripStatusLabel1.Text = "SELECT or TYPE Plate Number";
                TextBoxInput.Clear();
                TextBoxInput.Focus();
            }
        }

        //Double click to remove the vehicle plate number from list
        private void ListBoxVehicle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                vehicleRegos.RemoveAt(ListBoxVehicle.SelectedIndex);
                TextBoxInput.Clear();
                toolStripStatusLabel1.Text = "Plate Removed";
                vehicleRegos.Sort();
                DisplayRegoList();
            }
            catch (Exception)
            {
                return;
            }

        }

        //Q6: Edit Single data item in listbox and updated, duplicate record must not created.
        private void ButtonEdit_Click(object sender, EventArgs e)
        {
            if (ListBoxVehicle.SelectedIndex != -1 && !string.IsNullOrEmpty(TextBoxInput.Text))
            {
                String temp = vehicleRegos[ListBoxVehicle.SelectedIndex];
                if (!vehicleRegos.Contains(TextBoxInput.Text.ToUpper()))
                {
                    vehicleRegos[ListBoxVehicle.SelectedIndex] = TextBoxInput.Text.ToUpper();
                    toolStripStatusLabel1.Text = temp + " EDITED TO: " + TextBoxInput.Text.ToUpper();
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                    DisplayRegoList();
                }
                else
                {
                    toolStripStatusLabel1.Text = "Vehicle already exist";
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Select Plate Number from List and  Type to Edit";
                TextBoxInput.Focus();
            }
        }

        //Q13: Tag single data item with prefix Z if already contains prefeix Z then notifies the user that its already tagged.
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
                Trace.WriteLine("Not selected anything from list");
                toolStripStatusLabel1.Text = "Vehicle plate not selected";
            }
        }

        #endregion

        #region SEARCH
        //Q10: Binary search method with error trapping
        private void ButtonBinarySearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(TextBoxInput.Text))
            {
                vehicleRegos.Sort();
                if (vehicleRegos.BinarySearch(TextBoxInput.Text.ToUpper()) >= 0)
                {
                    toolStripStatusLabel1.Text = "Vehicle is Parked";
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                }
                else
                {
                    toolStripStatusLabel1.Text = "Vehicle not Parked";
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Type the vehicle plate";
                TextBoxInput.Focus();
            }
        }

        //Q11: Linear search method with error trapping
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
                {
                    toolStripStatusLabel1.Text = "Vehicle is Parked";
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                }
                else
                {
                    toolStripStatusLabel1.Text = "Vehicle not Parked";
                    TextBoxInput.Clear();
                    TextBoxInput.Focus();
                }
            }
            else
            {
                toolStripStatusLabel1.Text = "Type the vehicle plate";
                TextBoxInput.Focus();
            }
        }
        #endregion

        #region FILE HANDLING
        //code will execute if open button is entered
        private void ButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog OpenBinary = new OpenFileDialog
            {
                InitialDirectory = fileName
            }; //set the default filename as Rainbow.bin 
            DialogResult sr = OpenBinary.ShowDialog();
            if (sr == DialogResult.OK)
            {
                fileName = OpenBinary.FileName;
                currentFile = fileName;
                toolStripStatusLabel1.Text = fileName + " is opened.";
                vehicleRegos.Clear();
                vehicleRegos = BinaryToText(fileName, vehicleRegos);
                DisplayRegoList();
            }
            else
            {
                return;  //will exit without doing anything
            }
        }

        //execute the code when user clicks the save button, if pressed cancel button, Rainbow(int).bin will be created in user document folder

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveBinary = new SaveFileDialog
            {
                InitialDirectory = fileName,
                Filter = "binary files (*.bin)|*.bin|All files (*.*)|*.*",  //setting so that user can save as binary files
                DefaultExt = "bin",  //save the file as bin extension if all files is choosed.
                FileName = Path.GetFileName(currentFile) //set filename in the SaveFileDialog // getFileName extract the name only without path.
            };
            DialogResult sr = saveBinary.ShowDialog();
            if (sr == DialogResult.Cancel)
            {
                fileName = NewFileName(currentFile); //auto increment the filename 
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

        public List<string> BinaryToText(string fileName, List<string> tempList)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    try  //Added error trap to tackle if the binary file is manually modified and added text contents
                    {
                        while (stream.Position < stream.Length)
                        {
                            tempList.Add((string)binaryFormatter.Deserialize(stream));
                        }
                    }
                    catch (Exception)
                    {
                        toolStripStatusLabel1.Text = "File could not Deserialize, may contain non-binary Characters";
                    }

                }
            }
            catch (IOException)
            {
                MessageBox.Show("Cannot OPEN file");
            }
            return tempList;
        }

        //Method to write the content to the file
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

        //Method to generate new file name with number before extension. 
        public string NewFileName(String fileName)
        {
            string extension = Path.GetExtension(fileName);
            while (File.Exists(fileName))
            {
                string onlyName = Path.GetFileNameWithoutExtension(fileName);
                int a = 0;
                string temp = "";
                for (int i = 0; i < onlyName.Length; i++)
                {
                    if (Char.IsDigit(onlyName[i]))
                    {
                        temp += onlyName[i];

                    }
                }
                if (temp.Length > 0)
                {
                    a = int.Parse(temp);
                }
                if (a < 9)
                {
                    fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Demo_" + "0" + (a + 1) + extension);
                }
                else
                {
                    fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Demo_" + (a + 1) + extension);
                }
            }
            return fileName;
        }
        //Method to identify if the user has modifed the opened file or not
        public bool CompareList(List<string> a, List<string> b)
        {
            a.Sort();
            b.Sort();
            if (a.Count != b.Count)  //when the length of both list is not equal return false
            {
                return false;
            }

            for (int i = 0; i < a.Count; i++)  // when the index of list a not equal to index of b then return false
            {
                if (!a[i].Equals(b[i]))
                {
                    return false;
                }
            }
            return true;  // if both list equal in length and content then return true
        }
        #endregion

        #region UTILITIES
        //It will ask the user to save the if anything modified in a application otherwise exit
        private void VehicleRegoMgm_FormClosing(object sender, FormClosingEventArgs e)
        {
            List<string> currentList = new List<string>();
            currentList = BinaryToText(currentFile, currentList);
            if (!CompareList(vehicleRegos, currentList))
            {
                if (MessageBox.Show("Do you want to save changes to your File?", "Vehicle Management Application",
                          MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Saves the file if clicked yes 
                    TexttoBinary(currentFile, vehicleRegos);
                }
                else  //saves auto increment file name
                {
                    TexttoBinary(NewFileName(currentFile), vehicleRegos);
                }
            }
            else //saves filename with autoincrement 
            {
                TexttoBinary(NewFileName(currentFile), vehicleRegos);
            }
        }

        //To change the status bar notification
        private void VehicleRegoMgm_MouseDown(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = "Application running";
        }

        private void TextBoxInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            //only allow one -
            if ((e.KeyChar == '-') && (TextBoxInput.Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }
        }
        //Q8: A list click method allows user to select the record display in textbox
        private void ListBoxVehicle_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                TextBoxInput.Text = vehicleRegos.ElementAt(ListBoxVehicle.SelectedIndex);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
#endregion
