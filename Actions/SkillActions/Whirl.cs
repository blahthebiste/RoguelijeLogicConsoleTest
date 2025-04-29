public class Whirl : Action {

    public Whirl() {
        this.name = "Whirl";
        this.description = "Next attack also hits adjacent targets.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Whirl status effect
        target!.AddStatusEffect(new Whirling(magicNumber, target));
        return true;
    }
}