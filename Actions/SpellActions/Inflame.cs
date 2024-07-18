public class Inflame : Action {

    public Inflame() {
        this.name = "Inflame";
        this.description = "Gain 2 Strength.";
        this.actionType = ActionType.Spell;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 1;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity source, Entity? target, Modifier? modifier) {
        // Apply the Whirl status effect
        source.recieveStatusEffect(new Strength(magicNumber));
        base.use();
    }
}