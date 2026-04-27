public class SkillImmune : Action
{

    public SkillImmune()
    {
        this.name = "Skill-immune";
        this.actionType = ActionType.PASSIVE;
        this.description = "Cannot be targeted by enemy Skills.";
    }

    // All actual impact of this passive is hard-coded elsewhere in the code.
    // Action -> cannot be targeted by enemy Skill actions

}