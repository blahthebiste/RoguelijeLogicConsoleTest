public class DualSkillRest : DualActionCard {

    public DualSkillRest() {
        this.name = "Skill/Rest";
        this.description = "Perform a skill or rest action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.SKILL;
        this.actionType2 = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }

}