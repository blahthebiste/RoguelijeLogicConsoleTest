public class BatSwarm : Action {

    public BatSwarm() {
        this.name = "Bat Swarm";
        this.description = "Summon 4 Bats.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 4;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
    }

    public override bool useOnce(Modifier? modifier)
    {
        if (this.owner != null)
        {
            for (int i = 0; i < magicNumber; i++)
            { // TODO: This will error if used by a player character, currently
                Battlefield.SummonEntity("Bat", this.owner.hostile, 0, this.owner.name);
            }
            return true;
        }
        else
        {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return false;
        }
    }
}