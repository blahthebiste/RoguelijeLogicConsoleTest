public class Cower : Action {

    public Cower() {
        this.name = "Cower";
        this.description = "Generate 3 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 3;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(owner == null) {
            Console.WriteLine("ERROR: no owner for action!");
            return false;
        }
        int calcedBlock = this.owner.onGainBlock(block);
        // Generate Block.
        Battlefield.addBlock(calcedBlock, true);
        return base.use(target, modifier);
    }
}