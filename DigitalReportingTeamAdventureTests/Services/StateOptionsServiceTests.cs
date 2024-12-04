using Microsoft.VisualStudio.TestTools.UnitTesting;
using DigitalReportingTeamAdventure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalReportingTeamAdventure.Services.Services
{
    [TestClass()]
    public class StateOptionsServiceTests
    {
        

        [TestMethod()]
        public void The_Important_States_Have_Not_Been_Done_And_State_4_Is_Selected_Test()
        {
            List<string> expectedStateOptions = new List<string>
            {
                "2 Read all the new messages you have, the team seem quite annoyed"
            };

            StateOptionsService stateOptionsService = new StateOptionsService();

            stateOptionsService.AddPreviousStateToCompletedStates(0);
            stateOptionsService.GetStateOptions(1);

            stateOptionsService.AddPreviousStateToCompletedStates(1);
            stateOptionsService.GetStateOptions(2);

            stateOptionsService.AddPreviousStateToCompletedStates(3);
            stateOptionsService.GetStateOptions(5);

            stateOptionsService.AddPreviousStateToCompletedStates(5);
            List<string> options = stateOptionsService.GetStateOptions(4);

            CollectionAssert.AreEqual(expectedStateOptions, options);
        }

        [TestMethod()]
        public void The_Important_States_Have_Been_Done_And_State_4_Is_Selected_Test()
        {

            List<string> expectedStateOptions = new List<string>
            {
                "1 Make a cup of tea ready for the refinement to start"
            };

            StateOptionsService stateOptionsService = new StateOptionsService();

            stateOptionsService.AddPreviousStateToCompletedStates(0);
            stateOptionsService.GetStateOptions(1);

            stateOptionsService.AddPreviousStateToCompletedStates(1);
            stateOptionsService.GetStateOptions(2);

            stateOptionsService.AddPreviousStateToCompletedStates(2);
            stateOptionsService.GetStateOptions(3);

            stateOptionsService.AddPreviousStateToCompletedStates(3);
            stateOptionsService.GetStateOptions(5);

            stateOptionsService.AddPreviousStateToCompletedStates(5);
            List<string> options = stateOptionsService.GetStateOptions(4);

            CollectionAssert.AreEqual(expectedStateOptions, options);
        }


        [TestMethod()]
        public void The_Important_States_Have_Not_Been_Done_Test()
        {
            StateOptionsService stateOptionsService = new StateOptionsService();

            stateOptionsService.AddPreviousStateToCompletedStates(0);
            stateOptionsService.GetStateOptions(1);

            stateOptionsService.AddPreviousStateToCompletedStates(1);
            stateOptionsService.GetStateOptions(2);

            stateOptionsService.AddPreviousStateToCompletedStates(2);
            stateOptionsService.GetStateOptions(3);

            Assert.IsFalse(stateOptionsService.AreImportantStatesDone());
        }


        [TestMethod()]
        public void The_Important_States_Have_Been_Done_Test()
        {
            StateOptionsService stateOptionsService = new StateOptionsService();

            stateOptionsService.AddPreviousStateToCompletedStates(0);
            stateOptionsService.GetStateOptions(1);

            stateOptionsService.AddPreviousStateToCompletedStates(1);
            stateOptionsService.GetStateOptions(2);

            stateOptionsService.AddPreviousStateToCompletedStates(2);
            stateOptionsService.GetStateOptions(3);

            stateOptionsService.AddPreviousStateToCompletedStates(3);
            stateOptionsService.GetStateOptions(5);

            stateOptionsService.AddPreviousStateToCompletedStates(5);
            stateOptionsService.GetStateOptions(7);

            Assert.IsTrue(stateOptionsService.AreImportantStatesDone());
        }

        [TestMethod()]
        public void Correct_Options_Are_Shown_For_State_1_Test()
        {
            List<string> expectedStateOptions = new List<string>
            {
                "1 Ignore it, someone else can pick it up. Ask Brod for a ticket",
                "2 Check that his code works and give it a tick. Then read Tyler’s message",
                "3 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Read Tyler's message",
                "4 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Ask Brod for a ticket",
                "5 It can wait. Start a PluralSight Course",
                "6 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Start a pluralSight course",
                "7 Answer Sen saying you will review his code. Review it ready to give him a call and go through it when he is online. Have a look at the board to see if there are any tickets you can help with"
            };

            StateOptionsService stateOptionsService = new StateOptionsService();

            stateOptionsService.AddPreviousStateToCompletedStates(0);

            List<string> stateOptions = stateOptionsService.GetStateOptions(1);

            CollectionAssert.AreEqual(expectedStateOptions, stateOptions);
        }

        [TestMethod()]
        public void Correct_Options_Are_shown_For_State_2_After_State_1_And_3_have_been_Done_Test()
        {
            List<string> expectedStateOptions = new List<string>
            {
              "4 Start a PluralSight Course",
              "5 Take a look to see if there is a defect. Then Have a look at the board to see if there are any tickets you can help with"
            };

            StateOptionsService stateOptionsService = new StateOptionsService();

            //Go to state 1
            stateOptionsService.AddPreviousStateToCompletedStates(0);
            stateOptionsService.GetStateOptions(1);

            //Go to state 3
            stateOptionsService.AddPreviousStateToCompletedStates(1);
            stateOptionsService.GetStateOptions(3);

            //Go to state 2
            stateOptionsService.AddPreviousStateToCompletedStates(3);
            List<string> stateOptions = stateOptionsService.GetStateOptions(2);

            CollectionAssert.AreEqual(expectedStateOptions, stateOptions);
        }


        [TestMethod()]
        public void Selecting_Start_State_Option_4_Gives_Correct_State_5_Test()
        {
            StateOptionsService stateOptionsService = new StateOptionsService();
            StateService stateService = new StateService();

            int option = 4;
            int expectedState = 5;

            int state = stateOptionsService.getOptionState(option, stateService.State);

            Assert.AreEqual(expectedState, state);

        }
    }
}