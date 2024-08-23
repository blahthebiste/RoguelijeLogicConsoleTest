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
        //Console.Clear();
        Console.WriteLine("============================================================");
        if(cmd == null) continue;
        switch(cmd.ToLower().Trim().Split()[0]) 
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
            case "recommended":
                setDefaultParty();
                break;
            case "fighter":
            case "defender":
            case "healer":
            case "mage":
            case "thief":
                printStarterCharacterInfo(cmd);
                break;
            case "hero":
                if(cmd.ToLower().Trim().Split().Length < 2) {
                    Console.WriteLine("This command requires an argument -- hero name");
                }
                else {
                    printCharacterInfoFromName(cmd.ToLower().Trim().Split()[1]);
                }
                break;
            case "enemy":
                if(cmd.ToLower().Trim().Split().Length < 2) {
                    Console.WriteLine("This command requires an argument -- enemy name");
                }
                else {
                    printEnemyInfoFromName(cmd.ToLower().Trim().Split()[1]);
                }
                break;
            case "hand":
                printHand();
                break;
            case "draw":
                printDrawPile();
                break;
            case "discard":
                printDiscardPile();
                break;
            case "masterdeck":
                printMasterDeck();
                break;
            case "combat":
                printCombatSituation();
                break;
            case "end":
                endPlayerTurn();
                break;
            case "next":
                next();
                break;
            default:
                // If the first word is a number, and that number is less than 10, then the user is playing a card.
                if(int.TryParse(cmd.ToLower().Trim().Split()[0], out int n)) {
                    if(n <= CardManager.Hand.Count && n > 0) {
                        playCard(cmd);
                        printCombatSituation();
                    }
                    else {
                        Console.WriteLine("Invalid number. You only have "+CardManager.Hand.Count+" cards in your hand.");
                    }
                }
                else {   
                    Console.WriteLine("Unknown command '"+cmd+"'.\nType 'help' for a list of all commands.");
                }
                break;
        }
        Console.WriteLine("============================================================");
    }
}

void printHelp() {
    Console.WriteLine("exit,quit -- exits the game");
    if(!CurrentRun.InARun) {
        Console.WriteLine("start -- begins a run");
    }
    if(CurrentRun.InCombat) {
        Console.WriteLine("hand -- prints out your current hand");
        Console.WriteLine("1,2,3,4... <hero> [target] -- plays the card at that position in your hand");
        Console.WriteLine("draw -- prints out your current draw pile");
        Console.WriteLine("discard -- prints out your current discard pile");
        Console.WriteLine("hero <name> -- prints out details about the player character you named");
        Console.WriteLine("enemy <name> -- prints out details about the enemy you named");
        Console.WriteLine("end -- end your turn");
        Console.WriteLine("combat -- prints out the current combat situation");
    }
    else if(CurrentRun.InARun) {
        Console.WriteLine("party -- prints out info about your current party");
        Console.WriteLine("run -- prints out info about the current run");
        Console.WriteLine("masterdeck -- prints out your current deck");
        Console.WriteLine("depart -- sets off with the currently selected party");
        Console.WriteLine("zone -- selects the zone to travel to");
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

// Skips the party selection process to start the run immediately
void setDefaultParty(){
    PlayerCharacter newFighter = new Fighter();
    CurrentRun.Party.Add(newFighter);
    CurrentRun.MasterDeck.Add(newFighter.personalCard);
    PlayerCharacter newHealer = new Healer();
    CurrentRun.Party.Add(newHealer);
    CurrentRun.MasterDeck.Add(newHealer.personalCard);
    PlayerCharacter newDefender = new Defender();
    CurrentRun.Party.Add(newDefender);
    CurrentRun.MasterDeck.Add(newDefender.personalCard);
    zoneSelection();
    depart();
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
    Console.WriteLine("\n\tAnd we're off! Generating zone...");
    CurrentRun.SetZone(nextZoneID);
    // TODO: next step is generate the chosen zone
    CurrentRun.GenerateNextCombat();
    printCombatSituation();
}

// Allow the player to choose between a couple of events
void next(){
    Console.WriteLine("Which event would you like to go to next?");
    // TODO
}

// Asks the player what zone to travel to
void zoneSelection() {
    Console.WriteLine("Currently, the only zone available is the Medieval zone.");
    Console.WriteLine("\n\tIt is now set. Type 'depart' to set off to the zone.");
    nextZoneID = ZoneID.ZONE1;
}

void printStartingPartyMessage() {
    if(CurrentRun.Party.Count < CurrentRun.PartySize) {
        foreach(string line in DataRegistry.Messages.characterSelectMessage) {
            Console.WriteLine(line);
        }
        Console.WriteLine("\n(Or, type 'recommended' immediately set off with the default party.)");
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
        while(true) {
            Console.WriteLine("\nAdd this character to your party?");
            Console.WriteLine("\tYes\t\tBack");
            Console.WriteLine("============================================================");
            Console.Write("\n> ");
            cmd = Console.ReadLine();
            //Console.Clear();
            if(cmd == null) continue;
            if(cmd.ToLower().Trim() == "yes") {
                CurrentRun.Party.Add(newStarterCharacter);
                CurrentRun.MasterDeck.Add(newStarterCharacter.personalCard);
                Console.WriteLine("============================================================");
                Console.WriteLine("Added "+newStarterCharacter.name+" to party.");
                printStartingPartyMessage();
                return;
            }
            if(cmd.ToLower().Trim() == "back") {
                Console.WriteLine("============================================================");
                printStartingPartyMessage();
                return;
            }
            Console.WriteLine("Invalid response.");
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

void printMasterDeck() {
    Console.WriteLine("Master Deck:");
    foreach(ActionCard card in CurrentRun.MasterDeck) {
       Console.WriteLine(card.ToString());
    }
}

void getPartyInfo() {    
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
}


void printCharacterInfoFromName(string heroName) {
    foreach(PlayerCharacter hero in Battlefield.PlayerSide) {
        if(heroName.ToLower() == hero.name.ToLower()) {
            printCharacterInfo(hero);
            return;
        }
    }
    Console.WriteLine("Could not find hero of that name.");
}

void printCharacterInfo(PlayerCharacter character) {
    Console.WriteLine(character.name);
    Console.WriteLine("HP: "+character.currentHP+"/"+character.maxHP);
    Console.WriteLine("Actions:");
    foreach(Action action in character.ActionList) {
        if(action.equippedItem == null) {
            Console.WriteLine(action.ToString() + " (No item equipped)");
        }
        else {
            Console.WriteLine(action.ToString() + action.equippedItem.ToString());
        }
    }
    Console.WriteLine("Personal Card: "+character.personalCard);
}

void printEnemyInfoFromName(string enemyName) {
    foreach(Enemy enemy in Battlefield.EnemySide) {
        if(enemyName.ToLower() == enemy.name.ToLower()) {
            printEnemyInfo(enemy);
            return;
        }
    }
    Console.WriteLine("Could not find enemy of that name.");
}

void printEnemyInfo(Enemy enemy) {
    Console.WriteLine(enemy.name);
    Console.WriteLine("HP: "+enemy.currentHP+"/"+enemy.maxHP);
    Console.WriteLine("Actions:");
    foreach(Action action in enemy.ActionList) {
        if(action.equippedItem == null) {
            Console.WriteLine(action.ToString() + " (No item equipped)");
        }
        else {
            Console.WriteLine(action.ToString() + action.equippedItem.ToString());
        }
    }
}

void printCombatSituation() {
    Console.WriteLine("============================================================");
    if(!CurrentRun.InCombat || Battlefield.CurrentEncounter == null) {
            Console.WriteLine("Current Battle: None");
            return;
    }
    Console.WriteLine("Current Battle: "+Battlefield.CurrentEncounter.name);
    Console.WriteLine("Turn "+Battlefield.turnNumber);
    int max_entities;
    int numEnemies = Battlefield.EnemySide.Count;
    int numHeroes = Battlefield.PlayerSide.Count;
    int sizeDiff = Math.Abs(numEnemies - numHeroes);
    if(Battlefield.playerBlock > 0 || Battlefield.enemyBlock > 0) {
        Console.WriteLine("\tCurrent Block: "+Battlefield.playerBlock+"\t|\tEnemy Block: "+Battlefield.enemyBlock);
    } 
    Console.WriteLine("");
    
    List<Enemy?> enemyFilled = new List<Enemy?>();
    foreach(Enemy ent in Battlefield.EnemySide) {
        enemyFilled.Add(ent);
    }
    List<PlayerCharacter?> playerFilled = new List<PlayerCharacter?>();
    foreach(PlayerCharacter ent in Battlefield.PlayerSide) {
        playerFilled.Add(ent);
    }
    // Add filler lines to both lists, in between each existing line:
    for(int i = (numEnemies -1); i > 0; i--) {
        enemyFilled.Insert(i, null);
    }
    for(int i = (numHeroes -1); i > 0; i--) {
        playerFilled.Insert(i, null);
    }
    if(numEnemies > numHeroes) {
        // More enemies, so format based on their number
        max_entities = numEnemies;
        // Need to add filler rows to the player list
        bool swap = true;
        while(playerFilled.Count < (max_entities*2) -1) {
            if(swap) {
                playerFilled.Insert(0, null);
                swap = false;
            }
            else {
                playerFilled.Insert(playerFilled.Count-1, null);
                swap = true;
            }
        }
    }
    else {
        max_entities = numHeroes;
        // Need to add filler rows to the enemy list
        bool swap = true;
        while(enemyFilled.Count < (max_entities*2) -1) {
            if(swap) {
                enemyFilled.Insert(0, null);
                swap = false;
            }
            else {
                enemyFilled.Add(null);
                swap = true;
            }
        }
    }
   
    // Loop through the rows and print entity name/HP accordingly
    for(int i = 0; i < (max_entities*2) -1; i++) {
        string heroString = "";
        string enemyString = "";
        if(playerFilled[i] != null) {
            heroString = playerFilled[i]!.name+"["+playerFilled[i]!.currentHP+"/"+playerFilled[i]!.maxHP+" HP]";
            if(playerFilled[i]!.exhausted) heroString = "(E) "+heroString;
        }
        if(enemyFilled[i] != null) {
            string nextEnemyAction = "";
            string nextEnemyTarget = "";
            if(enemyFilled[i]!.ActionList.Count > 0) {
                nextEnemyAction = enemyFilled[i]!.ActionList[enemyFilled[i]!.nextActionIndex].description;
                if(enemyFilled[i]!.ActionList[enemyFilled[i]!.nextActionIndex].hasTarget()) {
                    nextEnemyTarget = enemyFilled[i]!.getNextTargetName();
                }
            }
            enemyString = enemyFilled[i]!.name+"["+enemyFilled[i]!.currentHP+"/"+enemyFilled[i]!.maxHP+" HP] - "
                        +nextEnemyAction+" Target: "+nextEnemyTarget;
            if(enemyFilled[i]!.exhausted) enemyString = "(E) "+enemyString;
        }
        
        string rowString = String.Format("{0,24}\t|\t{1,32}", heroString, enemyString);
        Console.WriteLine(rowString);
    }
    Console.WriteLine("");
    if(Battlefield.playerCharactersAllExhausted()) Console.WriteLine("All party members have acted. Enter 'end' to end your turn.");
    else {
        // Print current hand, but not in detail
        if(CardManager.Hand.Count > 0) {
            for(int i = 0; i < CardManager.Hand.Count; i++) {
                Console.Write("["+(i+1)+" - "+CardManager.Hand[i].name+"]\t");
            }
        }
        else {
            Console.WriteLine("No more cards in hand. Enter 'end' to end your turn.");
        }
    }
    Console.WriteLine("");
}

void endPlayerTurn() {
    Console.WriteLine("Ending turn.");
    Battlefield.endTurn();
    printCombatSituation();
}

void printHand() {
    if(CardManager.Hand.Count < 1) {
        Console.WriteLine("Hand is empty.");
    }
    else {
        for(int i = 0; i < CardManager.Hand.Count; i++) {
            Console.WriteLine("["+(i+1)+"] "+CardManager.Hand[i].ToString());
        }
    }
}

void printDrawPile() {
    if(CardManager.DrawPile.Count < 1) {
        Console.WriteLine("Draw Pile is empty.");
    }
    else {
        // Need to show draw pile in a different random order so as not to let the player see their next draw.
        List<ActionCard> RandomizedDrawPile = new List<ActionCard>(CardManager.DrawPile);
        CardManager.randomizeCardOrder(RandomizedDrawPile);
        foreach(ActionCard card in RandomizedDrawPile) {
            Console.WriteLine(card.ToString());
        }
    }
}

void printDiscardPile() {
    if(CardManager.DiscardPile.Count < 1) {
        Console.WriteLine("Discard Pile is empty.");
    }
    else {
        foreach(ActionCard card in CardManager.DiscardPile) {
            Console.WriteLine(card.ToString());
        }
    }
}

// The first element of the command is guaranteed to be an integer 10 or lower.
// Need to parse the second element to determine who is using the card action.
void playCard(string cmd) {
    if(cmd.ToLower().Trim().Split().Length < 2) {
        Console.WriteLine("You must include the name of hero that you want to use this action as an argument.");
        Console.WriteLine("Example:     2 fighter pengoon");
        return;
    }
    string whoIsUsingTheAction = cmd.ToLower().Trim().Split()[1];
    Entity? target = null;
    string? actionTarget;
    // If the command included a third argument, that is the players intended target for this action.
    if(cmd.ToLower().Trim().Split().Length > 2) {
        actionTarget = cmd.ToLower().Trim().Split()[2];
        foreach(PlayerCharacter hero in Battlefield.PlayerSide){
            // Check if target is here, if we were given one.:
            if(hero.name.ToLower().Trim() == actionTarget) {
                target = hero;
                break;
            }
        }
        foreach(Enemy enemy in Battlefield.EnemySide){
            // Check if target is here, if we were given one.:
            if(enemy.name.ToLower().Trim() == actionTarget) {
                target = enemy;
                break;
            }
        }
        // If the target is still null, error:
        if(target == null) {
            Console.WriteLine("No target with the name "+actionTarget+" exists in this battle.");
            return;
        }
    }
    int cardNumber; // The index in the hand where the card is; 1-indexed
    int.TryParse(cmd.ToLower().Trim().Split()[0], out cardNumber);
    foreach(PlayerCharacter hero in Battlefield.PlayerSide){
        if(hero.name.ToLower().Trim() == whoIsUsingTheAction) {
            ActionCard selectedCard = CardManager.Hand[cardNumber - 1];
            // Found the hero who should use the action.
            int numMatchingActions = selectedCard.numberMatchingActions(hero);
            // If the hero has no matching actions, print an error:
            if(numMatchingActions == 0) {
                Console.WriteLine("Card '"+selectedCard.name+"' cannot be used by "+hero.name+".");
                return;
            }
            // If the hero has multiple matching actions, prompt the player to choose one:
            if(numMatchingActions > 1) {
                Console.WriteLine(hero.name+" has multiple actions that this card can have them perform.");
                Console.WriteLine("Select one from the following by entering its number, or type something else to go back:");
                Console.WriteLine("");
                List<Action> matchingActions = new List<Action>();
                // Find all matching actions from the hero's action list:
                foreach(Action action in hero.ActionList) {
                    if(selectedCard.actionCanBeUsed(action)) {
                        matchingActions.Add(action);
                    }
                }
                // Print them out and await selection:
                for(int i = 0; i < matchingActions.Count; i++) {
                    Console.Write("["+(i+1)+" - "+matchingActions[i].name+"]\t");
                }
                Console.Write("\n> ");
                string? cmd2 = Console.ReadLine();
                if(cmd2 == null) return;
                if(int.TryParse(cmd2.ToLower().Trim(), out int actionSelection)) {
                    // If they entered a valid number for action selection, perform the action:
                    if(actionSelection <= matchingActions.Count && actionSelection > 0) {
                        selectedCard.use(hero, target, matchingActions[actionSelection - 1]);
                    }
                }
                // Return afterwards regardless.
                return;
            }
            // If the hero has exactly 1 matching action, use it:
            if(numMatchingActions == 1) {
                selectedCard.use(hero, target);
            }
            return;
        }
    }
    // If we got here, no hero matched.
    Console.WriteLine("No hero with the name "+whoIsUsingTheAction+" exists in this battle.");
}










Console.WriteLine("Exited Roguelije game logic test.");