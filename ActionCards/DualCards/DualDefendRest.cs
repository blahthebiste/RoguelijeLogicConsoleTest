public class DualDefendRest : DualActionCard {

    public DualDefendRest() {
        this.name = "Defend/Rest";
        this.description = "Perform a defend or rest action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.DEFEND;
        this.actionType2 = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }

}