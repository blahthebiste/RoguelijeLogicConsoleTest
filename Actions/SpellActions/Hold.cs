public class Hold : Action {

    public Hold() {
        this.name = "Hold";
        this.description = "Gain 2 Toughness for 2 turns.";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        // Apply toughness status effect
        Toughness tuff = new Toughness(magicNumber, this.owner);
        this.owner.AddStatusEffect(tuff);
        // Make it temporary; 1 delay = wears off after 2 turns
        this.owner.AddStatusEffect(new WearsOff(magicNumber, this.owner, tuff.name, 1));
        return true;
    }
}