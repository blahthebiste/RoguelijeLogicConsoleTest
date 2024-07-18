public class Whirl : Action {

    public Whirl() {
        this.name = "Whirl";
        this.description = "Next attack also hits adjacent targets.";
        this.actionType = ActionType.Skill;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity source, Entity? target, Modifier? modifier) {
        // Apply the Whirl status effect
        source.recieveStatusEffect(new Whirling(1));
    }
}