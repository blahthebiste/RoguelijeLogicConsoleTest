public class Channel : ActionCard {

    public Channel() {
        this.name = this.originalName = "Channel";
        this.description = this.originalDescription = "Regain all Spell uses, then perform a spell action.";
        this.actionType = ActionType.SPELL;
        this.owner = null;
        this.modifier = null;
        this.bypassOutOfUses = true; // Can play this card even on actions which are out of uses
    }

    public override bool BeforeUse(Entity entityToUseAction, Entity? target, Action hoveredAction)
    {
        // Restore spell uses.
        foreach (Action action in entityToUseAction.ActionList)
        {
            if (action.hasLimitedUses && action.uses < action.maxUses)
            {
                Console.WriteLine("Regaining uses for " + action.name + " up to " + action.maxUses);
                action.uses = action.maxUses;
            }
        }
        return base.BeforeUse(entityToUseAction, target, hoveredAction);        
    }

}