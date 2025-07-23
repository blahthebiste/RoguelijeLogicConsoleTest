public class DualSpellRest : DualActionCard {

    public DualSpellRest() {
        this.name = "Spell or Rest";
        this.description = "Perform a spell or rest action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.SPELL;
        this.actionType2 = ActionType.REST;
        this.owner = null;
        this.modifier = null;
    }

}