using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalReportingTeamAdventure.Services;

namespace DRTAdventure
{
    public class StateServiceTests
    {
        public void get_current_state_test()
        {
            StateService stateService = new StateService();
            int state = stateService.State;

            Assert.Equal('1', state);


        }
    }
}
