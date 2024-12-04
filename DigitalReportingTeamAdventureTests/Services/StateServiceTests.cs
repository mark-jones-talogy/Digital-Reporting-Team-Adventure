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
    public class StateServiceTests
    {
        [TestMethod()]
        public void Get_Current_State_Test()
        {
            StateService stateService = new StateService();

            Assert.AreEqual(0, stateService.State);
        }

        [TestMethod()]
        public void Get_Current_State()
        {
            StateService stateService = new StateService();
            stateService.State = 5;

            Assert.AreEqual(5, stateService.State);
        }
    }
}