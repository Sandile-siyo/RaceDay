using RaceDay.Models;
using Xunit;

namespace RaceDay.Tests
{//start of the namespace
    public class RegistrationTests
    {//start of the class
        [Fact]
        public void RegistrationStatus_ShouldBeConfirmed()
        {//start of the test method
            Registration registration = new Registration
            {//start of the object initializer
                RegistrationStatus = "Confirmed"
            };

            Assert.Equal(
                "Confirmed",
                registration.RegistrationStatus);
        }

        [Fact]
        public void Registration_ShouldStoreEventId()
        {//start of the test method
            Registration registration = new Registration
            {
                EventId = 1
            };

            Assert.Equal(1, registration.EventId);
        }//end of the test method
    }//end of the class
}//end of the namespace