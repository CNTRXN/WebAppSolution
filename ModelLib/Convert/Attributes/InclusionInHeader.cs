namespace ModelLib.Attributes
{
    public enum HeaderInclusion 
    {
        Include,
        NotInclude
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface)]
    public class InclusionInHeader(HeaderInclusion headerInclusion = HeaderInclusion.Include) : Attribute
    {
        public HeaderInclusion HeaderInclusion => headerInclusion;
    }
}
