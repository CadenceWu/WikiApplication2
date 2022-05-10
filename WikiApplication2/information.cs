using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiApplication2
{
    internal class information
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Structure { get; set; }
        public string Definition { get; set; }

        public void setName(string newName)
        {
            Name=newName;
        }
        public string getName()
        {
            return Name;
        }
        public void setCategory(string newCategory)
        { 
           Category=newCategory;
        }
        public string getCategory()
        {
            return Category;
        }
        public void setDefinition(string newDefinitioin)
        {
            Definition = newDefinitioin;
        }
        
        public string getDefinition()
        { 
            return Definition;
        }

        public string displayOneInfor()
        { 
        return getName() + " " + getDefinition();
        }



    }
}
