public class Strike : Action {

    public Strike() {
        this.name = "Strike";
        this.description = "Deal 6 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Deal damage to the target.
        Attack atk = new Attack(damage, this.owner!, target!, this.hitsAbove, this.hitsBelow);
        atk = owner!.onAttack(atk);
        target!.onReceiveAttack(atk);
        return true;
    }
}