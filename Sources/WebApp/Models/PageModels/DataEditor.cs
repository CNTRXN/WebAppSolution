namespace WebApp.Models.PageModels
{
    public enum SendType 
    {
        New,
        Edit
    }

    public class DataEditor
    {
        public string TypeName { get; set; }
        public dynamic? DataValue { get; set; }
        public SendType SendType { get; set; }
    }
}
