public class Parry : Action {

    public Parry() {
        this.name = "Parry";
        this.description = "Generate 4 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 4;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int calcedBlock = target!.onGainBlock(block);
        // Generate Block.
        Battlefield.addBlock(calcedBlock, (target!.playerControlled));
        return true;
    }
}