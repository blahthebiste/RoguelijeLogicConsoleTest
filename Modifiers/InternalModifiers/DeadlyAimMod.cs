public class DeadlyAimMod : Modifier {

    public DeadlyAimMod()
    {
        this.name = "DeadlyAimMod";
        this.description = "Used by the DeadlyAim status effect.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.damageMult *= 2;
    }

}