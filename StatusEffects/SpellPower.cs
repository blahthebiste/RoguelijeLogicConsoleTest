public class SpellPower : StatusEffect {

    public List<string> AffectedList = Action.SpellsWithDamageBlockOrHealing;

    public SpellPower(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Spell Power";
        this.description = "Increases damage/block/healing of spells.";
        this.owner = owner;
    }

    public override Action onUseAction(Action actionBeingUsed)
    {
        if(AffectedList.Contains(actionBeingUsed.name))
        {
            actionBeingUsed.ApplyModifierToProperties(new SpellPowerMod(this.amount));
            Console.WriteLine("DEBUG: apply spell-power bonus");
        }
        return actionBeingUsed;
    }
}