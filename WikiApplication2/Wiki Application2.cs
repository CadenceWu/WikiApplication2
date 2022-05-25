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

namespace WikiApplication2
{
    public partial class FormWikiApplication2 : Form
    {
        public FormWikiApplication2()
        {
            InitializeComponent();
        }
        List<Information> Wiki = new List<Information>();
        string selectRadio;
        string defaultFileName = "default.bin";


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
        private bool validName(string checkTheName)
        {
            if (Wiki.Exists(x=> x.getName().Equals(checkTheName)))
                return false;
            else
                return true;
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
            Information info =new Information();
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
            Information info=new Information();

            if (!string.IsNullOrWhiteSpace(textBoxName.Text) && !string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                !string.IsNullOrWhiteSpace(returnValue(selectRadio)) && !string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                if (!validName(textBoxName.Text))
                {
                    MessageBox.Show("The name is already existed");
                }
                else
                {
                    Information newInfo = new Information();
                    newInfo.setName(textBoxName.Text);
                    newInfo.setCategory(comboBoxCategory.Text.ToString());
                    newInfo.setStructure(returnValue(selectRadio));
                    newInfo.setDefinition(textBoxDefinition.Text);
                    Wiki.Add(newInfo);

                    MessageBox.Show(newInfo.getStructure());
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

        private void openRecord(string openFileName)
        {
            //try
            //{
            //    if (File.Exists(openFileName))
            //    {
            //        using (Stream stream = File.Open(openFileName, FileMode.Open, FileAccess.Read))
            //        {
            //            BinaryFormatter bin = new BinaryFormatter();

                      
            //                    //Read the information from the binary file and list them in the listview
            //                    Wiki = (string)bin.Deserialize(stream);
                         
            //        }
            //    }
            //}
            //catch (IOException ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
            //displayArray();
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {

        }
        private void saveRecord(string saveFileName)
        {
            try
            {
                //Open a stream for writing
                using (FileStream stream = File.Open(saveFileName, FileMode.Create))
                {
                    // Construct a BinaryFormatter and use it to serialize the data to the stream.
                    BinaryFormatter bin = new BinaryFormatter();
                    // BinaryWriter writer = new BinaryWriter(stream);
                    bin.Serialize(stream, Wiki);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialogVG = new SaveFileDialog();
            saveFileDialogVG.InitialDirectory = Application.StartupPath;
            saveFileDialogVG.Filter = "BIN Files|*.bin";//Filter files by extensioin
            saveFileDialogVG.Title = "Save a BIN File";
            saveFileDialogVG.DefaultExt = "bin";//Default file extension
            saveFileDialogVG.ShowDialog();

            string fileName = saveFileDialogVG.FileName;
            if (saveFileDialogVG.FileName != "")
            {
                saveRecord(fileName);
            }
            else
            {
                saveRecord(defaultFileName);
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
    }
}
