public static class Battlefield {
    
     public static List<Entity> EnemySide = new List<Entity>();
     public static List<Entity> PlayerSide = new List<Entity>();
     // Used for Dazed logic
     public static List<Entity> BeenDazed = new List<Entity>();
     public static int turnNumber = 0;
     public static int playerBlock = 0;
     public static int enemyBlock = 0;
    
    public static void LoadCombat(string combatID) {
        EnemySide = new List<Entity>();
        PlayerSide = new List<Entity>();
        DataRegistry.loadTroupeData(combatID);
        // Load in player party:
        foreach(Entity hero in CurrentRun.Party) {
            PlayerSide.Add(hero);
        }
        // TODO: Get troupeData here
        foreach(Entity enemy in DataRegistry.TroupeData) {
            EnemySide.Add(enemy);
        }
        turnNumber = 1;
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
    
}