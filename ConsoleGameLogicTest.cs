// See https://aka.ms/new-console-template for more information
Console.WriteLine("Initializing console test program for Roguelije game logic...");

string cmd;
// Enter command loop
commandLoop();


void commandLoop() {
    while(true) {
        Console.Write("\n> ");
        cmd = Console.ReadLine();
        switch(cmd) 
        {
            case "exit":
            case "quit":
                Console.WriteLine("User exited.");
                return;
            case "currentrun":
                debugCurrentRun();
                break;
            default:
                Console.WriteLine("Unknown command '"+cmd+"'");
                break;
        }
    }
}

void debugCurrentRun() {
    Console.WriteLine("Debugging CurrentRun info:");
    Console.WriteLine("Lives="+CurrentRun.Lives);
    Console.WriteLine("Money="+CurrentRun.Money);
    Console.WriteLine("LevelCap="+CurrentRun.LevelCap);
    Console.WriteLine("PartySize="+CurrentRun.PartySize);
    Console.WriteLine("DrawPerTurn="+CurrentRun.DrawPerTurn);
    Console.WriteLine("Party.Count="+CurrentRun.Party.Count);
    Console.WriteLine("Bench.Count="+CurrentRun.Bench.Count);
    Console.WriteLine("MasterDeck.Count="+CurrentRun.MasterDeck.Count);
    Console.WriteLine("Inventory.Count="+CurrentRun.Inventory.Count);
    Console.WriteLine("CurrentZone="+CurrentRun.CurrentZone);
    Console.WriteLine("ZoneProgress="+CurrentRun.ZoneProgress);
    Console.WriteLine("CompletedZones.Count="+CurrentRun.CompletedZones.Count);
}


















Console.WriteLine("Exited Roguelije game logic test.");