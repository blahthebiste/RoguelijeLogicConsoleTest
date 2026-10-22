public class SpellPower : StatusEffect {

    public List<string> AffectedList = Action.SpellsWithDamageBlockOrHealing;

    public SpellPower(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Spell Power";
        this.description = "Increases damage/block/healing of spells.";
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
            Console.WriteLine("DEBUG: apply spellpower bonus");
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
            Console.WriteLine("DEBUG: remove spellpower bonus");
        }
        base.onActionResolved(actionUsed);
    }
}