public class ConjureDagger : Action {

    public ConjureDagger() {
        this.name = "Conjure Dagger";
        this.description = "Summon 1 Mystical Dagger. Cannot be used if Mystical Dagger is alive.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
    }

    // Requires that there is not already a mystical dagger.
    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        foreach (Entity enemy in Battlefield.EnemySide)
        {
            if (enemy.name == "Mystical Dagger")
            {
                Console.WriteLine(this.name + " can only used if Mystical Dagger is dead!");
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
                Battlefield.SummonEntity("Mystical Dagger", this.owner.hostile, 0, this.owner.name);
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