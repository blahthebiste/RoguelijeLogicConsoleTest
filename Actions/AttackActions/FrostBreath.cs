public class FrostBreath : Action {

    public FrostBreath() {
        this.name = "Frost Breath";
        this.actionType = ActionType.ATTACK;
        this.damage = 6;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.hitsAbove = true;
        this.hitsBelow = true;
        this.description = "Deal "+damage+" damage to an enemy and adjacent enemies. Apply Frost equal to unblocked damage.";
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
        // Deal damage to the target.
        Attack atk = new Attack(damage + (modifier == null? 0 : modifier.damageMod), this.owner!, target, this.hitsAbove, this.hitsBelow);
        int finalDamage = Battlefield.performAttack(atk);
        // Apply frost based on final damage:
        if (finalDamage > 0)
        {
            Console.WriteLine(this.owner.name + "'s Frost Breath chills " + target.name + " to the bone -- Frost(" + finalDamage + ")!");
            target.AddStatusEffect(new Frost(finalDamage, target));
        }    
        return true;
    }
}