public class HomingMod : Modifier {

    public HomingMod()
    {
        this.name = "Homing";
        this.description = "Cannot be evaded/redirected.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.ignoresTaunt = true;
        prop.ignoresDodge = true;
    }
}