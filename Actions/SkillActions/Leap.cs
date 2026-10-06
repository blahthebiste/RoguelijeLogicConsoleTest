public class Leap : Action {

    public Leap() {
        this.name = "Leap";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
        this.description = "Move to the top position (free action).";
        this.freeAction = true;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        Console.WriteLine(owner.name + " Leaps to the top!");
        if(owner.hostile)
        {
            Battlefield.EnemySide.Remove(owner);
            Battlefield.EnemySide.Insert(0, owner);
        }
        else
        {
            Battlefield.PlayerSide.Remove(owner);
            Battlefield.PlayerSide.Insert(0, owner);
        }
        return true;
    }
}