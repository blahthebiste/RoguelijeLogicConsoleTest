public class Quarterstaff : EquipmentItem {

    Parry parryInstance;

    public Quarterstaff() {
        this.parryInstance = new Parry();
        this.name = "Quarterstaff";
        this.description = "You have Parry [Generate 4 Block] as an additional Defend action option.";
        this.slot = ActionType.ATTACK;
        this.price = 65;
    }

    public override void onEquip() {
        base.onEquip();
        // Add Strike action to owners action list
        if(this.getOwner() == null) {
            return;
        }
        parryInstance.owner = this.getOwner();
        this.getOwner()!.ActionList.Add(parryInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        if(this.getOwner() == null) {
            return;
        }
        // Remove Strike action to owners action list
        this.getOwner()!.ActionList.Remove(parryInstance);
    }

}