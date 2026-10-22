public class Charged : StatusEffect {

    public List<string> AffectedList = Action.SpellsWithDamageBlockOrHealing;
    
    public Charged(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Charged";
        this.description = "Increases damage/block/healing for the next spell.";
        this.owner = owner;
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.damage += this.amount;
            actionBeingUsed.block += this.amount;
            actionBeingUsed.healing += this.amount;
            Console.WriteLine("DEBUG: apply charged bonus");
        }
        return actionBeingUsed;
    }

    public override void onActionResolved(Action actionUsed)
    {
        if(AffectedList.Contains(actionUsed.name) && actionUsed.owner == this.owner)
        {
            actionUsed.damage -= this.amount;
            actionUsed.block -= this.amount;
            actionUsed.healing -= this.amount;
            Console.WriteLine(actionUsed.name+" used up "+this.name+".");
            this.Decrease(amount);
        }
        base.onActionResolved(actionUsed);
    }
}