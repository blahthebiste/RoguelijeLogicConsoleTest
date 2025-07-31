public class DualAttackSpell : DualActionCard {

    public DualAttackSpell() {
        this.name = this.originalName = "Attack/Spell";
        this.description = this.originalDescription = "Perform an attack or spell action.";
        this.actionType = ActionType.DUAL;
        this.actionType1 = ActionType.ATTACK;
        this.actionType2 = ActionType.SPELL;
        this.owner = null;
        this.modifier = null;
    }

}