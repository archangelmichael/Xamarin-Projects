using System.Diagnostics;
using Foundation;
using Security;

namespace KeychainX.iOS
{
	public interface IKeychain
	{
		bool AddData(string key, string value);

		string GetData(string key);

		bool UpdateData(string key, string value);

		bool RemoveData(string key);
	}

	public sealed class Keychain : IKeychain
	{
		public const string CERTIFICATE_KEY = "certificate-key";
		public const string CERTIFICATE_GROUP = "P4JVA28GY3.com.oryx.shared.keychain";
		public const string CERTIFICATE_SERVICE = "UConnect";

		string KeychainService { get; set; }
		string KeychainGroup { get; set; }

		public Keychain(string service,
						string group)
		{
			KeychainService = service;
			KeychainGroup = group;
		}

		public bool AddData(string key, string value)
		{
			var s = new SecRecord(SecKind.GenericPassword)
			{
				ValueData = NSData.FromString(value),
				Generic = NSData.FromString(key),
				Account = key,
				AccessGroup = KeychainGroup
			};

			var err = SecKeyChain.Add(s);
			Debug.WriteLine("[Keychain] AddData result: " + err);

			return err == SecStatusCode.Success;
		}

		public string GetData(string key)
		{
			var rec = new SecRecord(SecKind.GenericPassword)
			{
				Generic = NSData.FromString(key),
				Account = key,
				AccessGroup = KeychainGroup
			};

			SecStatusCode res;
			var match = SecKeyChain.QueryAsRecord(rec, out res);

			Debug.WriteLine("[Keychain] GetData result: " + res);

			if (res == SecStatusCode.Success)
				return match.ValueData.ToString();

			return null;
		}

		public bool UpdateData(string key, string value)
		{
			if (RemoveData(key))
				return AddData(key, value);

			return false;
		}

		public bool RemoveData(string key)
		{
			var rec = new SecRecord(SecKind.GenericPassword)
			{
				Generic = NSData.FromString(key),
				Account = key,
				AccessGroup = KeychainGroup
			};

			var err = SecKeyChain.Remove(rec);

			Debug.WriteLine("[Keychain] RemoveData result: " + err);

			return err == SecStatusCode.Success;
		}
	}
}
