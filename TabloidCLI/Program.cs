using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            //A Plesant Greeting#1
            //Main Menu Options #2 My Journal Management
            //Blog Management,Author Management,Post Management,Tag Management,Search by Tag,Exit
            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            while (ui != null)
            {
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }







        }
    }
}
