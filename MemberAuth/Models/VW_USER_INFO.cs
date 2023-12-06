namespace MemberAuth.Models
{
    public partial class VW_USER_INFO
    {
        public string uid { get; set; }
        public string pwd { get; set; }
        public string uname { get; set; }
        public string umail { get; set; }
        public string BIRTH_DATE { get; set; }
        public string BIRTH_DATE_TYPE { get; set; }
        public string DupInfo { get; set; }
        public string ConnInfo { get; set; }
        public string AuthType { get; set; }
        public string ORIGINAL_BIRTH_DATE { get; set; }
        public string Gender { get; set; }
        public string NATIONAL_INFO { get; set; }
        public string WEDD_YEAR { get; set; }
        public string WEDD_MONTH { get; set; }
        public string WEDD_DAY { get; set; }
        public string WEDDING_DAY { get; set; }
        public string WEDDING_HALL { get; set; }
        public string site_div { get; set; }
        public string SITE_DIV_NAME { get; set; }
        public string chk_sms { get; set; }
        public string chk_mailservice { get; set; }
        public string HPHONE { get; set; }
        public string PHONE { get; set; }
        public string ZIPCODE { get; set; }
        public string isJehu { get; set; }
        public string zip1 { get; set; }
        public string zip2 { get; set; }
        public string address { get; set; }
        public string addr_detail { get; set; }
        public string mkt_chk_flag { get; set; }
        public string CHOICE_AGREEMENT_FOR_SAMSUNG_MEMBERSHIP { get; set; }
        public string CHOICE_AGREEMENT_FOR_SAMSUNG_CHOICE_PERSONAL_DATA { get; set; }
        public string CHOICE_AGREEMENT_FOR_SAMSUNG_THIRDPARTY { get; set; }
        public DateTime? smembership_reg_date { get; set; }
        public string INTEGRATION_MEMBER_YORN { get; set; }
        public DateTime? INTERGRATION_DATE { get; set; }
        public string INTERGRATION_BEFORE_ID { get; set; }
        public string REFERER_SALES_GUBUN { get; set; }
        public string SELECT_SALES_GUBUN { get; set; }
        public string SELECT_USER_ID { get; set; }
        public string USE_YORN { get; set; }
        public DateTime reg_date { get; set; }
        public int? company_seq { get; set; }
        public string CHK_MYOMEE { get; set; }
        public DateTime? MYOMEE_REG_DATE { get; set; }
        public string isMCardAble { get; set; }
        public string inflow_route { get; set; }
        public string chk_iloommembership { get; set; }
        public DateTime? iloommembership_reg_date { get; set; }
        public string chk_lgmembership { get; set; }
        public DateTime? lgmembership_reg_date { get; set; }
    }
}
