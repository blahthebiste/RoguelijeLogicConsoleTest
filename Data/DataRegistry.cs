using System.Text.Json;

// Loads data from data files on startup.
public static class DataRegistry {

    public static List<Entity> TroupeData = new List<Entity>();

    public static void LoadData() {
        Console.WriteLine("Loading data...");
        CharacterData.LoadPlayerData();
        Console.WriteLine("Finished loading player character data.");
        CharacterData.LoadEntityData();
        Console.WriteLine("Finished loading entity character data.");
        EnemyTroupes.LoadTroupeData();
        Console.WriteLine("Finished loading enemy troupe data.");
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
            "\n\t* "+CharacterData.getPlayerDataByName("Archer")!.Name+": "+CharacterData.getPlayerDataByName("Archer")!.Description,
            "\n\t* "+CharacterData.getPlayerDataByName("Mage")!.Name+": "+CharacterData.getPlayerDataByName("Mage")!.Description,
            "\n\t* "+CharacterData.getPlayerDataByName("Healer")!.Name+": "+CharacterData.getPlayerDataByName("Healer")!.Description
        };

    }

    public static class CharacterData
    {
        public static string playerDataPath = "Data/Characters/PlayerCharacters.json";
        public static string EntityDataPath = "Data/Characters/Entities.json";

        public static List<PlayerData>? PlayerDataList = new List<PlayerData>();
        public static List<EntityData>? EntityDataList = new List<EntityData>();

        public static void LoadPlayerData()
        {
            string json = File.ReadAllText(playerDataPath);
            PlayerDataList = JsonSerializer.Deserialize<List<PlayerData>>(json);
            if (PlayerDataList == null)
            {
                Console.WriteLine("ERROR: Failed to load player data.");
                return;
            }
            Console.WriteLine("DEBUG: Loaded " + PlayerDataList.Count + " player characters from json.");
        }

        public static void LoadEntityData()
        {
            string json = File.ReadAllText(EntityDataPath);
            EntityDataList = JsonSerializer.Deserialize<List<EntityData>>(json);
            if (EntityDataList == null)
            {
                Console.WriteLine("ERROR: Failed to load entity data.");
                return;
            }
            Console.WriteLine("DEBUG: Loaded " + EntityDataList.Count + " entities from json.");
            foreach (EntityData data in EntityDataList)
            {
                // Console.WriteLine("DEBUG: Found " + data.Name);
            }
        }

        public static PlayerData? getPlayerDataByName(string characterName)
        {
            if (PlayerDataList == null)
            {
                Console.WriteLine("ERROR: Cannot get player data -- Failed to load.");
                return null;
            }
            foreach (PlayerData data in PlayerDataList)
            {
                if (data.Name.ToLower().Trim() == characterName.ToLower().Trim())
                {
                    //Console.WriteLine("DEBUG: Found match for player character with ID = " + characterName);
                    return data;
                }
            }
            Console.WriteLine("ERROR: No match found for player character with ID = " + characterName);
            return null;
        }

        // Just tells you whether the hero could be found in the playerdata list
        public static bool heroExists(string characterName)
        {
            if (PlayerDataList == null)
            {
                Console.WriteLine("ERROR: Cannot get player data -- Failed to load.");
                return false;
            }
            foreach (PlayerData data in PlayerDataList)
            {
                if (data.Name.ToLower().Trim() == characterName.ToLower().Trim())
                {
                    return true;
                }
            }
            return false;
        }

        public static EntityData? getEntityDataByName(string characterName)
        {
            if (EntityDataList == null)
            {
                Console.WriteLine("ERROR: Cannot get entity data -- Failed to load.");
                return null;
            }
            foreach (EntityData data in EntityDataList)
            {
                if (data.Name.ToLower().Trim() == characterName.ToLower().Trim())
                {
                    // Console.WriteLine("DEBUG: Found match for entity with ID = " + characterName);
                    return data;
                }
            }
            Console.WriteLine("ERROR: No match found for entity with ID = " + characterName);
            return null;
        }
        
        // Just tells you whether the entity could be found in the entitydata list
        public static bool entityExists(string characterName)
        {
            if(EntityDataList == null) {
                Console.WriteLine("ERROR: Cannot get player data -- Failed to load.");
                return false;
            }
            foreach(EntityData data in EntityDataList) {
                if(data.Name.ToLower().Trim() == characterName.ToLower().Trim()) {
                    return true;
                }
            }
            return false;
        }

    }

    public static class EnemyTroupes {

        public static string TroupeDataPath = "Data/Characters/EnemyTroupes.json";

        public static List<TroupeData>? TroupeDataList = new List<TroupeData>();

        public static void LoadTroupeData() {
            string json = File.ReadAllText(TroupeDataPath);
            TroupeDataList = JsonSerializer.Deserialize<List<TroupeData>>(json);
            if(TroupeDataList == null) {
                Console.WriteLine("ERROR: Failed to load Troupe data.");
                return;
            }
            Console.WriteLine("DEBUG: Loaded "+TroupeDataList.Count+" enemy Troupes from json.");
        }

        public static TroupeData? getTroupeDataByName(string troupeName) {
            if(TroupeDataList == null) {
                Console.WriteLine("ERROR: Cannot get Troupe data -- Failed to load.");
                return null;
            }
            foreach(TroupeData data in TroupeDataList) {
                if(data.Name.ToLower().Trim() == troupeName.ToLower().Trim()) {
                    // Console.WriteLine("DEBUG: Found match for Enemy Troupe with ID = "+troupeName);
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

                // Personal cards
                case "engage":
                    return new Engage();
                case "sword n' board":
                    return new SwordnBoard();
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
                case "cut":
                    return new Cut();
                case "dive":
                    return new Dive();
                case "frostbreath":
                    return new FrostBreath();
                case "gnaw":
                    return new Gnaw();
                case "infect":
                    return new Infect();
                case "jaws":
                    return new Jaws();
                case "loose":
                    return new Loose();
                case "ravage":
                    return new Ravage();
                case "shadowslash":
                    return new ShadowSlash();
                case "shadowstrike":
                    return new ShadowStrike();
                case "snipe":
                    return new Snipe();
                case "shoot":
                    return new Shoot();
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
                case "counter":
                    return new Counter();
                case "cower":
                    return new Cower();
                case "dodge":
                    return new Dodge();
                case "obstruct":
                    return new Obstruct();
                case "parry":
                    return new Parry();
                case "shift":
                    return new Shift();
                case "skitter":
                    return new Skitter();
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
                case "duel":
                    return new Duel();
                case "embeddedsword":
                    return new EmbeddedSword();
                case "inanimate":
                    return new Inanimate();
                case "everfull":
                    return new Everfull();
                case "infected":
                    return new Infected();
                case "madeofstone":
                    return new MadeOfStone();
                case "onguard":
                    return new OnGuard();
                case "permataunt":
                    return new PermaTaunt();
                case "rattified":
                    return new Rattified();
                case "returndagger":
                    return new ReturnDagger();
                case "royal":
                    return new Royal();
                case "skillimmune":
                    return new SkillImmune();
                case "tough":
                    return new Tough();
                case "warding":
                    return new Warding();

                //==========RESTS==========
                case "breathe":
                    return new Breathe();
                case "focus":
                    return new Focus();
                case "idle":
                    return new Idle();
                case "prepareritual":
                    return new PrepareRitual();
                case "recover":
                    return new Recover();
                case "repose":
                    return new Repose();
                case "rest":
                    return new Rest();
                case "resurrect":
                    return new Resurrect();
                case "tend":
                    return new Tend();
                case "wait":
                    return new Wait();

                //==========SKILLS==========
                case "backstab":
                    return new Backstab();
                case "charge":
                    return new Charge();
                case "climb":
                    return new Climb();
                case "connive":
                    return new Connive();
                case "daze":
                    return new Daze();
                case "devour":
                    return new Devour();
                case "grasp":
                    return new Grasp();
                case "hook":
                    return new Hook();
                case "nock":
                    return new Nock();
                case "rainofarrows":
                    return new RainOfArrows();
                case "subtlepoison":
                    return new SubtlePoison();
                case "takeaim":
                    return new TakeAim();
                case "takeflight":
                    return new TakeFlight();
                case "taunt":
                    return new Taunt();
                case "whirl":
                    return new Whirl();

                //==========SPELLS==========
                case "batswarm":
                    return new BatSwarm();
                case "chill":
                    return new Chill();
                case "conjureblade":
                    return new ConjureBlade();
                case "conjuredagger":
                    return new ConjureDagger();
                case "conjureguardian":
                    return new ConjureGuardian();
                case "corruption":
                    return new Corruption();
                case "crisiscontingency":
                    return new CrisisContingency();
                case "deadlybrew":
                    return new DeadlyBrew();
                case "deepfreeze":
                    return new DeepFreeze();
                case "eyeofthestorm":
                    return new EyeOfTheStorm();
                case "favor":
                    return new Favor();
                case "forcefield":
                    return new Forcefield();
                case "grow":
                    return new Grow();
                case "harden":
                    return new Harden();
                case "hex":
                    return new Hex();
                case "hold":
                    return new Hold();
                case "icespike":
                    return new IceSpike();
                case "icewall":
                    return new IceWall();
                case "imbue":
                    return new Imbue();
                case "immortalitypotion":
                    return new ImmortalityPotion();
                case "inflame":
                    return new Inflame();
                case "killingword":
                    return new KillingWord();
                case "livingflame":
                    return new LivingFlame();
                case "mirrorarmor":
                    return new MirrorArmor();
                case "mirrorforce":
                    return new MirrorForce();
                case "pickpocket":
                    return new Pickpocket();
                case "ratking":
                    return new RatKing();
                case "repel":
                    return new Repel();
                case "rightfulheir":
                    return new RightfulHeir();
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
                case "excalibur":
                    return new Excalibur();
                default:
                    Console.WriteLine("ERROR: no item registered under the name "+itemName);
                    return null;
            }
        }
    }
}
