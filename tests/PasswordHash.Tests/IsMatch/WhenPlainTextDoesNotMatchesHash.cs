using Xunit;

namespace PasswordHash.Tests.IsMatch
{
	public class WhenPlainTextDoesNotMatchesHash
	{
		[Fact]
		public void ItShouldFailToMatch()
		{
			const string plaintext = "Fo0!";
			const string incorrectPlaintext = "b4r1";
			const int iterations = 1000;
			const int bits = 256;

			var hash = PasswordHash.From(plaintext, iterations, bits / 8);

			Assert.False(hash.IsMatch(incorrectPlaintext));
		}
	}
}
