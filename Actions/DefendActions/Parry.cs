public class Parry : Action {

    public Parry() {
        this.name = "Parry";
        this.description = "Generate 4 Block.";
        this.actionType = ActionType.Defend;
        this.block = 4;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity source, Entity? target, Modifier? modifier) {
        // Generate Block.
        Battlefield.addBlock(block, true);
    }
}