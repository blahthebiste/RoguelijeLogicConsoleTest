public static class Battlefield
{

    public static CombatEncounter? CurrentEncounter;
    public static List<Enemy> EnemySide = new List<Enemy>();
    public static List<PlayerCharacter> PlayerSide = new List<PlayerCharacter>();

    // Used to keep track of who is currently taunting. Used for targeting restrictions.
    public static List<Entity> Taunters = new List<Entity>();

    // Used to keep track of who has died in combat (and maybe resurrect them)
    public static List<PlayerCharacter> DeadHeroes = new List<PlayerCharacter>();
    public static List<Enemy> DeadEnemies = new List<Enemy>();

    // Used for Dazed logic
    public static List<Entity> BeenDazed = new List<Entity>();
    public static int turnNumber = 0;
    public static int playerBlock = 0;
    public static int enemyBlock = 0;


    //========================================COMBAT GAMEPLAY LOOP========================================
    public static void LoadCombat(CombatEncounter combat)
    {
        CurrentEncounter = combat;
        EnemySide = new List<Enemy>();
        PlayerSide = new List<PlayerCharacter>();
        DeadEnemies = new List<Enemy>();
        DeadHeroes = new List<PlayerCharacter>();
        // Load in player party:
        foreach (PlayerCharacter hero in CurrentRun.Party.ToList())
        {
            PlayerSide.Add(hero);
            hero.currentHP = hero.maxHP;
            hero.previousAction = null;
        }
        foreach (Enemy enemy in combat.EnemyTroupe.ToList())
        {
            EnemySide.Add(enemy);
        }
        // Start of combat events (entities will pass this along to actions and items)
        foreach (PlayerCharacter hero in CurrentRun.Party.ToList())
        {
            hero.startOfCombat();
            hero.exhausted = false;
        }
        // Have enemies choose their targets:
        foreach (Enemy enemy in EnemySide.ToList())
        {
            enemy.enterCombat();
        }
        // Start of combat events for enemies, only after all have entered
        foreach (Enemy enemy in EnemySide.ToList())
        {
            enemy.startOfCombat();
        }
        playerBlock = 0;
        enemyBlock = 0;
        turnNumber = 1;
        Console.WriteLine("Beginning of turn " + Battlefield.turnNumber);
        startTurn();
    }

    public static void startTurn()
    {
        if (!CurrentRun.InCombat)
        {
            Console.WriteLine("Not in combat. Skipping startTurn resolution");
            return;
        }

        // Reset block:
        int permaBlockAmount = 0;
        foreach (PlayerCharacter hero in PlayerSide.ToList())
        {
            StatusEffect? permaBlock = hero.GetStatusEffect("PermaBlock");
            if (permaBlock != null)
            {
                Console.WriteLine(hero.name + " had Perma-Block; amount is " + permaBlock.amount);
                permaBlockAmount += permaBlock.amount;
            }
        }
        if (permaBlockAmount > 0) Console.WriteLine("Total Perma-Block amount is " + permaBlockAmount);
        playerBlock = (playerBlock <= permaBlockAmount) ? playerBlock : permaBlockAmount;

        // New hand
        CardManager.discardHand();
        CardManager.drawHand();

        // Run startOfTurn events:
        foreach (PlayerCharacter hero in PlayerSide.ToList())
        {
            hero.startOfTurn();
        }
        foreach (Enemy enemy in EnemySide.ToList())
        {
            enemy.startOfTurn();
        }

        // Remove exhaustion from the previous turn
        foreach (PlayerCharacter hero in PlayerSide.ToList())
        {
            hero.exhausted = false;
        }
        foreach (Enemy enemy in EnemySide.ToList())
        {
            enemy.exhausted = false;
        }
        resolveDeath();
        resolveFleeing();
    }

    public static void endTurn()
    {
        if (!CurrentRun.InCombat)
        {
            Console.WriteLine("Not in combat. Skipping endTurn resolution");
            return;
        }
        // Reset block:
        int permaBlockAmount = 0;
        foreach (Enemy enemy in EnemySide.ToList())
        {
            StatusEffect? permaBlock = enemy.GetStatusEffect("PermaBlock");
            if (permaBlock != null) permaBlockAmount += permaBlock.amount;
        }
        enemyBlock = (enemyBlock <= permaBlockAmount) ? enemyBlock : permaBlockAmount;
        // Enemies all take their turn:
        foreach (Enemy enemy in EnemySide.ToList())
        {
            enemy.takeTurn();
        }
        // Run endOfTurn events:
        foreach (PlayerCharacter hero in PlayerSide.ToList())
        {
            hero.endOfTurn();
        }
        foreach (Enemy enemy in EnemySide.ToList())
        {
            enemy.endOfTurn();
        }
        resolveDeath();
        resolveFleeing();
        turnNumber++;
    }

    public static void endCombat(bool playerWon)
    {
        CurrentRun.InCombat = false;
        CurrentRun.NextCombatEncounter = null;
        foreach (Entity hero in PlayerSide.ToList())
        {
            hero.endOfCombat();
            foreach (Action action in hero.ActionList.ToList())
            {
                if (action.hasLimitedUses)
                {
                    action.uses = action.maxUses;
                }
            }
            // Wipe status effects
            hero.EffectList = new List<StatusEffect>();
            hero.exhausted = false;
            hero.currentHP = hero.maxHP;
            hero.previousAction = null;
        }
        if (playerWon)
        {
            Console.WriteLine("VICTORY!");
            Console.WriteLine("");
            Console.WriteLine("Rewards: ");
            CurrentRun.InCombat = false;
            // Distribute rewards.
            CurrentRun.DistributeCombatRewards();
        }
        CurrentRun.ZoneProgress += 1;
        if (CurrentRun.ZoneProgress == 4)
        {
            // After act 1 of the zone, shops are more common
            for (int i = 0; i < 10; i++)
            {
                // Add 10x bonus shop event
                CurrentRun.EventPool.Add(new Shop());
            }
        }
    }

    //========================================UTILITY FUNCTIONS========================================

    // If all heroes are exhausted, we can prompt the player to end their turn.
    public static bool playerCharactersAllExhausted()
    {
        foreach (PlayerCharacter hero in PlayerSide.ToList())
        {
            if (!hero.exhausted) return false;
        }
        return true;
    }


    // Check if there are any more enemies.
    public static bool noMoreEnemies()
    {
        if (EnemySide.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Check if any player characters are still alive.
    public static bool noMoreHeroes()
    {
        if (PlayerSide.Count > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Returns false if the hero was not found in the Dead Heroes list.
    public static bool ReviveHero(string heroName, bool toFullHP = true, bool exhausted = true)
    {
        foreach (PlayerCharacter hero in DeadHeroes.ToList())
        {
            if (hero.name.ToLower().Trim() == heroName.ToLower().Trim())
            {
                Console.WriteLine("Reviving " + hero.name + "!");
                DeadHeroes.Remove(hero);
                PlayerSide.Add(hero);
                if (toFullHP)
                {
                    hero.currentHP = hero.maxHP;
                }
                else
                {
                    hero.currentHP = 1;
                }
                hero.previousAction = null;
                hero.exhausted = exhausted;
                // Do not wipe their status effects or debuffs?
                return true;
            }
        }
        return false;
    }

    // Returns false if the hero was not found in the Dead Heroes list.
    public static bool ReviveEnemy(string enemyName, bool toFullHP = true, bool exhausted = true)
    {
        foreach (Enemy enemy in DeadEnemies.ToList())
        {
            if (enemy.name.ToLower().Trim() == enemyName.ToLower().Trim())
            {
                Console.WriteLine(enemy.name + " was revived!");
                DeadEnemies.Remove(enemy);
                EnemySide.Add(enemy);
                if (toFullHP)
                {
                    enemy.currentHP = enemy.maxHP;
                }
                else
                {
                    enemy.currentHP = 1;
                }
                enemy.previousAction = null;
                enemy.exhausted = exhausted;
                // Do not wipe their status effects or debuffs?
                return true;
            }
        }
        return false;
    }

    public static void RemoveEntity(Entity entity)
    {
        if (PlayerSide.Contains(entity))
        {
            PlayerSide.Remove((PlayerCharacter)entity);
            CurrentRun.LoseLives(1);
            if (noMoreHeroes())
            {
                endCombat(false);
            }
        }
        else if (EnemySide.Contains(entity))
        {
            EnemySide.Remove((Enemy)entity);
            if (noMoreEnemies())
            {
                endCombat(true);
            }
        }
    }


    // Atk object already has the amount, source, and target, among other things.
    public static void performAttack(Attack atk)
    {
        atk = atk.source.onAttack(atk);
        atk = atk.target.onReceiveAttack(atk);
    }

    // Run through all block gain events for the source, then apply it
    public static void addBlock(int blockAmount, Entity source)
    {
        blockAmount = source.onGainBlock(blockAmount);
        if (source.playerControlled)
        {
            playerBlock += blockAmount;
        }
        else
        {
            enemyBlock += blockAmount;
        }
    }

    // Entities do not immediately die upon losing all HP; instead, they die at predefined checkpoints,
    // after resolving an action or trigger.
    public static void resolveDeath()
    {
        for (int i = EnemySide.Count - 1; i >= 0; i--)
        {
            if (!EnemySide[i].isAlive())
            {
                EnemySide[i].die();
            }
        }
        for (int i = PlayerSide.Count - 1; i >= 0; i--)
        {
            if (!PlayerSide[i].isAlive())
            {
                PlayerSide[i].die();
            }
        }
    }

    // Used mainly for Piety
    public static void resolveFleeing()
    {
        for (int i = EnemySide.Count - 1; i >= 0; i--)
        {
            if (EnemySide[i].fleeing)
            {
                EnemySide[i].flee();
            }
        }
    }


    public static void ResetCombat()
    {
        EnemySide = new List<Enemy>();
        PlayerSide = new List<PlayerCharacter>();
        Taunters = new List<Entity>();
        turnNumber = 0;
    }




}