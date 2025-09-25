public class DeepFreeze : Action {

    public DeepFreeze() {
        this.name = "Deep Freeze";
        this.description = "Apply 8 Frost to an enemy.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 1;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    // This will apply 8 frost to the target.
    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Not affected by spell power or charged.
        // Apply the frost to the target.
        target.AddStatusEffect(new Frost(magicNumber, target));
        return true;
    }
}