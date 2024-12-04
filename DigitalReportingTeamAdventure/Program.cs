using DigitalReportingTeamAdventure.Services;

StateService stateService = new StateService();
StateOptionsService stateOptionsService = new StateOptionsService();
BadOptionService badOptionService = new BadOptionService();
GameService consoleController = new GameService(stateService, badOptionService);

Console.WriteLine(WelcomeService.GetWelcome());
Console.WriteLine("");
Console.WriteLine(stateService.StateText);
Console.WriteLine("");

List<string> options = stateOptionsService.GetStartStateOptions();
options.ForEach(Console.WriteLine);

while(!stateService.GameEnded)
{
    try
    {
        int option = Convert.ToInt16(Console.ReadLine());

        int newState = stateOptionsService.getOptionState(option, stateService.State);

        int currentState = stateService.State;

        consoleController.SetStateBadOption(option, currentState);

        string newStateText = consoleController.SetNewStateReturnStateText(newState);

        Console.WriteLine("----------------------------------------------------------------------");
        Console.WriteLine(newStateText);
        Console.WriteLine("");

        stateOptionsService.AddPreviousStateToCompletedStates(currentState);

        List<string> stateOptions = stateOptionsService.GetStateOptions(newState);
        stateOptions.ForEach(Console.WriteLine);
    } catch
    {
        Console.WriteLine("Let's try that again.");
    }
}
