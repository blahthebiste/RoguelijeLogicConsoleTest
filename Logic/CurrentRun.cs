public static class CurrentRun {
    //==============================DATA==============================
    public static Random rng = new Random();
    public static int Lives; // How many lives the player has left before losing this run.
    public static int Money;
    public static int LevelCap;
    public static int PartySize;
    public static int DrawPerTurn;
    public static List<PlayerCharacter> Party; // List of all characters currently in the party.
    public static List<PlayerCharacter> Bench; // List of all characters NOT currently in the party.
    public static List<ActionCard> MasterDeck; // The current deck that the player starts each combat with.
    public static List<Item> Inventory; // All unequipped items, unused modifiers, unused legend books, and unused ascension books.
    public static Zones CurrentZone;
    public static int ZoneProgress; // The number of combat encounters that have been completed in this zone.
    public static List<Zone> CompletedZones;
    //==============================END DATA==============================
    
    //==============================CONSTRUCTORS==============================
    static CurrentRun() {
        Lives = 6; // Subject to change
        Money = 0; // Subject to change
        LevelCap = 1; // Player cannot level anyone up until they acquire a Chaos Tome.
        PartySize = 3; // Also increases later via Chaos Tomes.
        DrawPerTurn = 4; // Also increases later via Chaos Tomes.
        Party = new List<PlayerCharacter>(); // Decided shortly, but not yet
        Bench = new List<PlayerCharacter>(); // Starts empty.
        MasterDeck = new List<ActionCard>(); // Populate the default starter deck (2 of each? Or 3?)
        MasterDeck.Add(new BasicAttack());
        MasterDeck.Add(new BasicAttack());
        MasterDeck.Add(new BasicAttack());
        MasterDeck.Add(new BasicDefend());
        MasterDeck.Add(new BasicDefend());
        MasterDeck.Add(new BasicDefend());
        MasterDeck.Add(new BasicSkill());
        MasterDeck.Add(new BasicSkill());
        MasterDeck.Add(new BasicSkill());
        MasterDeck.Add(new BasicSpell());
        MasterDeck.Add(new BasicSpell());
        MasterDeck.Add(new BasicSpell());
        MasterDeck.Add(new BasicRest());
        MasterDeck.Add(new BasicRest());
        MasterDeck.Add(new BasicRest());
        Inventory = new List<Item>(); // Starts empty(?).
        CurrentZone = Zones.Hub; // Party is selected in the Hub world.
        ZoneProgress = 0;
        CompletedZones = new List<Zone>(); // Starts empty
    }
    //==============================END CONSTRUCTORS==============================
    
    //==============================PARTY FUNCTIONS==============================
    // Whether there are any empty slots in the party currently.
    public static bool RoomInParty() {
        if(Party.Count < PartySize){
            return true;
        }
        return false;
    }
    
    // When the player gains a character.
    public static void AddToParty(PlayerCharacter newPartyMember){
        if(RoomInParty()) {
            // There is room in the party; add them immediately
            Party.Add(newPartyMember);
            // Add their action card to the master deck:
            MasterDeck.Add(newPartyMember.personalCard);
        }
        else {
            // Party is full; send them to the bench.
            Bench.Add(newPartyMember);
        }
    }
    
    // Move a character from the bench to the party
    public static void MoveToParty(PlayerCharacter partyMember){
        if(Bench.Contains(partyMember)) {
            if(RoomInParty()) {
                // There is room in the party
                Bench.Remove(partyMember);
                Party.Add(partyMember);
                // Add their action card to the master deck:
                MasterDeck.Add(partyMember.personalCard);
            }
            else {
                // print error; this should never happen
            }
        }
        else {
            // do nothing
        }
    }
    
    // Move a party member from the party to the bench.
    public static void MoveToBench(PlayerCharacter partyMember){
        if(Party.Contains(partyMember)) {
            // They were in the party. Move them to the bench.
            Party.Remove(partyMember);
            // Remove their action card from the master deck:
            MasterDeck.Remove(partyMember.personalCard);
            Bench.Add(partyMember);
        }
        else {
            // do nothing (maybe print error?)
        }
    }
    
    // Deletes a character entirely. Used only on levelups.
    public static void RemoveCharacter(PlayerCharacter partyMember) {
        // Remove their items and put back in the inventory.
        foreach(Action action in partyMember.ActionList) {
            if(action.equippedItem != null) {
                Inventory.Add(action.equippedItem);
                action.equippedItem = null;
            }
        }
        if(Party.Contains(partyMember)) {
            // They were in the party.
            Party.Remove(partyMember);
            // Remove their action card from the master deck:
            MasterDeck.Remove(partyMember.personalCard);
        }
        else {
            // They were on the bench.
            Bench.Remove(partyMember);
        }
    }
    
    // Swaps the order of player characters in the party:
    public static void SwapPartyOrder(PlayerCharacter hero1, PlayerCharacter hero2) {
        if(!Party.Contains(hero1) || !Party.Contains(hero2)) {
            // Error: both heroes must be in the party to be swapped!
        }
        Party[Party.IndexOf(hero1)] = hero2;
        Party[Party.IndexOf(hero2)] = hero1;
    }
    //==============================END PARTY FUNCTIONS==============================
    
    //==============================REWARDS FUNCTIONS==============================
    
    // When the player wins, give them stuff.
    public static void DistributeCombatRewards() {
            int randomMoneyReward = rng.Next(40, 61);
            int extraMoneyReward = 0;
            // Generate rewards based on the ZoneProgress.
            if(ZoneProgress % 10 == 0) // Every 10th stage is a Boss combat
            {
                extraMoneyReward = rng.Next(40, 61);
                GenerateNextChaosTome();
            }
            else if(ZoneProgress % 3 == 0) // Every 3rd stage is a MiniBoss combat
            {
                extraMoneyReward = rng.Next(40, 61);
                Inventory.Add(new AscensionBook());
            }
            else // Normal combat.
            {
                // do nothing, no special rewards other than the money.
            }
            Money = Money + randomMoneyReward + extraMoneyReward;
        }

    public static void GenerateNextChaosTome() {
        // TODO
    }
    //==============================END REWARDS FUNCTIONS==============================
    
    //==============================ZONE FUNCTIONS==============================
    public static void SetZone(Zones newZone) {
        CurrentZone = newZone;
        ZoneProgress = 1; // Reset zone progress to area 1.
    }
    
    //==============================END ZONE FUNCTIONS==============================
}
