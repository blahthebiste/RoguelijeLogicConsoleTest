public class Stab : Action {

    public Stab() {
        this.name = "Stab";
        this.description = "Deal 5 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 5;
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
            atk = owner!.onAttack(atk);
            target!.onReceiveAttack(atk);
            return true;
        }
        return false;
    }
}