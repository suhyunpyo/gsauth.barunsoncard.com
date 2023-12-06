using MemberAuth.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml.Linq;

namespace MemberAuth.Controllers
{
	public class HomeController : Controller
	{
		public HomeController(BarunnConfig barun_config)
		{
			_barun_config = barun_config;
		}
		private readonly BarunnConfig _barun_config;

		public IActionResult Index(string returnUrl = null)
		{			
			Response.Cookies.Delete("gauth_uid");			
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["BarunnfamilyUrl"] = _barun_config.BarunnfamilyUrl;
            return View();
		}


		public async Task<NiceCryptoData> InitCPClient()
		{
			string returnUrl = _barun_config.GSAuthUrl.ToString() + "Member/CPClientReturnSuccess";
			return await GetNiceApiEncData("CPClient", returnUrl, null, "M");
		}


		[HttpGet]
		public async Task<IActionResult> GetNiceApiEncrypt(string authModule)
		{
			string returnUrl = string.Empty;

			try
			{
				if(string.IsNullOrEmpty(authModule)) {
					authModule = "CPClient";
				}
				
				returnUrl = _barun_config.GSAuthUrl.ToString() +  "Member/CPClientReturnSuccess";				
				
				var result =	await GetNiceApiEncData(authModule, returnUrl, null, "M");
				
				if(result==null)
				{
					return BadRequest($"error");
				}
				else
				{
					return Ok(result);
				}
			
			}
			catch (Exception ex)
			{
				throw;
			}
		}


		/// <summary>
		/// 나이스 인증용 암호화
		/// </summary>
		/// <param name="authModule">CPClient or IPIN</param>
		/// <param name="returnUrl"></param>
		/// <param name="receiveData"></param>
		/// <param name="methodType"></param>
		/// <param name="popupYn"></param>
		/// <returns></returns>
		private async Task<NiceCryptoData> GetNiceApiEncData(string authModule, string returnUrl, string? receiveData, string? authType)
		{
			NiceCryptoData resItem = null;
			string privateApiUrl = string.Empty;

			if (authModule == "CPClient")
			{
				privateApiUrl = string.Format("https://privateapi.barunsoncard.com/api/Nice/Encrypt?returnUrl={0}&receiveData={1}&methodType=post&popupYn=Y&authType={2}",
													string.IsNullOrEmpty(returnUrl) ? "" : UrlEncoder.Default.Encode(returnUrl),
													string.IsNullOrEmpty(receiveData) ? "" : UrlEncoder.Default.Encode(receiveData),
													string.IsNullOrEmpty(authType) ? "" : authType);
			}
			else
			{
				privateApiUrl = string.Format("https://privateapi.barunsoncard.com/api/Nice/IpinEncrypt?returnUrl={0}&receiveData={1}&methodType=post&popupYn=Y",
												string.IsNullOrEmpty(returnUrl) ? "" : UrlEncoder.Default.Encode(returnUrl),
												string.IsNullOrEmpty(receiveData) ? "" : UrlEncoder.Default.Encode(receiveData));
			}
			
			using (var request = new HttpRequestMessage())
			{
				request.Method = HttpMethod.Get;
				request.RequestUri = new Uri(privateApiUrl);
				
				HttpClient client = new();

				var response = await client.SendAsync(request);
				response.EnsureSuccessStatusCode();				
				
				var resBytes = await response.Content.ReadAsByteArrayAsync();
				var resString = Encoding.UTF8.GetString(resBytes);
				resItem = JsonSerializer.Deserialize<NiceCryptoData>(resString);

			}

			return resItem;			
		}


	}
}
