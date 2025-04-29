public class Cower : Action {

    public Cower() {
        this.name = "Cower";
        this.description = "Generate 3 Block.";
        this.actionType = ActionType.DEFEND;
        this.block = 3;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int calcedBlock = target!.onGainBlock(block);
        // Generate Block.
        Battlefield.addBlock(calcedBlock, (target!.playerControlled));            
        return true;
    }
}