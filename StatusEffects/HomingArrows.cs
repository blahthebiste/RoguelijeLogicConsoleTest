public class HomingArrows : StatusEffect {

    public List<string> AffectedList = Action.SkillsWithDamage;
    public HomingArrows(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Homing Arrows";
        this.description = "Next X shots cannot be dodged or intercepted.";
        this.owner = owner;
    }

    // Ignore taunt with all actions
    public override ActionProperties ModifyActionProperties(ActionProperties prop)
	{
        prop.ignoresTaunt = true;
		return prop;
	}

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new HomingMod());
            Console.WriteLine("This shot gains homing!");
            this.Decrease(1); // Wears down by 1
        }
        return actionBeingUsed;
    }
}