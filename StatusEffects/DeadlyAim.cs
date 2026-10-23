public class DeadlyAim : StatusEffect {

    public List<string> AffectedList = Action.SkillsWithDamage;

    public DeadlyAim(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Deadly Aim";
        this.description = "Next X skills deal double damage.";
        this.owner = owner;
    }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new DeadlyAimMod());
            Console.WriteLine("DEBUG: apply deadly aim bonus");
            this.Decrease(1); // Wears down by 1
        }
        return actionBeingUsed;
    }

}