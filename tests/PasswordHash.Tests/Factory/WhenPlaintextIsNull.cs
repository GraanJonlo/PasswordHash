using System;
using Xunit;

namespace PasswordHash.Tests.Factory
{
	public class WhenPlaintextIsNull
	{
		[Fact]
		public void FactoryMethodShouldThrowException()
		{
			const string plaintext = null;
			const int iterations = 1000;
			const int bits = 256;

			Assert.Throws<ArgumentNullException>(() => PasswordHash.From(plaintext, iterations, bits / 8));
		}
	}
}
