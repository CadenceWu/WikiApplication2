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

            foreach (information info in Wiki)
            {
                ListViewItem item;
                item = listViewWiki.Items.Add(info.getName());
                item.SubItems.Add(info.getCategory());
                item.SubItems.Add(info.getStructure());
                item.SubItems.Add(info.getDefinition());
            }
        }
        private void vlaidName()
        {
          
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
            MessageBox.Show(selectRadio);
            return selectRadio;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            information newInfo = new information();
            newInfo.setName(textBoxName.Text);
            newInfo.setCategory(comboBoxCategory.Text.ToString());
            newInfo.setStructure(returnValue(selectRadio));
            newInfo.setDefinition(textBoxDefinition.Text);
            Wiki.Add(newInfo);
            clearTextbox();
            displayInformation();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {

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
            String name=listViewWiki.SelectedItems[0].SubItems[0].Text;
            String category = listViewWiki.SelectedItems[0].SubItems[1].Text;
            String definition = listViewWiki.SelectedItems[0].SubItems[3].Text;
            highLightValue();
            textBoxName.Text = name;
            comboBoxCategory.Text = category;
            textBoxDefinition.Text = definition;


        }
    }
}
