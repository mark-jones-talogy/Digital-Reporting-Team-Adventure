using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;

namespace DigitalReportingTeamAdventure.Services
{
    public class StateOptionsService
    {
        private List<int> completedStates = new List<int>();
        private readonly List<int> importantStates = new List<int> {1,2,3,5};
        private bool importantStatesDone = false;
        private List<List<int>> stateOptions = new List<List<int>>
        {
            new List<int> {1,2,3,5,4},
            new List<int> {3,2,2,3,4,4,5},
            new List<int> {3,1,3,4,5},
            new List<int> {1,2,5,4},
            new List<int> {7,6},
            new List<int> {1,2,3,4},
            new List<int> {},
            new List<int> {8,9},
            new List<int> {},
            new List<int> {},
            new List<int> {}
        };

        private List<List<string>> stateOptionsText = new List<List<string>>
        {
            new List<string>
            {
                "1 Read Sen’s message",
                "2 Read Tyler’s message",
                "3 Ask Brod for a ticket",
                "4 Have a look at the board to see if there are any tickets you can help with",
                "5 Start a PluralSight Course"
            },
            new List<string>
            {
                "1 Ignore it, someone else can pick it up. Ask Brod for a ticket",
                "2 Check that his code works and give it a tick. Then read Tyler’s message",
                "3 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Read Tyler's message",
                "4 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Ask Brod for a ticket",
                "5 It can wait. Start a PluralSight Course",
                "6 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Start a pluralSight course",
                "7 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Have a look at the board to see if there are any tickets you can help with"
            },
            new List<string>
            {
                "1 Ignore it. Automated tests aren’t your problem. Ask Brod for a ticket",
                "2 Take a look and see if there is a defect. Then read Sen’s message",
                "3 Take a look to see if there is a defect. Then ask Brod for a ticket",
                "4 Start a PluralSight Course",
                "5 Take a look to see if there is a defect. Then Have a look at the board to see if there are any tickets you can help with"
            },
            new List<string>
            {
                "1 Read Sen’s message",
                "2 Read Tyler’s message",
                "3 Have a look at the board to see if there are any tickets you can help with",
                "4 Start a PluralSight Course"
            },
            new List<string>
            {
                "1 Make a cup of tea ready for the refinement to start",
                "2 Read all the new messages you have, the team seem quite annoyed",
            },
            new List<string>
            {
                "1 Read Sen’s message",
                "2 Read Tyler’s message",
                "3 Ask Brod for a ticket",
                "4 Start a PluralSight Course",
            },
            new List<string>
            {

            },
            new List<string>
            {
                "1 Just go with it, you can work out what it is supposed to do later.\r\n",
                "2 Ask that the acceptance criteria be improved."
            },
            new List<string>
            {

            },
            new List<string>
            {

            },
            new List<string>
            {

            }
        };

        public List<string> GetStateOptions(int state)
        {
            if (state == 4)
            {
                return this.DealWithState4();
            }

            List<string> stateOptionsText = this.stateOptionsText[state];

            List<int?> stateOptions = this.stateOptions[state].Select(option => completedStates.Contains(option) ? null : (int?)option).ToList();

            var filteredText = stateOptions.Zip(stateOptionsText, (option, text) => new { option, text })
                                       .Where(x => x.option != null)
                                       .Select(x => x.text)
                                       .ToList();
            return filteredText;
        }

        public void AddPreviousStateToCompletedStates(int state)
        {
            this.completedStates.Add(state);
        }

        public List<string> GetStartStateOptions()
        {
            return this.stateOptionsText[0];
        }

        public int getOptionState(int option, int currentState)
        {
            List<int> options = stateOptions[currentState];

            if (option >= 0 && option <= options.Count)
            {
                int newState = options[option - 1];

                if (!this.completedStates.Contains(newState))
                {
                    return newState;
                }
            }
            throw new InvalidOperationException("Invalid choice");
        }

        public bool AreImportantStatesDone()
        {
            if (importantStates.All(state => completedStates.Contains(state)))
            {
                this.importantStatesDone = true;
                return true;
            }
            return false;
        }

        private List<string> DealWithState4()
        {
            List<string> options = this.stateOptionsText[4];

            if (this.AreImportantStatesDone())
            {
                options.RemoveAt(1);

                return options;
            }
            options.RemoveAt(0);
            return options;
        }
    }
}
