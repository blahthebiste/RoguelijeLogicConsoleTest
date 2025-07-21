public class PhoenixWand : EquipmentItem {

    LivingFlame livingFlameInstance;

    public PhoenixWand()
    {
        this.livingFlameInstance = new LivingFlame();
        this.livingFlameInstance.equippedItem = this;
        this.name = "Phoenix Wand";
        this.description = "Replace your Spell action with '" + livingFlameInstance + "'.";
        this.slot = ActionType.SPELL;
        this.price = 130;
        this.tier = 2;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        livingFlameInstance.owner = this.getOwner();
        // Replace parent action in action list with Living Flame
        this.replaceAction(this.livingFlameInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}