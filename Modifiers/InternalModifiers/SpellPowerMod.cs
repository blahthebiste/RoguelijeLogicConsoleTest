public class SpellPowerMod : Modifier {

    public int amount;

    public SpellPowerMod(int amount)
    {
        this.name = "SpellPowerMod";
        this.description = "Used by the Spell Power status effect.";
        this.amount = amount;
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damage += amount;
        prop.block += amount;
        prop.healing += amount;
    }

}