public class IntimidatedMod : Modifier {

    int amount;
    public IntimidatedMod(int amount)
    {
        this.name = "IntimidatedMod";
        this.description = "Used by the Intimidated status effect.";
        this.amount = amount;
    }

    // Intimidated saps both attack damage and block gain. Basically the inverse of strength.
    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damage -= amount;
        prop.block -= amount;
        Console.WriteLine("New damage: "+prop.damage);
    }
}