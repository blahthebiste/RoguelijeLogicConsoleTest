public class DualAttackSpell : DualActionCard {

    public DualAttackSpell() {
        this.name = "Attack/Spell";
        this.description = "Perform an attack or spell action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.ATTACK;
        this.actionType2 = ActionType.SPELL;
        this.owner = null;
        this.modifier = null;
    }

}