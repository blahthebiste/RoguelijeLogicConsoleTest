public class Robes : EquipmentItem {


    public Robes() {
        this.name = "Robes";
        this.description = "+1 Spell Power.";
        this.slot = ActionType.SPELL;
        this.price = 65;
    }

    public override void startOfCombat() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        // Add 1 Spell Power:
        this.getOwner().AddStatusEffect(new SpellPower(1, this.getOwner()));
    }



}