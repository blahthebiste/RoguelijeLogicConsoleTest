public static class CurrentRun
{
    //==============================DATA==============================
    //===
    //===
    //===
    public static Random rng = new Random();
    public static int Lives; // How many lives the player has left before losing this run.
    public static int Money;
    public static int LevelCap;
    public static int PartySize;
    public static int MinimumDeckSize;
    public static int DrawPerTurn;
    public static List<PlayerCharacter> Party; // List of all characters currently in the party.
    public static List<PlayerCharacter> Bench; // List of all characters NOT currently in the party.
    public static List<ActionCard> MasterDeck; // The current deck that the player starts each combat with.
    public static List<ActionCard> CardCollection; // The extra cards that the player collects throughout a run.
    //public static Dictionary<ActionType, > CardOrderDict; // Used for determining what order to show cards in
    public static List<Item> Inventory; // All unequipped items, unused modifiers, unused legend books, and unused ascension books.
    public static Zone CurrentZone;
    public static int ZoneProgress; // The number of combat encounters that have been completed in this zone.
    public static List<Zone> CompletedZones;
    public static bool InARun;
    public static bool InCombat;
    public static bool LastEncounterWasEncounter; // Signifies whether the player has completed their Encounter yet.

    public static List<string> Tier1ItemPool;
    public static List<string> Tier2ItemPool;
    public static List<string> Tier3ItemPool;

    public static List<Encounter> EncounterPool; // Resets after each act

    public static int NUMDRAFTABLEBASICCARDS = 10;
    public static List<ActionCard> DraftableCardPool; // Pool of cards that the player can draft. Starts full of basic action cards.
    public static List<ActionCard> ComplexCardPool; // Pool of advanced cards that the player can eventually draft.

    public static CombatReward NextCombatReward = new CombatReward();
    public static CombatEncounter? NextCombatEncounter;
    //===
    //===
    //===
    //==============================END DATA==============================




    //==============================CONSTRUCTORS==============================
    //===
    //===
    //===
    static CurrentRun()
    {
        Lives = 6; // Subject to change
        Money = 0; // Subject to change
        LevelCap = 1; // Player cannot level anyone up until they acquire a Chaos Tome.
        PartySize = 3; // Also increases later via Chaos Tomes.
        MinimumDeckSize = 15; // Can be changed during a run through Encounters
        DrawPerTurn = 4; // Also increases later via Chaos Tomes.
        Party = new List<PlayerCharacter>(); // Decided shortly, but not yet
        Bench = new List<PlayerCharacter>(); // Starts empty.
        MasterDeck = new List<ActionCard>(); // Populate the default starter deck (2 of each? Or 3?)
        CardCollection = new List<ActionCard>(); // Starts empty
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
        CurrentZone = DataRegistry.GenerateZone(ZoneID.HUB); // Party is selected in the Hub world.
        ZoneProgress = 0;
        CompletedZones = new List<Zone>(); // Starts empty
        InARun = false;
        InCombat = false;
        LastEncounterWasEncounter = true; // Starts true so that the player goes to combat first
        Tier1ItemPool = new List<string>();
        Tier2ItemPool = new List<string>();
        Tier3ItemPool = new List<string>();
        DraftableCardPool = new List<ActionCard>();
        ComplexCardPool = new List<ActionCard>();
        EncounterPool = new List<Encounter>();
        PopulateItemPools();
        PopulateDraftPool();
        PopulateComplexDraftPool();
        PopulateEncounterPool();
    }
    //===
    //===
    //===
    //==============================END CONSTRUCTORS==============================




    //==============================PARTY FUNCTIONS==============================
    //===
    //===
    //===
    // Whether there are any empty slots in the party currently.
    public static bool RoomInParty()
    {
        if (Party.Count < PartySize)
        {
            return true;
        }
        return false;
    }

    // When the player gains a character.
    public static void AddToParty(PlayerCharacter newPartyMember)
    {
        if (RoomInParty())
        {
            // There is room in the party; add them immediately
            Party.Add(newPartyMember);
            // Add their action card to the master deck:
            MasterDeck.Add(newPartyMember.personalCard);
        }
        else
        {
            // Party is full; send them to the bench.
            Bench.Add(newPartyMember);
        }
    }

    // Move a character from the bench to the party
    public static void MoveToParty(PlayerCharacter partyMember)
    {
        if (Bench.Contains(partyMember))
        {
            if (RoomInParty())
            {
                // There is room in the party
                Bench.Remove(partyMember);
                Party.Add(partyMember);
                // Add their action card to the master deck:
                MasterDeck.Add(partyMember.personalCard);
            }
            else
            {
                // print error; this should never happen
            }
        }
        else
        {
            // do nothing
        }
    }

    // Move a party member from the party to the bench.
    public static void MoveToBench(PlayerCharacter partyMember)
    {
        if (Party.Contains(partyMember))
        {
            // They were in the party. Move them to the bench.
            Party.Remove(partyMember);
            // Remove their action card from the master deck:
            MasterDeck.Remove(partyMember.personalCard);
            Bench.Add(partyMember);
        }
        else
        {
            // do nothing (maybe print error?)
        }
    }

    // Deletes a character entirely. Used only on levelups.
    public static void RemoveCharacter(PlayerCharacter partyMember)
    {
        // Remove their items and put back in the inventory.
        foreach (Action action in partyMember.ActionList)
        {
            if (action.equippedItem != null)
            {
                Inventory.Add(action.equippedItem);
                action.equippedItem = null;
            }
        }
        if (Party.Contains(partyMember))
        {
            // They were in the party.
            Party.Remove(partyMember);
            // Remove their action card from the master deck:
            MasterDeck.Remove(partyMember.personalCard);
        }
        else
        {
            // They were on the bench.
            Bench.Remove(partyMember);
        }
    }

    // Swaps the order of player characters in the party:
    public static void SwapPartyOrder(PlayerCharacter hero1, PlayerCharacter hero2)
    {
        if (!Party.Contains(hero1) || !Party.Contains(hero2))
        {
            // Error: both heroes must be in the party to be swapped!
        }
        Party[Party.IndexOf(hero1)] = hero2;
        Party[Party.IndexOf(hero2)] = hero1;
    }
    //===
    //===
    //===
    //==============================END PARTY FUNCTIONS==============================




    //==============================DECK FUNCTIONS==============================
    //===
    //===
    //===
    // Whether there are any empty slots in the master deck currently.
    public static bool RoomInDeck()
    {
        if (MasterDeck.Count < MinimumDeckSize)
        {
            return true;
        }
        return false;
    }

    // Re-orders the given list of cards by action type->rarity.
    public static List<ActionCard> ReorderCards(List<ActionCard> deck)
    {
        List<ActionCard> orderedDeck = new List<ActionCard>();
        foreach (ActionCard card in deck)
        {
            // For now, do not actually change the order
            // TODO: use LINQ for ordering or something
            orderedDeck.Add(card);
        }
        return orderedDeck;
    }

    // Moves a card from the Master deck into the collection
    public static void MoveToCollection(ActionCard card)
    {
        if (MasterDeck.Contains(card))
        {
            Console.WriteLine("Removing " + card.name + " from master deck");
            MasterDeck.Remove(card);
            CardCollection.Add(card);
            CardCollection = ReorderCards(CardCollection);
        }
        else
        {
            // print error
            Console.WriteLine("ERROR: card " + card.name + " does not exist in your master deck!");
        }
    }

    // Move a character from the bench to the party
    public static void MoveToMasterDeck(ActionCard card)
    {
        if (CardCollection.Contains(card))
        {
            if (RoomInDeck())
            {
                // There is room in the deck
                Console.WriteLine("Adding " + card.name + " to master deck");
                CardCollection.Remove(card);
                MasterDeck.Add(card);
                MasterDeck = ReorderCards(MasterDeck);
            }
            else
            {
                // print error
                Console.WriteLine("ERROR: cannot add card to master deck; master deck is full!");
            }
        }
        else
        {
            // print error
            Console.WriteLine("ERROR: card " + card.name + " does not exist in your collection!");
        }
    }


    //===
    //===
    //===
    //==============================END DECK FUNCTIONS==============================




    //==============================REWARDS FUNCTIONS==============================
    //===
    //===
    //===
    // When the player wins, give them stuff.
    public static void DistributeCombatRewards()
    {
        int randomMoneyReward = rng.Next(40, 61);
        int extraMoneyReward = 0;
        // Generate rewards based on the ZoneProgress.
        if (ZoneProgress % 10 == 0) // Every 10th stage is a Boss combat
        {
            extraMoneyReward = rng.Next(40, 61);
            GenerateNextChaosTome(); // TODO: make claimable combat reward
        }
        else if (ZoneProgress % 3 == 0) // Every 3rd stage is a MiniBoss combat
        {
            extraMoneyReward = rng.Next(40, 61);
            NextCombatReward.itemRewards.Add(new AscensionBook());
        }
        else // Normal combat. // Prompt for card draft
        {
            CardDraft draft = new CardDraft();
            draft.execute();

        }
        // TODO: lore bonus based on combat difficulty?
        NextCombatReward.moneyReward = randomMoneyReward + extraMoneyReward;
        Money += NextCombatReward.moneyReward;
        Console.WriteLine("Earned $" + NextCombatReward.moneyReward);
        foreach (Item item in NextCombatReward.itemRewards)
        {
            Inventory.Add(item);
            Console.WriteLine("Acquired a " + item.name);
        }
    }


    public static void GenerateNextChaosTome()
    {
        Console.WriteLine("YOU HAVE ACQUIRED A CHAOS TOME.");
        NextCombatReward.itemRewards.Add(new ChaosTome(CompletedZones.Count));
    }

    //===
    //===
    //===
    //==============================END REWARDS FUNCTIONS==============================




    //==============================ENCOUNTER FUNCTIONS==============================
    //===
    //===
    //===

    // Fills the pool of draftable cards with basic action cards.
    public static void PopulateDraftPool()
    {
        for (int i = 0; i < NUMDRAFTABLEBASICCARDS; i++)
        {
            DraftableCardPool.Add(new BasicAttack());
            DraftableCardPool.Add(new BasicDefend());
            DraftableCardPool.Add(new BasicRest());
            DraftableCardPool.Add(new BasicSkill());
            DraftableCardPool.Add(new BasicSpell());
        }
        Console.WriteLine("Populated draftable card pool.");
    }

    // Fills the pool of not-quite-yet-draftable cards with complex action cards.
    public static void PopulateComplexDraftPool()
    {
        ComplexCardPool.Add(new DualAttackDefend());
        // Randomize the order:
        Shuffle(ComplexCardPool);
        Console.WriteLine("Populated complex card pool.");
    }

    // Add the card at the specified index to the player's master deck, and replace it in the pool with a complex action card. 
    public static void DraftCard(int index)
    {
        if (index < 0 || index > DraftableCardPool.Count)
        {
            Console.WriteLine("ERROR: index out of bounds for DraftableCardPool!");
        }
        else
        {
            ActionCard newCard = DraftableCardPool[index]!;
            CardCollection.Add(newCard);
            Console.WriteLine("Added one '" + newCard.name + "' card to your collection.");
            DraftableCardPool.Remove(newCard); // Remove drafted card from pool
            DraftableCardPool.Add(ComplexCardPool[0]); // Move complex card into draft pool
            ComplexCardPool.RemoveAt(0);
            // If ComplexCardPool is exhausted, add a Wound
            if (ComplexCardPool.Count == 0)
            {
                ComplexCardPool.Add(new Wound());
            }
        }
    }

    // TODO: fill out
    public static void PopulateEncounterPool()
    {
        // Remove card draft from encounter pool, it's just way worse than other options
        // for(int i = 0; i < 25; i++) {
        //     // Add 25x card draft Encounter
        //     EncounterPool.Add(new CardDraft());
        // }
        for (int i = 0; i < 10; i++)
        {
            // Add 10x shop Encounter
            EncounterPool.Add(new Shop());
        }
        for (int i = 0; i < 10; i++)
        {
            // Add 10x plunder Encounter
            EncounterPool.Add(new Plunder());
        }
        for (int i = 0; i < 2; i++)
        {
            // Add 2x duplicate Encounter
            EncounterPool.Add(new Duplicate());
        }
        for (int i = 0; i < 2; i++)
        {
            // Add 2x removal Encounter
            EncounterPool.Add(new Removal());
        }

        // For debugging:
        // for (int i = 0; i < 100; i++)
        // {
        //     EncounterPool.Add(new Removal());
        // }

        Console.WriteLine("Populated Encounter pool.");
    }


    // Picks 3 valid Encounters from the Encounter pool.
    // 2 will be shown to the player, 1 will be hidden.
    // The player will decide which of the 3 Encounters they want to go to.
    public static void GenerateEncounters()
    {
        // Shuffle the pool so that the first 3 are random:
        Shuffle(EncounterPool);
        // Reroll duplicates (should the mysery Encounter always be non-duplicate too?)
        while (EncounterPool[1]!.name == EncounterPool[0]!.name)
        {
            Encounter dupeEncounter = EncounterPool[1];
            // Move to the bottom of the list
            EncounterPool.RemoveAt(1);
            EncounterPool.Add(dupeEncounter);
        }
        while (EncounterPool[2]!.name == EncounterPool[1]!.name || EncounterPool[2]!.name == EncounterPool[0]!.name)
        {
            Encounter dupeEncounter = EncounterPool[2];
            // Move to the bottom of the list
            EncounterPool.RemoveAt(2);
            EncounterPool.Add(dupeEncounter);
        }
        // The pool should always be large enough that there are 3 unique Encounters, and thus the de-duping code should always resolve.
        // The player never exhausts all Encounters in the pool during a Zone.

        // Display the first 3
        while (true)
        {
            Console.WriteLine("Choose one of the following Encounters to visit, by entering its number:\n");
            Console.WriteLine("\t[1] " + EncounterPool[0]!.name);
            Console.WriteLine("\t[2] ??? Mystery Encounter ???");
            Console.WriteLine("\t[3] " + EncounterPool[2]!.name);
            Console.WriteLine("");
            Console.Write("\n> ");
            string? cmd2 = Console.ReadLine();
            if (cmd2 == null) continue;
            if (int.TryParse(cmd2.ToLower().Trim(), out int encounterSelection))
            {
                // If they entered a valid number for card selection, add it to their collection:
                if (encounterSelection <= 3 && encounterSelection > 0)
                {
                    EnterEncounter(encounterSelection - 1);
                    return;
                }
                else
                {
                    Console.WriteLine("Must enter a number between 1 and 3.\n");
                }
            }
            else
            {
                Console.WriteLine("Must enter a number between 1 and 3.\n");
            }
        }
    }

    // Execute the Encounter at the specified index of the Encounter pool, and remove it from the pool.
    public static void EnterEncounter(int index)
    {
        Encounter chosenEncounter = EncounterPool[index];
        EncounterPool.Remove(chosenEncounter);
        Console.WriteLine("Entering " + chosenEncounter.name + "...\n");
        chosenEncounter.execute();
        LastEncounterWasEncounter = true;
    }
    //===
    //===
    //===
    //==============================END ENCOUNTER FUNCTIONS==============================

    //==============================ZONE FUNCTIONS==============================
    //===
    //===
    //===
    public static void SetZone(ZoneID newZoneID)
    {
        CurrentZone = DataRegistry.GenerateZone(newZoneID);
        ZoneProgress = 1; // Reset zone progress to area 1.
        EncounterPool = new List<Encounter>(); // Reset Encounter pool
        PopulateEncounterPool();
    }


    // Sets the NextCombatEncounter (which can then be started by EnterCombat().)
    // Picks 3 valid combats from the encounter pool, and display them and their difficulty rating to the player.
    // The player will decide which of the 3 fights they want to battle next.
    // For now, combat options are predetermined by the zone and progress (this may change).
    public static void GenerateNextCombat()
    {
        if (NextCombatEncounter != null)
        {
            Console.WriteLine("Next combat encounter has already been chosen!");
            return;
        }
        List<CombatEncounter> nextEncounterOptions = new List<CombatEncounter>();
        switch (ZoneProgress)
        {
            case 1:
            case 2:
                nextEncounterOptions = GetRandomCombatEncounters(CurrentZone.Phase1CombatEncounters, 3);
                break;
            case 3:
                nextEncounterOptions = GetRandomCombatEncounters(CurrentZone.Miniboss1Encounters, 3);
                break;
            case 4:
            case 5:
                nextEncounterOptions = GetRandomCombatEncounters(CurrentZone.Phase2CombatEncounters, 3);
                break;
            case 6:
                nextEncounterOptions = GetRandomCombatEncounters(CurrentZone.Miniboss2Encounters, 3);
                break;
            case 7:
            case 8:
                nextEncounterOptions = GetRandomCombatEncounters(CurrentZone.Phase3CombatEncounters, 3);
                break;
            case 9:
                nextEncounterOptions = GetRandomCombatEncounters(CurrentZone.Miniboss3Encounters, 3);
                break;
            case 10:
                nextEncounterOptions = GetRandomCombatEncounters(CurrentZone.BossEncounters, 3);
                break;
            default:
                Console.WriteLine("ERROR: invalid zone progress while generating next combats");
                return;
        }
        if (nextEncounterOptions == null || nextEncounterOptions.Count == 0)
        {
            // No valid combats
            Console.WriteLine("ERROR: No valid combat encounters found!");
            return;
        }
        if (nextEncounterOptions.Count == 1)
        {
            // Only 1 valid combat was found.
            NextCombatEncounter = nextEncounterOptions[0];
            PrintNextCombat();
            return;
        }
        else
        {
            // Multiple valid combat encounters found.
            // Display them all and let the player choose.
            while (true)
            {
                Console.WriteLine("Choose one of the following combat encounters, by entering its number:\n");
                for (int i = 0; i < nextEncounterOptions.Count; i++)
                {
                    Console.WriteLine("\t[" + (i + 1) + "] " + nextEncounterOptions[i]);
                }
                Console.WriteLine("");
                Console.Write("\n> ");
                string? cmd2 = Console.ReadLine();
                if (cmd2 == null) continue;
                if (int.TryParse(cmd2.ToLower().Trim(), out int combatSelection))
                {
                    // If they entered a valid number for combat selection, set it as next:
                    if (combatSelection <= nextEncounterOptions.Count && combatSelection > 0)
                    {
                        NextCombatEncounter = nextEncounterOptions[combatSelection - 1]!;
                        CurrentZone.RemoveEncounter(NextCombatEncounter); // Prevent it from appearing again
                        PrintNextCombat();
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Must enter a number between 1 and " + nextEncounterOptions.Count + ".\n");
                    }
                }
                else
                {
                    Console.WriteLine("Must enter a number between 1 and " + nextEncounterOptions.Count + ".\n");
                }
            }
        }
    }

    public static void PrintNextCombat()
    {
        if (NextCombatEncounter == null)
        {
            Console.WriteLine("Next combat encounter has not been selected.");
            return;
        }
        switch (ZoneProgress)
        {
            case 1:
            case 2:
            case 4:
            case 5:
            case 7:
            case 8:
                Console.WriteLine("Next combat:");
                break;
            case 3:
            case 6:
            case 9:
                Console.WriteLine("Upcoming Miniboss:");
                break;
            case 10:
                Console.WriteLine("Prepare for the Boss:");
                break;
        }
        Console.WriteLine(NextCombatEncounter.name);
    }

    // Begins combat
    public static void EnterCombat()
    {
        if (NextCombatEncounter == null)
        {
            Console.WriteLine("Cannot enter combat; need to select next combat encounter!");
            return;
        }
        Console.WriteLine("Entering combat...");
        LastEncounterWasEncounter = false;
        InCombat = true;
        Battlefield.LoadCombat(NextCombatEncounter);
        CardManager.beginCombat();
    }

    // Tries to get <numEncountersRequested> random combat encounters from the given list.
    // May return a list shorter than numEncountersRequested if encounterList does not have enough.
    public static List<CombatEncounter> GetRandomCombatEncounters(List<CombatEncounter> encounterList, int numEncountersRequested)
    {
        List<CombatEncounter> randomEncounters = new List<CombatEncounter>();
        Shuffle(encounterList);
        int returnCount = (numEncountersRequested < encounterList.Count) ? numEncountersRequested : encounterList.Count;
        for (int i = 0; i < returnCount; i++)
        {
            randomEncounters.Add(encounterList[i]);
        }
        return randomEncounters;
    }
    //===
    //===
    //===
    //==============================END ZONE FUNCTIONS==============================




    //==============================ITEM POOL FUNCTIONS==============================
    //===
    //===
    //===
    // Populate all 3 item pools
    public static void PopulateItemPools()
    {
        // TODO
        Tier1ItemPool.Add("Shortsword");
        Tier1ItemPool.Add("Longbow");
        Tier1ItemPool.Add("TowerShield");
        Tier1ItemPool.Add("Dagger");
        Tier1ItemPool.Add("LeatherBoots");
        Tier1ItemPool.Add("Rope");
        Tier1ItemPool.Add("IceWand");
        Tier1ItemPool.Add("Medkit");
        Tier1ItemPool.Add("Dreamcatcher");
        Tier1ItemPool.Add("Pavise");
        Tier1ItemPool.Add("Tomahawk");
        Tier1ItemPool.Add("Quarterstaff");
        Tier1ItemPool.Add("Sapphire");
        Tier1ItemPool.Add("Robes");
        Tier1ItemPool.Add("Campfire");
        Tier1ItemPool.Add("IronHelm");
        Tier1ItemPool.Add("Chainmail");
        Tier1ItemPool.Add("HealthPotion");
        Tier1ItemPool.Add("ManaPotion");
        Tier1ItemPool.Add("PoisonPotion");
        Tier1ItemPool.Add("Grog");
        Tier1ItemPool.Add("HairTrigger");
        Tier1ItemPool.Add("HeartCrystal");

        Tier2ItemPool.Add("TimeTurner");
        Tier2ItemPool.Add("PlateArmor");
        Tier2ItemPool.Add("Battleaxe");
        Tier2ItemPool.Add("Banner");
        Tier2ItemPool.Add("WhiteFlag");
        Tier2ItemPool.Add("PhoenixWand");
        Tier2ItemPool.Add("GrapplingHook");
        Tier2ItemPool.Add("RevivePotion");

        Tier3ItemPool.Add("MasterSword");
        Tier3ItemPool.Add("ElderWand");
        Tier3ItemPool.Add("ResurrectionStone");
        Tier3ItemPool.Add("InvisibilityCloak");
        Tier3ItemPool.Add("HeavyArmor");
        Tier3ItemPool.Add("FortressShield");

        // For debugging items:
        Inventory.Add(new Campfire());
        Inventory.Add(new ManaPotion());
        Inventory.Add(new PlateArmor());
        Inventory.Add(new RevivePotion());
    }

    // Gets a random item from the specified tier (1-3).
    // By default, the item is removed from the pools.
    public static EquipmentItem getRandomItemFromPool(int tier, bool removeFromPool = true)
    {
        List<string> itemPool;
        switch (tier)
        {
            case 1:
                itemPool = Tier1ItemPool;
                break;
            case 2:
                itemPool = Tier2ItemPool;
                break;
            case 3:
                itemPool = Tier3ItemPool;
                break;
            default:
                Console.WriteLine("ERROR: No Item pool exists for tier " + tier);
                return new RubberDuck();
        }

        if (itemPool.Count < 1)
        {
            Console.WriteLine("WARNING: Item pool is empty. Generating placeholder");
            return new RubberDuck();
        }
        Item? retrievedItem;
        // Generate a random item
        int randomIndex = rng.Next(0, itemPool.Count);
        string chosenItemName = itemPool[randomIndex];
        retrievedItem = DataRegistry.ItemData.getItemByName(chosenItemName);
        if (retrievedItem is EquipmentItem)
        {
            if (removeFromPool)
            {
                // Remove from all item pools
                Tier1ItemPool.Remove(chosenItemName);
                Tier2ItemPool.Remove(chosenItemName);
                Tier3ItemPool.Remove(chosenItemName);
            }
            return (EquipmentItem)retrievedItem;
        }
        Console.WriteLine("WARNING: Item was null or not equippable");
        return new RubberDuck();
    }
    //===
    //===
    //===
    //============================END ITEM POOL FUNCTIONS============================





    //============================MISC UTIL FUNCTIONS============================
    //===
    //===
    //===
    public static void LoseLives(int numLives)
    {
        Lives -= numLives;
        if (Lives < 1)
        {
            // Game over, man!
            Console.WriteLine("GAME OVER.");
        }
    }

    // Source: https://stackoverflow.com/a/69220421/5086634
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    // Attempts to equip the specified item to the hero.
    // If multiple action slots are valid options, prompts the user to choose.
    // Errors if the item or hero is not found, or if the hero is exhausted
    public static void equipItem(string itemName, string heroName) {
        PlayerCharacter? heroToEquip = null;
        EquipmentItem? itemToEquip = null;
        // Works a little differently depending on whether we are in combat or not.
        if(InCombat) {
            // Combat version: only alive heroes who are not on the bench can item swap.
            // This takes the hero's action.

            // First, find the hero:
            foreach(PlayerCharacter hero in Battlefield.PlayerSide){
                // PlayerSide only includes living heroes
                if(hero.name.ToLower().Trim() == heroName.ToLower().Trim()) {
                    heroToEquip = hero;
                }
            }
            // Now find the item:
            foreach(Item item in CurrentRun.Inventory){
                if(item.name.ToLower().Trim().Replace(' ','_') == itemName.ToLower().Trim() || item.name.ToLower().Trim() == itemName.ToLower().Trim()) {
                    if(item is EquipmentItem) {
                        itemToEquip = (EquipmentItem)item;
                    }
                    else {
                        Console.WriteLine("ERROR: "+itemName+" is not an equippable item!");
                        return;
                    }
                }
            }
            // Finally, check if the hero can act:
            if(heroToEquip != null && heroToEquip.exhausted) {
                Console.WriteLine("ERROR: "+heroName+" cannot swap items because they are exhausted.");
                return;
            }
        }
        else if(CurrentRun.InARun){
            // Non-combat version: free swapping for all heroes, bench or not
            // First, find the hero:
            foreach(PlayerCharacter hero in CurrentRun.Party){
                // PlayerSide only includes living heroes
                if(hero.name.ToLower().Trim() == heroName.ToLower().Trim()) {
                    heroToEquip = hero;
                }
            }
            // Also search the benched heroes:
            foreach(PlayerCharacter hero in CurrentRun.Bench){
                // PlayerSide only includes living heroes
                if(hero.name.ToLower().Trim() == heroName.ToLower().Trim()) {
                    heroToEquip = hero;
                }
            }
            // Now find the item:
            foreach(Item item in CurrentRun.Inventory){
                if(item.name.ToLower().Trim().Replace(' ','_') == itemName.ToLower().Trim()) {
                    if(item is EquipmentItem) {
                        itemToEquip = (EquipmentItem)item;
                    }
                    else {
                        Console.WriteLine("ERROR: "+itemName+" is not an equippable item!");
                        return;
                    }
                }
            }
            
        }
        if(itemToEquip == null) {
            Console.WriteLine("ERROR: Could not find item with name "+itemName);
            return;
        }
        if(heroToEquip == null) {
            Console.WriteLine("ERROR: Could not find hero with name "+heroName);
            return;
        }
        int numMatchingActions = itemToEquip.numberMatchingActions(heroToEquip);
        // If the hero has no matching actions, print an error:
        if(numMatchingActions == 0) {
            Console.WriteLine("Item '"+itemName+"' cannot be equipped by "+heroName+"; no actions match item's slot restrictions");
            return;
        }
        // If the hero has multiple matching actions, prompt the player to choose one:
        if(numMatchingActions > 1) {
            Console.WriteLine(heroName+" has multiple actions that this item can be equipped to.");
            Console.WriteLine("Select one from the following by entering its number, or type something else to go back:");
            Console.WriteLine("");
            List<Action> matchingActions = new List<Action>();
            // Find all matching actions from the hero's action list:
            foreach(Action action in heroToEquip.ActionList) {
                if(itemToEquip.matchesActionType(action)) {
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
                // If they entered a valid number for action selection, equip the item to the action:
                if(actionSelection <= matchingActions.Count && actionSelection > 0) {
                    itemToEquip.Equip(matchingActions[actionSelection-1]);
                }
            }
            // Return afterwards regardless.
            return;
        }
        // If the hero has exactly 1 matching action, equip the item to the action:
        if(numMatchingActions == 1) {
            // Find the first matching action from the hero's action list:
            foreach(Action action in heroToEquip.ActionList) {
                if(itemToEquip.matchesActionType(action)) {
                    action.Equip(itemToEquip);
                    break;
                }
            }
        }
    }
    //===
    //===
    //===
    //============================END MISC UTIL FUNCTIONS============================
}
