public class HeavyArmor : EquipmentItem {


    public HeavyArmor() {
        this.name = "Heavy Armor";
        this.description = "+12 max HP. Cannot dodge.";
        this.slot = ActionType.ANY;
        this.price = 260;
    }

    // Each combat, gain Impeded
    public override void startOfCombat(){
        if(this.getOwner() == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        this.getOwner()!.AddStatusEffect(new Impeded(99, this.getOwner()!));
    }


    public override void onEquip() {
        base.onEquip();
        // Add +12 max HP to owner
        if(this.getOwner() == null) {
            return;
        }
        this.getOwner()!.changeMaxHP(12);
        if(CurrentRun.InCombat) {
            // Add 99 Impeded:
            this.getOwner()!.AddStatusEffect(new Impeded(99, this.getOwner()!));
        }
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove the bonus HP
        this.getOwner()!.changeMaxHP(-12);
        if(CurrentRun.InCombat) {
            // Remove 99 Impeded:
            this.getOwner()!.AddStatusEffect(new Impeded(-99, this.getOwner()!));
        }
    }

}