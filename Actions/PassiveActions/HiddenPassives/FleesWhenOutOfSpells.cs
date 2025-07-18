public class FleesWhenOutOfSpells : Action {

    public FleesWhenOutOfSpells()
    {
        this.name = "Flees when out of spells";
        this.description = "This entity will flee whenever all of their spell uses hit zero.";
        this.actionType = ActionType.PASSIVE;
        this.hiddenAction = true;
    }

    // Check whether this entity has any spell uses remaining.
    public override void startOfTurn()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        foreach (Action act in this.owner.ActionListMinusPassives.ToList())
        {
            if (act.hasLimitedUses && act.uses > 0)
            {
                Console.WriteLine(act.name + " has uses remaining.");
                return;
            }
        }
        // All out of spells, run away!
        Console.WriteLine(this.owner.name + " is all out of spells!");
        ((Enemy)this.owner).fleeing = true;
        return;
    }
}