public class Charge : Action {

    public Charge() {
        this.name = "Charge";
        this.description = "Next spell has +5 to damage/block/healing.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 5;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Apply the Charged status effect
            owner!.AddStatusEffect(new Charged(magicNumber, owner));
            return true;
        }
        return false;
    }
}