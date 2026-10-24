public class Lightning : Action {

    public Lightning() {
        this.name = "Lightning";
        this.actionType = ActionType.SPELL;
        this.damage = 9;
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SINGLE_ENEMY;
        this.description = "Deal "+damage+" damage. Excess damage chains to the next enemy.";
    }

    public override bool useOnTarget(Entity? target) {
        if(owner == null)
        {
            Console.WriteLine("ERROR: null owner for action '"+name+"'.");
            return false;
        }
        // Apply the overkill effect to the target:
        target!.AddStatusEffect(new ChainLightning(owner, target));
        // Deal damage to the target.
        Attack atk = new Attack(properties, this.owner!, target);
        //atk = owner.onAttack(atk); // Don't trigger onAttack for the owner, since it is a spell?
        target!.onReceiveAttack(atk);
        return true;
    }

}