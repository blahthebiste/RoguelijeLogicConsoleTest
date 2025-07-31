public class DualAttackDefend : DualActionCard {

    public DualAttackDefend() {
        this.name = this.originalName = "Attack/Defend";
        this.description = this.originalDescription = "Perform an attack or defend action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.ATTACK;
        this.actionType2 = ActionType.DEFEND;
        this.owner = null;
        this.modifier = null;
    }

}