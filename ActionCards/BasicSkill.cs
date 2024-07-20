public class BasicSkill : ActionCard {

    public BasicSkill() {
        this.name = "Basic Skill";
        this.description = "Perform a skill action.";
        this.actionType = ActionType.SKILL;
        this.owner = null;
        this.modifier = null;
    }

}