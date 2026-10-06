public class Reposition : ActionCard {

    public Swap swapAction;

    public Reposition() {
        this.name = this.originalName = "Reposition";
        this.description = this.originalDescription = "Swap places with an ally, then perform a skill action.";
        this.actionType = ActionType.SKILL;
        this.owner = null;
        this.modifier = null;
        this.swapAction = new Swap();
    }

    public override bool BeforeUse(Entity entityToUseAction, Entity? target, Action hoveredAction)
    {
        // Swap places with an ally.
        this.swapAction.owner = hoveredAction.owner;
        swapAction.promptUse();
        return base.BeforeUse(entityToUseAction, target, hoveredAction);        
    }

}