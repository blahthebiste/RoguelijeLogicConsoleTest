public class SwordnBoard : ActionCard {

    public SwordnBoard() {
        this.name = this.originalName = "Sword n' Board";
        this.description = this.originalDescription = "Perform an attack action and a defend action.";
        this.actionType = ActionType.ATTACK;
        this.owner = null;
        this.modifier = null;
    }


    public override bool use(Entity entityToUseAction, Entity? target, Action hoveredAction)
    { 
        if(base.use(entityToUseAction, target, hoveredAction) && entityToUseAction is PlayerCharacter)
        {
            // On successful use, have them attempt to use a defend action as well:
            ((PlayerCharacter)entityToUseAction).PromptForTypedAction(ActionType.DEFEND);
            return true;
        }
        return false;        
    }
}