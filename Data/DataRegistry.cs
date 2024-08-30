using System.Text.Json;

// Loads data from data files on startup.
public static class DataRegistry {

    public static List<Entity> TroupeData = new List<Entity>();

    public static void LoadData() {
        Console.WriteLine("Loading data...");
        CharacterData.LoadPlayerData();
        Console.WriteLine("Loaded player character data.");
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
            "\n\t* Fighter",
            "\n\t* Defender",
            "\n\t* Thief - Stabby, sneaky goblin with bonus items",
            "\n\t* Mage - Uses spells to blast enemies",
            "\n\t* Healer"
        };

    }

    public static class CharacterData {
        public static string playerDataPath = "Data/PlayerCharacters.json";

        public static List<PlayerData>? PlayerDataList = new List<PlayerData>();

        public static void LoadPlayerData() {
            string json = File.ReadAllText(playerDataPath);
            PlayerDataList = JsonSerializer.Deserialize<List<PlayerData>>(json);
            if(PlayerDataList == null) {
                Console.WriteLine("Failed to load player data.");
                return;
            }
            Console.WriteLine("Loaded "+PlayerDataList.Count+" player characters from json.");
            // foreach(PlayerData data in PlayerDataList){
            //     Console.WriteLine("Found "+data.Name);
            // }
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

    }

    public static class EnemyData {
        public static class PengoonData {
            public static string Name = "Pengoon";
            public static string Description = "Useless enemy for the tutorial";
            public static int HP = 7;
            
            public static List<Action> ActionList = new List<Action>() {
                new Swipe()
            };

        }

        // Translates an enemy name into an enemy object. Returns null if none are found.
        public static Entity? getEnemyByName(string enemyName){ 
            switch(enemyName.ToLower().Trim()) {
                case "pengoon":
                    return new Pengoon();
                default:
                    Console.WriteLine("ERROR: no enemy registered under the name "+enemyName);
                    return null;
            }
        }
    }

    public static class EnemyTroupes {

        public static string TroupeDataPath = "Data/EnemyTroupes.json";

        public static List<TroupeData>? TroupeDataList = new List<TroupeData>();

        public static void LoadTroupeData() {
            string json = File.ReadAllText(TroupeDataPath);
            TroupeDataList = JsonSerializer.Deserialize<List<TroupeData>>(json);
            if(TroupeDataList == null) {
                Console.WriteLine("Failed to load Troupe data.");
                return;
            }
            Console.WriteLine("Loaded "+TroupeDataList.Count+" enemy Troupes from json.");
            // foreach(TroupeData data in TroupeDataList){
            //     Console.WriteLine("Found "+data.Name);
            // }
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
                case "bash":
                    return new Bash();
                case "strike":
                    return new Strike();
                case "swipe":
                    return new Swipe();
                case "whack":
                    return new Whack();
                case "block":
                    return new Block();
                case "cower":
                    return new Cower();
                case "parry":
                    return new Parry();
                case "recover":
                    return new Recover();
                case "rest":
                    return new Rest();
                case "daze":
                    return new Daze();
                case "taunt":
                    return new Taunt();
                case "whirl":
                    return new Whirl();
                case "harden":
                    return new Harden();
                case "inflame":
                    return new Inflame();
                case "restore":
                    return new Restore();
                default:
                    Console.WriteLine("ERROR: no action registered under the name "+actionName);
                    return null;
            }
        }
    }
}
