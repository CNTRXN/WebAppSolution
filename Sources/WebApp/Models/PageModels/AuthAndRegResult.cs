namespace WebApp.Models.PageModels
{
    public enum SendFormStatus
    {
        None,
        HaveError,
        SuccesfulLogin,
        SuccesfulReg,
    }
    public class AuthAndRegResult(bool isStartModel = true)
    {
        public string Message { get; set; } = string.Empty;
        public SendFormStatus SendFormStatus { get; set; } = SendFormStatus.None;
        public bool IsStartModel { get; private set; } = isStartModel;
    }
}
