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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<information> Wiki = new List<information>();
        private void clearTextbox()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
        }
        private void displayInformation()
        {
            listViewWiki.Items.Clear();

            foreach (var info in Wiki)
            {
                ListViewItem list = new ListViewItem();
            }

        }
        private void vlaidName()
        {
            textBoxName.Clear();
            textBoxDefinition.Clear();
        }


        private void buttonAdd_Click(object sender, EventArgs e)
        {
            information newInfor = new information();

            newInfor.setName(textBoxName.Text);
            newInfor.setCategory(comboBoxCategory.SelectedIndex.ToString());
            newInfor.setDefinition(textBoxDefinition.Text);
            Wiki.Add(newInfor);
            clearTextbox();
            displayInformation();
        }

        
    }
}
