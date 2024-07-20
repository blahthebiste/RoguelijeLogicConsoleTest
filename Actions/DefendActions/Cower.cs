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

    public override void use(Entity? target, Modifier? modifier) {
        // Generate Block.
        Battlefield.addBlock(block, true);
    }
}