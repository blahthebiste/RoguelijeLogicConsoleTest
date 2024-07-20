// Loads data from data files on startup.
public static class DataRegistry {

    public static List<Entity> TroupeData = new List<Entity>();
    public static void fillCharacterData(PlayerCharacter pc, string characterID) {
        // TODO: get info from data
        pc.name = "data.name";
        pc.maxHP = 1;
        pc.personalCard = new BasicAttack();
        // foreach(Action action in data.ActionList) {
        //     pc.ActionList.Add(action);
        // }
    }

    public static void fillEnemyData(Enemy enemy, string enemyID) {
        // TODO: get info from data
        enemy.name = "data.name";
        enemy.maxHP = 1;
        // foreach(Action action in data.ActionList) {
        //     enemy.ActionList.Add(action);
        // }
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

        public static class Fighter {
            public static string Name = "Fighter";
            public static string Description = "Sturdy attacker that can handle any situation";
            public static int HP = 10;
            
            public static ActionCard PersonalCard = new BasicAttack();
            public static List<Action> ActionList = new List<Action>() {
                new Strike(),
                new Parry(),
                new Rest(),
                new Whirl(),
                new Inflame()
            };

        }

        public static class Defender {
            public static string Name = "Defender";
            public static string Description = "Healthy wall that protects weaker allies";
            public static int HP = 12;
            
            public static ActionCard PersonalCard = new BasicDefend();
            public static List<Action> ActionList = new List<Action>() {
                new Bash(),
                new Block(),
                new Rest(),
                new Taunt(),
                new Harden()
            };

        }
        public static class Healer {
            public static string Name = "Healer";
            public static string Description = "Restores HP and rests well";
            public static int HP = 10;
            
            public static ActionCard PersonalCard = new BasicRest();
            public static List<Action> ActionList = new List<Action>() {
                new Whack(),
                new Cower(),
                new Recover(),
                new Daze(),
                new Restore()
            };

        }

    }
}
