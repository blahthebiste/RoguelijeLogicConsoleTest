public static class Battlefield {
    
    public static CombatEncounter? CurrentEncounter;
    public static List<Enemy> EnemySide = new List<Enemy>();
    public static List<PlayerCharacter> PlayerSide = new List<PlayerCharacter>();

     // Used for Dazed logic
    public static List<Entity> BeenDazed = new List<Entity>();
    public static int turnNumber = 0;
    public static int playerBlock = 0;
    public static int enemyBlock = 0;
     

    
    public static void LoadCombat(CombatEncounter combat) {
        CurrentEncounter = combat;
        EnemySide = new List<Enemy>();
        PlayerSide = new List<PlayerCharacter>();
        // Load in player party:
        foreach(PlayerCharacter hero in CurrentRun.Party) {
            PlayerSide.Add(hero);
        }
        foreach(Enemy enemy in combat.EnemyTroupe) {
            EnemySide.Add(enemy);
        }
        turnNumber = 1;
    }
    
    public static void ResetCombat() {
        EnemySide = new List<Enemy>();
        PlayerSide = new List<PlayerCharacter>();
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

    public static void endTurn() {
        enemyBlock = 0;
        // Enemies all take their turn:
        foreach(Enemy enemy in EnemySide) {
            enemy.takeTurn();
        }
        // Now reset everyting:
        playerBlock = 0;
        CardManager.discardHand();
        CardManager.drawHand();
        foreach(PlayerCharacter hero in PlayerSide) {
            hero.exhausted = false;
        }
        foreach(Enemy enemy in EnemySide) {
            enemy.exhausted = false;
        }
        turnNumber++;
    }

    public static bool playerCharactersAllExhausted(){
        foreach(PlayerCharacter hero in PlayerSide) {
            if(!hero.exhausted) return false;
        }
        return true;
    }

    public static void RemoveEntity(Entity entity) {
        if(PlayerSide.Contains(entity)) {
            PlayerSide.Remove((PlayerCharacter)entity);
            CurrentRun.LoseLives(1);
            if(noMoreHeroes()) {
                endCombat(false);
            }
        }
        else if(EnemySide.Contains(entity)) {
            EnemySide.Remove((Enemy)entity);
            if(noMoreEnemies()) {
                endCombat(true);
            }
        }
    }
    
    public static void endCombat(bool playerWon) {
        CurrentRun.InCombat = false;
        if(playerWon) {
            Console.WriteLine("VICTORY!");
            Console.WriteLine("");
            Console.WriteLine("Rewards: ");
            // distribute rewards.
            CurrentRun.GenerateCombatRewards();
            CurrentRun.DistributeCombatRewards();
        }
    }
}