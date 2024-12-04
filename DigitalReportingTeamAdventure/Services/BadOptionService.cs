using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalReportingTeamAdventure.Services
{
    public class BadOptionService
    {
        private List<List<int>> badOptions = new List<List<int>>
        {
            new List<int> {},
            new List<int> {1,2,5},
            new List<int> {1},
            new List<int> {},
            new List<int> {},
            new List<int> {},
            new List<int> {},
            new List<int> {},
            new List<int> {},
            new List<int> {},
            new List<int> {}
        };

        public bool BadOption(int option, int state)
        {
            if (badOptions[state].Contains(option))
            {
                return true;
            }
            return false;
        }
    }
}
