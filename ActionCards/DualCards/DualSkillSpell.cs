public class DualSkillSpell : DualActionCard {

    public DualSkillSpell() {
        this.name = this.originalName = "Skill/Spell";
        this.description = this.originalDescription = "Perform a skill or spell action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.SKILL;
        this.actionType2 = ActionType.SPELL;
        this.owner = null;
        this.modifier = null;
    }

}