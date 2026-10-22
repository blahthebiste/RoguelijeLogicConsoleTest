public class StrengthMod : Modifier {

    int amount;
    public StrengthMod(int amount)
    {
        this.name = "StrengthMod";
        this.description = "Used by the Strength status effect.";
        this.amount = amount;
    }


    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damage += amount;
        prop.block += amount;
    }
}