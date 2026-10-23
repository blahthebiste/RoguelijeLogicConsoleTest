public class RainOfArrows : Action {

    public RainOfArrows() {
        this.name = "Rain Of Arrows";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.damage = 3;
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