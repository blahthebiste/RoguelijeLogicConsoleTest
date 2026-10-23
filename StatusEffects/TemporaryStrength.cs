public class TemporaryStrength : StatusEffect {

    public List<string> AffectedList = Action.AttacksOrDefendsWithDamageOrBlock;
    public TemporaryStrength(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Temporary Strength";
        this.description = "Increases attack damage and block for the next action.";
        this.owner = owner;
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    // Just add the proper modifier to the action's modifier list. It will be cleared automatically
    // after the action resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new StrengthMod(this.amount));
            Console.WriteLine("DEBUG: apply str bonus");
            owner.RemoveStatusEffectByName(this.name);
        }
        return actionBeingUsed;
    }
}