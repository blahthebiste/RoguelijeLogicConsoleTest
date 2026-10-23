public class Cut : Action {

    public Cut() {
        this.name = "Cut";
        this.actionType = ActionType.ATTACK;
        this.damage = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+this.damage+" damage. Hits twice if the target has no Block.";
    }

    public override bool useOnTarget(Entity? target) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        bool hit_twice = false;
        // Check the battlefield for block:
        if ((target.hostile && Battlefield.enemyBlock == 0) || (!target.hostile && Battlefield.playerBlock == 0))
        {
            hit_twice = true;
        }
        // Deal damage to the target.
        Attack atk = new Attack((int)(properties.damage*properties.damageMult), this.owner, target, properties.hitsAbove, properties.hitsBelow);
        if(hit_twice)
        {
            // Second hit
            Battlefield.performAttack(atk);
        }
        return true;
    }
    
    
}