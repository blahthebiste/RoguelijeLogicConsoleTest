public class SplittingArrows : StatusEffect {

    public List<string> AffectedList = Action.SkillsWithDamage;
    public SplittingArrows(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Splitting Arrows";
        this.description = "Next X shots also hit adjacent targets.";
        this.owner = owner;
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new SplittingMod());
            Console.WriteLine("This shot gains splitting!");
            this.Decrease(1); // Wears down by 1
        }
        return actionBeingUsed;
    }

}