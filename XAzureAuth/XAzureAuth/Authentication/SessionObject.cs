using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XAzureAuth.Authentication
{
	public class SessionObject
	{
		public string User { get; set; }

		public string SAMLToken { get; set; }
	}
}
