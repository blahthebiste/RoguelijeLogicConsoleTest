public class Duel : Action {

    public Duel() {
        this.name = "Duel";
        this.actionType = ActionType.ATTACK;
        this.damage = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+this.damage+" damage. Deals "+this.damage+" more if the target has no Block.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        int power = this.damage;
        // Check the battlefield for block:
        if ((target.hostile && Battlefield.enemyBlock == 0) || (!target.hostile && Battlefield.playerBlock == 0))
        {
            power *= 2;
        }
        // Deal damage to the target.
        Attack atk = new Attack(power + (modifier == null? 0 : modifier.damageMod), this.owner, target, this.hitsAbove, this.hitsBelow);
        Battlefield.performAttack(atk);    
        return true;
    }
}