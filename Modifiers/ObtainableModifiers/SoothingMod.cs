public class SoothingMod : Modifier {

    public SoothingMod()
    {
        this.name = "Soothing";
        this.description = "Increases the healing of the action by 2.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.healing += 2;
    }
}