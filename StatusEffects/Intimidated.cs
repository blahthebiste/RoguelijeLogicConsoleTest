public class Intimidated : StatusEffect
{

    public List<string> AffectedList = Action.AttacksOrDefendsWithDamageOrBlock;

    public Intimidated(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Intimidated";
        this.description = "Temporary Strength-down.";
        this.owner = owner;
        this.isDebuff = true;
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    // Just add the proper modifier to the action's modifier list. It will be cleared automatically
    // after the action resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new IntimidatedMod(this.amount));
            Console.WriteLine("DEBUG: apply intimidated penalty");
    
        }
        return actionBeingUsed;
    }
    
    // Remove Intimidated when at the start of each round
    public override void startOfRound() {
        Console.WriteLine(this.name+" wore off!");
        owner.RemoveStatusEffectByName(this.name);
    }
}