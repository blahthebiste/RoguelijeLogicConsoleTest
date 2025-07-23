public class DualAttackSkill : DualActionCard {

    public DualAttackSkill() {
        this.name = "Attack or Skill";
        this.description = "Perform an attack or skill action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.ATTACK;
        this.actionType2 = ActionType.SKILL;
        this.owner = null;
        this.modifier = null;
    }

}