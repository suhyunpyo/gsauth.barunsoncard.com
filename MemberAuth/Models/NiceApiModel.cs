using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MemberAuth.Models
{
	public class NiceApiViewModel 
	{
		public NiceCryptoData CPCleintEncData { get; set; }
		public NiceCryptoData IPINEncData { get; set; }
	}

	/// <summary>
	/// Nice Api 암호화 응답 데이터
	/// </summary>
	public class NiceCryptoData
	{
		/// <summary>
		/// 사용한 토큰의 버전 아이디. 복호화시 필요한 키값
		/// </summary>
		[JsonPropertyName("tokenVersionId")]
		public string TokenVersionId { get; set; }
		/// <summary>
		/// 암호화된 데이터
		/// </summary>
		[JsonPropertyName("encData")]
		public string EncData { get; set; }
		/// <summary>
		/// 암호화 무결성 값
		/// </summary>
		[JsonPropertyName("integrityValue")]
		public string IntegrityValue { get; set; }
	}


	/// <summary>
	/// Nice CPClient 복호화 응답 데이터
	/// </summary>
	public class NiceApiResponseData
	{
		/// <summary>
		/// 결과코드 result_code가 성공(0000)일 때만 전달
		/// </summary>
		[JsonPropertyName("resultcode")]
		public string? ResultCode { get; set; }

		/// <summary>
		/// [필수] 서비스 요청 고유 번호
		/// </summary>
		[JsonPropertyName("requestno")]
		public string? RequestNo { get; set; }

		/// <summary>
		/// 암호화 일시(YYYYMMDDHH24MISS)
		/// </summary>
		[JsonPropertyName("enctime")]
		public string? EncTime { get; set; }

		/// <summary>
		/// [필수] 암호화토큰요청 API에 응답받은 site_code
		/// </summary>
		[JsonPropertyName("sitecode")]
		public string? SiteCode { get; set; }

		/// <summary>
		/// 응답고유번호
		/// </summary>
		[JsonPropertyName("responseno")]
		public string? ResponseNo { get; set; }

		/// <summary>
		/// 인증수단
		/// M	휴대폰인증		
		/// C 카드본인확인
		/// X 공동인증서
		/// F 금융인증서
		/// S PASS인증서
		/// </summary>
		[JsonPropertyName("authtype")]
		public string? AuthType { get; set; }

		/// <summary>
		/// 이름
		/// </summary>
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		/// <summary>
		/// UTF8로 URLEncoding된 이름 값
		/// </summary>
		[JsonPropertyName("utf8_name")]
		public string? utf8_name { get; set; }

		/// <summary>
		/// 생년월일 8자리
		/// </summary>
		[JsonPropertyName("birthdate")]
		public string? BirthDate { get; set; }

		/// <summary>
		/// 성별 0:여성, 1:남성
		/// </summary>
		[JsonPropertyName("gender")]
		public string? Gender { get; set; }

		/// <summary>
		/// 내외국인 0:내국인, 1:외국인
		/// </summary>
		[JsonPropertyName("nationalinfo")]
		public string? NationalInfo { get; set; }

		/// <summary>
		/// 이통사 구분(휴대폰 인증 시)
		/// 1	SK텔레콤		
		/// 2	KT		
		/// 3	LGU+		
		/// 5	SK텔레콤 알뜰폰		
		/// 6	KT 알뜰폰		
		/// 7	LGU+ 알뜰폰
		/// </summary>
		[JsonPropertyName("mobileco")]
		public string? MobileCo { get; set; }

		/// <summary>
		/// 휴대폰 번호(휴대폰 인증 시)
		/// </summary>
		[JsonPropertyName("mobileno")]
		public string? MobileNo { get; set; }

		/// <summary>
		/// 개인 식별 코드(CI)
		/// </summary>
		[JsonPropertyName("ci")]
		public string? ci { get; set; }

		/// <summary>
		/// 개인 식별 코드(di)
		/// </summary>
		[JsonPropertyName("di")]
		public string? di { get; set; }

		/// <summary>
		/// 사업자번호(법인인증서 인증시)
		/// </summary>
		[JsonPropertyName("businessno")]
		public string? BusinessNo { get; set; }

		/// <summary>
		/// 인증 후 전달받을 데이터 세팅 (요청값 그대로 리턴)
		/// </summary>
		[JsonPropertyName("receivedata")]
		public string ReceiveData { get; set; } = "";
	}

	/// <summary>
	/// Nice IPIN 복호화 응답 데이터
	/// </summary>
	public class NiceIPinApiResponseData
	{
		/// <summary>
		/// 결과코드 result_code가 성공(1)일 때만 전달
		/// </summary>
		[JsonPropertyName("resultcode")]
		public string? ResultCode { get; set; }

		/// <summary>
		/// [필수] 서비스 요청 고유 번호
		/// </summary>
		[JsonPropertyName("requestno")]
		public string? RequestNo { get; set; }

		/// <summary>
		/// 암호화 일시(YYYYMMDDHH24MISS)
		/// </summary>
		[JsonPropertyName("enctime")]
		public string? EncTime { get; set; }

		/// <summary>
		/// [필수] 암호화토큰요청 API에 응답받은 site_code
		/// </summary>
		[JsonPropertyName("sitecode")]
		public string? SiteCode { get; set; }

		/// <summary>
		/// 인증서버 아이피
		/// </summary>
		[JsonPropertyName("ipaddr")]
		public string? Ipaddr { get; set; }


		/// <summary>
		/// 이름
		/// </summary>
		[JsonPropertyName("name")]
		public string? Name { get; set; }

		/// <summary>
		/// UTF8로 URLEncoding된 이름 값
		/// </summary>
		[JsonPropertyName("utf8_name")]
		public string? utf8_name { get; set; }

		/// <summary>
		/// 아이핀 번호
		/// </summary>
		[JsonPropertyName("vnumber")]
		public string? VNumber { get; set; }

		/// <summary>
		/// 성별 0:여성, 1:남성
		/// </summary>
		[JsonPropertyName("gendercode")]
		public string? Gender { get; set; }

		/// <summary>
		/// 생년월일 8자리
		/// </summary>
		[JsonPropertyName("birthdate")]
		public string? BirthDate { get; set; }

		/// <summary>
		/// 내외국인 0:내국인, 1:외국인
		/// </summary>
		[JsonPropertyName("nationalinfo")]
		public string? NationalInfo { get; set; }

		/// <summary>
		/// 연령 코드
		/// </summary>
		[JsonPropertyName("agecode")]
		public string? AgeCode { get; set; }

		/// <summary>
		/// 개인 식별 코드 (CI)
		/// </summary>
		[JsonPropertyName("coinfo1")]
		public string? ConInfo1 { get; set; }

		/// <summary>
		/// 개인 식별 코드 (CI2)
		/// </summary>
		[JsonPropertyName("coinfo2")]
		public string? ConInfo2 { get; set; }

		/// <summary>
		/// 개인 식별 코드(di)
		/// </summary>
		[JsonPropertyName("dupinfo")]
		public string? DupInfo { get; set; }

		/// <summary>
		/// CI 버전 정보
		/// </summary>
		[JsonPropertyName("ciupdate")]
		public string? CiUpdate { get; set; }

		/// <summary>
		/// 인증 후 전달받을 데이터 세팅 (요청값 그대로 리턴)
		/// </summary>
		[JsonPropertyName("receivedata")]
		public string ReceiveData { get; set; } = "";
	}
}
