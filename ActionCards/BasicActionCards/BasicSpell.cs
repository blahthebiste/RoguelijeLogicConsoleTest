public class BasicSpell : ActionCard {

    public BasicSpell() {
        this.name = this.originalName = "Basic Spell";
        this.description = this.originalDescription = "Perform a spell action.";
        this.actionType = ActionType.SPELL;
        this.owner = null;
        this.modifier = null;
    }

}