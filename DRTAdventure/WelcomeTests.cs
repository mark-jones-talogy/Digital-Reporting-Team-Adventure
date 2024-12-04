using DigitalReportingTeamAdventure.Controllers;
using DigitalReportingTeamAdventure.Services;

namespace DRTAdventure
{
    public class WelcomeTests
    {
        [Fact]
        public void display_welcome_test()
        {
            ConsoleController consoleController = new ConsoleController();

            string welcome = ConsoleController.Welcome();

            Assert.Equal("Welcome to The Digital Reporting Team Adventure!", welcome);
        }
    }
}