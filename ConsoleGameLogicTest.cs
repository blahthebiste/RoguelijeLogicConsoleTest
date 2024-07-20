// See https://aka.ms/new-console-template for more information
Console.WriteLine("Initializing console test program for Roguelije game logic...");

string? cmd;
printStartScreen();
// Enter command loop
commandLoop();


void commandLoop() {
    while(true) {
        Console.Write("\n> ");
        cmd = Console.ReadLine();
        if(cmd == null) continue;
        switch(cmd.ToLower().Trim()) 
        {
            case "exit":
            case "quit":
                Console.WriteLine("User exited.");
                return;
            case "help":
                printHelp();
                break;
            case "start":
                startRun();
                break;
            case "run":
                getRunInfo();
                break;
            case "fighter":
            case "defender":
            case "healer":
            case "mage":
            case "thief":
                printStarterCharacterInfo(cmd);
                break;
            default:
                Console.WriteLine("Unknown command '"+cmd+"'.\nType 'help' for a list of all commands.");
                break;
        }
    }
}

void printHelp() {
    Console.WriteLine("exit,quit -- exits the game");
    Console.WriteLine("start -- begins a run");
    Console.WriteLine("run -- prints out info about the current run");
    if(CurrentRun.InARun) {
        if(CurrentRun.Party.Count < CurrentRun.PartySize) {
            // Starting party is not yet chosen. Show character options
            
        }
    }
}

void printStartScreen() {
    foreach(string line in DataRegistry.Messages.startScreenMessage) {
        Console.WriteLine(line);
    }

}

void startRun() {
    if(CurrentRun.InARun) {
        Console.WriteLine("\nInvalid command -- already in a run");
        return;
    }
    Console.WriteLine("\n\tStarting new run...");
    CurrentRun.InARun = true;
    printStartingPartyMessage();
}


void printStartingPartyMessage() {
    foreach(string line in DataRegistry.Messages.characterSelectMessage) {
        Console.WriteLine(line);
    }
}
 
void printStarterCharacterInfo(string characterName) {
    PlayerCharacter newStarterCharacter;
    switch(characterName.ToLower().Trim())
    {
        case "fighter":
            newStarterCharacter = new Fighter();
            Console.WriteLine(newStarterCharacter.name+" - "+DataRegistry.CharacterData.Fighter.Description);
            Console.WriteLine("HP: "+newStarterCharacter.maxHP);
            Console.WriteLine("Actions:");
            foreach(Action action in newStarterCharacter.ActionList) {
                Console.WriteLine(action.ToString());
            }
            Console.WriteLine("Personal Card: "+newStarterCharacter.personalCard);
            break;
        case "defender":
            newStarterCharacter = new Defender();
            Console.WriteLine(newStarterCharacter.name+" - "+DataRegistry.CharacterData.Defender.Description);
            Console.WriteLine("HP: "+newStarterCharacter.maxHP);
            Console.WriteLine("Actions:");
            foreach(Action action in newStarterCharacter.ActionList) {
                Console.WriteLine(action.ToString());
            }
            Console.WriteLine("Personal Card: "+newStarterCharacter.personalCard);
            break;
        default:
            newStarterCharacter = new PlayerCharacter();
            break;
    }
    Console.WriteLine("\nAdd this character to your party?");
    Console.WriteLine("\tYes\t\tBack");
    while(true) {
        Console.Write("\n> ");
        cmd = Console.ReadLine();
        if(cmd == null) continue;
        if(cmd.ToLower().Trim() == "yes") {
            CurrentRun.Party.Add(newStarterCharacter);
            Console.WriteLine("Added "+newStarterCharacter.name+" to party.");
            printStartingPartyMessage();
            return;
        }
        if(cmd.ToLower().Trim() == "back") {
            printStartingPartyMessage();
            return;
        }
    }
}

void getRunInfo() {
    Console.WriteLine("Current Run info:");
    Console.WriteLine("Lives="+CurrentRun.Lives);
    Console.WriteLine("Money="+CurrentRun.Money);
    Console.WriteLine("LevelCap="+CurrentRun.LevelCap);
    Console.WriteLine("PartySize="+CurrentRun.PartySize);
    Console.WriteLine("MinimumDeckSize="+CurrentRun.MinimumDeckSize);
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