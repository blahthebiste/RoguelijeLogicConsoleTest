public class Ravage : Action {

    public Ravage() {
        this.name = "Ravage";
        this.actionType = ActionType.ATTACK;
        this.damage = 4;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "Deal "+damage+" damage to ALL enemies.";
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