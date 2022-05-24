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
           // String structure = listViewWiki.SelectedItems[0].SubItems[2].Text;
            int currentRecord = listViewWiki.SelectedIndices[0];
            String structure=listViewWiki.SelectedItems[0].SubItems[2].Text;
            //textBoxName.Text = Wiki[currentRecord].getName();

            if (structure == "Linear")
            {
               // radioButtonLinear.Select();
                //radioButtonLinear.BackColor = Color.Yellow;
                radioButtonLinear.Text=Wiki[currentRecord].getStructure();
            }
            else if (structure == "Non-Linear")
            {
                //radioButtonNonLinear.Select();
                // radioButtonNonLinear.BackColor = Color.Yellow;
                radioButtonNonLinear.Text = Wiki[currentRecord].getStructure();
            }

            return true;
        }
        private string returnValue(string selectRadio)
        {
            //Think of a way to use group box.
            Information info =new Information();
           // string selectRadio;

            if (radioButtonLinear.Checked)
            {

                selectRadio = radioButtonLinear.Text;
                info.setStructure(selectRadio);
                
            }
            if (radioButtonNonLinear.Checked)
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
            if (!validName(textBoxName.Text))
            {
                MessageBox.Show("The name is already existed");
            }

            else if (string.IsNullOrWhiteSpace(textBoxName.Text) && string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                string.IsNullOrWhiteSpace(returnValue(info.getStructure())) && string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                MessageBox.Show("Some text boxes are empty.");
            }
            else 
            {
                Information newInfo = new Information();
                newInfo.setName(textBoxName.Text);
                newInfo.setCategory(comboBoxCategory.Text.ToString());
                newInfo.setStructure(returnValue(info.getStructure()));
                newInfo.setDefinition(textBoxDefinition.Text);
                Wiki.Add(newInfo);
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
            if (!validName(textBoxName.Text))
            {
                MessageBox.Show("The name is already existed");
            }
            else if (string.IsNullOrWhiteSpace(textBoxName.Text) && string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                string.IsNullOrWhiteSpace(returnValue(info.getStructure())) && string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                MessageBox.Show("Some text boxes are empty.");
            }
            else 
            {
                int currentRecord = listViewWiki.SelectedIndices[0];

                //Wiki.Insert(currentRecord, )
                //listViewWiki.SelectedItems[0].Text = textBoxName.Text;
                //listViewWiki.SelectedItems[0].SubItems[1].Text = comboBoxCategory.Text;
                //listViewWiki.SelectedItems[0].SubItems[3].Text = textBoxDefinition.Text;
                //listViewWiki.SelectedItems[0].SubItems[2].Text = returnValue(info.getStructure());

                textBoxName.Text = Wiki[currentRecord].getName();
                comboBoxCategory.Text = Wiki[currentRecord].getCategory();
                highLightValue();
                textBoxDefinition.Text = Wiki[currentRecord].getDefinition();

                info.setName(textBoxName.Text);
                info.setCategory(comboBoxCategory.Text.ToString());
                info.setStructure(returnValue(info.getStructure()));
                info.setDefinition(textBoxDefinition.Text);
                Wiki.Add(info);
                clearTextbox();
                sortData();
            }
            displayInformation();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            int index = Wiki.BinarySearch(new Information(textBoxSearch.Text));

            //listViewWiki.Items.Clear();
            if (!string.IsNullOrEmpty(textBoxSearch.Text))
            {
                if (index >= 0)
                {
                    toolStripStatusLabel.Text = "Found";
                    //displayInformation();
                    listViewWiki.Items[index].BackColor = Color.Yellow;
                    //String name = listViewWiki.SelectedItems[index].Text;
                   // String category = listViewWiki.SelectedItems[index].SubItems[1].Text;
                    //String definition = listViewWiki.SelectedItems[index].SubItems[3].Text;
                    //highLightValue();
                    //textBoxName.Text = name;
                    //comboBoxCategory.Text = category;
                    //textBoxDefinition.Text = definition;
                    //textBoxName.Text= listViewWiki.SelectedItems[index].Text;
                    //comboBoxCategory.Text = listViewWiki.SelectedItems[index].SubItems[1].Text;
                   // textBoxDefinition.Text= listViewWiki.SelectedItems[index].SubItems[3].Text;
                    //String structure = listViewWiki.SelectedItems[index].SubItems[2].Text;
                    //highLightValue();
                }
                else
                {
                    toolStripStatusLabel.Text = " ";
                    toolStripStatusLabel.Text = " Not Found ";
                   // MessageBox.Show("Not found");
                  //  displayInformation();
                }
                //displayInformation();
                textBoxSearch.Clear();
            }
            else 
            { 
                MessageBox.Show("Enter the searched data name.");
            }
            //displayInformation();
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
            //String name = listViewWiki.SelectedItems[0].SubItems[0].Text;
            //String category = listViewWiki.SelectedItems[0].SubItems[1].Text;
            //String definition = listViewWiki.SelectedItems[0].SubItems[3].Text;
            //highLightValue();
            //textBoxName.Text = name;
            //comboBoxCategory.Text = category;
            //textBoxDefinition.Text = definition;
            int currentRecord = listViewWiki.SelectedIndices[0];
            if (currentRecord < 0)
            {
                MessageBox.Show("Select a record from the List Box");
            }
            else 
            {
                textBoxName.Text = Wiki[currentRecord].getName();
                comboBoxCategory.Text = Wiki[currentRecord].getCategory();
                highLightValue();
                textBoxDefinition.Text = Wiki[currentRecord].getDefinition();
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
