using MemberAuth.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberAuth.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<VW_USER_INFO> VW_USER_INFOs { get; set; }
        public virtual DbSet<UserTokenInfo> UserTokenInfos { get; set; }
		public virtual DbSet<UserCertificationLog> UserCertificationLogs { get; set; }
		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VW_USER_INFO>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_USER_INFO");

                entity.Property(e => e.AuthType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.BIRTH_DATE)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.BIRTH_DATE_TYPE)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CHK_MYOMEE)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CHOICE_AGREEMENT_FOR_SAMSUNG_CHOICE_PERSONAL_DATA)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CHOICE_AGREEMENT_FOR_SAMSUNG_MEMBERSHIP)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.CHOICE_AGREEMENT_FOR_SAMSUNG_THIRDPARTY)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ConnInfo)
                    .HasMaxLength(88)
                    .IsUnicode(false);

                entity.Property(e => e.DupInfo)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.HPHONE)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.INTEGRATION_MEMBER_YORN)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.INTERGRATION_BEFORE_ID)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.INTERGRATION_DATE).HasColumnType("datetime");

                entity.Property(e => e.MYOMEE_REG_DATE).HasColumnType("datetime");

                entity.Property(e => e.NATIONAL_INFO)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ORIGINAL_BIRTH_DATE)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.PHONE)
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.REFERER_SALES_GUBUN)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SELECT_SALES_GUBUN)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.SELECT_USER_ID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SITE_DIV_NAME)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.USE_YORN)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.WEDDING_DAY)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.WEDDING_HALL)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.WEDD_DAY)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.WEDD_MONTH)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.WEDD_YEAR)
                    .IsRequired()
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.ZIPCODE)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.addr_detail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.chk_iloommembership)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.chk_lgmembership)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.chk_mailservice)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.chk_sms)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.iloommembership_reg_date).HasColumnType("datetime");

                entity.Property(e => e.inflow_route)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.isJehu)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.isMCardAble)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.lgmembership_reg_date).HasColumnType("datetime");

                entity.Property(e => e.mkt_chk_flag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.pwd)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.reg_date).HasColumnType("datetime");

                entity.Property(e => e.site_div)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.smembership_reg_date).HasColumnType("datetime");

                entity.Property(e => e.uid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.umail)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.uname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.zip1)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.zip2)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserTokenInfo>(entity =>
            {
                entity.HasKey(e => e.guid_id);

                entity.ToTable("UserTokenInfo");

                entity.Property(e => e.access_token)
                    .IsUnicode(false);

                entity.Property(e => e.user_id)
                    .IsUnicode(false);

                entity.Property(e => e.reg_date).HasColumnType("datetime");
            });

			modelBuilder.Entity<UserCertificationLog>(entity =>
			{
				entity.HasKey(e => e.CertSeq);

				entity.ToTable("User_Certification_Log");

				entity.Property(e => e.CertID)
				    .IsRequired()
					.IsUnicode(false);

				entity.Property(e => e.CertType)
					.IsRequired()
					.IsUnicode(false);

				entity.Property(e => e.DupInfo)
					.IsRequired()
					.IsUnicode(false);

				entity.Property(e => e.CertData)
					.IsUnicode(false);

				entity.Property(e => e.RegDate).HasColumnType("datetime");
			});
		}
    }
}
