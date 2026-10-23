public class Snipe : Action {

    public Snipe()
    {
        this.name = "Snipe";
        this.actionType = ActionType.SKILL;
        this.damage = 10;
        this.hasLimitedUses = true;
        this.uses = 4;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.ignoresTaunt = true;
        this.description = "Deal "+this.damage+" damage. Ignores Taunt.";
    }


    public override bool useOnTarget(Entity? target) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '" + this.name + "'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack((int)(properties.damage*properties.damageMult), this.owner, target, properties.hitsAbove, properties.hitsBelow);
        // Not considered a direct attack, so do not trigger onAttack?
        target!.onReceiveAttack(atk);
        return true;
    }
    
    
}