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

		public static PasswordHash From(string plaintext, uint iterations, uint hashSizeInBytes)
		{
			if (plaintext == null)
			{
				throw new ArgumentNullException(nameof(plaintext), "Plaintext cannot be null");
			}

			if (iterations == 0u)
			{
				throw new ArgumentOutOfRangeException(nameof(iterations), "Iterations must be greater than zero");
			}

			if (hashSizeInBytes < 8)
			{
				throw new ArgumentException("Salt is not at least eight bytes", nameof(hashSizeInBytes));
			}

			var salt = CreateSalt(hashSizeInBytes);
			var hash = CreateHash(plaintext, salt, iterations, hashSizeInBytes);

			return new PasswordHash(hash, salt, iterations);
		}

		public bool IsMatch(string plaintext)
		{
			var testHash = CreateHash(plaintext, _salt, Iterations, Convert.ToUInt32(_salt.Length));

			var match = true;

			// This is deliberately NOT optimised
			for (int i = 0; i < _hash.Length; i++)
			{
				if (testHash[i] != _hash[i])
				{
					match = false;
				}
			}

			return match;
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

		private static byte[] CreateHash(string plaintext, byte[] salt, uint iterations, uint hashSizeInBytes)
		{
			var plaintextBytes = Encoding.Unicode.GetBytes(plaintext);

			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(plaintextBytes, salt, Convert.ToInt32(iterations));

			return pbkdf2.GetBytes(Convert.ToInt32(hashSizeInBytes));
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
