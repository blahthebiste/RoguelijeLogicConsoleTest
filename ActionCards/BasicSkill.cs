public class BasicSkill : ActionCard {

    public BasicSkill() {
        this.name = "Basic Skill";
        this.description = "Perform a skill action.";
        this.actionType = ActionType.Skill;
        this.owner = null;
        this.modifier = null;
    }

}