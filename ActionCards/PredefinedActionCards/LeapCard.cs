public class LeapCard : PredefinedActionCard
{

    public LeapCard()
    {
        this.name = this.originalName = "Leap";
        this.description = this.originalDescription = "Move to the top position (free action).";
        this.actionType = ActionType.MOVEMENT;
        this.predefinedAction = new Leap();
    }

}