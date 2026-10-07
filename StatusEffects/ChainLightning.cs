using System.Reflection.Metadata;

public class ChainLightning : StatusEffect
{
    Lightning lightningInstance;

    public ChainLightning(Entity origin, Entity owner)
    {
        this.amount = 1;
        this.name = "Chain Lightning";
        this.description = "If you die to Lightning, chain the excess damage to an adjacent ally.";
        this.owner = owner;
        lightningInstance = new Lightning();
        lightningInstance.owner = origin;
        this.isDebuff = true;
    }

    public override void onDeath()
    {
        Console.WriteLine("DEBUG: initiating chain lightning onDeath from "+owner.name+"("+owner.getIndex()+")");
        // Do nothing if the owner was not overkilled.
        if(owner == null || owner.currentHP > -1)
        {
            return;
        }
        Entity? nextTarget = null;
        int targetIndex = -1;
        // Check for adjacent living targets.
        if(owner.hostile)
        {
            // Check for enemies:
            if(Battlefield.EnemySide.Count < 2)
            {
                Console.WriteLine("DEBUG: no more enemies to chain to!");
                base.onDeath();
                return;
            }
            int index = Battlefield.EnemySide.IndexOf(owner);
            if(index == 0)
            { // Top enemy. Chain down.
                nextTarget = Battlefield.EnemySide[index+1];
                targetIndex = index+1;
                Console.WriteLine("DEBUG: only option is to chain down.");
            }
            else if(index == Battlefield.EnemySide.Count - 1)
            { // Bottom enemy. Chain up.
                nextTarget = Battlefield.EnemySide[index-1];
                targetIndex = index-1;
                Console.WriteLine("DEBUG: only option is to chain up.");
            }
            else
            { // Somewhere in the middle. Chain up or down randomly.
                // First check if either target is valid:
                bool canChainUp = false;
                bool canChainDown = false;
                Entity enemyAbove = Battlefield.EnemySide[index-1];
                Entity enemyBelow = Battlefield.EnemySide[index+1];
                if(enemyBelow.isAlive() && !enemyBelow.HasStatusEffect("Chain Lightning") && !Battlefield.DyingEntities.Contains(enemyBelow))
                { // Down is valid target
                    canChainDown = true;
                }
                if(enemyAbove.isAlive() && !enemyAbove.HasStatusEffect("Chain Lightning") && !Battlefield.DyingEntities.Contains(enemyAbove))
                { // Up is valid target
                    canChainUp = true;
                }
                if(canChainDown && canChainUp)
                {
                    // Chain up or down randomly.
                    int coinFlip = CurrentRun.rng.Next(0, 2);
                    if(coinFlip == 1)
                    { // Chain down
                        nextTarget = enemyBelow;
                        targetIndex = index+1;
                        Console.WriteLine("DEBUG: chained down randomly.");
                    }
                    else
                    { // Chain up
                        nextTarget = enemyAbove;
                        targetIndex = index-1;
                        Console.WriteLine("DEBUG: chained up randomly.");
                    }
                }
                else if(canChainDown)
                { // Can only chain down.
                    nextTarget = enemyBelow;
                    targetIndex = index+1;
                    Console.WriteLine("DEBUG: chained down.");
                }
                else if(canChainUp)
                { // Can only chain up.
                    nextTarget = enemyAbove;
                    targetIndex = index-1;
                    Console.WriteLine("DEBUG: chained up.");
                }
                else
                {
                    Console.WriteLine("DEBUG: no more enemies to chain to!");
                    base.onDeath();
                    return;
                }
            }
            Console.WriteLine("DEBUG: chaining to "+nextTarget.name+", index="+targetIndex);
        }
        else // owner is non-hostile.
        {
            // Check for allies:
            if(Battlefield.PlayerSide.Count < 2)
            {
                Console.WriteLine("DEBUG: no more allies to chain to!");
                base.onDeath();
                return;
            }
            int index = Battlefield.PlayerSide.IndexOf(owner);
            if(index == 0)
            { // Top ally. Chain down.
                nextTarget = Battlefield.PlayerSide[index+1];
                targetIndex = index+1;
                Console.WriteLine("DEBUG: only option is to chain down.");
            }
            else if(index == Battlefield.PlayerSide.Count - 1)
            { // Bottom ally. Chain up.
                nextTarget = Battlefield.PlayerSide[index-1];
                targetIndex = index-1;
                Console.WriteLine("DEBUG: only option is to chain up.");
            }
            else
            { // Somewhere in the middle. 
                // First check if either target is valid:
                bool canChainUp = false;
                bool canChainDown = false;
                Entity enemyAbove = Battlefield.EnemySide[index-1];
                Entity enemyBelow = Battlefield.EnemySide[index+1];
                if(enemyBelow.isAlive() && !enemyBelow.HasStatusEffect("Chain Lightning") && !Battlefield.DyingEntities.Contains(enemyBelow))
                { // Down is valid target
                    canChainDown = true;
                }
                if(enemyAbove.isAlive() && !enemyAbove.HasStatusEffect("Chain Lightning") && !Battlefield.DyingEntities.Contains(enemyAbove))
                { // Up is valid target
                    canChainUp = true;
                }
                if(canChainDown && canChainUp)
                {
                    // Chain up or down randomly.
                    int coinFlip = CurrentRun.rng.Next(0, 2);
                    if(coinFlip == 1)
                    { // Chain down
                        nextTarget = enemyBelow;
                        targetIndex = index+1;
                        Console.WriteLine("DEBUG: chained down randomly.");
                    }
                    else
                    { // Chain up
                        nextTarget = enemyAbove;
                        targetIndex = index-1;
                        Console.WriteLine("DEBUG: chained up randomly.");
                    }
                }
                else if(canChainDown)
                { // Can only chain down.
                    nextTarget = enemyBelow;
                    targetIndex = index+1;
                    Console.WriteLine("DEBUG: chained down.");
                }
                else if(canChainUp)
                { // Can only chain up.
                    nextTarget = enemyAbove;
                    targetIndex = index-1;
                    Console.WriteLine("DEBUG: chained up.");
                }
                else
                {
                    Console.WriteLine("DEBUG: no more allies to chain to!");
                    base.onDeath();
                    return;
                }
            }
            Console.WriteLine("DEBUG: chaining to "+nextTarget.name+", index="+targetIndex);
        }
        if (nextTarget == null)
        {
            Console.WriteLine("DEBUG: no more allies to chain to!");
            base.onDeath();
            return;
        }
        if(nextTarget.HasStatusEffect("Chain Lightning"))
        {
            Console.WriteLine("DEBUG: cannot chain to "+nextTarget.name+"; they have already been targeted by Lightning during this attack!");
            base.onDeath();
            return;
        }
        if(!nextTarget.isAlive() || Battlefield.DyingEntities.Contains(nextTarget))
        {
            Console.WriteLine("DEBUG: cannot chain to "+nextTarget.name+"; they are already dead or inanimate!");
            base.onDeath();
            return;
        }
        // Owners current HP will be the overkill damage.
        this.lightningInstance.magicNumber = -1*owner.currentHP;
        this.lightningInstance.useOnTarget(nextTarget, null); // Currently modifiers are not chained.
        base.onDeath();
    }

    public override void onActionResolved(Action actionUsed)
    {
        // Remove this effect, regardless of what action was used or what the result was
        Console.WriteLine("DEBUG: onActionResolved in chain lightning");
        this.Decrease(1);        
    }

}