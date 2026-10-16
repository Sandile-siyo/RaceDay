using Xunit;

namespace RaceDay.Tests
{//start of the namespace
    public class AuthTests
    {//start of the test class
        [Fact]
        public void ValidPassword_ShouldPass()
        {//start of the test method
            string password = "Password123";

            string hash =
                BCrypt.Net.BCrypt.HashPassword(password);

            bool result =
                BCrypt.Net.BCrypt.Verify(password, hash);

            Assert.True(result);
        }

        [Fact]
        public void InvalidPassword_ShouldFail()
        {//start of the test method
            string password = "Password123";

            string hash =
                BCrypt.Net.BCrypt.HashPassword(password);

            bool result =
                BCrypt.Net.BCrypt.Verify("WrongPassword", hash);

            Assert.False(result);
        }//end of the test method
    }//end of the test class
}//end of the namespace