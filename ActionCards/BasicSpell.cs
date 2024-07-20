public class BasicSpell : ActionCard {

    public BasicSpell() {
        this.name = "Basic Spell";
        this.description = "Perform a spell action.";
        this.actionType = ActionType.SPELL;
        this.owner = null;
        this.modifier = null;
    }

}