public class Sapphire : EquipmentItem {


    public Sapphire() {
        this.name = "Sapphire";
        this.description = "+1 Spell use.";
        this.slot = ActionType.SPELL;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        // Add 1 spell use and max spell use for the action:
        this.parentAction.uses += 1;
        this.parentAction.maxUses += 1;
    }


    public override void onUnequip() {
        base.onUnequip();
        // Remove 1 spell use and max spell use from the action:
        if(this.parentAction.uses > 0) this.parentAction.uses -= 1; // Don't let uses get negative
        this.parentAction.maxUses -= 1;
    }

}