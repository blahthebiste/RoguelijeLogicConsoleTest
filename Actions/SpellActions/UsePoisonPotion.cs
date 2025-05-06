public class UsePoisonPotion : Action {

    public UsePoisonPotion() {
        this.name = "Use Poison Potion";
        this.description = "Apply 2 Poison.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Not affected by spell power or charged.
        // Apply the poison to the target.
        target!.AddStatusEffect(new Poison(magicNumber, target));
        return true;
    }
}