public class Infect : Action {

    public Infect() {
        this.name = "Infect";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage. Apply "+magicNumber+" Poison if not blocked.";
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
        // Deal damage to the target.
        Attack atk = new Attack((int)(properties.damage*properties.damageMult), this.owner!, target, properties.hitsAbove, properties.hitsBelow);
        int finalDamage = Battlefield.performAttack(atk);
        // Apply poison based on final damage:
        if (finalDamage > 0)
        {
            Console.WriteLine(this.owner.name + " infects " + target.name + " with Poison(" + magicNumber + ")!");
            target.AddStatusEffect(new Poison(magicNumber, target));
        }    
        return true;
    }
    
    
}