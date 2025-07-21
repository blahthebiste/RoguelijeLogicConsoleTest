public class HeartCrystal : EquipmentItem {


    public HeartCrystal()
    {
        this.name = "Heart Crystal";
        this.description = "+1 Regen.";
        this.slot = ActionType.ANY;
        this.price = 65;
        this.tier = 1;
    }

    public override void startOfCombat() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        // Add 1 regen:
        this.getOwner()!.AddStatusEffect(new Regen(1, this.getOwner()!));
    }

    // Also apply regen on equip if in combat:
    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        if(CurrentRun.InCombat) {
            // Add 1 regen:
            this.getOwner()!.AddStatusEffect(new Regen(1, this.getOwner()!));
        }
    }

    // Remove 1 regen if in combat
    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        if(CurrentRun.InCombat) {
            // Remove 1 regen:
            this.getOwner()!.AddStatusEffect(new Regen(-1, this.getOwner()!));
        }
    }

}