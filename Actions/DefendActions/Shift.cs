public class Shift : Action {

    public Shift() {
        this.name = "Shift";
        this.actionType = ActionType.DEFEND;
        this.block = 3;
        this.targetting = TargetCategory.SELF;
        this.description = "Move 1 space up or down. Generate "+block+" Block.";
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        if (this.owner is not PlayerCharacter) {
            Console.WriteLine("ERROR: enemies cannot use action '"+this+"'.");
            return false;
        }
        bool canMoveUp = true;
        bool canMoveDown = true;
        // First check if the user can move up or down.
        int originalIndex = Battlefield.PlayerSide.IndexOf(owner);
        if(originalIndex == 0)
        { // Already at the top; cannot move up.
            canMoveUp = false;
        }
        if(originalIndex == Battlefield.PlayerSide.Count - 1)
        { // Already at the bottom; cannot move down.
            canMoveDown = false;
        }
        // In the case where the owner cannot move up or down, fail to use the action:
        if(!canMoveUp && !canMoveDown)
        {
            Console.WriteLine("Cannot use Shift: nowehere to move.");
            return false;
        }
        // In the case where the owner can ONLY move up, move them up:
        if(canMoveUp && !canMoveDown)
        {
            Console.WriteLine(owner.name + " Shifts up!");
            Battlefield.PlayerSide.Remove(owner);
            Battlefield.PlayerSide.Insert(originalIndex - 1, owner);
            // Generate Block.
            Battlefield.addBlock(block + (modifier == null? 0 : modifier.blockMod), this.owner);
            return true;     
        }
        // In the case where the owner can ONLY move down, move them down:
        if(!canMoveUp && canMoveDown)
        {
            Console.WriteLine(owner.name + " Shifts down!");
            Battlefield.PlayerSide.Remove(owner);
            Battlefield.PlayerSide.Insert(originalIndex + 1, owner);
            // Generate Block.
            Battlefield.addBlock(block + (modifier == null? 0 : modifier.blockMod), this.owner);
            return true;   
        }
        // In the case where the owner can move either up OR down, let them choose:
        if(canMoveUp && canMoveDown)
        {
            while (true)
            {
                Console.WriteLine("Shift up or down? (or 'back' to cancel)");
                Console.WriteLine("");
                Console.Write("\n> ");
                string? cmd = Console.ReadLine();
                if (cmd == null) continue;
                if (cmd.ToLower().Trim() == "exit" || cmd.ToLower().Trim() == "quit" || cmd.ToLower().Trim() == "back" || cmd.ToLower().Trim() == "cancel")
                {
                    Console.WriteLine("");
                    Console.WriteLine("Cancelling Shift.");
                    return false;
                }
                if (cmd.ToLower().Trim() == "up" || cmd.ToLower().Trim() == "u")
                {                    
                    Console.WriteLine(owner.name + " Shifts up!");
                    Battlefield.PlayerSide.Remove(owner);
                    Battlefield.PlayerSide.Insert(originalIndex - 1, owner);
                    // Generate Block.
                    Battlefield.addBlock(block + (modifier == null? 0 : modifier.blockMod), this.owner);
                    return true;                    
                }
                if (cmd.ToLower().Trim() == "down" || cmd.ToLower().Trim() == "d")
                {
                    Console.WriteLine(owner.name + " Shifts down!");
                    Battlefield.PlayerSide.Remove(owner);
                    Battlefield.PlayerSide.Insert(originalIndex + 1, owner);
                    // Generate Block.
                    Battlefield.addBlock(block + (modifier == null? 0 : modifier.blockMod), this.owner);
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