public static class Battlefield {
    
    public static List<Entity> EnemySide = new List<Entity>();
    public static List<Entity> PlayerSide = new List<Entity>();

     // Used for Dazed logic
    public static List<Entity> BeenDazed = new List<Entity>();
    public static int turnNumber = 0;
    public static int playerBlock = 0;
    public static int enemyBlock = 0;

    // Used for organizing visual placement of combatants.
    // There should be an empty row in between any 2 allies.
    public static int maxRows = 9;
     

    
    public static void LoadCombat(CombatEncounter combat) {
        EnemySide = new List<Entity>();
        PlayerSide = new List<Entity>();
        // Load in player party:
        foreach(Entity hero in CurrentRun.Party) {
            PlayerSide.Add(hero);
        }
        foreach(Entity enemy in combat.EnemyTroupe) {
            EnemySide.Add(enemy);
        }
        turnNumber = 1;
    }
    
    public static void ResetCombat() {
        EnemySide = new List<Entity>();
        PlayerSide = new List<Entity>();
        turnNumber = 0;
    }
    
    // Check if there are any more enemies.
    public static bool noMoreEnemies() {
        if(EnemySide.Count > 0) {
            return false;
        }
        else {
            return true;
        }
    }
    
    // Check if any player characters are still alive.
    public static bool noMoreHeroes() {
        if(PlayerSide.Count > 0) {
            return false;
        }
        else {
            return true;
        }
    }

    public static void addBlock(int blockAmount, bool toPlayerTeam){
        if(toPlayerTeam) {
            playerBlock += blockAmount;
        }
        else {
            enemyBlock += blockAmount;
        }
    }

    // Tells what row the entity belongs in. Tries to center everyone around the middle.
    public static int calculateRow() {
        
    } 
    
}