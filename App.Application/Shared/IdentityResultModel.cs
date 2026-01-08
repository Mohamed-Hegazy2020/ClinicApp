namespace App.Application.Shared
{
    public class IdentityResultModel
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public IdentityResultModel()
        {
            Succeeded=false;
            Message = "";
        }
    }
}
