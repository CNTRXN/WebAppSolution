namespace ModelLib.Convert.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface)]
    public class InclusionInHeader(PropertyInclusion headerInclusion = PropertyInclusion.Include) : Attribute
    {
        public PropertyInclusion HeaderInclusion => headerInclusion;
    }
}
