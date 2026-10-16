using Xunit;

namespace RaceDay.Tests
{
    public class AuthTests
    {
        [Fact]
        public void ValidPassword_ShouldPass()
        {
            string password = "Password123";

            string hash =
                BCrypt.Net.BCrypt.HashPassword(password);

            bool result =
                BCrypt.Net.BCrypt.Verify(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void InvalidPassword_ShouldFail()
        {
            string password = "Password123";

            string hash =
                BCrypt.Net.BCrypt.HashPassword(password);

            bool result =
                BCrypt.Net.BCrypt.Verify("WrongPassword", hash);

            Assert.False(result);
        }
    }
}