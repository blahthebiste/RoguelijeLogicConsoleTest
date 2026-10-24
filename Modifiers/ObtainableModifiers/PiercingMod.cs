public class PiercingMod : Modifier {

    public PiercingMod()
    {
        this.name = "Piercing";
        this.description = "Pierces through Block and Toughness.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.ignoresBlock = true;
        prop.ignoresToughness = true;
    }
}