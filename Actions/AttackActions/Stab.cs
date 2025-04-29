public class Stab : Action {

    public Stab() {
        this.name = "Stab";
        this.description = "Deal 5 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 5;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Deal damage to the target.
        Attack atk = new Attack(damage, this.owner!, target!);
        atk = owner!.onAttack(atk);
        target!.onReceiveAttack(atk);
        return true;
    }
}