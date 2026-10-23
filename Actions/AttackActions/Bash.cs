public class Bash : Action {

    public Bash() {
        this.name = "Bash";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
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