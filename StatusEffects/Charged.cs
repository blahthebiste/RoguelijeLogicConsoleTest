public class Charged : StatusEffect {

    public List<string> AffectedList = Action.SpellsWithDamageBlockOrHealing;
    
    public Charged(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Charged";
        this.description = "Increases damage/block/healing for the next spell.";
        this.owner = owner;
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    // Just add the proper modifier to the action's modifier list. It will be cleared automatically
    // after the action resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new ChargedMod(this.amount));
            Console.WriteLine("DEBUG: apply charged bonus");
            owner.RemoveStatusEffectByName(this.name);
        }
        return actionBeingUsed;
    }
}