public class SplittingMod : Modifier {

    public SplittingMod()
    {
        this.name = "Splitting";
        this.description = "Also hits adjacent targets.";
    }

    public override void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
        prop.hitsAbove = true;
        prop.hitsBelow = true;
    }
}