public class TowerShield : EquipmentItem {


    public TowerShield() {
        this.name = "Tower Shield";
        this.description = "At the start of combat, gain PermaBlock equal to the Block of your Defend action.";
        this.slot = ActionType.DEFEND;
        this.price = 65;
    }

    // Apply the effect to keep block
    public override void startOfCombat() {
        if(this.parentAction != null && this.parentAction.block > 0) {
            int shieldAmount = this.parentAction.block;
            if(this.parentAction.owner != null) {
                Console.WriteLine("Tower Shield granted "+this.parentAction.owner.name+" "+shieldAmount+" PermaBlock.");
                this.parentAction.owner.AddStatusEffect(new PermaBlock(shieldAmount, this.parentAction.owner));
            }
        }
    }

}