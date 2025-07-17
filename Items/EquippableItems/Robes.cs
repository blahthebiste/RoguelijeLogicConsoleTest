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
        this.getOwner()!.AddStatusEffect(new SpellPower(1, this.getOwner()!));
    }

    
    // Also apply SpellPower on equip if in combat:
    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        if(CurrentRun.InCombat) {
            // Add 1 SpellPower:
            this.getOwner()!.AddStatusEffect(new SpellPower(1, this.getOwner()!));
        }
    }

    // Remove 1 SpellPower if in combat
    public override void onUnequip() {
        if(this.getOwner() == null) {
            return;
        }
        base.onUnequip();
        if(CurrentRun.InCombat) {
            // Remove 1 SpellPower:
            this.getOwner()!.AddStatusEffect(new SpellPower(-1, this.getOwner()!));
        }
    }

}