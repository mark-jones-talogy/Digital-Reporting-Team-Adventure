using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalReportingTeamAdventure.Services
{
    public class WelcomeService
    {
        public WelcomeService() { }

        public static string GetWelcome()
        {
            return "Welcome to The Digital Reporting Team Adventure!\n\n" +
                "You are a developer in the UK taking on the adventure of a fun day at work…\n\n";
        }
    }
}
