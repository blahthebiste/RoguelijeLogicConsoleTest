public class Strike : Action {

    public Strike() {
        this.name = "Strike";
        this.actionType = ActionType.ATTACK;
        this.damage = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage.";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack((int)(properties.damage*properties.damageMult), this.owner!, target, properties.hitsAbove, properties.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }
    
    
}