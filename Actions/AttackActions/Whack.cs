public class Whack : Action {

    public Whack() {
        this.name = "Whack";
        this.description = "Deal 3 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override bool use(Entity? target, Modifier? modifier) {
        if(target == null) {
            Console.WriteLine("Must target an enemy");
            return false;
        }
        else {
            // Deal damage to the target.
            target.recieveAttack(damage);
            return base.use(target, modifier);
        }
    }
}