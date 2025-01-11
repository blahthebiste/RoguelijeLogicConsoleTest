public class Swipe : Action {

    public Swipe() {
        this.name = "Swipe";
        this.description = "Deal 2 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 2;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    // For now, nothing special.
    public override bool canUse(Entity? target, Modifier? modifier) {
        return base.canUse(target, modifier);
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(base.use(target, modifier)) {
            // Deal damage to the target.
            Attack atk = new Attack(damage, this.owner!, target!);
            atk = this.owner!.onAttack(atk);
            target!.onReceiveAttack(atk);
            return true;
        }
        return false;
    }
}