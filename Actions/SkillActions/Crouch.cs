public class Crouch : Action {

    public Crouch() {
        this.name = "Crouch";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
        this.description = "Move to the bottom position (free action).";
        this.freeAction = true;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        Console.WriteLine(owner.name + " Crouches to the bottom!");
        if(owner.hostile)
        {
            Battlefield.EnemySide.Remove(owner);
            Battlefield.EnemySide.Insert(Battlefield.EnemySide.Count, owner);
        }
        else
        {
            Battlefield.PlayerSide.Remove(owner);
            Battlefield.PlayerSide.Insert(Battlefield.PlayerSide.Count, owner);
        }
        return true;
    }
}