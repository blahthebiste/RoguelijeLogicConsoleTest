public class Strength : StatusEffect {

    public List<string> AffectedList = Action.AttacksOrDefendsWithDamageOrBlock;

    public Strength(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Strength";
        this.description = "Increases attack damage and block.";
        this.owner = owner;
    }

    // Strength boosts attack damage
    // public override Attack onAttack(Attack atk) {
    //     atk.damage += this.amount;
    //     return atk;
    // }

    // ...and block gain.
    // public override int onGainBlock(int block) {
    //     return block + this.amount;
    // }

    // New version: only affect actions within the list. Modifies the action in place until it resolves.
    // Just add the proper modifier to the action's modifier list. It will be cleared automatically
    // after the action resolves.
    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new StrengthMod(this.amount));
            Console.WriteLine("DEBUG: apply str bonus");
        }
        return actionBeingUsed;
    }
}