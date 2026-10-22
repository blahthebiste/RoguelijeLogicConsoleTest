public class ChargedMod : Modifier {

    public int amount;

    public ChargedMod(int amount)
    {
        this.name = "ChargedMod";
        this.description = "Used by the Charged status effect.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damage += amount;
        prop.block += amount;
        prop.healing += amount;
    }

}