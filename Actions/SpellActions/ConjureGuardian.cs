public class ConjureGuardian : Action {

    public ConjureGuardian() {
        this.name = "Conjure Guardian";
        this.description = "Summon a Frost Knight. Cannot be used if Frost Knight is alive.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 0;
        this.maxUses = 1;
        this.targetting = TargetCategory.NONE;
    }

    // Requires that there is not already a Frost Knight.
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        foreach (Entity enemy in Battlefield.EnemySide)
        {
            if (enemy.name == "Frost Knight")
            {
                Console.WriteLine(this.name + " can only used if Frost Knight is dead!");
                return false;
            }
        }
        return base.canUse(target, modifier);        
    }
    
    public override bool useOnce(Modifier? modifier)
    {
        if (this.owner != null)
        {
            for (int i = 0; i < magicNumber; i++)
            { // TODO: This will error if used by a player character, currently
                Battlefield.SummonEntity("Frost Knight", this.owner.hostile, 0, this.owner.name);
            }
            return true;
        }
        else
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return false;
        }
    }
}