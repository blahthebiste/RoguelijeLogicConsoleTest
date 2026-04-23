// See https://aka.ms/new-console-template for more information
Console.WriteLine("Initializing console test program for Roguelije game logic...");

string? cmd;
ZoneID nextZoneID = ZoneID.HUB;
DataRegistry.LoadData();
Compendium.Initialize();
printStartScreen();
// Enter command loop
commandLoop(); // Effectively main

void printSeparator() {
    Console.WriteLine("============================================================");
}

void commandLoop() {
    while(true) {
        Console.Write("\n> ");
        cmd = Console.ReadLine();
        //Console.Clear();
        if(cmd == null) continue;
        printSeparator();
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
            case "tutorial":
                tutorial();
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
                    continue;
                }
                else {
                    printCharacterInfoFromName(cmd.ToLower().Trim().Split()[1]);
                }
                break;
            case "enemy":
                if(cmd.ToLower().Trim().Split().Length < 2) {
                    Console.WriteLine("This command requires an argument -- enemy name");
                    continue;
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
            case "deck":
                printMasterDeck();
                break;
            case "collection":
                printCardCollection();
                break;
            case "combat":
                printCombatSituation();
                break;
            case "end":
                endPlayerTurn();
                break;
            case "inventory":
                printInventory();
                break;
            case "next":
                nextNode();
                break;
            case "equip":
                if(cmd.ToLower().Trim().Split().Length < 3) {
                    Console.WriteLine("Incorrect syntax -- requires <item> and <hero> arguments");
                    continue;
                }
                else {
                    CurrentRun.equipItem(cmd.ToLower().Trim().Split()[1],cmd.ToLower().Trim().Split()[2]);
                }
                break;
            case "unequip":
                if(cmd.ToLower().Trim().Split().Length < 3) {
                    Console.WriteLine("Incorrect syntax -- requires <item> and <hero> arguments");
                    continue;
                }
                else {
                    unequipItem(cmd.ToLower().Trim().Split()[1],cmd.ToLower().Trim().Split()[2]);
                }
                break;
            case "attach":
                if(cmd.ToLower().Trim().Split().Length < 3) {
                    Console.WriteLine("Incorrect syntax -- requires <modifier> and <actionCard> arguments");
                    continue;
                }
                else {
                    attachModifier(cmd.ToLower().Trim().Split()[1],cmd.ToLower().Trim().Split()[2]);
                }
                break;
            case "edit":
                editMasterDeck();
                break;
            default:
                // If the first word is a number, and that number is equal to or less than current hand count, then the user is playing a card.
                if(int.TryParse(cmd.ToLower().Trim().Split()[0], out int n)) {
                    if(n <= CardManager.Hand.Count && n > 0) {
                        playCard(cmd);
                        printCombatSituation();
                    }
                    else {
                        Console.WriteLine("Invalid number. You only have "+CardManager.Hand.Count+" cards in your hand.");
                        continue;
                    }
                }
                else {   
                    Console.WriteLine("Unknown command '"+cmd+"'.\nType 'help' for a list of all commands.");
                    continue;
                }
                break;
        }
        printSeparator();
    }
}

void printHelp() {
    Console.WriteLine("exit,quit -- exits the game");
    if(!CurrentRun.InARun) {
        Console.WriteLine("start -- begins a run");
        Console.WriteLine("tutorial -- starts the tutorial");
        return; // All other commands only show once the run starts
    }
    if(CurrentRun.InCombat) {
        Console.WriteLine("hand -- prints out your current hand");
        Console.WriteLine("1,2,3,4... <hero> [target] -- plays the card at that position in your hand");
        Console.WriteLine("draw -- prints out your current draw pile");
        Console.WriteLine("discard -- prints out your current discard pile");
        Console.WriteLine("enemy <name> -- prints out details about the enemy you named");
        Console.WriteLine("end -- end your turn");
        Console.WriteLine("combat -- prints out the current combat situation");
    }
    else {
        Console.WriteLine("depart -- sets off with the currently selected party");
        Console.WriteLine("zone -- selects the zone to travel to");       
        Console.WriteLine("deck -- prints out your current deck");
        Console.WriteLine("collection -- prints out your entire card collection");
        Console.WriteLine("edit -- enter the deck editor");
        Console.WriteLine("attach <modifier> <card> -- attach a modifier to a card");
    }
    // These commands are always available once the run starts
    Console.WriteLine("party -- prints out info about your current party");
    Console.WriteLine("run -- prints out info about the current run");
    Console.WriteLine("inventory -- prints out your inventory");
    Console.WriteLine("hero <name> -- prints out details about the player character you named");
    Console.WriteLine("equip <item> <hero> -- equip an item to a hero (takes hero's action)");
    Console.WriteLine("unequip <item> <hero> -- unequip an item from a hero (takes hero's action)");
    if(nextZoneID == ZoneID.HUB && CurrentRun.Party.Count < CurrentRun.PartySize) {
        // Starting party is not yet chosen. Show character options
        Console.WriteLine("<hero> -- view a hero, and choose whether to add them to the party");
    }
}

// Version of printHelp that shows valid commands in the deck editor
void printDeckEditorHelp() {
    Console.WriteLine("exit,quit -- exits the editor");
    Console.WriteLine("move <card> <in/out> -- move the specified card either in or out of your master deck");
    Console.WriteLine("party -- prints out info about your current party");
    Console.WriteLine("run -- prints out info about the current run");
}

void printStartScreen() {
    foreach(string line in DataRegistry.Messages.startScreenMessage) {
        Console.WriteLine(line);
    }

}

// Starts a run and auto-selects party and encounter
void tutorial()
{
    if (CurrentRun.InARun)
    {
        Console.WriteLine("\nInvalid command -- already in a run");
        return;
    }
    Console.WriteLine("\n\tStarting new Tutorial run...");
    CurrentRun.InARun = true;
    setDefaultParty();
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
void setDefaultParty()
{
    PlayerCharacter newFighter = new PlayerCharacter("Fighter");
    CurrentRun.Party.Add(newFighter);
    PlayerCharacter newThief = new PlayerCharacter("Thief");
    CurrentRun.Party.Add(newThief);
    PlayerCharacter newDefender = new PlayerCharacter("Defender");
    CurrentRun.Party.Add(newDefender);
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
    CurrentRun.ZoneProgress = 9; // For debugging
    CurrentRun.GenerateNextCombat();
}

// Allow the player to choose between a couple of Encounters, if they were just in combat;
// Or, allow them to choose between a couple of combat encounters, if they just came from an Encounter.
// If they have already been to an Encounter and selected their next combat, enter combat.
void nextNode(){
    if(CurrentRun.InCombat) {
        Console.WriteLine("ERROR: must complete combat to proceed!");
        return;
    }
    if(CurrentRun.Party.Count < 1) {
        Console.WriteLine("ERROR: must have at least 1 hero in your party to proceed!");
        return;
    }
    if(CurrentRun.MasterDeck.Count < CurrentRun.MinimumDeckSize) {
        Console.WriteLine("ERROR: must have at least "+CurrentRun.MinimumDeckSize+" cards in your deck to proceed!");
        return;
    }
    // Determine if the next node is an Encounter or combat:
    if(!CurrentRun.LastEncounterWasEncounter) {
        Console.WriteLine("Which Encounter would you like to go to next?");
        // Present multiple options, let player choose 1
        CurrentRun.GenerateEncounters();
    }
    else if(CurrentRun.NextCombatEncounter == null) {
        // Player has already done an Encounter, but has not yet selected combat
        CurrentRun.GenerateNextCombat();
    }
    else {
        // Encounter is completed, and next combat is selected; enter combat
        CurrentRun.EnterCombat();
        printCombatSituation();
    }
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
    PlayerCharacter newStarterCharacter = new PlayerCharacter(characterName.ToLower().Trim());
    Console.WriteLine(newStarterCharacter.name+" - "+newStarterCharacter.description);
    Console.WriteLine("HP: "+newStarterCharacter.maxHP);
    Console.WriteLine("Actions:");
    foreach(Action action in newStarterCharacter.ActionList) {
        Console.WriteLine(action.ToString());
    }
    Console.WriteLine("Personal Card: "+newStarterCharacter.personalCard);
            
    if(CurrentRun.Party.Count < CurrentRun.PartySize) {
        while(true) {
            Console.WriteLine("\nAdd this character to your party?");
            Console.WriteLine("\tYes\t\tBack");
            printSeparator();
            Console.Write("\n> ");
            cmd = Console.ReadLine();
            //Console.Clear();
            if(cmd == null) continue;
            if(cmd.ToLower().Trim() == "yes") {
                CurrentRun.Party.Add(newStarterCharacter);
                printSeparator();
                Console.WriteLine("Added "+newStarterCharacter.name+" to party.");
                printStartingPartyMessage();
                return;
            }
            if(cmd.ToLower().Trim() == "back") {
                printSeparator();
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
    if(CurrentRun.MasterDeck.Count == 0) {
        Console.WriteLine("\tYour deck is empty.");
        return;
    }
    foreach(ActionCard card in CurrentRun.MasterDeck) {
       Console.WriteLine(card.ToString());
    }
}

void printCardCollection() {
    Console.WriteLine("Card Collection:");
    if(CurrentRun.CardCollection.Count == 0) {
        Console.WriteLine("\tYour card collection is empty.");
        return;
    }
    foreach(ActionCard card in CurrentRun.CardCollection) {
       Console.WriteLine(card.ToString());
    }
}

// Displays the master deck and collection side by side.
// Shows a number next to each card to use as its ID for moving it to/from the master deck.
void printDeckEditor(){
    string deckHeader = "Master Deck (minimum: "+CurrentRun.MinimumDeckSize+")";
    string collectionHeader = "Card Collection";
    string headerString = String.Format("{0,24}\t|\t{1,32}", deckHeader, collectionHeader);
    Console.WriteLine(headerString);
    int sizeMasterDeck = CurrentRun.MasterDeck.Count;
    int sizeCollection = CurrentRun.CardCollection.Count;
    int maxRows = (sizeMasterDeck > sizeCollection) ? sizeMasterDeck : sizeCollection;
    for(int i = 0; i < maxRows; i++) {
        string deckString = "";
        string collectionString = "";
        if(CurrentRun.MasterDeck.Count > i) {
            deckString = "["+(i+1)+"] "+CurrentRun.MasterDeck[i]!.name;
        }
        if(CurrentRun.CardCollection.Count > i) {
            collectionString = "["+(i+1)+"] "+CurrentRun.CardCollection[i]!.name;
        }
        string rowString = String.Format("{0,24}\t|\t{1,32}", deckString, collectionString);
        Console.WriteLine(rowString);
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
    foreach(PlayerCharacter hero in CurrentRun.Party) {
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
        if(!action.hasEquipmentSlot) {
            Console.WriteLine(action.ToString());
        }
        else if(action.equippedItem == null) {
            Console.WriteLine(action.ToString() + " (No item equipped)");
        }
        else {
            Console.WriteLine(action.ToString() + " Item: " +action.equippedItem.ToString());
        }
    }
    if(character.EffectList.Count > 0) {
        Console.WriteLine("Current effects:");
        foreach(StatusEffect effect in character.EffectList) {
            Console.WriteLine("\t"+effect.ToString());
        }
    }
    Console.WriteLine("Personal Card: "+character.personalCard);
}

void printEnemyInfoFromName(string enemyName) {
    foreach(Entity enemy in Battlefield.EnemySide) {
        if(enemyName.ToLower().Replace('_',' ') == enemy.name.ToLower()) {
            printEnemyInfo(enemy);
            return;
        }
    }
    Console.WriteLine("Could not find enemy of that name.");
}

void printEnemyInfo(Entity enemy) {
    Console.WriteLine(enemy.name);
    if(!enemy.isEnvironment) Console.WriteLine("HP: "+enemy.currentHP+"/"+enemy.maxHP);
    Console.WriteLine("Actions:");
    foreach(Action action in enemy.ActionList) {
        if (action.hiddenAction)
        {
            // Do not show hidden actions.
            continue;
        }
        if (action.equippedItem == null)
        {
            Console.WriteLine(action.ToString());
        }
        else
        { // Unnecessary: enemies never use items
            Console.WriteLine(action.ToString() + action.equippedItem.ToString());
        }
    }
    if(enemy.EffectList.Count > 0) {
        Console.WriteLine("Current effects:");
        foreach(StatusEffect effect in enemy.EffectList) {
            Console.WriteLine("\t"+effect.ToString());
        }
    }
}

void printCombatSituation() {
    Entity? targetLockingEnemy = null;
    printSeparator();
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
    if (Battlefield.playerBlock > 0 || Battlefield.enemyBlock > 0)
    {
        string playerBlockString = "Current Block: " + Battlefield.playerBlock;
        string enemyBlockString = "Enemy Block: " + Battlefield.enemyBlock;
        string blockString = String.Format("{0,40}\t|{1,40}", playerBlockString, enemyBlockString);
        Console.WriteLine(blockString);
    } 
    Console.WriteLine("");
    
    List<Entity?> enemySideFilled = new List<Entity?>();
    foreach (Entity ent in Battlefield.EnemySide)
    {
        // Check for Target Lock in the action list
        enemySideFilled.Add(ent);
        if(targetLockingEnemy == null) {
            foreach (Action act in ent.ActionList) {
                if (act is TargetLock targetLockPassive && targetLockPassive.lockedTarget == null)
                { // Only print the first match, and only if they have not locked onto a target yet
                    targetLockingEnemy = ent;
                    Console.WriteLine("The next hero to act will be locked into a duel with "+targetLockingEnemy.name+"!");
                    break;
                }
            }
        }
        
    }
    List<Entity?> playerSideFilled = new List<Entity?>();
    foreach(Entity ent in Battlefield.PlayerSide) {
        playerSideFilled.Add(ent);
    }

    // Add filler lines to both lists, in between each existing line:
    for (int i = numEnemies - 1; i > 0; i--)
    {
        enemySideFilled.Insert(i, null);
    }
    for(int i = numHeroes - 1; i > 0; i--) {
        playerSideFilled.Insert(i, null);
    }
    if(numEnemies > numHeroes) {
        // More enemies, so format based on their number
        max_entities = numEnemies;
        // Need to add filler rows to the player list
        bool swap = true;
        while(playerSideFilled.Count < (max_entities*2) -1) {
            if(swap) {
                playerSideFilled.Insert(0, null);
                swap = false;
            }
            else {
                playerSideFilled.Add(null);
                swap = true;
            }
        }
    }
    else {
        max_entities = numHeroes;
        // Need to add filler rows to the enemy list
        bool swap = true;
        while(enemySideFilled.Count < (max_entities*2) -1) {
            if(swap) {
                enemySideFilled.Insert(0, null);
                swap = false;
            }
            else {
                enemySideFilled.Add(null);
                swap = true;
            }
        }
    }
   
    // Loop through the rows and print entity name/HP accordingly
    for(int i = 0; i < (max_entities*2) -1; i++) {
        string heroString = "";
        string enemyString = "";
        if (playerSideFilled[i] != null)
        {
            heroString = formatEntityInCombat(playerSideFilled[i]!);            
        }
        if(enemySideFilled[i] != null) {
            enemyString = formatEntityInCombat(enemySideFilled[i]!);
        }
        
        string rowString = String.Format("{0,40}\t|\t{1,40}", heroString, enemyString);
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

string formatEntityInCombat(Entity ent)
{
    string formattedString;
    if (ent.isEnvironment)
    {   // If the entity is just an enviroment, do not display their HP:
        formattedString = ent.name;
    }
    else
    {
        formattedString = ent.name+ "[" + ent.currentHP + "/" + ent.maxHP + " HP]"; // Name and HP
    }
    
    if (ent.exhausted) formattedString = "(E) " + formattedString; // Exhausted
    else if (!ent.playerControlled) // Target and next move, if not player controlledL
    {
        if (ent.ActionList.Count > 0)
        {
            formattedString += " - " + ent.getNextAction().description;
            if (ent.getNextTargetName() != null && ent.getNextTargetName() != "None")
            {
                formattedString += " Target: " + ent.getNextTargetName();
            }
        }
    }
    return formattedString;
}

void endPlayerTurn() {
    if (CurrentRun.InCombat)
    {
        Battlefield.endTurn();
    }
    // Combat may end with that turn. Only run startRound if it is still going
    if (CurrentRun.InCombat)
    {
        Battlefield.startRound();
    }
    // Combat may end during startOfRound triggers. Only run printCombatSituation if it is still going
    if (CurrentRun.InCombat)
    {
        printCombatSituation();
    }
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
        CurrentRun.Shuffle(RandomizedDrawPile);
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

void printInventory() {
    Console.WriteLine("Inventory ("+CurrentRun.Inventory.Count+" items):");
    foreach(Item item in CurrentRun.Inventory) {
        if (item is Modifier)
        {
            Console.WriteLine("\t* [Card Modifier] "+item.ToString());
        }
        else
        {
            Console.WriteLine("\t*\t" + item.ToString());
        }
    }
}

// The first element of the command is guaranteed to be an integer 10 or lower.
// Need to parse the second element to determine who is using the card action.
void playCard(string cmd) {
    if(cmd.ToLower().Trim().Split().Length < 2) {
        Console.WriteLine("You must include the name of hero that you want to use this action as an argument.");
        Console.WriteLine("Example:    > 2 fighter pengoon");
        return;
    }
    string whoIsUsingTheAction = cmd.ToLower().Trim().Split()[1];
    Entity? target = null;
    string? actionTarget;
    // If the command included a third argument, that is the players intended target for this action.
    if (cmd.ToLower().Trim().Split().Length > 2)
    {
        actionTarget = cmd.Split()[2].ToLower().Trim().Replace('_', ' ');
        foreach (PlayerCharacter hero in Battlefield.PlayerSide.OfType<PlayerCharacter>())
        {
            // Check if target is here, if we were given one.
            if (hero.name.ToLower().Trim() == actionTarget)
            {
                target = hero;
                break;
            }
        }
        if (target == null) // If the target is still null, check enemies
        {
            foreach (Entity enemy in Battlefield.EnemySide)
            {
                // Check if target is here, if we were given one.
                if (enemy.name.ToLower().Trim() == actionTarget)
                {
                    target = enemy;
                    break;
                }
            }
        }
        if (target == null) // If the target is still null, check dead heroes
        {
            // Check dead entities, just in case:
            foreach (Entity hero in Battlefield.DeadHeroes)
            {
                // Check if target is here, if we were given one.
                if (hero.name.ToLower().Trim() == actionTarget)
                {
                    target = hero;
                    break;
                }
            }
        }
        if (target == null) // If the target is still null, check dead enemies
        {
            foreach (Entity enemy in Battlefield.DeadEnemies)
            {
                // Check if target is here, if we were given one.
                if (enemy.name.ToLower().Trim() == actionTarget)
                {
                    target = enemy;
                    break;
                }
            }
        }
        // If the target is still null, error:
        if (target == null)
        {
            Console.WriteLine("No target with the name " + actionTarget + " exists in this battle.");
            return;
        }
        // If the target is an environment, error:
        if (target.isEnvironment)
        {
            Console.WriteLine("Cannot target " + actionTarget + ", it is part of the environment.");
            return;
        }
    }
    int cardNumber; // The index in the hand where the card is; 1-indexed
    int.TryParse(cmd.ToLower().Trim().Split()[0], out cardNumber); // No need to check, this is only run if arg 1 is an int
    foreach(PlayerCharacter hero in Battlefield.PlayerSide.OfType<PlayerCharacter>()){
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

// Attempts to unequip the specified item from the hero.
// Errors if the item or hero is not found, or if the hero is exhausted
void unequipItem(string itemName, string heroName) {
    PlayerCharacter? heroToUnequip = null;
    // Works a little differently depending on whether we are in combat or not.
    if(CurrentRun.InCombat) {
        // Combat version: only alive heroes who are not on the bench can item swap.
        // This takes the hero's action.

        // First, find the hero:
        foreach(PlayerCharacter hero in Battlefield.PlayerSide.OfType<PlayerCharacter>()){
            // PlayerSide only includes living heroes
            if(hero.name.ToLower().Trim() == heroName.ToLower().Trim()) {
                heroToUnequip = hero;
            }
        }
        // Finally, check if the hero can act:
        if(heroToUnequip != null && heroToUnequip.exhausted) {
            Console.WriteLine("ERROR: "+heroName+" cannot swap items because they are exhausted.");
            return;
        }
    }
    else if(CurrentRun.InARun){
        // Non-combat version: free swapping for all heroes, bench or not
        // First, find the hero:
        foreach(PlayerCharacter hero in CurrentRun.Party){
            if(hero.name.ToLower().Trim() == heroName.ToLower().Trim()) {
                heroToUnequip = hero;
            }
        }
        // Also search the benched heroes:
        foreach(PlayerCharacter hero in CurrentRun.Bench){
            if(hero.name.ToLower().Trim() == heroName.ToLower().Trim()) {
                heroToUnequip = hero;
            }
        }
    }
    
    if(heroToUnequip == null) {
        Console.WriteLine("ERROR: Could not find hero with name "+heroName);
        return;
    }
    // Find the action in the hero's action list that has the item equipped:
    foreach(Action action in heroToUnequip.ActionList) {
        if(action.equippedItem != null && action.equippedItem.name.ToLower().Trim().Replace(' ','_') == itemName) {
            // Have to check for replaced actions too
            if(action.equippedItem.oldAction != null) {
                action.equippedItem.oldAction.Unequip();
            }
            else {
                action.Unequip();
            }
            Console.WriteLine(heroName+" unequipped "+itemName+".");
            return;
        }
    }
    Console.WriteLine("ERROR: Could not find item '"+itemName+"' in "+heroName+"'s equipped items.");
}

void attachModifier(string modifierName, string actionCardName)
{
    Modifier? modifierToAttach = null;
    ActionCard? cardToModify = null;
    if (CurrentRun.InCombat)
    {
        Console.WriteLine("Cannot attach modifiers during combat!");
        return;
    }
    // First find the modifier in the inventory:
    foreach (Modifier mod in CurrentRun.Inventory.OfType<Modifier>())
    {
        if (mod.name.ToLower().Trim() == modifierName.ToLower().Trim())
        {
            modifierToAttach = mod;
            break;
        }
    }
    if (modifierToAttach == null)
    {
        Console.WriteLine("ERROR: Could not find modifier with name " + modifierName + " within your inventory.");
        return;
    }
    // Then find the action card:
    foreach (ActionCard card in CurrentRun.MasterDeck.ToList())
    {
        // Skip cards that already have a modifier:
        if (card.modifier != null) continue;

        if (card.name.ToLower().Trim() == actionCardName.ToLower().Trim().Replace('_', ' '))
        {
            cardToModify = card;
            break;
        }
    }
    // If it is not in the master deck, search the collection:
    if (cardToModify == null)
    {
        foreach (ActionCard card in CurrentRun.CardCollection.ToList())
        {
            // Skip cards that already have a modifier:
            if (card.modifier != null) continue;

            if (card.name.ToLower().Trim() == actionCardName.ToLower().Trim().Replace('_', ' '))
            {
                cardToModify = card;
                break;
            }
        }
    }
    // If still could not find the card, error:
    if (cardToModify == null)
    {
        Console.WriteLine("ERROR: Could not find card with name " + actionCardName + " within your deck or collection.");
        return;
    }
    // Finally, we have both selected; execute
    CurrentRun.AttachModifier(modifierToAttach, cardToModify);
}

// Enters the collection, where the player can edit their deck
void editMasterDeck() {
    if(CurrentRun.InCombat) {
        Console.WriteLine("Cannot edit deck during combat!");
        return;
    }
    // Outside of combat, loop through deck editor commands:
    while(true) {
        printDeckEditor();
        Console.Write("\n> ");
        cmd = Console.ReadLine();
        if(cmd == null) continue;
        //Console.Clear();
        printSeparator();
        switch(cmd.ToLower().Trim().Split()[0]) 
        {
            case "exit":
            case "quit":
                Console.WriteLine("Exited deck editor.");
                return;
            case "help":
                printDeckEditorHelp();
                break;
            case "run":
                getRunInfo();
                break;
            case "party":
                getPartyInfo();
                break;
            case "move":
                // The player wants to move a card between their master deck and collection.
                if(cmd.ToLower().Trim().Split().Length < 3) {
                    Console.WriteLine("Incorrect syntax -- requires <card> and <in/out> arguments");
                    continue;
                }
                else {
                    int cardIndex; // The index in the deck/collection where the card is; 1-indexed
                    if(!int.TryParse(cmd.ToLower().Trim().Split()[1], out cardIndex)){
                        Console.WriteLine("Incorrect syntax -- first argument must be an integer");
                        continue;
                    }
                    cardIndex--; // Make it 0-indexed for our convenience
                    string direction = cmd.ToLower().Trim().Split()[2];
                    if(direction != "in" && direction != "out") {
                        Console.WriteLine("Incorrect syntax -- second argument must either be 'in' or 'out'");
                        continue;
                    }
                    if(direction == "in") {
                        // No need to check if deck is full, the MoveToMasterDeck function handles that with an error message
                        if(cardIndex < 0 || cardIndex > CurrentRun.CardCollection.Count) {
                            Console.WriteLine("ERROR -- Card index must be in the range 1 to "+CurrentRun.CardCollection.Count);
                            continue;
                        }
                        // We now know the index is valud. Perform the move
                        CurrentRun.MoveToMasterDeck(CurrentRun.CardCollection[cardIndex]);

                    }
                    else { // Direction must be 'out'
                        if(cardIndex < 0 || cardIndex > CurrentRun.MasterDeck.Count) {
                            Console.WriteLine("ERROR -- Card index must be in the range 1 to "+CurrentRun.MasterDeck.Count);
                            continue;
                        }
                        // We now know the index is valud. Perform the move
                        CurrentRun.MoveToCollection(CurrentRun.MasterDeck[cardIndex]);
                    }

                }
                break;
            default:
                Console.WriteLine("Unknown command '"+cmd+"'.\nType 'help' for a list of valid commands.");
                break;
        }
        printSeparator();
    }
}


// Compares two strings.
// Ignores all whitespace and capitalization.
/*
static bool StringsMatchIgnoreWhitespaceLower(string str1, string str2) {
    string strippedStr1 = string.Concat(str1.Split(null)); // Split on whitespace, then reform, to remove all whitespace
    string strippedStr2 = string.Concat(str2.Split(null)); // Split on whitespace, then reform, to remove all whitespace
    string loweredStr1 = strippedStr1.ToLower();
    string loweredStr2 = strippedStr2.ToLower();
    return loweredStr1 == loweredStr2;
}*/


Console.WriteLine("Exited Roguelije game logic test.");