public static class Battlefield {
    
    public static CombatEncounter? CurrentEncounter;
    public static List<Enemy> EnemySide = new List<Enemy>();
    public static List<PlayerCharacter> PlayerSide = new List<PlayerCharacter>();
    
    // Used to keep track of who is currently taunting. Used for targeting restrictions.
    public static List<Entity> Taunters = new List<Entity>();

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
            // Start of combat events for actions and their items
            foreach(Action action in hero.ActionList) {
                if(action.equippedItem != null) {
                    action.equippedItem.startOfCombat();
                }
            }
            hero.exhausted = false;
            hero.currentHP = hero.maxHP;
        }
        foreach(Enemy enemy in combat.EnemyTroupe) {
            EnemySide.Add(enemy);
        }
        // Have enemies choose their targets:
        foreach(Enemy enemy in EnemySide) {
            enemy.enterCombat();
        }
        playerBlock = 0;
        enemyBlock = 0;
        turnNumber = 1;
    }
    
    public static void ResetCombat() {
        EnemySide = new List<Enemy>();
        PlayerSide = new List<PlayerCharacter>();
        Taunters = new List<Entity>();
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
        // Reset block:
        int permaBlockAmount = 0;
        foreach(Enemy enemy in EnemySide) {
            StatusEffect? permaBlock = enemy.GetStatusEffect("PermaBlock");
            if(permaBlock != null) permaBlockAmount += permaBlock.amount;
        }
        enemyBlock = (enemyBlock <= permaBlockAmount) ? enemyBlock : permaBlockAmount;
        // Enemies all take their turn:
        foreach(Enemy enemy in EnemySide) {
            enemy.takeTurn();
        }
        // Run endOfTurn events:
        foreach(PlayerCharacter hero in PlayerSide) {
            hero.endOfTurn();
        }
        foreach(Enemy enemy in EnemySide) {
            enemy.endOfTurn();
        }
        turnNumber++;
    }
    public static void startTurn() {

        // Reset block:
        int permaBlockAmount = 0;
        foreach(PlayerCharacter hero in PlayerSide) {
            StatusEffect? permaBlock = hero.GetStatusEffect("PermaBlock");
            if(permaBlock != null) {
                Console.WriteLine(hero.name+" had PermaBlock; amount is "+permaBlock.amount);
                permaBlockAmount += permaBlock.amount;
            }
        }
        Console.WriteLine("Total PermaBlock amount is "+permaBlockAmount);
        playerBlock = (playerBlock <= permaBlockAmount) ? playerBlock : permaBlockAmount;
        
        // New hand
        CardManager.discardHand();
        CardManager.drawHand();
        
        // Run startOfTurn events:
        foreach(PlayerCharacter hero in PlayerSide) {
            hero.startOfTurn();
        }
        foreach(Enemy enemy in EnemySide) {
            enemy.startOfTurn();
        }
        
        // Remove exhaustion from the previous turn
        foreach(PlayerCharacter hero in PlayerSide) {
            hero.exhausted = false;
        }
        foreach(Enemy enemy in EnemySide) {
            enemy.exhausted = false;
        }
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
        CurrentRun.NextCombatEncounter = null;
        foreach(Entity hero in PlayerSide) {
            foreach(Action action in hero.ActionList) {
                action.endOfCombat();
                if(action.hasLimitedUses) {
                    action.uses = action.maxUses;
                }
            }
            // Wipe status effects
            hero.EffectList = new List<StatusEffect>();
            hero.exhausted = false;
            hero.currentHP = hero.maxHP;
        }
        if(playerWon) {
            Console.WriteLine("VICTORY!");
            Console.WriteLine("");
            Console.WriteLine("Rewards: ");
            CurrentRun.InCombat = false;
            // Distribute rewards.
            CurrentRun.DistributeCombatRewards();
        }
        CurrentRun.ZoneProgress += 1;
        if(CurrentRun.ZoneProgress == 4) {
            // After act 1 of the zone, shops are more common
            for(int i = 0; i < 10; i++) {
                // Add 10x bonus shop event
                CurrentRun.EventPool.Add(new Shop());
            }
        }
    }
}