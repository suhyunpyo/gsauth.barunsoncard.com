namespace MemberAuth.Models
{
    public partial class UserTokenInfo
    {
        public Guid guid_id { get; set; }
        public string access_token { get; set; }
        public string user_id { get; set; }
        public DateTime reg_date { get; set; }
    }
}
