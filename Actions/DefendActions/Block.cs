public class Block : Action {

    public Block() {
        this.name = "Block";
        this.description = "Generate 6 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 6;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity? target, Modifier? modifier) {
        // Generate Block.
        Battlefield.addBlock(block, true);
    }
}