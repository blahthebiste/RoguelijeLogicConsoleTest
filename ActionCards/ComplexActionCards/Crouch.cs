public class Crouch : MovementCard
{

    public Crouch()
    {
        this.name = this.originalName = "Crouch";
        this.description = this.originalDescription = "Move to the bottom position (free action).";
        this.actionType = ActionType.MOVEMENT;
        this.owner = null;
        this.modifier = null;
    }

    public override bool executeAction(PlayerCharacter hero)
    {
        Console.WriteLine(hero.name + " Crouches to the bottom!");
        Battlefield.PlayerSide.Remove(hero);
        Battlefield.PlayerSide.Insert(Battlefield.PlayerSide.Count, hero);
        return true;
    }

}