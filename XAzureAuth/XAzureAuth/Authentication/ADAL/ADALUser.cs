using System;
using Newtonsoft.Json;

namespace XAzureAuth.Authentication
{
    public class ADALUser
    {
		[JsonProperty("immutableId")]
		public string ImmutableId { get; set; }

		[JsonProperty("employeeId")]
		public string EmployeeId { get; set; }

		[JsonProperty("givenName")]
		public string GivenName { get; set; }

		[JsonProperty("surname")]
		public string Surname { get; set; }

		[JsonProperty("displayName")]
		public string DisplayName { get; set; }

		[JsonProperty("userPrincipalName")]
		public string UserPrincipalName { get; set; }

		[JsonProperty("mailNickname")]
		public string MailNickname { get; set; }

		[JsonProperty("mail")]
		public string Mail { get; set; }

		[JsonProperty("companyName")]
		public string CompanyName { get; set; }

		[JsonProperty("jobTitle")]
		public string JobTitle { get; set; }

		[JsonProperty("userType")]
		public string UserType { get; set; }
    }
}
