public class FrostMod : Modifier {

    int amount;
    public FrostMod(int amount)
    {
        this.name = "FrostMod";
        this.description = "Used by the Frost status effect.";
        this.amount = amount;
    }

    // Frost saps both attack damage and block gain. Basically the inverse of strength.
    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damage -= amount;
        prop.block -= amount;
    }
}