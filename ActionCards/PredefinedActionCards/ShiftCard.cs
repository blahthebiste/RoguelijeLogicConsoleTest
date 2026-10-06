public class ShiftCard : PredefinedActionCard
{

    public ShiftCard()
    {
        this.name = this.originalName = "Shift";
        this.description = this.originalDescription = "Move up or down 1 space (free action).";
        this.actionType = ActionType.MOVEMENT;
        this.owner = null;
        this.modifier = null;
        this.predefinedAction = new Shift();
    }

}