public class BasicSkill : ActionCard {

    public BasicSkill() {
        this.name = this.originalName = "Basic Skill";
        this.description = this.originalDescription = "Perform a skill action.";
        this.actionType = ActionType.SKILL;
        this.owner = null;
        this.modifier = null;
    }

}