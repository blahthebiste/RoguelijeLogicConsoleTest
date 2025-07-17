public class Vanish : Action
{

    public Vanish()
    {
        this.name = "Vanish";
        this.description = "Dodge all attacks this turn.";
        this.actionType = ActionType.DEFEND;
        this.magicNumber = 1;
        this.targetting = TargetCategory.SELF;
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier)
    {
        // Apply the Dodge status effect
        target!.AddStatusEffect(new Vanished(magicNumber, target));
        return true;
    }
}