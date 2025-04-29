public class Dodge : Action {

    public Dodge() {
        this.name = "Dodge";
        this.description = "Dodge the next attack this turn.";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Dodge status effect
        target!.AddStatusEffect(new Dodging(magicNumber, target));
        return true;
    }
}