public class Dive : Action {

    public Dive() {
        this.name = "Dive";
        this.actionType = ActionType.ATTACK;
        this.damage = 5;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.description = "Deal "+damage+" damage to ALL enemies. Stop flying.";
    }

    // Can only be used while flying.
    public override bool canUse(Entity? target, Modifier? modifier)
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
        return base.canUse(target, modifier);
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = damage;
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        if (owner!.HasStatusEffect("Deadly Aim"))
        {
            power *= 2;
        }
        // Deal damage to the target.
        Attack atk = new Attack(power + (modifier == null? 0 : modifier.damageMod), this.owner!, target, this.hitsAbove, this.hitsBelow);
        Battlefield.performAttack(atk);
        return true;
    }

    public override bool useOnce(Modifier? modifier){
        if (owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (owner!.HasStatusEffect("Deadly Aim"))
        {
            owner!.GetStatusEffect("Deadly Aim")!.Decrease(1);
        }
        return owner.RemoveStatusEffectByName("Flying");
    }
}