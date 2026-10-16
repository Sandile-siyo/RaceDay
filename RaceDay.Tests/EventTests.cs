using RaceDay.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class EventTests
    {
        [Fact]
        public void EventName_ShouldBeStored()
        {
            Event raceEvent = new Event
            {
                EventName = "Johannesburg Marathon"
            };

            Assert.Equal(
                "Johannesburg Marathon",
                raceEvent.EventName);
        }

        [Fact]
        public void Distance_ShouldBeStored()
        {
            Event raceEvent = new Event
            {
                Distance = 42.2
            };

            Assert.Equal(42.2, raceEvent.Distance);
        }
    }
}