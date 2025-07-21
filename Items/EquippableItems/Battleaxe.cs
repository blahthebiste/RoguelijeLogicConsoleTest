public class Battleaxe : EquipmentItem {


    public Battleaxe()
    {
        this.name = "Battleaxe";
        this.description = "Your Attack action also hits the enemy below.";
        this.slot = ActionType.ATTACK;
        this.price = 130;
        this.tier = 2;
    }

    public override void onEquip() {
        base.onEquip();
        // Make the equipped action hit below
        this.parentAction!.hitsBelow = true;
        this.parentAction!.description += " Also hits enemy below.";
    }


    public override void onUnequip() {
        base.onUnequip();
        // Get a fresh copy of the action, to remove our modifications
        this.parentAction = DataRegistry.ActionData.getActionByName(this.parentAction!.name);
    }

}