public class Zap : Action {

    public Zap() {
        this.name = "Zap";
        this.actionType = ActionType.SPELL;
        this.damage = 9;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage.";
    }

    public override bool useOnTarget(Entity? target) {
        // Deal damage to the target.
        Attack atk = new Attack(properties.damage, this.owner!, target!);
        target!.onReceiveAttack(atk);
        return true;
    }
}