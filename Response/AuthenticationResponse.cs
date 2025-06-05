namespace Librarykuno.Response
{
    public class AuthenticationResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public MemberResponse? Member { get; set; }
        public string? AccessToken { get; set; }
    }

}
