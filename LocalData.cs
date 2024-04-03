namespace BankApplication
{
    internal class LocalData
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NumberPhone { get; set; }
        public string NumberPassport { get { if (AuthenticationService.PermissionsAccess) return _NumberPassport; else { return Secure; } } set { _NumberPassport = value; } }
        public string Locate { get; set; }

        private string _NumberPassport {  get; set; }
        private string Secure = "**********";
    }

}
