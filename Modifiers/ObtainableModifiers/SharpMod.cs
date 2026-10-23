public class SharpMod : Modifier {

    public SharpMod()
    {
        this.name = "Sharp";
        this.description = "Increases the damage done by the action by 2.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damage += 2;
    }
}