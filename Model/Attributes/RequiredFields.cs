using System;

namespace Model.Attributes
{
    #region Comment
    /*
     * Here we have created our RequiredFields attribute class we will be using for our entities Description will be when we don't enter a field for example btnCityName then it will write Enter the (City Name)
     * and ControlName is our btnCityName
     */
    #endregion
    public class RequiredFields:Attribute
    {

        public string Description { get;}
        public string ControlName { get;}

        public RequiredFields(string description,string controlName)
        {
            Description = description;
            ControlName = controlName;
        }
    }
}
