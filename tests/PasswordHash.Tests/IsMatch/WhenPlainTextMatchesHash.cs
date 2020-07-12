using Xunit;

namespace PasswordHash.Tests.IsMatch
{
	public class WhenPlainTextMatchesHash
	{
		[Fact]
		public void ItShouldMatch()
		{
			const string plaintext = "Fo0!";
			const int iterations = 1000;
			const int bits = 256;

			var hash = PasswordHash.From(plaintext, iterations, bits / 8);

			Assert.True(hash.IsMatch(plaintext));
		}
	}
}
