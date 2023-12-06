using MemberAuth.Common;
using MemberAuth.Data;
using MemberAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http.Extensions;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MemberAuth.Controllers.Member
{
	public class MemberController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly BarunnConfig _barun_config;

		public MemberController(BarunnConfig configuration, ApplicationDbContext db)
		{
            _barun_config = configuration;
			_db = db;
		}
		

		/// <summary>
		/// 핸드폰 인증 성공 
		/// </summary>
		/// <param name="EncodeData"></param>
		/// <param name="param_r1"></param>
		/// <returns></returns>
		[Route("Member/CPClientReturnSuccess")]
		[AllowAnonymous]
		public IActionResult CPClientReturnSuccess(string enc_data, string integrity_value, string token_version_id)
		{
			var cpClient = new NiceCryptoData(){ 
				EncData = UrlEncoder.Default.Encode(enc_data),
				IntegrityValue = UrlEncoder.Default.Encode(integrity_value),	
				TokenVersionId = token_version_id	
			};

            

            var result = new NiceApiViewModel()
			{
				CPCleintEncData = cpClient
			};			
			
			return View(result);
		}



		/// <summary>
		/// 회원 수정
		/// </summary>
		/// <returns>List Of UserAccounts</returns>
		[Route("Member/Update")]

		public IActionResult Member_Update()
		{
			string CertID = string.Empty;
			string gauth_uid = string.Empty;
            string gauth_dupinfo = string.Empty;

            try
			{
                gauth_uid = Request.Cookies["gauth_uid"].ToString();
                if (!string.IsNullOrEmpty(gauth_uid))
                {
                    gauth_dupinfo = (from User in _db.VW_USER_INFOs.Where(x => x.uid.Equals(gauth_uid)) select User.DupInfo).FirstOrDefault();
                }

				if (!string.IsNullOrEmpty(gauth_dupinfo))
				{
					CertID = Guid.NewGuid().ToString().ToUpper();
					var data = new UserCertificationLog()
					{
						CertID = CertID,
						CertType = "NONE",
						DupInfo = gauth_dupinfo,
						RegDate = DateTime.Now
					};

					_db.UserCertificationLogs.Add(data);
					_db.SaveChanges();

					return Redirect(_barun_config.BarunnfamilyUrl.ToString()+"Profile-Modify?CertID=" + CertID + "&ReturnUrl=&SiteDiv=GS");
				}
                else
                {
                    return Redirect(_barun_config.GShopUrl.ToString());
                }

            }
            catch (Exception ex)
			{
				return Redirect(_barun_config.GShopUrl.ToString());
			}
			
		}

		/// <summary>
		/// 회원 탈퇴
		/// </summary>
		/// <returns>List Of UserAccounts</returns>
		[Route("Member/Leave")]
		public IActionResult Member_Leave()
		{
            string CertID = string.Empty;
            string gauth_dupinfo = string.Empty;
            string gauth_uid = string.Empty;

            try
            {
                gauth_uid = Request.Cookies["gauth_uid"].ToString();
                if (!string.IsNullOrEmpty(gauth_uid))
                {
                    gauth_dupinfo = (from User in _db.VW_USER_INFOs.Where(x => x.uid.Equals(gauth_uid)) select User.DupInfo).FirstOrDefault();
                }

                if (!string.IsNullOrEmpty(gauth_dupinfo))
                {
                    CertID = Guid.NewGuid().ToString().ToUpper();
                    var data = new UserCertificationLog()
                    {
                        CertID = CertID,
                        CertType = "NONE",
                        DupInfo = gauth_dupinfo,
                        RegDate = DateTime.Now
                    };

                    _db.UserCertificationLogs.Add(data);
                    _db.SaveChanges();

                    return Redirect(_barun_config.BarunnfamilyUrl.ToString()+"Secession?CertID=" + CertID + "&ReturnUrl=&SiteDiv=GS");
                }
				else
				{
                    return Redirect(_barun_config.GShopUrl.ToString());
                }
               
			}
			catch (Exception ex)
			{
				return Redirect(_barun_config.GShopUrl.ToString());
				
			}


			
		}

		/// <summary>
		/// 회원 로그아웃
		/// </summary>
		/// <returns>List Of UserAccounts</returns>
		[Route("Member/LogOut")]
		public string LogOut()
		{
			string ReturnStr = "";

			try
			{
				Response.Cookies.Delete("gauth_uid");
				ReturnStr = "SUCCESS";
			}
			catch (Exception ex)
			{
				ReturnStr = "ERROR";
			}


			return ReturnStr;

		}


		/// <summary>
		/// 나이스 인증 복호화
		/// </summary>
		/// <param name="authModule">CPClient or IPIN</param>
		/// <param name="returnUrl"></param>
		/// <param name="receiveData"></param>
		/// <param name="methodType"></param>
		/// <param name="popupYn"></param>
		/// <returns></returns>
		private async Task<NiceApiResponseData> GetNiceApiDecData(NiceCryptoData item)
		{
			NiceApiResponseData resItem = null;
			string privateApiUrl = string.Empty;

			privateApiUrl = string.Format("https://privateapi.barunsoncard.com/api/Nice/Decrypt?tokenVersionId={0}&encData={1}&integrityValue={2}",
						item.TokenVersionId,
						item.EncData.Contains("%") ? item.EncData : UrlEncoder.Default.Encode(item.EncData),
						item.IntegrityValue.Contains("%") ? item.IntegrityValue : UrlEncoder.Default.Encode(item.IntegrityValue));

			try
			{
				using (var request = new HttpRequestMessage())
				{
					request.Method = HttpMethod.Get;
					request.RequestUri = new Uri(privateApiUrl);

					HttpClient client = new();

					var response = await client.SendAsync(request);
					response.EnsureSuccessStatusCode();

					var resBytes = await response.Content.ReadAsByteArrayAsync();
					var resString = Encoding.UTF8.GetString(resBytes);
					resItem = JsonSerializer.Deserialize<NiceApiResponseData>(resString);
				}
			} 
			catch (Exception ex) {
				resItem = null;
			}	

			return resItem;
		}

		/// <summary>
		/// 아이디 찾기로 유입
		/// </summary>
		/// <param name="IntegrityValue"></param>
		/// <param name="EncData"></param>
		/// <param name="TokenVersionId"></param>		
		/// <returns></returns>
		[HttpPost]
		[Route("Member/Member_Chk")]
		[AllowAnonymous] // 인증되지 않은 사용자도 접근 가능
		public async Task<IActionResult> Member_Chk(string IntegrityValue, string EncData, string TokenVersionId)
		{
			string User_Id = "";
			EncData = System.Net.WebUtility.UrlDecode(EncData);
			TokenVersionId = System.Net.WebUtility.UrlDecode(TokenVersionId);

			var item = new NiceCryptoData()
			{
				IntegrityValue= IntegrityValue,
				EncData= EncData,
				TokenVersionId=TokenVersionId
			};

			var result =  await GetNiceApiDecData(item);

			try
			{
				if (result != null)
				{
					User_Id = (from User in _db.VW_USER_INFOs.Where(x => x.DupInfo.Equals(result.di)) select User.uid).FirstOrDefault();
				}

			}
			catch
			{

			}

			int User_YN = !string.IsNullOrEmpty(User_Id) ? 1 : 0;

			if (string.IsNullOrEmpty(User_Id))
			{
				return LocalRedirect("/Member/ID_Search?User_ID=F");

			}
			else
			{
				return LocalRedirect("/Member/ID_Search?User_ID=" + User_Id);
			}

		}



		[Route("Member/ID_Search")]
		[AllowAnonymous] // 인증되지 않은 사용자도 접근 가능
		public IActionResult ID_Search(string User_ID)
		{
			if (User_ID.Equals("F"))
			{
				ViewData["Auth_User_ID"] = User_ID;
			}
			else
			{
				string decode = System.Net.WebUtility.UrlDecode(User_ID);


				ViewData["Auth_User_ID"] = User_ID;
			}

			ViewData["GSAuthUrl"] = _barun_config.GSAuthUrl.ToString();


			return View();
		}


		[Route("Member/PW_Search")]		
		[AllowAnonymous] // 인증되지 않은 사용자도 접근 가능
		public IActionResult PW_Search(string CertID)
		{
            ViewData["CertID"] = !string.IsNullOrEmpty(CertID) ? CertID : "";

            ViewData["GSAuthUrl"] = _barun_config.GSAuthUrl.ToString();
            ViewData["BarunnfamilyUrl"] = _barun_config.BarunnfamilyUrl.ToString();           

            return View();
		}


        /// <summary>
        /// 비밀번로 찾기로 유입
        /// </summary>
        /// <param name="IntegrityValue"></param>
        /// <param name="EncData"></param>
        /// <param name="TokenVersionId"></param>        
        /// <param name="SearchID_Result"></param>
        /// <returns></returns>
        [HttpPost]
		[Route("Member/Member_Pw_Chk")]

		[AllowAnonymous] // 인증되지 않은 사용자도 접근 가능		
		public async Task<IActionResult> Member_Pw_Chk(string IntegrityValue, string EncData, string TokenVersionId, string SearchID_Result)
		{
			string User_DUPINFO = string.Empty;
			string CertID = string.Empty;

			EncData = System.Net.WebUtility.UrlDecode(EncData);
			TokenVersionId = System.Net.WebUtility.UrlDecode(TokenVersionId);

			var item = new NiceCryptoData()
			{
				IntegrityValue = IntegrityValue,
				EncData = EncData,
				TokenVersionId = TokenVersionId
			};

			var result = await GetNiceApiDecData(item);
			
            try
			{
				if (result != null)
				{
					var query = (from User in _db.VW_USER_INFOs.Where(x => x.DupInfo.Equals(result.di) && x.uid.Equals(SearchID_Result)) select User.DupInfo);
					User_DUPINFO = await query.FirstOrDefaultAsync();

                }
				
				if(!string.IsNullOrEmpty(User_DUPINFO))
				{
					CertID = Guid.NewGuid().ToString().ToUpper();
					var data = new UserCertificationLog()
					{
						CertID = CertID,
						CertType = "NONE",
						DupInfo = User_DUPINFO,
						RegDate = DateTime.Now
					};

					_db.UserCertificationLogs.Add(data);
					await _db.SaveChangesAsync();
				}
			}
			catch
			{

			}
			
			CertID = string.IsNullOrEmpty(CertID) ? "F" : CertID;
			return LocalRedirect("/Member/PW_Search?CertID=" + CertID);

		}

	}
}
