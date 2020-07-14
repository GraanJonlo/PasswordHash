using System;
using Xunit;

namespace PasswordHash.Tests.Factory
{
	public class WhenSaltIsLessThanEightBytes
	{
		[Fact]
		public void FactoryMethodShouldThrowException()
		{
			const string plaintext = "F0o!";
			const int iterations = 1;

			Assert.Throws<ArgumentException>(() => PasswordHash.From(plaintext, iterations, 7));
		}
	}
}
