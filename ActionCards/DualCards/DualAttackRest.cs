public class DualAttackRest : DualActionCard {

    public DualAttackRest() {
        this.name = "Attack or Rest";
        this.description = "Perform an attack or rest action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.ATTACK;
        this.actionType2 = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }

}