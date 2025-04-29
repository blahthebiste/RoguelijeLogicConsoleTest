public class Charge : Action {

    public Charge() {
        this.name = "Charge";
        this.description = "Next spell has +5 to damage/block/healing.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 5;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the Charged status effect
        target!.AddStatusEffect(new Charged(magicNumber, target));
        return true;
    }
}