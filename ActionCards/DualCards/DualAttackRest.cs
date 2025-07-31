public class DualAttackRest : DualActionCard {

    public DualAttackRest() {
        this.name = this.originalName = "Attack/Rest";
        this.description = this.originalDescription = "Perform an attack or rest action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.ATTACK;
        this.actionType2 = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }

}