public class Dive : Action {

    public Dive() {
        this.name = "Dive";
        this.actionType = ActionType.ATTACK;
        this.damage = 5;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "Deal "+damage+" damage to ALL enemies. Stop flying.";
    }

    // Can only be used while flying.
    public override bool canUse(Entity? target)
    {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (!this.owner.HasStatusEffect("Flying"))
        {
            Console.WriteLine(this.owner.name + " needs to be flying to use "+this.name+"!");
            return false;
        }
        return base.canUse(target);
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack(properties.damage, this.owner!, target, properties.hitsAbove, properties.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }

    public override bool useOnce(){
        if (owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        return owner.RemoveStatusEffectByName("Flying");
    }
}