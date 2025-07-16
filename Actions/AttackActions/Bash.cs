public class Bash : Action {

    public Bash() {
        this.name = "Bash";
        this.description = "Deal 4 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
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