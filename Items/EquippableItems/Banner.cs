public class Banner : EquipmentItem {

    Taunt tauntInstance;

    public Banner()
    {
        this.name = "Banner";
        this.description = "Your Defend action also performs Taunt.";
        this.slot = ActionType.DEFEND;
        this.price = 130;
        this.tauntInstance = new Taunt();
        this.tier = 2;
    }

    // On action use:
    public override Action onUseEquippedAction(Action actionBeingUsed) {
        Console.WriteLine("Banner performs Taunt.");
        this.tauntInstance.promptUse();
        return actionBeingUsed;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        this.tauntInstance.owner = this.getOwner();
    }


    public override void onUnequip() {
        base.onUnequip();
        this.tauntInstance.owner = null;
        if(this.getOwner() == null) {
            return;
        }
    }

}