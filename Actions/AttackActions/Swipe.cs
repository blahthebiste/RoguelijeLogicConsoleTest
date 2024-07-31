public class Swipe : Action {

    public Swipe() {
        this.name = "Swipe";
        this.description = "Deal 2 damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 2;
        this.targetting = TargetCategory.SINGLE_ENEMY;
    }

    // For now, nothing special.
    public override bool canUse() {
        return base.canUse();
    }

    public override void use(Entity? target, Modifier? modifier) {
        if(target == null) {
            Console.WriteLine("Must target an enemy");
        }
        else {
            // Deal damage to the target.
            target.recieveAttack(damage);
        }
    }
}