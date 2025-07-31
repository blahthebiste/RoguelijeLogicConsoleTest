public class Leap : MovementCard
{

    public Leap()
    {
        this.name = this.originalName = "Leap";
        this.description = this.originalDescription = "Move to the top position (free action).";
        this.actionType = ActionType.MOVEMENT;
        this.owner = null;
        this.modifier = null;
    }

    public override bool executeAction(PlayerCharacter hero)
    {
        Console.WriteLine(hero.name + " Leaps to the top!");
        Battlefield.PlayerSide.Remove(hero);
        Battlefield.PlayerSide.Insert(0, hero);
        return true;
    }

}