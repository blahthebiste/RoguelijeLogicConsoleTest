public class DualDefendSpell : DualActionCard {

    public DualDefendSpell() {
        this.name = "Defend/Spell";
        this.description = "Perform a defend or spell action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.DEFEND;
        this.actionType2 = ActionType.SPELL;
        this.owner = null;
        this.modifier = null;
    }

}