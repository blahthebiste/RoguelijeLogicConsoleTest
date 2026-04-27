public class Focus : Action
{

    public Focus()
    {
        this.name = "Focus";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.SELF;
        this.description = "Regain all Spell uses.";
    }

    public override bool useOnce(Modifier? modifier)
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        // Restore spell uses.
        foreach (Action action in this.owner.ActionList)
        {
            if (action.hasLimitedUses)
            {
                Console.WriteLine("Regaining uses for " + action.name + " up to " + action.maxUses);
                action.uses = action.maxUses;
            }
        }
        return true;
    }
    
}