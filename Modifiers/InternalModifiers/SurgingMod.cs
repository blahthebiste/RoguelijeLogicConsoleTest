public class SurgingMod : Modifier {

    public SurgingMod()
    {
        this.name = "SurgingMod";
        this.description = "Used by the Surging status effect.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damage *= 2;
    }

}