using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private Boolean highLightValue()
        {
            int currentRecord = listViewWiki.SelectedIndices[0];
            string structure = Wiki[currentRecord].getStructure();

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
                Information newInfo = new Information();
                newInfo.setName(textBoxName.Text);
                newInfo.setCategory(comboBoxCategory.Text.ToString());
                newInfo.setStructure(returnValue(selectRadio));
                newInfo.setDefinition(textBoxDefinition.Text);
                Wiki.Add(newInfo);

                MessageBox.Show(newInfo.getStructure());
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
            listViewWiki.Items.RemoveAt(listViewWiki.SelectedIndices[0]);

           // listViewWiki.SelectedItems[0].Remove();
            //Wiki.Remove();

            //foreach (information info in Wiki)
            //{
                
            //    //if(info.getName().Equals(textBoxName.Text))
            //    //{

            //    //}
            //    info.setName(textBoxName.Text);
            //    info.setCategory(comboBoxCategory.Text.ToString());
            //    info.setStructure(returnValue(selectRadio));
            //    info.setDefinition(textBoxDefinition.Text);
            //    Wiki.Remove(info);
            //}
            //displayInformation();
            // Wiki.RemoveAt(listViewWiki.Items.IndexOf(itemSelected.Index));

            //foreach (ListViewItem itemSelected in listViewWiki.SelectedItems)

            //{
            //    listViewWiki.Items.Remove(itemSelected);
            //    //listViewWiki.SelectedIndices.Remove(itemSelected.Index);

            //    //information newInfo = new information();
            //    //newInfo.setName(textBoxName.Text);
            //    //newInfo.setCategory(comboBoxCategory.Text.ToString());
            //    //newInfo.setStructure(returnValue(selectRadio));
            //    //newInfo.setDefinition(textBoxDefinition.Text);
            //    //Wiki.Remove(newInfo);

            //   

            //    //if (itemSelected != null)
            //    //{


            //    //foreach (information item in Wiki) 
            //    //{

            //    //}
            //    //}

            //}
            //// var itemRemove = Wiki.Single(r => r.getName() == listViewWiki.SelectedIndicese[0].SubItems[0].Text);
            // Wiki.Remove(itemRemove);
            // displayInformation();

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
                int currentRecord = listViewWiki.SelectedIndices[0];

                Wiki[currentRecord].setName(textBoxName.Text);
                Wiki[currentRecord].setCategory(comboBoxCategory.Text);
                Wiki[currentRecord].setStructure(returnValue(info.getStructure()));
                Wiki[currentRecord].setDefinition(textBoxDefinition.Text);
                displayInformation();
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
                    textBoxName.Text = Wiki[index].getName();
                    comboBoxCategory.Text = Wiki[index].getCategory();
                    textBoxDefinition.Text = Wiki[index].getDefinition();
                    displayInformation();
                    listViewWiki.Items[index].BackColor = Color.Yellow;
                }
                else
                {
                    toolStripStatusLabel.Text = " ";
                    toolStripStatusLabel.Text = " Not Found ";
                }
                displayInformation();
                toolStripStatusLabel.Text = "Found";
                textBoxSearch.Clear();
            }
            else
            {
                MessageBox.Show("Enter the searched data name.");
            }
            displayInformation();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void FormWikiApplication2_Load(object sender, EventArgs e)
        {
            listViewWiki.FullRowSelect = true;
        }

        private void listViewWiki_MouseClick(object sender, MouseEventArgs e)
        {
            int currentRecord = listViewWiki.SelectedIndices[0];
            if (currentRecord < 0)
            {
                MessageBox.Show("Select a record from the List Box");
            }
            else 
            {
                textBoxName.Text = Wiki[currentRecord].getName();
                comboBoxCategory.Text = Wiki[currentRecord].getCategory();
                textBoxDefinition.Text = Wiki[currentRecord].getDefinition();
                highLightValue();
            }
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
