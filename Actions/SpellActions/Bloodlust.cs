public class Bloodlust : Action {

    public Bloodlust() {
        this.name = "Bloodlust";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 4; // Strength
        this.magicNumber2 = 2; // Bleed
        this.hasLimitedUses = true;
        this.uses = 2;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Gain "+magicNumber+" Strength and "+magicNumber2+" Bleed. Wears off when you rest.";
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply Strength status effect
        this.owner.AddStatusEffect(new Strength(magicNumber, this.owner));
        // Apply Bleed status effect
        this.owner.AddStatusEffect(new Bleed(magicNumber2, this.owner));
        return true;
    }

    // Remove effects when resting
    public override Action onUseAction(Action actionBeingUsed) {
        if(owner != null && actionBeingUsed.actionType == ActionType.REST) {
            // Remove Strength status effect
            this.owner.AddStatusEffect(new Strength(-magicNumber, this.owner));
            // Bleed status effect removes itself when resting anyway.
        }
        return actionBeingUsed;
    }
}