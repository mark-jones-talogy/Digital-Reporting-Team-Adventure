using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigitalReportingTeamAdventure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalReportingTeamAdventureTests.Services
{
    [TestClass()]
    public class WelcomeServiceTests
    {
        [TestMethod()]
        public void GetWelcomeTest()
        {
            string welcome = WelcomeService.GetWelcome();

            Assert.AreEqual("Welcome to The Digital Reporting Team Adventure!\n\n" +
                "You are a developer in the UK taking on the adventure of a fun day at work…\n\n", welcome);
        }
    }
}