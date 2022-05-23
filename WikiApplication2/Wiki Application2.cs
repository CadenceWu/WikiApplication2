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
        List<information> Wiki = new List<information>();
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
            foreach (information info in Wiki)
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
            String structure = listViewWiki.SelectedItems[0].SubItems[2].Text;

            if (structure == "Linear")
            {
                radioButtonLinear.Select();
                //radioButtonLinear.BackColor = Color.Yellow;
            }
            else if (structure == "Non-Linear")
            {
                radioButtonNonLinear.Select();
               // radioButtonNonLinear.BackColor = Color.Yellow;
            }

            return true;
        }
        private string returnValue(string selectRadio)
        {
            if (radioButtonLinear.Checked)
            {
                selectRadio = radioButtonLinear.Text;
            }
            if (radioButtonNonLinear.Checked)
            {
                selectRadio = radioButtonNonLinear.Text;
            }
           // MessageBox.Show(selectRadio);
            return selectRadio;
        }
        private void sortData() {

           Wiki = Wiki.OrderBy(o => o.getName()).ToList();
           //listViewWiki.Sorting = SortOrder.Ascending;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {

            if (!validName(textBoxName.Text))
            {
                MessageBox.Show("The name is already existed");
            }

            else if (string.IsNullOrWhiteSpace(textBoxName.Text) && string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                string.IsNullOrWhiteSpace(returnValue(selectRadio)) && string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                MessageBox.Show("Some text boxes are empty.");
            }
            else 
            {
                information newInfo = new information();
                newInfo.setName(textBoxName.Text);
                newInfo.setCategory(comboBoxCategory.Text.ToString());
                newInfo.setStructure(returnValue(selectRadio));
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

            if (!validName(textBoxName.Text))
            {
                MessageBox.Show("The name is already existed");
            }
            else if (string.IsNullOrWhiteSpace(textBoxName.Text) && string.IsNullOrWhiteSpace(comboBoxCategory.Text) &&
                string.IsNullOrWhiteSpace(returnValue(selectRadio)) && string.IsNullOrWhiteSpace(textBoxDefinition.Text))
            {
                MessageBox.Show("Some text boxes are empty.");
            }
            else 
            {
                listViewWiki.SelectedItems[0].Text = textBoxName.Text;
                listViewWiki.SelectedItems[0].SubItems[1].Text = comboBoxCategory.Text;
                listViewWiki.SelectedItems[0].SubItems[3].Text = textBoxDefinition.Text;
                listViewWiki.SelectedItems[0].SubItems[2].Text = returnValue(selectRadio);
                clearTextbox();
                sortData();
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            //Wiki.BinarySearch();
            //if (!string.IsNullOrWhiteSpace(textBoxName.Text))
            //{
            //    information searchInfo=new information();
            //    textBoxSearch.Text=searchInfo.getName();
            //    sortData();
            //    if (Wiki.BinarySearch(searchInfo.getName()) >=0)
            //        MessageBox.Show("Found");
            //    else
            //    { }
            //        MessageBox.Show("Not found");
            //        textBoxSearch.Clear();
            //}
            //else
            //{
            //    MessageBox.Show("Please enter a plate");
            //}
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void radioButtonLinear_CheckedChanged(object sender, EventArgs e)
        {
          
          //  structure = "Linear";
        }

        private void radioButtonNonLinear_CheckedChanged(object sender, EventArgs e)
        {

            //structure = "Non-Linear";

        }

        private void groupBoxStructure_Enter(object sender, EventArgs e)
        {

        }

        private void FormWikiApplication2_Load(object sender, EventArgs e)
        {
            listViewWiki.FullRowSelect = true;
        }

        private void listViewWiki_MouseClick(object sender, MouseEventArgs e)
        {
            String name = listViewWiki.SelectedItems[0].SubItems[0].Text;
            String category = listViewWiki.SelectedItems[0].SubItems[1].Text;
            String definition = listViewWiki.SelectedItems[0].SubItems[3].Text;
            highLightValue();
            textBoxName.Text = name;
            comboBoxCategory.Text = category;
            textBoxDefinition.Text = definition;

        }

    }
}
