public class ChargedMod : Modifier {

    public int amount;

    public ChargedMod(int amount)
    {
        this.name = "ChargedMod";
        this.description = "Used by the Charged status effect.";
        this.amount = amount;
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        Console.WriteLine("[charged] Original damage: "+prop.damage+"; amount="+this.amount);
        prop.damage += amount;
        prop.block += amount;
        prop.healing += amount;
        Console.WriteLine("[charged] New damage: "+prop.damage);
    }

}