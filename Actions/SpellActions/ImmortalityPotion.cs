public class ImmortalityPotion : Action {

    public ImmortalityPotion() {
        this.name = "Immortality Potion";
        this.description = "Recover full HP. Can only be used if below half HP.";
        this.actionType = ActionType.SPELL;
        this.hasLimitedUses = true;
        this.uses = 1;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool canUse(Entity? target, Modifier? modifier)
    {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (this.owner.currentHP >= 0.5 * this.owner.maxHP)
        {
            Console.WriteLine(this.owner.name + " cannot use "+this.name+", HP is not below half!");
            return false;
        }
        return base.canUse(target, modifier);
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Restore HP.
        this.owner.ReceiveHealing(this.owner.maxHP);
        return true;
    }
}