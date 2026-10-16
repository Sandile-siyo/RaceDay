using RaceDay.Models;
using Xunit;

namespace RaceDay.Tests
{//start of the namespace
    public class EventTests
    {//start of the test class
        [Fact]
        public void EventName_ShouldBeStored()
        {//start of the test method
            Event raceEvent = new Event
            {
                EventName = "Johannesburg Marathon"
            };

            Assert.Equal(
                "Johannesburg Marathon",
                raceEvent.EventName);
        }//end of the test method

        [Fact]
        public void Distance_ShouldBeStored()
        {//start of the test method
            Event raceEvent = new Event
            {
                Distance = 42.2
            };

            Assert.Equal(42.2, raceEvent.Distance);
        }//end of the test method

    }//end of the test class
}//end of the namespace