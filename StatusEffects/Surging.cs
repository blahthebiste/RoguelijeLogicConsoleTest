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
            actionBeingUsed.ApplyModifierToProperties(new SurgingMod());
            Console.WriteLine("DEBUG: apply surging bonus");
            this.Decrease(1); // Wears down by 1
        }
        return actionBeingUsed;
    }
}