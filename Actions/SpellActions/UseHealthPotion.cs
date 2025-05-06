public class UseHealthPotion : Action {

    public UseHealthPotion() {
        this.name = "Use Health Potion";
        this.description = "Recover full HP.";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Restore HP.
        target!.ReceiveHealing(target.maxHP);
        return true;
    }
}