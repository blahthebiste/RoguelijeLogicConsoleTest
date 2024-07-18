public class Strike : Action {

    public Strike() {
        this.name = "Strike";
        this.description = "Deal 6 damage.";
        this.actionType = ActionType.Attack;
        this.damage = 6;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity source, Entity? target, Modifier? modifier) {
        if(target == null) {
            Console.WriteLine("Must target an enemy");
        }
        else {
            // Deal damage to the target.
            target.recieveAttack(damage);
        }
    }
}