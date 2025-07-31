public class Infect : Action {

    public Infect() {
        this.name = "Infect";
        this.description = "Deal 3 damage. Apply Poison equal to unblocked damage.";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
        this.targetting = TargetCategory.SINGLE_ENEMY;
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
        // Apply poison based on final damage:
        if (finalDamage > 0)
        {
            Console.WriteLine(this.owner.name + " infects " + target.name + " with Poison(" + finalDamage + ")!");
            target.AddStatusEffect(new Poison(finalDamage, target));
        }    
        return true;
    }
}