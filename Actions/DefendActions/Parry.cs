public class Parry : Action {

    public Parry() {
        this.name = "Parry";
        this.description = "Generate 4 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 4;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            int calcedBlock = this.owner!.onGainBlock(block);
            // Generate Block.
            Battlefield.addBlock(calcedBlock, true);
            return true;
        }
        return false;
    }
}