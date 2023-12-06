using MemberAuth.Data;
using MemberAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;

namespace MemberAuth.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        private readonly ApplicationDbContext _db;
        private readonly BarunnConfig _barun_config;
        public AccountController(JwtSettings jwtSettings, ApplicationDbContext db, BarunnConfig barun_config)
        {
            this.jwtSettings = jwtSettings;
            _db = db;
            _barun_config = barun_config;
        }

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogins)
        {
            try
            {
                var userinfo = new UserInfo();
                var vw_user_info = _db.VW_USER_INFOs.Where(s => s.uid == userLogins.Userid).ToList();
                var Valid = vw_user_info.Any();
                if (Valid)
                {
                    var vwuserinfo = vw_user_info.FirstOrDefault();

                    var p_UID = new SqlParameter("@UID", userLogins.Userid);
                    var p_PWD = new SqlParameter("@PWD", userLogins.Password);
                    var p_DUPINFO = new SqlParameter("@DUPINFO", vwuserinfo.DupInfo);
                    Valid = _db.VW_USER_INFOs.FromSqlRaw($"EXEC dbo.SP_SELECT_VW_USER_INFO @UID, @PWD, @DUPINFO", p_UID, p_PWD, p_DUPINFO).ToList().Any();
                    if (Valid)
                    {
                        var Token = JwtHelpers.JwtHelpers.GenTokenkey(new UserTokens()
                        {
                            GuidId = Guid.NewGuid(),
                            Id = userLogins.Userid
                        }, jwtSettings);

                        var usertokeninfo = new UserTokenInfo()
                        {
                            guid_id = Token.GuidId,
                            access_token = Token.Token,
                            user_id = Token.Id,
                            reg_date = DateTime.Now
                        };

                        _db.Add(usertokeninfo);
                        _db.SaveChanges();

                        usertokeninfo = _db.UserTokenInfos.Where(s => s.access_token == Token.Token).FirstOrDefault();
                        userinfo.AccessToken = usertokeninfo.access_token;
                        userinfo.UserId = usertokeninfo.user_id;

						var cookieOptions = new CookieOptions
						{
							Secure = true,
							HttpOnly = true,
							SameSite = SameSiteMode.None,
							Path = "/",
							Expires = DateTimeOffset.UtcNow.AddDays(1)
						};

						Response.Cookies.Delete("gauth_uid");
						
						Response.Cookies.Append("gauth_uid", vwuserinfo.uid, cookieOptions);


					}
                    else
                    {
                        return BadRequest($"wrong userinfo");
                    }
                }
                else
                {
                    return BadRequest($"wrong userinfo");
                }
                return Ok(userinfo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get List of UserAccounts
        /// </summary>
        /// <returns>List Of UserAccounts</returns>
        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
		public IActionResult MemberInfo(string gubun, string returnUrl)
		{
			try
			{
				var userinfo = new UserInfo();
				var dupinfo = "";
				var sitediv = "GS";

				var token = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]).Parameter;
				var usertokeninfo = _db.UserTokenInfos.Where(s => s.access_token == token).ToList();
				var Valid = usertokeninfo.Any();
				if (Valid)
				{
					var user = usertokeninfo.FirstOrDefault();
					var vw_user_info = _db.VW_USER_INFOs.Where(s => s.uid == user.user_id).ToList();
					Valid = vw_user_info.Any();
					if (Valid)
					{
						var vwuserinfo = vw_user_info.FirstOrDefault();

						userinfo.AccessToken = user.access_token;
						userinfo.UserId = vwuserinfo.uid;
						userinfo.UserEmail = vwuserinfo.umail;
						userinfo.UserName = vwuserinfo.uname;

						dupinfo = vwuserinfo.DupInfo;
					}
					else
					{
						return BadRequest($"wrong userinfo");
					}
				}
				else
				{
					return BadRequest($"wrong accessinfo");
				}

				if (gubun == "search")
				{
					return Ok(userinfo);
				}
				else if (gubun == "modify")
				{                    
                        string CertID = Guid.NewGuid().ToString().ToUpper();
                        var data = new UserCertificationLog()
                        {
                            CertID = CertID,
                            CertType = "NONE",
                            DupInfo = dupinfo,
                            RegDate = DateTime.Now
                        };

                        _db.UserCertificationLogs.Add(data);
                        _db.SaveChanges();
                 
                        return Redirect(_barun_config.GShopUrl.ToString()+"Profile-Modify?CertID=" + CertID + "&ReturnUrl=" + returnUrl + "&SiteDiv=" + sitediv);
                }
				else if (gubun == "leave")
				{
                    string CertID = Guid.NewGuid().ToString().ToUpper();
                    var data = new UserCertificationLog()
                    {
                        CertID = CertID,
                        CertType = "NONE",
                        DupInfo = dupinfo,
                        RegDate = DateTime.Now
                    };

                    _db.UserCertificationLogs.Add(data);
                    _db.SaveChanges();

                    return Redirect(_barun_config.GShopUrl.ToString()+"Secession ?CertID=" + CertID + "&ReturnUrl=" + returnUrl + "&SiteDiv=" + sitediv);
                }
				else
				{
					return Ok();
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}


	}
}
