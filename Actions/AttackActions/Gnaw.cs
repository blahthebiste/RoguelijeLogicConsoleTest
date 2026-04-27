public class Gnaw : Action {

    public Gnaw() {
        this.name = "Gnaw";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage. Apply "+magicNumber+" Bleed if not blocked.";
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
        // Apply bleed:
        if (finalDamage > 0)
        {
            Console.WriteLine(this.owner.name + " inflicts Bleed onto " + target.name +"!");
            target.AddStatusEffect(new Bleed(magicNumber, target));
        }    
        return true;
    }
}