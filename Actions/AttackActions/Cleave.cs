public class Cleave : Action {

    public Cleave() {
        this.name = "Cleave";
        this.description = "Deal 4 damage to an enemy and adjacent enemies.";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.hitsAbove = true;
        this.hitsBelow = true;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        // Deal damage to the target.
        Attack atk = new Attack(damage, this.owner!, target!, this.hitsAbove, this.hitsBelow);
        atk = this.owner!.onAttack(atk);
        target!.onReceiveAttack(atk);
        return true;
    }
}