using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.Convert.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SelectedValue(bool isSelected) : Attribute
    {
        public bool IsSelected => isSelected;
    }
}
