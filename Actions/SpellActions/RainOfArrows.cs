public class RainOfArrows : Action {

    public RainOfArrows() {
        this.name = "Rain Of Arrows";
        this.actionType = ActionType.SPELL;
        this.targetting = TargetCategory.ALL_ENEMIES;
        this.damage = 3;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.description = "Deal "+damage+" damage to ALL enemies.";
    }

    public override bool useOnTarget(Entity? target) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Deal damage to the target.
        Attack atk = new Attack((int)(properties.damage*properties.damageMult), this.owner!, target, properties.hitsAbove, properties.hitsBelow);
        // Not considered a direct attack, so do not trigger onAttack?
        target!.onReceiveAttack(atk);
        return true;
    }

    
}