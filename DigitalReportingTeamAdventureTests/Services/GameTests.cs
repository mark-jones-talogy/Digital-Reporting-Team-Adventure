using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Metadata.Ecma335;
using DigitalReportingTeamAdventure.Services;
using Microsoft.VisualBasic.FileIO;

namespace DigitalReportingTeamAdventureTests.Services
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void Start_State_And_Options_Set_On_Creation_Of_Services_Test()
        {
            List<string> expectedStartOptions = new List<string>
            {
                "1 Read Sen’s message",
                "2 Read Tyler’s message",
                "3 Ask Brod for a ticket",
                "4 Have a look at the board to see if there are any tickets you can help with",
                "5 Start a PluralSight Course"
            };

            string expectedStartState = "You are ready to start a new day at work." +
                " You turn on Teams and you are greeted by a message from Sen and message from Tyler. " +
                "You are not currently working on a ticket. What do you do?";

            StateService stateService = new StateService();
            StateOptionsService stateOptionsService = new StateOptionsService();

            string startState = stateService.StateText;

            List<string> startOptions = stateOptionsService.GetStartStateOptions();

            Assert.AreEqual(expectedStartState, startState);

            CollectionAssert.AreEqual(expectedStartOptions, startOptions);

        }

        [TestMethod()]
        public void Display_New_State_Text_Test()
        {
            int chosenOption = 1;

            string expectedStateText = "Sen is asking for a code review on his change. What do you do?";


            StateService stateService = new StateService();
            StateOptionsService stateOptionsService = new StateOptionsService();
            BadOptionService badOptionService = new BadOptionService();
            GameService gameService = new GameService(stateService, badOptionService);

            string newStateText = gameService.SetNewStateReturnStateText(chosenOption);

            Assert.AreEqual(expectedStateText, newStateText);
        }

        [TestMethod()]
        public void Selecting_State_4_Option_2_Gives_Correct_State_6_Test()
        {
            StateService stateService = new StateService();
            StateOptionsService stateOptionsService = new StateOptionsService();
            BadOptionService badOptionService = new BadOptionService();
            GameService gameService = new GameService(stateService, badOptionService);

            int option = 2;
            int expectedState = 6;

            string text = gameService.SetNewStateReturnStateText(4);

            int state = stateOptionsService.getOptionState(option, stateService.State);

            Console.WriteLine(state);

            Assert.AreEqual(expectedState, state);
        }

        [TestMethod()]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Visit_State_2_Then_State_3_Then_State_2_But_Dont_Change_To_State_2_Test()
        {
            StateService stateService = new StateService();
            StateOptionsService stateOptionsService = new StateOptionsService();
            BadOptionService badOptionService = new BadOptionService();
            GameService gameService = new GameService(stateService, badOptionService);

            //Go to state 2
            int newState = stateOptionsService.getOptionState(2, stateService.State);
            int currentState = stateService.State;
            string newStateText = gameService.SetNewStateReturnStateText(newState);
            stateOptionsService.AddPreviousStateToCompletedStates(currentState);
            stateOptionsService.GetStateOptions(newState);

            //Go to state 3
            int newState2 = stateOptionsService.getOptionState(1, stateService.State);
            int currentState2 = stateService.State;
            string newStateText2 = gameService.SetNewStateReturnStateText(newState2);
            stateOptionsService.AddPreviousStateToCompletedStates(currentState2);
            stateOptionsService.GetStateOptions(newState2);

            //try to go to state 2 again
            int newState3 = stateOptionsService.getOptionState(2, stateService.State);
            int currentState3 = stateService.State;
            string newStateText3 = gameService.SetNewStateReturnStateText(newState3);
            stateOptionsService.AddPreviousStateToCompletedStates(currentState3);
            stateOptionsService.GetStateOptions(newState3);
        }

        [TestMethod()]
        public void End_Game_With_Bad_Option_Test()
        {
            StateService stateService = new StateService();
            StateOptionsService stateOptionsService = new StateOptionsService();
            BadOptionService badOptionService = new BadOptionService();
            GameService gameService = new GameService(stateService,  badOptionService);

            //Go to state 1
            int newState = stateOptionsService.getOptionState(1, stateService.State);
            int currentState = stateService.State;
            string newStateText = gameService.SetNewStateReturnStateText(newState);
            stateOptionsService.AddPreviousStateToCompletedStates(currentState);
            stateOptionsService.GetStateOptions(newState);

            //Now pick a bad option on state 1
            int newState2 = stateOptionsService.getOptionState(2, stateService.State);
            int currentState2 = stateService.State;
            gameService.SetStateBadOption(2, currentState2);
            string newStateText2 = gameService.SetNewStateReturnStateText(newState2);
            stateOptionsService.AddPreviousStateToCompletedStates(currentState2);
            stateOptionsService.GetStateOptions(newState2);

            Assert.IsTrue(stateService.BadOptionChosen);

            //Go to the final state and get the correct text
            string endStateText = gameService.SetNewStateReturnStateText(9);

            Assert.AreEqual("You’ve had an okay day but you can still be a better developer.", endStateText);
        }

        [TestMethod()]
        public void End_Game_With_No_Bad_Option_Test()
        {
            StateService stateService = new StateService();
            StateOptionsService stateOptionsService = new StateOptionsService();
            BadOptionService badOptionService = new BadOptionService();
            GameService gameService = new GameService(stateService, badOptionService);

            string expectedText = "The team agree and clarify the acceptance criteria. You start work on the ticket and it is dreamy allowing you to get stuck into the" +
            " problem rather than trying to second guess what the ticket is really about. The team becomes much more successful and you get a" +
            " promotion. Life feels good and you wonder how you could have ever worked any other way. Congratulations, " +
            "you have successfully completed The Digital Reporting Team Adventure! ";

            //Go to state 1
            int newState = stateOptionsService.getOptionState(1, stateService.State);
            int currentState = stateService.State;
            string newStateText = gameService.SetNewStateReturnStateText(newState);
            stateOptionsService.AddPreviousStateToCompletedStates(currentState);
            stateOptionsService.GetStateOptions(newState);

            //Now pick a good option on state 1
            int newState2 = stateOptionsService.getOptionState(3, stateService.State);
            int currentState2 = stateService.State;
            gameService.SetStateBadOption(3, currentState2);
            string newStateText2 = gameService.SetNewStateReturnStateText(newState2);
            stateOptionsService.AddPreviousStateToCompletedStates(currentState2);
            stateOptionsService.GetStateOptions(newState2);

            Assert.IsFalse(stateService.BadOptionChosen);

            //Go to the final state and get the correct text
            string endStateText = gameService.SetNewStateReturnStateText(9);

            Assert.AreEqual(expectedText, endStateText);
        }

        [TestMethod()]
        public void End_state_Is_Reached_State_6_Test()
        {
            StateService stateService = new StateService();
            BadOptionService badOptionService = new BadOptionService();
            GameService controller = new GameService(stateService, badOptionService);

            controller.SetNewStateReturnStateText(6);

            Assert.IsTrue(stateService.GameEnded);
        }

        [TestMethod()]
        public void End_state_Is_Reached_State_8_Test()
        {
            StateService stateService = new StateService();
            BadOptionService badOptionService = new BadOptionService();
            GameService controller = new GameService(stateService, badOptionService);

            controller.SetNewStateReturnStateText(8);

            Assert.IsTrue(stateService.GameEnded);
        }

        [TestMethod()]
        public void End_state_Is_Reached_State_9_Test()
        {
            StateService stateService = new StateService();
            BadOptionService badOptionService = new BadOptionService();
            GameService controller = new GameService(stateService, badOptionService);

            controller.SetNewStateReturnStateText(9);

            Assert.IsTrue(stateService.GameEnded);
        }
    }
}