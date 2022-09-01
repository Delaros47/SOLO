using System;

namespace Model.Attributes
{
    public class PrivateCode:Attribute
    {
        public string Description { get;}
        public string ControlName { get;}

        public PrivateCode(string description, string controlName)
        {
            Description = description;
            ControlName = controlName;
        }
    }
}
