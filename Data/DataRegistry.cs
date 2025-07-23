using System.Text.Json;

// Loads data from data files on startup.
public static class DataRegistry {

    public static List<Entity> TroupeData = new List<Entity>();

    public static void LoadData() {
        Console.WriteLine("Loading data...");
        CharacterData.LoadPlayerData();
        Console.WriteLine("Loaded player character data.");
        CharacterData.LoadEnemyData();
        Console.WriteLine("Loaded enemy character data.");
        EnemyTroupes.LoadTroupeData();
        Console.WriteLine("Loaded enemy troupe data.");
        // TODO: load item data?
    }

    public static void loadTroupeData(string combatID) {
        // TODO
    }

    public static Zone GenerateZone(ZoneID zoneID) {
        // TODO
        return new Zone(zoneID);
    }

    public static class Messages {
        public static List<string> startScreenMessage = new List<string>(){
            "============================================================",
            "Welcome to Roguelije!\n",
            "\tYou can view a list of all commands by typing 'help'.",
            "\tFor now, choose one of the following options:\n",
            "\texit - exits the game\t\tstart - begin a run\n",
            "============================================================"
        };

        public static List<string> characterSelectMessage = new List<string>(){
            "\nChoose starting party",
            "Enter the name of a character to learn more about them.",
            "\n\t* "+CharacterData.getPlayerDataByName("Fighter")!.Name+": "+CharacterData.getPlayerDataByName("Fighter")!.Description,
            "\n\t* "+CharacterData.getPlayerDataByName("Defender")!.Name+": "+CharacterData.getPlayerDataByName("Defender")!.Description,
            "\n\t* "+CharacterData.getPlayerDataByName("Thief")!.Name+": "+CharacterData.getPlayerDataByName("Thief")!.Description,
            "\n\t* "+CharacterData.getPlayerDataByName("Mage")!.Name+": "+CharacterData.getPlayerDataByName("Mage")!.Description,
            "\n\t* "+CharacterData.getPlayerDataByName("Healer")!.Name+": "+CharacterData.getPlayerDataByName("Healer")!.Description
        };

    }

    public static class CharacterData {
        public static string playerDataPath = "Data/Characters/PlayerCharacters.json";
        public static string enemyDataPath = "Data/Characters/Enemies.json";

        public static List<PlayerData>? PlayerDataList = new List<PlayerData>();
        public static List<EnemyData>? EnemyDataList = new List<EnemyData>();

        public static void LoadPlayerData() {
            string json = File.ReadAllText(playerDataPath);
            PlayerDataList = JsonSerializer.Deserialize<List<PlayerData>>(json);
            if(PlayerDataList == null) {
                Console.WriteLine("Failed to load player data.");
                return;
            }
            Console.WriteLine("Loaded "+PlayerDataList.Count+" player characters from json.");
        }
        
        public static void LoadEnemyData()
        {
            string json = File.ReadAllText(enemyDataPath);
            EnemyDataList = JsonSerializer.Deserialize<List<EnemyData>>(json);
            if (EnemyDataList == null)
            {
                Console.WriteLine("Failed to load enemy data.");
                return;
            }
            Console.WriteLine("Loaded " + EnemyDataList.Count + " enemies from json.");
            foreach(EnemyData data in EnemyDataList){
                Console.WriteLine("Found "+data.Name);
            }
        }

        public static PlayerData? getPlayerDataByName(string characterName) {
            if(PlayerDataList == null) {
                Console.WriteLine("Cannot get player data -- Failed to load.");
                return null;
            }
            foreach(PlayerData data in PlayerDataList) {
                if(data.Name.ToLower().Trim() == characterName.ToLower().Trim()) {
                    Console.WriteLine("Found match for player character with ID = "+characterName);
                    return data;
                }
            }
            Console.WriteLine("ERROR: No match found for player character with ID = "+characterName);
            return null;
        }
        
        public static EnemyData? getEnemyDataByName(string characterName)
        {
            if (EnemyDataList == null)
            {
                Console.WriteLine("Cannot get enemy data -- Failed to load.");
                return null;
            }
            foreach (EnemyData data in EnemyDataList)
            {
                if (data.Name.ToLower().Trim() == characterName.ToLower().Trim())
                {
                    Console.WriteLine("Found match for enemy with ID = " + characterName);
                    return data;
                }
            }
            Console.WriteLine("ERROR: No match found for enemy with ID = " + characterName);
            return null;
        }

    }

    public static class EnemyTroupes {

        public static string TroupeDataPath = "Data/Characters/EnemyTroupes.json";

        public static List<TroupeData>? TroupeDataList = new List<TroupeData>();

        public static void LoadTroupeData() {
            string json = File.ReadAllText(TroupeDataPath);
            TroupeDataList = JsonSerializer.Deserialize<List<TroupeData>>(json);
            if(TroupeDataList == null) {
                Console.WriteLine("Failed to load Troupe data.");
                return;
            }
            Console.WriteLine("Loaded "+TroupeDataList.Count+" enemy Troupes from json.");
        }

        public static TroupeData? getTroupeDataByName(string troupeName) {
            if(TroupeDataList == null) {
                Console.WriteLine("Cannot get Troupe data -- Failed to load.");
                return null;
            }
            foreach(TroupeData data in TroupeDataList) {
                if(data.Name.ToLower().Trim() == troupeName.ToLower().Trim()) {
                    Console.WriteLine("Found match for Enemy Troupe with ID = "+troupeName);
                    return data;
                }
            }
            Console.WriteLine("ERROR: No match found for Enemy Troupe with ID = "+troupeName);
            return null;
        }
    }

    public static class CardData {

        // Translates a card name into a card object. Returns null if none are found.
        public static ActionCard? getCardByName(string cardName) {
            switch(cardName.ToLower().Trim()) {

                // Basics
                case "basic attack":
                    return new BasicAttack();
                case "basic defend":
                    return new BasicDefend();
                case "basic rest":
                    return new BasicRest();
                case "basic skill":
                    return new BasicSkill();
                case "basic spell":
                    return new BasicSpell();

                // Dual
                case "attack/defend":
                    return new DualAttackDefend();
                case "attack/skill":
                    return new DualAttackSkill();
                case "attack/spell":
                    return new DualAttackSpell();
                case "attack/rest":
                    return new DualAttackRest();
                case "defend/skill":
                    return new DualDefendSkill();
                case "defend/spell":
                    return new DualDefendSpell();
                case "defend/rest":
                    return new DualDefendRest();
                case "skill/spell":
                    return new DualSkillSpell();
                case "skill/rest":
                    return new DualSkillRest();
                case "spell/rest":
                    return new DualSpellRest();

                // Movement
                case "leap":
                    return new Leap();
                case "crouch":
                    return new Crouch();
                case "strafe":
                    return new Strafe();
                    
                default:
                    Console.WriteLine("ERROR: no card registered under the name "+cardName);
                    return null;
            }
        }
    }
    public static class ActionData {

        // Translates an action name into an action object. Returns null if none are found.
        public static Action? getActionByName(string actionName) {
            switch(actionName.ToLower().Trim()) {

                //==========ATTACKS==========
                case "bash":
                    return new Bash();
                case "bite":
                    return new Bite();
                case "cleave":
                    return new Cleave();
                case "infect":
                    return new Infect();
                case "ravage":
                    return new Ravage();
                case "stab":
                    return new Stab();
                case "strike":
                    return new Strike();
                case "swipe":
                    return new Swipe();
                case "twinslash":
                    return new TwinSlash();
                case "whack":
                    return new Whack();

                //==========DEFENDS==========
                case "block":
                    return new Block();
                case "cower":
                    return new Cower();
                case "dodge":
                    return new Dodge();
                case "parry":
                    return new Parry();
                case "vanish":
                    return new Vanish();

                //==========HIDDEN PASSIVES==========
                case "fleeswhenoutofspells":
                    return new FleesWhenOutOfSpells();
                case "summonedminion":
                    return new SummonedMinion();

                //==========PASSIVES==========
                case "assertive":
                    return new Assertive();
                case "conniving":
                    return new Conniving();
                case "everfull":
                    return new Everfull();
                case "infected":
                    return new Infected();
                case "onguard":
                    return new OnGuard();
                case "tough":
                    return new Tough();

                //==========RESTS==========
                case "breathe":
                    return new Breathe();
                case "focus":
                    return new Focus();
                case "idle":
                    return new Idle();
                case "recover":
                    return new Recover();
                case "rest":
                    return new Rest();
                case "resurrect":
                    return new Resurrect();
                case "tend":
                    return new Tend();

                //==========SKILLS==========
                case "backstab":
                    return new Backstab();
                case "charge":
                    return new Charge();
                case "climb":
                    return new Climb();
                case "counter":
                    return new Counter();
                case "daze":
                    return new Daze();
                case "devour":
                    return new Devour();
                case "grasp":
                    return new Grasp();
                case "hook":
                    return new Hook();
                case "subtlepoison":
                    return new SubtlePoison();
                case "taunt":
                    return new Taunt();
                case "whirl":
                    return new Whirl();

                //==========SPELLS==========
                case "batswarm":
                    return new BatSwarm();
                case "deadlybrew":
                    return new DeadlyBrew();
                case "harden":
                    return new Harden();
                case "hex":
                    return new Hex();
                case "icewall":
                    return new IceWall();
                case "immortalitypotion":
                    return new ImmortalityPotion();
                case "inflame":
                    return new Inflame();
                case "killingword":
                    return new KillingWord();
                case "livingflame":
                    return new LivingFlame();
                case "pickpocket":
                    return new Pickpocket();
                case "restore":
                    return new Restore();
                case "usehealthpotion":
                    return new UseHealthPotion();
                case "usemanapotion":
                    return new UseManaPotion();
                case "usepoisonpotion":
                    return new UsePoisonPotion();
                case "userevivepotion":
                    return new UseRevivePotion();
                case "zap":
                    return new Zap();

                default:
                    Console.WriteLine("ERROR: no action registered under the name "+actionName);
                    return null;
            }
        }
    }

    public static class ItemData {
        // Translates an action name into an action object. Returns null if none are found.
        public static Item? getItemByName(string itemName) {
            switch(itemName.ToLower().Trim()) {
                case "shortsword":
                    return new Shortsword();
                case "longbow":
                    return new Longbow();
                case "towershield":
                    return new TowerShield();
                case "dagger":
                    return new Dagger();
                case "leatherboots":
                    return new LeatherBoots();
                case "rope":
                    return new Rope();
                case "icewand":
                    return new IceWand();
                case "medkit":
                    return new Medkit();
                case "dreamcatcher":
                    return new Dreamcatcher();
                case "pavise":
                    return new Pavise();
                case "tomahawk":
                    return new Tomahawk();
                case "quarterstaff":
                    return new Quarterstaff();
                case "sapphire":
                    return new Sapphire();
                case "robes":
                    return new Robes();
                case "campfire":
                    return new Campfire();
                case "ironhelm":
                    return new IronHelm();
                case "chainmail":
                    return new Chainmail();
                case "healthpotion":
                    return new HealthPotion();
                case "manapotion":
                    return new ManaPotion();
                case "poisonpotion":
                    return new PoisonPotion();
                case "grog":
                    return new Grog();
                case "hairtrigger":
                    return new HairTrigger();
                case "heartcrystal":
                    return new HeartCrystal();
                case "timeturner":
                    return new TimeTurner();
                case "platearmor":
                    return new PlateArmor();
                case "battleaxe":
                    return new Battleaxe();
                case "banner":
                    return new Banner();
                case "whiteflag":
                    return new WhiteFlag();
                case "revivepotion":
                    return new RevivePotion();
                case "phoenixwand":
                    return new PhoenixWand();
                case "grapplinghook":
                    return new GrapplingHook();
                case "mastersword":
                    return new MasterSword();
                case "elderwand":
                    return new ElderWand();
                case "resurrectionstone":
                    return new ResurrectionStone();
                case "invisibilitycloak":
                    return new InvisibilityCloak();
                case "heavyarmor":
                    return new HeavyArmor();
                case "fortressshield":
                    return new FortressShield();
                default:
                    Console.WriteLine("ERROR: no item registered under the name "+itemName);
                    return null;
            }
        }
    }
}
