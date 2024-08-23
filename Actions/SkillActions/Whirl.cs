public class Whirl : Action {

    public Whirl() {
        this.name = "Whirl";
        this.description = "Next attack also hits adjacent targets.";
        this.actionType = ActionType.SKILL;
        this.magicNumber = 1;
        this.targetting = TargetCategory.NONE;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        // Apply the Whirl status effect
        owner.recieveStatusEffect(new Whirling(magicNumber, owner));
        return base.use(target, modifier);
    }
}