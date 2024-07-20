// See https://aka.ms/new-console-template for more information
Console.WriteLine("Initializing console test program for Roguelije game logic...");

string? cmd;
ZoneID nextZoneID = ZoneID.HUB;
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
            case "depart":
                depart();
                break;
            case "zone":
                zoneSelection();
                break;
            case "party":
                getPartyInfo();
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
    Console.WriteLine("party -- prints out info about your current party");
    Console.WriteLine("depart -- sets off with the currently selected party");
    Console.WriteLine("zone -- selects the zone to travel to");
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

void depart() {
    if(CurrentRun.PartySize > CurrentRun.Party.Count) {
        Console.WriteLine("\n===Cannot depart yet -- must finish choosing your party===\n");
        printStartingPartyMessage();
        return;
    }
    if(nextZoneID == ZoneID.HUB) {
        Console.WriteLine("\n===Cannot depart yet -- must select the Zone to travel to===");
        zoneSelection();
        return;
    }
    Console.WriteLine("\n\tAnd we're off!");
    CurrentRun.SetZone(nextZoneID);
    // TODO: next step is generate the chosen zone
}

// Asks the player what zone to travel to
void zoneSelection() {
    Console.WriteLine("============================================================");
    Console.WriteLine("Currently, the only zone available is the Medieval zone.");
    Console.WriteLine("\n\tIt is now set.");
    Console.WriteLine("============================================================");
    nextZoneID = ZoneID.ZONE1;
}

void printStartingPartyMessage() {
    if(CurrentRun.Party.Count < CurrentRun.PartySize) {
        foreach(string line in DataRegistry.Messages.characterSelectMessage) {
            Console.WriteLine(line);
        }
    }
    else {
        if(nextZoneID == ZoneID.HUB) {
            Console.WriteLine("Your party is ready to go! Type 'party' to view them, or 'zone' to choose what Zone to travel to.");
        }
        else {
            Console.WriteLine("Your party is ready to go! Type 'party' to view them, or 'depart' to begin your run!");
        }
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
        case "healer":
            newStarterCharacter = new Healer();
            Console.WriteLine(newStarterCharacter.name+" - "+DataRegistry.CharacterData.Healer.Description);
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
    if(CurrentRun.Party.Count < CurrentRun.PartySize) {
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

void getPartyInfo() {
    Console.WriteLine("============================================================");
    
    if(CurrentRun.Party.Count > 0){
        Console.WriteLine("Current Party info:\n");
        foreach(PlayerCharacter character in CurrentRun.Party) {
            printCharacterInfo(character);
            Console.WriteLine("");
        }
    }
    else {
        Console.WriteLine("\nParty currently empty.\n");
    }
    if(CurrentRun.Bench.Count > 0){
        Console.WriteLine("\nOn the Bench:\n");
        foreach(PlayerCharacter character in CurrentRun.Bench) {
            printCharacterInfo(character);
        }
    }
    Console.WriteLine("============================================================");
}


void printCharacterInfo(PlayerCharacter character) {
    Console.WriteLine(character.name);
    Console.WriteLine("HP: "+character.currentHP+"/"+character.maxHP);
    Console.WriteLine("Actions:");
    foreach(Action action in character.ActionList) {
        if(action.equippedItem == null) {
            Console.WriteLine(action.ToString() + "(No item equipped)");
        }
        else {
            Console.WriteLine(action.ToString() + action.equippedItem.ToString());
        }
    }
    Console.WriteLine("Personal Card: "+character.personalCard);
}















Console.WriteLine("Exited Roguelije game logic test.");