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
        int originalIndex = Battlefield.PlayerSide.IndexOf(hero);
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
            if (cmd.ToLower().Trim() == "up")
            {
                if (originalIndex == 0)
                {
                    Console.WriteLine(hero.name + " cannot Strafe up, they are already at the top!");
                }
                else
                {
                    Console.WriteLine(hero.name + " Strafes up!");
                    Battlefield.PlayerSide.Remove(hero);
                    Battlefield.PlayerSide.Insert(originalIndex - 1, hero);
                    return true;
                }
            }
            if (cmd.ToLower().Trim() == "down")
            {
                if (originalIndex + 1 >= Battlefield.PlayerSide.Count)
                {
                    Console.WriteLine(hero.name + " cannot Strafe down, they are already at the bottom!");
                }
                else
                {
                    Console.WriteLine(hero.name + " Strafes down!");
                    Battlefield.PlayerSide.Remove(hero);
                    Battlefield.PlayerSide.Insert(originalIndex + 1, hero);
                    return true;
                }
            }
        }
    }

}