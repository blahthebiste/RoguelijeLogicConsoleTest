public class SafeHaven : ActionCard {

    public SafeHaven() {
        this.name = this.originalName = "Safe Haven";
        this.description = this.originalDescription = "All allies perform a rest action.";
        this.actionType = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }


    public override bool AfterUse(Entity entityToUseAction, Entity? target, Action hoveredAction)
    { 
        // Loop through allies, trigger their first rest action
        foreach (Entity ally in Battlefield.PlayerSide.ToList())
        {
            // Make sure not to double up on the ally who ussed Safe Haven!
            if (hoveredAction.owner == ally)
            {
                continue;
            }
            foreach (Action act in ally.ActionListMinusPassives.ToList())
            {
                // Match the first action that is type rest and targets self:
                if (act.actionType == ActionType.REST && act.targetting == TargetCategory.SELF)
                {
                    Console.WriteLine(ally.name + " rests due to Safe Haven!");
                    act.promptUse(free: true);
                    break;
                }
            }
        }
        return true;
    }
}