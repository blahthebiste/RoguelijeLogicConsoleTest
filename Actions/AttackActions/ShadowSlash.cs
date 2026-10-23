public class ShadowSlash : Action {

    public ShadowSlash() {
        this.name = "Shadow Slash";
        this.actionType = ActionType.ATTACK;
        this.damage = 3;
        this.magicNumber = 5;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage. Apply "+magicNumber+" Curse.";
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
        Battlefield.performAttack(atk);
        // Apply curse
        Console.WriteLine(this.owner.name + " applies " +magicNumber+ " Curse to " + target.name + "!");
        target.AddStatusEffect(new Curse(magicNumber, target)); 
        return true;
    }
    
    
}