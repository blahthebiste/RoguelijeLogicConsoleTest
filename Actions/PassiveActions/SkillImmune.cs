public class SkillImmune : Action
{

    public SkillImmune()
    {
        this.name = "Skill-immune";
        this.description = "Cannot be targeted by enemy Skills.";
        this.actionType = ActionType.PASSIVE;
    }

    // All actual impact of this passive is hard-coded elsewhere in the code.
    // Action -> cannot be targeted by enemy Skill actions

}