namespace TwoFactorAuth.Domain.Tests.Helpers
{
    public class Attempt
    {
        public string AttemptName { get; set; }
        public bool Result { get; set; }
        public string UserName { get; set; }
    }
}