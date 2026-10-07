public class RampageVictim : StatusEffect
{
    Decimate decimateInstance;

    public RampageVictim(Entity origin, Entity owner)
    {
        this.amount = 1;
        this.name = "Rampage Victim";
        this.description = "If you die to a Berserker, chain the excess damage to a random ally.";
        this.owner = owner;
        decimateInstance = new Decimate();
        decimateInstance.owner = origin;
        this.isDebuff = true;
    }

    public override void onDeath()
    {
        Console.WriteLine("DEBUG: initiating Rampage onDeath from "+owner.name+"("+owner.getIndex()+")");
        // Do nothing if the owner was not overkilled.
        if(owner == null || owner.currentHP > -1)
        {
            return;
        }
        Entity? nextTarget = null;
        int targetIndex = -1;
        // This apparently queries list for all entites with HP > 0. LINQ is magic!
        var ValidTargetsLINQ = from Entity ent in Battlefield.EnemySide where ent.isAlive() select ent;
        if(!owner.hostile)
        {
            // Other side
            ValidTargetsLINQ = from Entity ent in Battlefield.PlayerSide where ent.isAlive() select ent;
        }
        List<Entity> ValidTargets = ValidTargetsLINQ.ToList();
        ValidTargets.Remove(owner); // Cannot target self
        // Check for targets:
        if(ValidTargets.Count < 1)
        {
            Console.WriteLine("DEBUG: no more enemies to chain to!");
            base.onDeath();
            return;
        }
        // Select target randomly:
        targetIndex = CurrentRun.rng.Next(0, ValidTargets.Count);
        nextTarget = ValidTargets[targetIndex];
        Console.WriteLine("DEBUG: chaining to "+nextTarget.name+", index="+targetIndex);
        if (nextTarget == null)
        {
            Console.WriteLine("DEBUG: no more allies to chain to!");
            base.onDeath();
            return;
        }
        if(nextTarget.HasStatusEffect("Rampage Victim"))
        {
            Console.WriteLine("DEBUG: cannot chain to "+nextTarget.name+"; they have already been targeted by Rampage during this attack!");
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
        this.decimateInstance.damage = -1*owner.currentHP;
        this.decimateInstance.useOnTarget(nextTarget, null); // Currently modifiers are not chained.
        base.onDeath();
    }

    public override void onActionResolved(Action actionUsed)
    {
        // Remove this effect, regardless of what action was used or what the result was
        Console.WriteLine("DEBUG: onActionResolved in Rampage Victim");
        this.Decrease(1);        
    }

}