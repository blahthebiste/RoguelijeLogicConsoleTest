public class Whack : Action {

    public Whack() {
        this.name = "Whack";
        this.description = "Deal 3 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Deal damage to the target.
        Attack atk = new Attack(damage, this.owner!, target!, this.hitsAbove, this.hitsBelow);
        atk = this.owner!.onAttack(atk);
        target!.onReceiveAttack(atk);
        return true;
    }
}