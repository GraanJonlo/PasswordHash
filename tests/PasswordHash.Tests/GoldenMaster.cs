using System;
using Xunit;

namespace PasswordHash.Tests
{
	public class GoldenMaster
	{
		[Fact]
		public void GoldenMasterTest()
		{
			var hashAsBase64 = "+MXi0vIqD1LPrPWDkIOmhg==";
			var hash = Convert.FromBase64String(hashAsBase64);
			var salt = Convert.FromBase64String("mXO4M+XEh/0NlsY9VibZlA==");
			var iterations = 100u;

			var sut = new PasswordHash(hash, salt, iterations);

			Assert.Equal(hash, sut.Hash);
			Assert.Equal(salt, sut.Salt);
			Assert.Equal(iterations, sut.Iterations);
			Assert.Equal(hashAsBase64, sut.ToString());
		}
	}
}
