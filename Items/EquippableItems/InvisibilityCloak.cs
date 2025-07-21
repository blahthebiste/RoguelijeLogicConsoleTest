public class InvisibilityCloak : EquipmentItem {


    public InvisibilityCloak()
    {
        this.name = "Invisibility Cloak";
        this.description = "Cannot be targeted by enemies while any allies are alive.";
        this.slot = ActionType.SKILL;
        this.price = 260;
        this.tier = 3;
    }

    // Each combat, gain invisibility
    public override void startOfCombat(){
        if(this.getOwner() == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        this.getOwner()!.AddStatusEffect(new Invisibility(99, this.getOwner()!));
    }


    // Also apply Invibility on equip if in combat:
    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        if(CurrentRun.InCombat) {
            // Add 99 Invisibility:
            this.getOwner()!.AddStatusEffect(new Invisibility(99, this.getOwner()!));
        }
    }

    // Remove 99 Invibility if in combat
    public override void onUnequip() {
        if(this.getOwner() == null) {
            return;
        }
        base.onUnequip();
        if(CurrentRun.InCombat) {
            // Remove 99 Toughness:
            this.getOwner()!.AddStatusEffect(new Invisibility(-99, this.getOwner()!));
        }
    }

}