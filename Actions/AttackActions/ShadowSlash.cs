public class ShadowSlash : Action {

    public ShadowSlash() {
        this.name = "Shadow Slash";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
        this.magicNumber = 5;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage. Apply "+magicNumber+" Curse.";
    }

    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        int power = damage;
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
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
        // Apply curse
        Console.WriteLine(this.owner.name + " applies " +magicNumber+ " Curse to " + target.name + "!");
        target.AddStatusEffect(new Curse(magicNumber, target)); 
        return true;
    }
    
    // Decrement buffs after dealing all damage
    public override bool useOnce(Modifier? modifier){
        if (owner!.HasStatusEffect("Deadly Aim"))
        {
            owner!.GetStatusEffect("Deadly Aim")!.Decrease(1);
        }
        return true;
    }
}