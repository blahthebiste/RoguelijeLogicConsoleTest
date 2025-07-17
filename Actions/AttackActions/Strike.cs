public class Strike : Action {

    public Strike() {
        this.name = "Strike";
        this.description = "Deal 6 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(damage, this.owner!, target, this.hitsAbove, this.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }
}