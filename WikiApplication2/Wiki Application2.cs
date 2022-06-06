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

        List<Information> Wiki = new List<Information>();
        string selectRadio;
        string currentFileName = "";
        //string currentFileName = "";

        //Create a custom method to populate the ComboBox 
        private void populateComboBox(Array arrayCategory)
        {
            var combobox = new ComboBox
            {
                DataSource = arrayCategory,//Specify the combobox datasource as custom array.
                DropDownStyle = ComboBoxStyle.DropDownList,
            };
            comboBoxCategory.Items.AddRange((object[])arrayCategory);//Add the custom array as combocategory items.
        }

        private void clearTextbox()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
            comboBoxCategory.Text = String.Empty;
            radioButtonLinear.Checked = false;
            radioButtonNonLinear.Checked = false;
        }
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
        //6.5 Create a custom ValidName method which will take a parameter string value from the Textbox Name
        //and returns a Boolean after checking for duplicates.Use the built in List<T> method “Exists” 
        private bool validName(string checkTheName)
        {
            //Check if there is any Data Structure name in the Wiki list the same as user input name
            if (Wiki.Exists(x => x.getName().Equals(checkTheName)))

            return false;

            else
                return true;
            Trace.Indent();
            Trace.WriteLine("Just print sth");

            Trace.Assert(Wiki.Exists(x => x.getName().Equals(checkTheName)), "It is a valid name");
            Trace.Unindent();
           // Trace.WriteLineIf(Wiki.Exists(x => x.getName().Equals(checkTheName)), "");
        }

        private Boolean highLightValue(int index)
        {
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
        private string returnValue(string selectRadio)
        {
            Information info = new Information();
            // string selectRadio;

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
            // MessageBox.Show(selectRadio);
            return selectRadio;
        }
        private void sortData() {

            //Wiki = Wiki.OrderBy(o => o.getName()).ToList();
            //listViewWiki.Sorting = SortOrder.Ascending;
            Wiki.Sort();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Information info = new Information();

            if (!string.IsNullOrWhiteSpace(textBoxName.Text) && !string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                !string.IsNullOrWhiteSpace(returnValue(selectRadio)) && !string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                if (!validName(textBoxName.Text))
                {
                    MessageBox.Show("The name is already existed");

                   // Trace.WriteLine("Just print sth");
                    //Trace.Assert(validName(textBoxName.Text), "It is not a valid name");

                }
                else
                {
                    Information newInfo = new Information();
                    newInfo.setName(textBoxName.Text);
                    newInfo.setCategory(comboBoxCategory.Text.ToString());
                    newInfo.setStructure(returnValue(selectRadio));
                    newInfo.setDefinition(textBoxDefinition.Text);
                    Wiki.Add(newInfo);
                    //Trace.WriteLineIf(validName(textBoxName.Text), "It is a valid name");
                }
            }
            else
            {
                MessageBox.Show("Some text boxes are empty.");
            }

            clearTextbox();
            displayInformation();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int currentRecord = listViewWiki.SelectedIndices[0];

                DialogResult deleteRecord;

                //If there is an item selected
                if (currentRecord != -1)
                {
                    deleteRecord = MessageBox.Show("Do you want to delete this data?",
                    "Delete Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (deleteRecord == DialogResult.Yes)
                    {
                        Wiki.RemoveAt(currentRecord);
                        if (deleteRecord == DialogResult.Cancel)
                        {
                            return;
                        }
                    }
                    displayInformation();
                    clearTextbox();
                    Trace.WriteLine("Trace Testing");
                    Debug.WriteLine("Debug Information-Product Starting ");
                }
            }
            catch
            {
                MessageBox.Show("Please select an item from the list box");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            Information info = new Information();
            if (!string.IsNullOrWhiteSpace(textBoxName.Text) && !string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                !string.IsNullOrWhiteSpace(returnValue(selectRadio)) && !string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                if (!validName(textBoxName.Text))
                {
                    MessageBox.Show("The name is already existed");
                }
                else
                {
                    int currentRecord = listViewWiki.SelectedIndices[0];

                    Wiki[currentRecord].setName(textBoxName.Text);
                    Wiki[currentRecord].setCategory(comboBoxCategory.Text);
                    Wiki[currentRecord].setStructure(returnValue(info.getStructure()));
                    Wiki[currentRecord].setDefinition(textBoxDefinition.Text);
                    displayInformation();
                }
                clearTextbox();
            }
            else
            {
                MessageBox.Show("Some text boxes are empty.");
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
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

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();//Creat an OpenFileDialog control object
           // openFileDialog.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
            openFileDialog.Filter = "Bin Flies (*.bin)|*.bin";
            openFileDialog.DefaultExt = "bin";//Default file extension
            openFileDialog.FileName="defaultName.bin";
            openFileDialog.ShowDialog();
            //currentFileName = openFile.FileName;
            try
            {
                using (var stream = File.Open(openFileDialog.FileName, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        Wiki.Clear();
                        while (stream.Position < stream.Length)
                        {
                          //FileName= openFileDialog.FileName;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //The SaveFileDialog control prompts the user to select a location for saving
            //a file and allows the user to specify the name of the file to save data.
            SaveFileDialog saveFileDialogVG = new SaveFileDialog();
            //saveFileDialogVG.InitialDirectory = Application.StartupPath;
            saveFileDialogVG.Filter = "BIN Files|*.bin";//Filter files by extensioin
            saveFileDialogVG.Title = "Save a BIN File";
            saveFileDialogVG.DefaultExt = "bin";//Default file extension
            saveFileDialogVG.FileName = "defaultFileName.bin";
            saveFileDialogVG.ShowDialog();

            saveFile(saveFileDialogVG.FileName);    
            
        }
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

        private void listViewWiki_MouseClick(object sender, MouseEventArgs e)
        {
            int currentRecord = listViewWiki.SelectedIndices[0];
            
            textBoxName.Text = Wiki[currentRecord].getName();
            comboBoxCategory.Text = Wiki[currentRecord].getCategory();
            textBoxDefinition.Text = Wiki[currentRecord].getDefinition();
            highLightValue(currentRecord);
        }

        private void textBoxName_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            clearTextbox();
            textBoxSearch.Clear();
        }
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
                    saveFile(currentFileName);

                }
                else if (savedRecord == DialogResult.Cancel)
                {
                    return;
                }
            }
            catch 
            {
                saveFile(FileName);
                MessageBox.Show("Save File as AutoSaved.bin");
            }

        }

    }
}
