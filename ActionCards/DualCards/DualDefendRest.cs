public class DualDefendRest : DualActionCard {

    public DualDefendRest() {
        this.name = this.originalName = "Defend/Rest";
        this.description = this.originalDescription = "Perform a defend or rest action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.DEFEND;
        this.actionType2 = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }

}