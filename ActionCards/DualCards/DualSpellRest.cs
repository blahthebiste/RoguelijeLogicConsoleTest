public class DualSpellRest : DualActionCard {

    public DualSpellRest() {
        this.name = this.originalName = "Spell/Rest";
        this.description = this.originalDescription = "Perform a spell or rest action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.SPELL;
        this.actionType2 = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }

}