namespace MemberAuth.Models
{
	public partial class UserCertificationLog
	{
		public int CertSeq { get; set; }
		public string CertID { get; set; }
		public string CertType { get; set; }
		public string DupInfo { get; set; }
		public string? CertData { get; set; }
		public DateTime RegDate { get; set; }
	}
}

