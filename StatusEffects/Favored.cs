public class Favored : StatusEffect {

    public Favored(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Favored";
        this.description = "Next X attacks cannot be blocked, dodged, or intercepted.";
        this.owner = owner;
    }

    // Ignore taunt with all actions
    public override ActionProperties ModifyActionProperties(ActionProperties prop)
	{
        prop.ignoresTaunt = true;
		return prop;
	}


    public override Attack onAttack(Attack atk) {
        atk.ignoresBlock = true;
        atk.ignoresDodge = true;
        this.Decrease(1); // Wears down by 1
        return base.onAttack(atk);
    }

}