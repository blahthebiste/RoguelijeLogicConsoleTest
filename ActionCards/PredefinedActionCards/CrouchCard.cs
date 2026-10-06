public class CrouchCard : PredefinedActionCard
{

    public CrouchCard()
    {
        this.name = this.originalName = "Crouch";
        this.description = this.originalDescription = "Move to the bottom position (free action).";
        this.actionType = ActionType.MOVEMENT;
        this.owner = null;
        this.modifier = null;
        this.predefinedAction = new Crouch();
    }

}