using RaceDay.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class RegistrationTests
    {
        [Fact]
        public void RegistrationStatus_ShouldBeConfirmed()
        {
            Registration registration = new Registration
            {
                RegistrationStatus = "Confirmed"
            };

            Assert.Equal(
                "Confirmed",
                registration.RegistrationStatus);
        }

        [Fact]
        public void Registration_ShouldStoreEventId()
        {
            Registration registration = new Registration
            {
                EventId = 1
            };

            Assert.Equal(1, registration.EventId);
        }
    }
}