using System;
using Xunit;

namespace PasswordHash.Tests.Factory
{
	public class WhenIterationsIsZero
	{
		[Fact]
		public void FactoryMethodShouldThrowException()
		{
			const string plaintext = "F0o!";
			const int iterations = 0;
			const int bits = 256;

			Assert.Throws<ArgumentOutOfRangeException>(() => PasswordHash.From(plaintext, iterations, bits / 8));
		}
	}
}
