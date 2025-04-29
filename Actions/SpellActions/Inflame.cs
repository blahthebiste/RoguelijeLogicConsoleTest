public class Inflame : Action {

    public Inflame() {
        this.name = "Inflame";
        this.description = "Gain 2 Strength.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Apply the strength buff
        target!.AddStatusEffect(new Strength(magicNumber, target));
        return true;
    }
}