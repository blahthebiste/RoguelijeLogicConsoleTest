public abstract class DualActionCard : ActionCard {
    // These are action cards that let you choose between 2 actions to play.
    public ActionType actionType1;
    public ActionType actionType2;

    // Whether this card can be played on that action.
    public override bool actionCanBeUsed(Action hoveredAction) {
        if(hoveredAction.actionType == this.actionType1 || hoveredAction.actionType == this.actionType2) return true;
        else return base.actionCanBeUsed(hoveredAction);;
    }

}