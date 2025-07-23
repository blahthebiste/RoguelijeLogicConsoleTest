public class DualAttackDefend : DualActionCard {

    public DualAttackDefend() {
        this.name = "Attack/Defend";
        this.description = "Perform an attack or defend action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.ATTACK;
        this.actionType2 = ActionType.DEFEND;
        this.owner = null;
        this.modifier = null;
    }

}