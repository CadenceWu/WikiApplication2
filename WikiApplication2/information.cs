using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//6.1 A separate class file to hold the four data items of the Data Structure (use the Data Structure Matrix as a guide).
//    Use auto-implemented properties for the fields which must be of type “string”. Save the class as “Information.cs”.
namespace WikiApplication2
{   //Implements an IComparable<T> interface. 
    //The Inforamtion class has following attributes: Name,Category, Structure and Definition
    internal class Information : IComparable<Information>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Structure { get; set; }
        public string Definition { get; set; }

        public Information()
        {

        }
        public Information(string name)
        {
            this.Name = name;
        }
        public int CompareTo(Information otherName) 
        {   
            //Compare current parameter with otheName.Name parameter
            return this.Name.CompareTo(otherName.Name);
        }

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
        public void setStructure(string newStructure)
        { 
           Structure=newStructure;
        }
        public string getStructure()
        {
            return Structure;
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
