namespace AulaAWS.Application.DTOs
{
    public class ErrorResponseModel
    {
        public string ErrorMessage { get; set; }

        public ErrorResponseModel(string message)
        {
            ErrorMessage = message;
        }
    }
}