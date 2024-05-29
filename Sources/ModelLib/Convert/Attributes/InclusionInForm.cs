using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.Convert.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface)]
    public class InclusionInForm(PropertyInclusion headerInclusion = PropertyInclusion.Include) : Attribute
    {
        public PropertyInclusion HeaderInclusion => headerInclusion;
    }
}
