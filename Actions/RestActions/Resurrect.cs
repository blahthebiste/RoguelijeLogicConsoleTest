public class Resurrect : Action {

    public Resurrect() {
        this.name = "Resurrect";
        this.actionType = ActionType.REST;
        this.targetting = TargetCategory.DEAD_ALLY;
        this.description = "Resurrect an ally to full HP.";
    }


    public override bool useOnTarget(Entity? target, Modifier? modifier) {
        if (target == null) {
            Console.WriteLine("ERROR: null target for action '"+this+"'.");
            return false;
        }
        // Bring them back into the fight
        return Battlefield.ReviveHero(target.name, true, true);
    }
}