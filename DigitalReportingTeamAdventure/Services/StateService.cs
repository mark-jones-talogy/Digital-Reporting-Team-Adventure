using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalReportingTeamAdventure.Services
{
    public class StateService
    {
        const int startStatePosition = 0;

        private int state;
        private string? stateText;
        private List<int> endStates = new List<int>{6,8,9};
        private bool badOptionChosen = false;
        private bool gameEnded = false;
        private List<string> stateTexts = new List<string>
        {
            "You are ready to start a new day at work. You turn on Teams and you are greeted by a message from Sen and message from Tyler." +
            " You are not currently working on a ticket. What do you do?",

            "Sen is asking for a code review on his change. What do you do?",

            "Tyler has flagged that one of the automated tests on your last ticket is failing and has asked you to take a look. What do you do?",

            "Brod says he will put a refinement in this afternoon. What do you do?",

            "You spend a pleasant few hours on PluralSight learning something new which will help the team. " +
            "You decide to do a microlearning on it when it is your turn next. What do you do next?",

            "There are no tickets that need your attention on the board. What do you do?",

            "Everyone is asking what you are up to and if you have had a chance to look at Sen’s and Tyler’s messages? You have annoyed the team." +
            " Mark pulls the smell face at you.",

            "The refinement is really good. But the ticket still has vague acceptance criteria on it. What do you do?",

            "You get stuck in development hell on the ticket and question is it even worth living anymore? You get so upset you quit your" +
            " job and become a goat herder in the Andes. You are known as the lonely goat herder for the rest of your life.",

            "The team agree and clarify the acceptance criteria. You start work on the ticket and it is dreamy allowing you to get stuck into the" +
            " problem rather than trying to second guess what the ticket is really about. The team becomes much more successful and you get a" +
            " promotion. Life feels good and you wonder how you could have ever worked any other way. Congratulations, " +
            "you have successfully completed The Digital Reporting Team Adventure! ",

            "You’ve had an okay day but you can still be a better developer."
        };

        public StateService()
        {
            this.State = startStatePosition;
        }

        public int State 
        {  
            get { 
                return this.state;
            }
            set
            {
                if (endStates.Contains(value))
                {
                    this.gameEnded = true;
                }
                this.state = value;
                this.StateText = this.stateTexts[value];
            }
        }

        public string StateText
        {
            get
            {
                if (this.State == 9 && this.badOptionChosen)
                {
                    return this.stateTexts[10];
                }

                return stateText;
            }
            private set
            {
                this.stateText = value;
            }
        }

        public bool GameEnded
        {
            get 
            { 
                return this.gameEnded; 
            }
            private set
            {
                this.gameEnded = value;
            }
        }

        public bool BadOptionChosen
        {
            get
            {
                return this.badOptionChosen;
            }
            set
            {
                this.badOptionChosen = value;
            }
        }
    }
}
