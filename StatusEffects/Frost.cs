public class Frost : StatusEffect
{

    public List<string> AffectedList = Action.AttacksOrDefendsWithDamageOrBlock;
    public Frost(int amount, Entity owner)
    {
        this.amount = amount;
        this.name = "Frost";
        this.description = "Decreases attack damage and block until you rest.";
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
            actionBeingUsed.ApplyModifierToProperties(new FrostMod(this.amount));
            Console.WriteLine("DEBUG: apply frost penalty");
    
        }
        // Remove frost when resting:
        if(actionBeingUsed.actionType == ActionType.REST) {
            owner.RemoveStatusEffectByName(this.name);
        }
        return actionBeingUsed;
    }
    
    
   
}