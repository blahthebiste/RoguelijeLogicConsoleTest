public class ElderWand : EquipmentItem {

    KillingWord killingwordInstance;

    public ElderWand()
    {
        this.killingwordInstance = new KillingWord();
        this.killingwordInstance.equippedItem = this;
        this.name = "Elder Wand";
        this.description = "Replace your Spell action with '" + killingwordInstance + "'.";
        this.slot = ActionType.SPELL;
        this.price = 260;
        this.tier = 3;
    }

    public override void onEquip() {
        base.onEquip();
        if(this.getOwner() == null) {
            return;
        }
        killingwordInstance.owner = this.getOwner();
        // Replace parent action in action list with Living Flame
        this.replaceAction(this.killingwordInstance);
    }


    public override void onUnequip() {
        base.onUnequip();
        // Replace parent action in action list with the original action that was replaced
        this.restoreOriginalAction();
    }

}