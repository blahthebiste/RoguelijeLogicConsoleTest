public class RightfulHeir : Action {

    public RightfulHeir() {
        this.name = "Rightful Heir";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.NONE;
        this.description = "Summon all 3 members of the King's Court to fight for you.";
    }

    public override bool useOnce(Modifier? modifier)
    {
        if (this.owner != null)
        {
            // TODO: This will error if used by an enemy character, currently
            Battlefield.SummonEntity("Hand of the King", this.owner.hostile, 0, this.owner.name);
            Battlefield.SummonEntity("Royal Sorcerer", this.owner.hostile, 0, this.owner.name);
            Battlefield.SummonEntity("Deadeye Assassin", this.owner.hostile, 0, this.owner.name);
            return true;
        }
        else
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return false;
        }
    }
}