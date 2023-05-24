using System.Security.Cryptography;
using System.Text;

namespace ScissorsGame.Models
{
	internal class SecurityService
	{
		private readonly RandomNumberGenerator _rng;

		public SecurityService()
		{
			_rng = RandomNumberGenerator.Create();
		}

		public string GenerateKey()
		{
			byte[] bytes = new byte[16];
			_rng.GetBytes(bytes);

			return BitConverter.ToString(bytes).Replace("-", "");
		}

		public string GenerateHMAC(string key, string message)
		{
			using var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key));
			byte[] hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(message));

			return BitConverter.ToString(hash).Replace("-", "");
		}
	}
}
