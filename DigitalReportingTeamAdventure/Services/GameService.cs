using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

namespace DigitalReportingTeamAdventure.Services
{
    public class GameService
    {
        private StateService stateService;
        private StateOptionsService stateOptionsService;
        private BadOptionService badOptionService;

        public GameService(StateService stateService, BadOptionService badOptionService)
        {
            this.stateService = stateService;
            this.badOptionService = badOptionService;
        }

        public string SetNewStateReturnStateText(int chosenOption)
        {
            stateService.State = chosenOption;

            return stateService.StateText;
        }

        public void SetStateBadOption(int option, int currentState)
        {
            if (badOptionService.BadOption(option, currentState))
            {
                stateService.BadOptionChosen = true;
            }
        }

    }
}
