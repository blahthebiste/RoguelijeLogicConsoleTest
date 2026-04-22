public static class Battlefield
{

    public static CombatEncounter? CurrentEncounter;
    public static List<Entity> EnemySide = new List<Entity>();
    public static List<Entity> PlayerSide = new List<Entity>();

    // Used to keep track of who is currently taunting. Used for targeting restrictions.
    public static List<Entity> Taunters = new List<Entity>();

    // Used to keep track of how many entities are environments in this combat:
    public static int numEnvironmentEntities = 0;

    // Used to keep track of who has died in combat (and maybe resurrect them)
    public static List<Entity> DeadHeroes = new List<Entity>();
    public static List<Entity> DeadEnemies = new List<Entity>();

    // Used for Dazed logic
    public static List<Entity> BeenDazed = new List<Entity>();
    public static int turnNumber = 0;
    public static int playerBlock = 0;
    public static int enemyBlock = 0;


    //========================================COMBAT GAMEPLAY LOOP========================================
    public static void LoadCombat(CombatEncounter combat)
    {
        playerBlock = 0;
        enemyBlock = 0;
        CurrentEncounter = combat;
        EnemySide = new List<Entity>();
        PlayerSide = new List<Entity>();
        DeadEnemies = new List<Entity>();
        DeadHeroes = new List<Entity>();
        // Load in player party:
        foreach (PlayerCharacter hero in CurrentRun.Party.ToList())
        {
            PlayerSide.Add(hero);
            hero.currentHP = hero.maxHP;
            hero.previousAction = null;
        }
        // Load in enemies:
        foreach (Entity enemy in combat.EnemyTroupe.ToList())
        {
            EnemySide.Add(enemy);
        }
        // Start of combat events (entities will pass this along to actions and items)
        foreach (Entity ally in PlayerSide.ToList())
        {
            ally.startOfCombat();
        }
        // Start of combat events for enemies
        foreach (Entity enemy in EnemySide.ToList())
        {
            enemy.startOfCombat();
        }
        turnNumber = 1;
        Console.WriteLine("Beginning of turn " + turnNumber);
        startRound();
    }

    // Start of each round after everything from the previous round has resolved.
    // 1. Reset block for players
    // 2. Draw a new hand
    // 3. Trigger start of round events for players
    // 4. startOfTurn events for players
    // 5. Trigger start of round events for enemies
    // 6. Reset exhaustion for both teams
    // 7. Resolve unresolved death or fleeing
    public static void startRound()
    {
        if (!CurrentRun.InCombat)
        {
            Console.WriteLine("Not in combat, cannot start round.");
            return;
        }
        Console.WriteLine("Beginning of turn " + turnNumber);

        // Reset block:
        int permaBlockAmount = 0;
        foreach (Entity ally in PlayerSide.ToList())
        {
            StatusEffect? permaBlock = ally.GetStatusEffect("PermaBlock");
            if (permaBlock != null)
            {
                Console.WriteLine(ally.name + " had Perma-Block; amount is " + permaBlock.amount);
                permaBlockAmount += permaBlock.amount;
            }
        }
        if (permaBlockAmount > 0) Console.WriteLine("Total Perma-Block amount is " + permaBlockAmount);
        playerBlock = (playerBlock <= permaBlockAmount) ? playerBlock : permaBlockAmount;

        // New hand
        CardManager.discardHand();
        CardManager.drawHand();

        // Run startOfRound events:
        foreach (Entity ally in PlayerSide.ToList())
        {
            ally.startOfRound();
        }
        // Run startOfTurn events for players:
        foreach (Entity ally in PlayerSide.ToList())
        {
            ally.startOfTurn();
        }
        // Choose targets AFTER players' startOfTurn events have resolved
        foreach (Entity enemy in EnemySide.ToList())
        {
            enemy.startOfRound();
        }
        // Removing exhaustion from the previous turn -- covered in base Entity startOfRound event
        resolveDeath();
        resolveFleeing();
    }

    // All the stuff that resolves once the player hits 'end turn'.
    // 1. endOfTurn events for player characters
    // 2. Reset enemy block
    // 3. startOfTurn events for enemies
    // 4. Enemies take their turns
    // 5. endOfTurn events for enemies
    // 6. endOfRound events for both players and enemies
    // 7. Resolve unresolved death or fleeing
    // 8. Tick up the turn counter
    public static void endTurn()
    {
        if (!CurrentRun.InCombat)
        {
            Console.WriteLine("Not in combat, cannot end rurn.");
            return;
        }
        Console.WriteLine("Ending turn.");
        // Friendly NPCs all take their turn:
        foreach (Entity ally in PlayerSide.ToList())
        {
            if(!ally.playerControlled) ally.takeTurn();
        }
        // Run endOfTurn events for heroes:
        foreach (Entity ally in PlayerSide.ToList())
        {
            ally.endOfTurn();
        }
        // Reset block:
        int permaBlockAmount = 0;
        foreach (Entity enemy in EnemySide.ToList())
        {
            StatusEffect? permaBlock = enemy.GetStatusEffect("PermaBlock");
            if (permaBlock != null) permaBlockAmount += permaBlock.amount;
        }
        enemyBlock = (enemyBlock <= permaBlockAmount) ? enemyBlock : permaBlockAmount;

        // Run startOfTurn events for enemies:
        foreach (Entity enemy in EnemySide.ToList())
        {
            enemy.startOfTurn();
        }
        // Enemies all take their turn:
        foreach (Entity enemy in EnemySide.ToList())
        {
            enemy.takeTurn();
        }
        // End of turn events for enemies
        foreach (Entity enemy in EnemySide.ToList())
        {
            enemy.endOfTurn();
        }
        // End of round events for both players and enemies
        foreach (Entity ally in PlayerSide.ToList())
        {
            ally.endOfRound();
        }
        foreach (Entity enemy in EnemySide.ToList())
        {
            enemy.endOfRound();
        }
        resolveDeath();
        resolveFleeing();
        turnNumber++;
    }

    public static void endCombat(bool playerWon)
    {
        CurrentRun.InCombat = false;
        CurrentRun.NextCombatEncounter = null;
        numEnvironmentEntities = 0;
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
            hero.currentHP = hero.maxHP;
            hero.previousAction = null;
        }
        foreach (Entity hero in DeadHeroes.ToList())
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
            hero.currentHP = hero.maxHP;
            hero.previousAction = null;
        }
        // Trigger end of combat for items in inventory as well:
        foreach(Item item in CurrentRun.Inventory.ToList())
        {
            item.endOfCombat(); // Mostly to remove temporary items.
        }
        if (playerWon)
        {
            Console.WriteLine("VICTORY!");
            Console.WriteLine("");
            Console.WriteLine("Rewards: ");
            // Distribute rewards.
            CurrentRun.DistributeCombatRewards();
        }
        else
        {
            Console.WriteLine("DEFEAT!");
        }
        CurrentRun.ZoneProgress += 1;
        if (CurrentRun.ZoneProgress == 4)
        {
            // After act 1 of the zone, shops are more common
            for (int i = 0; i < 10; i++)
            {
                // Add 10x bonus shop Encounter
                CurrentRun.EncounterPool.Add(new Shop());
            }
        }
    }

    //========================================UTILITY FUNCTIONS========================================

    // If all heroes are exhausted, we can prompt the player to end their turn.
    public static bool playerCharactersAllExhausted()
    {
        foreach (PlayerCharacter hero in PlayerSide.OfType<PlayerCharacter>())
        {
            if (!hero.exhausted) return false;
        }
        return true;
    }


    // Check if there are any more enemies. Environments do not count.
    public static bool noMoreEnemies()
    {
        return EnemySide.Count - numEnvironmentEntities <= 0;
    }

    // Check if any player characters are still alive.
    public static bool noMoreHeroes()
    {
        return PlayerSide.Count <= 0;
    }

    // Returns false if the hero was not found in the Dead Heroes list.
    public static bool ReviveHero(string heroName, bool toFullHP = true, bool exhausted = true)
    {
        foreach (Entity hero in DeadHeroes.ToList())
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

    // Returns false if the enemy was not found in the Dead Enemies list.
    public static bool ReviveEnemy(string enemyName, bool toFullHP = true, bool exhausted = true)
    {
        foreach (Entity enemy in DeadEnemies.ToList())
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

    // Also handles the logic for the player losing a life
    public static void RemoveEntity(Entity entity)
    {
        if (PlayerSide.Contains(entity))
        {
            PlayerSide.Remove(entity);
            CurrentRun.LoseLives(1);
            if (noMoreHeroes())
            {
                endCombat(false);
            }
        }
        else if (EnemySide.Contains(entity))
        {
            EnemySide.Remove(entity);
            if (noMoreEnemies())
            {
                endCombat(true);
            }
        }
    }


    // Atk object already has the amount, source, and target, among other things.
    // Returns final damage done after block and status effects.
    public static int performAttack(Attack atk)
    {
        atk = atk.source.onAttack(atk);
        atk = atk.target.onReceiveAttack(atk);
        return atk.damage;
    }

    // Run through all block gain events for the source, then apply it
    public static void addBlock(int blockAmount, Entity source)
    {
        blockAmount = source.onGainBlock(blockAmount);
        if (!source.hostile)
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
    // Environments are never killed.
    public static void resolveDeath()
    {
        for (int i = EnemySide.Count - 1; i >= 0; i--)
        {
            if (!EnemySide[i].isAlive() && !EnemySide[i].isEnvironment)
            {
                EnemySide[i].die();
            }
        }
        for (int i = PlayerSide.Count - 1; i >= 0; i--)
        {
            if (!PlayerSide[i].isAlive() && !PlayerSide[i].isEnvironment)
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
        EnemySide = new List<Entity>();
        PlayerSide = new List<Entity>();
        Taunters = new List<Entity>();
        turnNumber = 0;
    }

    // Adds a new entity to the battlefield.
    // hostile determines which side.
    public static bool SummonEntity(string entityName, bool hostile, int index = 0, string? master = null)
    {
        // First check if entity is a hero or entity:
        bool isHero = false;
        Entity? newEntity = new Entity();
        if (DataRegistry.CharacterData.heroExists(entityName))
        {
            isHero = true;
            newEntity = new PlayerCharacter(entityName);
        }
        else if (DataRegistry.CharacterData.entityExists(entityName))
        {
            // Now try a normal entity:
            newEntity = new Entity(entityName);
        }
        else
        {
            Console.WriteLine("Given name '" + entityName + "' did not match any entity or hero.");
            return false;
        }
        // Found entity to summon.
        if (master != null) newEntity.master = master;
        newEntity.exhausted = true;
        if (hostile)
        { // Summoning to the enemy side.
            newEntity.hostile = true;
            newEntity.playerControlled = false;
            EnemySide.Insert(index, newEntity!);
            newEntity.previousAction = null;
            Console.WriteLine("Successfully summoned '" + newEntity.name + "' to enemy side.");
            return true;
        }
        else
        { // Summoning to the player's side.
            newEntity.hostile = false;
            if (isHero)
            {
                newEntity.playerControlled = true;
            }
            else
            { // must be entity
                newEntity.playerControlled = false;
            }
            PlayerSide.Insert(index, newEntity!);
            newEntity.previousAction = null;
            Console.WriteLine("Successfully summoned '" + newEntity.name + "' to player side.");
            return true;
        }

    }



}