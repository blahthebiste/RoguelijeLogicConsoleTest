public class Hold : Action {

    public Hold() {
        this.name = "Hold";
        this.actionType = ActionType.SPELL;
        this.magicNumber = 2;
        this.magicNumber2 = 2;
        this.hasLimitedUses = true;
        this.uses = 3;
        this.maxUses = this.uses;
        this.targetting = TargetCategory.SELF;
        this.description = "Gain "+magicNumber+" Toughness for "+magicNumber2+" turns.";
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