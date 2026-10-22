public class Surging : StatusEffect {

    public List<string> AffectedList = Action.SpellsWithDamageBlockOrHealing;

    public Surging(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Surging";
        this.description = "Doubles damage/block/healing for the next X spells.";
        this.owner = owner;
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.damage *= 2;
            actionBeingUsed.block *= 2;
            actionBeingUsed.healing *= 2;
            Console.WriteLine("DEBUG: apply SURGING bonus");
        }
        return actionBeingUsed;
    }

    public override void onActionResolved(Action actionUsed)
    {
        if(AffectedList.Contains(actionUsed.name) && actionUsed.owner == this.owner)
        {
            actionUsed.damage /= 2;
            actionUsed.block /= 2;
            actionUsed.healing /= 2;
            Console.WriteLine(actionUsed.name+" used up "+this.name+".");
            this.Decrease(amount);
        }
        base.onActionResolved(actionUsed);
    }
}