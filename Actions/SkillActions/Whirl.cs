public class Whirl : Action {

    public Whirl() {
        this.name = "Whirl";
        this.description = "Next attack also hits adjacent targets.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 1;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Apply the Whirl status effect
            owner!.AddStatusEffect(new Whirling(magicNumber, owner));
            return true;
        }
        return false;
    }
}