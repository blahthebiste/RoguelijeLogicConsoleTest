public class IronHelm : EquipmentItem {


    public IronHelm()
    {
        this.name = "Iron Helm";
        this.description = "+1 Toughness.";
        this.slot = ActionType.ANY;
        this.price = 65;
        this.tier = 1;
    }


    // Each combat, gain +1 Toughness (it will be removed end of combat by the Battlefield logic)
    public override void startOfCombat(){
        if(this.getOwner() == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        this.getOwner()!.AddStatusEffect(new Toughness(1, this.getOwner()!));
    }


    // Also apply Toughness on equip if in combat:
    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        if(CurrentRun.InCombat) {
            // Add 1 Toughness:
            this.getOwner()!.AddStatusEffect(new Toughness(1, this.getOwner()!));
        }
    }

    // Remove 1 Toughness if in combat
    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        if(CurrentRun.InCombat) {
            // Remove 1 Toughness:
            this.getOwner()!.AddStatusEffect(new Toughness(-1, this.getOwner()!));
        }
    }
}