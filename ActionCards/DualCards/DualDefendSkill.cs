public class DualDefendSkill : DualActionCard {

    public DualDefendSkill() {
        this.name = this.originalName = "Defend/Skill";
        this.description = this.originalDescription = "Perform a defend or skill action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.DEFEND;
        this.actionType2 = ActionType.SKILL;
        this.owner = null;
        this.modifier = null;
    }

}