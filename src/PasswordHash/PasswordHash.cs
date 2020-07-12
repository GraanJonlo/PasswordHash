using System;
using System.Security.Cryptography;
using System.Text;

namespace PasswordHash
{
	public class PasswordHash
	{
		public PasswordHash(byte[] hash, byte[] salt, uint iterations)
		{
			_hash = hash;
			_salt = salt;
			Iterations = iterations;
		}

		public bool IsMatch(string plaintext)
		{
			var testHash = CreateHash(plaintext, _salt, Iterations, Convert.ToUInt32(_salt.Length));

			var match = true;

			for (int i = 0; i < _hash.Length; i++)
			{
				if (testHash[i] != _hash[i])
				{
					match = false;
				}
			}

			return match;
		}

		public static PasswordHash From(string plaintext, uint iterations, uint hashSize)
		{
			var salt = CreateSalt(hashSize);
			var hash = CreateHash(plaintext, salt, iterations, hashSize);

			return new PasswordHash(hash, salt, iterations);
		}

		public byte[] Hash
		{
			get => (byte[])_hash.Clone();
		}

		public byte[] Salt
		{
			get => (byte[])_salt.Clone();
		}

		public uint Iterations { get; }

		public override string ToString()
		{
			return Convert.ToBase64String(_hash);
		}

		private readonly byte[] _hash;
		private readonly byte[] _salt;

		private static byte[] CreateHash(string plaintext, byte[] salt, uint iterations, uint hashSize)
		{
			var plaintextBytes = Encoding.Unicode.GetBytes(plaintext);

			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(plaintextBytes, salt, Convert.ToInt32(iterations));

			return pbkdf2.GetBytes(Convert.ToInt32(hashSize));
		}

		private static byte[] CreateSalt(uint saltSize)
		{
			RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
			byte[] salt = new byte[saltSize];
			provider.GetBytes(salt);

			return salt;
		}
	}
}
