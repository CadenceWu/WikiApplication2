using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Diagnostics;
//C# Assessment 2 (WikiApplication 2)
//Date:4/2022-06/2022
/*A Wiki application which stores Data structure information. The application allows a user
to Add, Delete, Edit, and search the data. It also allows a user to open and save the file.*/
namespace WikiApplication2
{
    public partial class FormWikiApplication2 : Form
    {
        public FormWikiApplication2()
        {
            InitializeComponent();
            //6.4 Create and initialise a global string array with the six categories as indicated in the Data Structure Matrix. 
            string[] categories = new[] { "Array", "List", "Tree", "Graphs", "Abstract", "Hash" };
            populateComboBox(categories);//Call the method to initialize combo box
        }
        //6.2 Create a global List<T> of type Information called Wiki.
        List<Information> Wiki = new List<Information>();
        string selectRadio;
        string currentFileName = "";

        //6.4 Create a custom method to populate the ComboBox 
        private void populateComboBox(Array arrayCategory)
        {
            var combobox = new ComboBox
            {
                DataSource = arrayCategory,//Specify the combobox datasource as custom array.
                DropDownStyle = ComboBoxStyle.DropDownList,//Using DropDownList style in the ComboBox
            };
            comboBoxCategory.Items.AddRange((object[])arrayCategory);//Add the custom array as combocategory items.
        }
        //A method to clear all textboxes except search text box
        private void clearTextbox()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
            comboBoxCategory.Text = String.Empty;
            radioButtonLinear.Checked = false;
            radioButtonNonLinear.Checked = false;
        }
        //Display Information objects in the Wiki list on the ListView 
        private void displayInformation()
        {
            listViewWiki.Items.Clear();
            sortData();
            foreach (Information info in Wiki)
            {
                ListViewItem item;
                item = listViewWiki.Items.Add(info.getName());
                item.SubItems.Add(info.getCategory());
                item.SubItems.Add(info.getStructure());
                item.SubItems.Add(info.getDefinition());
            }
        }
        /*6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name
            and returns a Boolean after checking for duplicates.Use the built in List<T> method “Exists” */
        private bool validName(string checkTheName)
        {
            //Check if there is any Data Structure name in the Wiki list the same as user input name
            if (Wiki.Exists(x => x.getName().Equals(checkTheName)))
                return false;
            else
                return true;
        }
        //6.6 Create a highlight method to send an integer index which will highlight an appropriate radio button.
        private Boolean highLightValue(int index)
        {
            //Specify the index from the Information variable Structure
            string structure = Wiki[index].getStructure();

            if (structure == "Linear")
            {
                radioButtonLinear.Checked = true;
            }
            if (structure == "Non-Linear")
            {
                radioButtonNonLinear.Checked = true;
            }
            return true;
        }
        //6.6 Create a return method to return a string value from the selected radio button (Linear or Non-Linear)
        private string returnValue(string selectRadio)
        {
            Information info = new Information();

            if (radioButtonLinear.Checked)
            {
                selectRadio = radioButtonLinear.Text;
                info.setStructure(selectRadio);
            }
            else if (radioButtonNonLinear.Checked)
            {
                selectRadio = radioButtonNonLinear.Text;
                info.setStructure(selectRadio);
            }
            return selectRadio;
        }
        //6.9 Create a single custom method that will sort and then display the Name and Category from the wiki information in the list.
        private void sortData() {

           /*Call the Sort() method on a List<Item> object, the List will automatically call the Item's CompareTo method to determine
           the order of the objects*/
            Wiki.Sort();
        }
        /*6.3 Create a button method to ADD a new item to the list. Use a TextBox for the Name input,
              ComboBox for the Category, Radio group for the Structure and Multiline TextBox for the Definition.*/
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            //If the four textboxes are not empty
            if (!string.IsNullOrWhiteSpace(textBoxName.Text) && !string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                !string.IsNullOrWhiteSpace(returnValue(selectRadio)) && !string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                if (!validName(textBoxName.Text))
                {
                    MessageBox.Show("The name is already existed");
                    
                    Trace.Indent();//Indent():Increase the current indentLevel by one
                    /*The Assert(Boolean,String) is used to detect and identify errors in runtime.
                      If the first argument is false, the message will show up*/
                    Trace.Assert(validName(textBoxName.Text), "Trace: It is not a valid name");
                    Trace.Unindent();//Decreases the current indentLevel by one
                }
                else
                {
                    //Add the inserted data into the Wiki List
                    Information newInfo = new Information();
                    newInfo.setName(textBoxName.Text);
                    newInfo.setCategory(comboBoxCategory.Text.ToString());
                    newInfo.setStructure(returnValue(selectRadio));
                    newInfo.setDefinition(textBoxDefinition.Text);
                    Wiki.Add(newInfo);
                    toolStripStatusLabel.Text = "Added a data";

                    Trace.Indent();
                    Trace.WriteLineIf(!validName(textBoxName.Text), "Trace: It is a valid name");
                    Trace.Unindent();
                }
            }
            else
            {
                MessageBox.Show("Some text boxes are empty.");
            }
            clearTextbox();
            displayInformation();
        }

        /*6.7 A button method that will delete the currently selected record in the ListView.
             Using a dialog box to ensure the user has the option to backout of this action
             Display an updated version of the sorted list at the end of this process.*/
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            int currentRecord =0;

            try
            {
               currentRecord = listViewWiki.SelectedIndices[0];

                DialogResult deleteRecord;

                //If there is an item selected
                if (currentRecord != -1)
                {
                    deleteRecord = MessageBox.Show("Do you want to delete this data?",
                    "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteRecord == DialogResult.Yes)
                    {
                        Wiki.RemoveAt(currentRecord);
                        toolStripStatusLabel.Text = "Deleted the selected data";
                        Trace.Indent();
                        Trace.WriteLine("The item has been deleted");
                        Trace.Unindent();
                    }
                    else if(deleteRecord == DialogResult.Cancel)
                    {
                        return;
                    }
                   

                    toolStripStatusLabel.Text = "";

                    displayInformation();
                    clearTextbox();
                }
            }
            catch
            {
                    MessageBox.Show("Please select an item from the list box");
                    Trace.Indent();
                    Trace.WriteLine("The item hasn't been selected");
                    Trace.Unindent();
            }
        }
        /*6.8 Create a button method that will save the edited record of the currently selected item in the ListView.
              All the changes in the input controls will be written back to the list.
              Display an updated version of the sorted list at the end of this process.*/
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            if (!string.IsNullOrWhiteSpace(textBoxName.Text) && !string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                !string.IsNullOrWhiteSpace(returnValue(selectRadio)) && !string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                int currentRecord = listViewWiki.SelectedIndices[0];

                Wiki[currentRecord].setName(textBoxName.Text);
                Wiki[currentRecord].setCategory(comboBoxCategory.Text);
                Wiki[currentRecord].setStructure(returnValue(info.getStructure()));
                Wiki[currentRecord].setDefinition(textBoxDefinition.Text);
                displayInformation();
                toolStripStatusLabel.Text = "Update the selected information";

                clearTextbox();
            }
            else
            {
                MessageBox.Show("Some text boxes are empty.");
            }
        }
        /*6.10 Create a button method that will use the builtin binary search to find a Data Structure name.
              If the record is found the associated details will populate the appropriate input controls and 
              highlight the name in the ListView. At the end of the search process the search input TextBox must be cleared.*/
        private void buttonSearch_Click(object sender, EventArgs e)
        {
           //Searchs the sorted List<T> for an element using the default comparer and returns the Zero-based index of the element
           //List.BinarySearch(T item)
            int index = Wiki.BinarySearch(new Information(textBoxSearch.Text));

            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                if (index >= 0)
                {
                    toolStripStatusLabel.Text = "Found";
                    displayInformation();
                    listViewWiki.Items[index].BackColor = Color.Yellow;
                    textBoxName.Text = Wiki[index].getName();
                    comboBoxCategory.Text = Wiki[index].getCategory();
                    textBoxDefinition.Text = Wiki[index].getDefinition();
                    highLightValue(index);
                }
                else
                {
                    toolStripStatusLabel.Text = " ";
                    toolStripStatusLabel.Text = " Not Found ";
                    displayInformation();
                }
                textBoxSearch.Clear();
            }
            else
            {
                MessageBox.Show("Enter the name to search.");
            }
        }
        //6.14 Create a Open button to open the saved file, this must have a dialog box to select a file
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            //Displays a standard dialog box that prompts the user to open a file. 
            OpenFileDialog openFileDialog = new OpenFileDialog();//Creat an OpenFileDialog control object
            openFileDialog.Filter = "Bin Flies (*.bin)|*.bin";
            openFileDialog.DefaultExt = "bin";//Default file extension
            openFileDialog.FileName="defaultName.bin";
            openFileDialog.ShowDialog();
            try
            {
                using (var stream = File.Open(openFileDialog.FileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        Wiki.Clear();
                        while (stream.Position < stream.Length)
                        {
                          currentFileName = openFileDialog.FileName;

                            Information readInfo = new Information();
                            readInfo.setName(reader.ReadString());
                            readInfo.setCategory(reader.ReadString());
                            readInfo.setStructure(reader.ReadString());
                            readInfo.setDefinition(reader.ReadString());
                            Wiki.Add(readInfo);
                        }
                    }
                }
                displayInformation();
            }
            catch (IOException)
            {
                return;
            }
        }
        //6.14 Create a Save button to save the file or rename a saved file. All Wiki data is stored/retrieved using a binary file format. 
        private void buttonSave_Click(object sender, EventArgs e)
        {
            //The SaveFileDialog control prompts the user to select a location for saving
            //a file and allows the user to specify the name of the file to save data.
            SaveFileDialog saveFileDialogVG = new SaveFileDialog();
            saveFileDialogVG.Filter = "BIN Files|*.bin";//Filter files by extensioin
            saveFileDialogVG.Title = "Save a BIN File";
            saveFileDialogVG.DefaultExt = "bin";//Default file extension
            saveFileDialogVG.FileName = "defaultFileName.bin";
            saveFileDialogVG.ShowDialog();

            saveFile(saveFileDialogVG.FileName);    
            
        }
        //A save method used to save the file using binary format
        private void saveFile(string filename)
        {
            try
            {
                using (var stream = File.Open(filename, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
                    {
                        foreach (Information x in Wiki)
                        {
                            writer.Write(x.getName());
                            writer.Write(x.getCategory());
                            writer.Write(x.getStructure());
                            writer.Write(x.getDefinition());
                        }
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("File not saved");
            }
        }
        private void FormWikiApplication2_Load(object sender, EventArgs e)
        {
            listViewWiki.FullRowSelect = true;
        }
        /*6.11 Create a ListView event so a user can select a Data Structure Name from the list of Names and the associated information will be displayed
               in the related text boxes combo box and radio button.*/
        private void listViewWiki_MouseClick(object sender, MouseEventArgs e)
        {
            int currentRecord = listViewWiki.SelectedIndices[0];
            
            textBoxName.Text = Wiki[currentRecord].getName();
            comboBoxCategory.Text = Wiki[currentRecord].getCategory();
            textBoxDefinition.Text = Wiki[currentRecord].getDefinition();
            highLightValue(currentRecord);
        }
        //6.13 Create a double click event on the Name TextBox to clear the TextBboxes, ComboBox and Radio button.
        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            clearTextbox();
            textBoxSearch.Clear();
        }
        //6.12 Create a custom method that will clear and reset the TextBboxes, ComboBox and Radio button
        public void reset()
        {
            listViewWiki.Items.Clear();
            Wiki.Clear();
            clearTextbox();
        }
        
        private void buttonReset_Click(object sender, EventArgs e)
        {
            reset();
        }
        //6.15 The Wiki application will save data when the form closes. 
        private void FormWikiApplication2_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult savedRecord;
            string FileName = "AutoSaved.bin";
         
            try
            {   
                savedRecord = MessageBox.Show("Do you want to save the changes? \n( Ignore the message if there was no change ))",
                 "Saved Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (savedRecord == DialogResult.Yes)
                {
                    saveFile(currentFileName);//Save the file back to the selected file

                }
                else if (savedRecord == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch 
            {
                //If it is a new-created file, save it as AutoSaved.bin
                saveFile(FileName);
                MessageBox.Show("Save File as AutoSaved.bin");
            }

        }

    }
}
