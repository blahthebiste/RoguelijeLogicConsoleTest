public class Strafe : MovementCard
{

    public Strafe()
    {
        this.name = this.originalName = "Strafe";
        this.description = this.originalDescription = "Move up or down 1 space (free action).";
        this.actionType = ActionType.MOVEMENT;
        this.owner = null;
        this.modifier = null;
    }

    public override bool executeAction(PlayerCharacter hero)
    {
        bool canMoveUp = true;
        bool canMoveDown = true;
        // First check if the user can move up or down.
        int originalIndex = Battlefield.PlayerSide.IndexOf(hero);
        if(originalIndex == 0)
        { // Already at the top; cannot move up.
            canMoveUp = false;
        }
        if(originalIndex == Battlefield.PlayerSide.Count - 1)
        { // Already at the bottom; cannot move down.
            canMoveDown = false;
        }
        // In the case where the hero cannot move up or down, fail to use the action:
        if(!canMoveUp && !canMoveDown)
        {
            Console.WriteLine("Cannot use Strafe: nowehere to move.");
            return false;
        }
        // In the case where the hero can ONLY move up, move them up:
        if(canMoveUp && !canMoveDown)
        {
            Console.WriteLine(hero.name + " Strafes up!");
            Battlefield.PlayerSide.Remove(hero);
            Battlefield.PlayerSide.Insert(originalIndex - 1, hero);
            return true;     
        }
        // In the case where the hero can ONLY move down, move them down:
        if(!canMoveUp && canMoveDown)
        {
            Console.WriteLine(hero.name + " Strafes down!");
            Battlefield.PlayerSide.Remove(hero);
            Battlefield.PlayerSide.Insert(originalIndex + 1, hero);
            return true;   
        }
        // In the case where the hero can move either up OR down, let them choose:
        if(canMoveUp && canMoveDown)
        {
            while (true)
            {
                Console.WriteLine("Strafe up or down? (or 'back' to cancel)");
                Console.WriteLine("");
                Console.Write("\n> ");
                string? cmd = Console.ReadLine();
                if (cmd == null) continue;
                if (cmd.ToLower().Trim() == "exit" || cmd.ToLower().Trim() == "quit" || cmd.ToLower().Trim() == "back" || cmd.ToLower().Trim() == "cancel")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Cancelling Strafe.");
                    return false;
                }
                if (cmd.ToLower().Trim() == "up" || cmd.ToLower().Trim() == "u")
                {                    
                    Console.WriteLine(hero.name + " Strafes up!");
                    Battlefield.PlayerSide.Remove(hero);
                    Battlefield.PlayerSide.Insert(originalIndex - 1, hero);
                    return true;                    
                }
                if (cmd.ToLower().Trim() == "down" || cmd.ToLower().Trim() == "d")
                {
                    Console.WriteLine(hero.name + " Strafes down!");
                    Battlefield.PlayerSide.Remove(hero);
                    Battlefield.PlayerSide.Insert(originalIndex + 1, hero);
                    return true;
                }
            }
        }
        else
        {
            return false;
        }
    }

}