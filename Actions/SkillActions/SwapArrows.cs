public class SwapArrows : Action {

    public SwapArrows() {
        this.name = "SwapArrows";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.SELF;
        this.magicNumber = 1;
        this.freeAction = true;
        this.description = "Free Action. Change your next shot to Piercing, Homing, or Splitting.";
    }

    public override bool useOnce() {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        PiercingArrows pArrows = new PiercingArrows(magicNumber, owner);
        HomingArrows hArrows = new HomingArrows(magicNumber, owner);
        SplittingArrows sArrows = new SplittingArrows(magicNumber, owner);
        // Prompt player to choose a type
        Console.WriteLine("Which arrow type do you want to swap to?\n");
        Console.WriteLine("\t[1] "+pArrows.ToString());
        Console.WriteLine("\t[2] "+hArrows.ToString());
        Console.WriteLine("\t[3] "+sArrows.ToString());
        Console.WriteLine("\n(enter anything else to go back)\n");
        Console.Write("\n> ");
        string? cmd = Console.ReadLine();
        if (cmd == null) {
            Console.WriteLine("Cancelling action.");
            return false;
        }
        if (int.TryParse(cmd.ToLower().Trim(), out int selection))
        {
            if(selection < 1 || selection > 3)
            {
                Console.WriteLine("Cancelling action.");
                return false;
            }
        }
        else
        {
            Console.WriteLine("Cancelling action.");
            return false;
        }
        // At this point they have entered a valid number from 1-3.
        // Remove other types:
        owner.RemoveStatusEffectByName("Piercing Arrows");
        owner.RemoveStatusEffectByName("Homing Arrows");
        owner.RemoveStatusEffectByName("Splitting Arrows");
        // Apply chosen arrow type:
        switch(selection)
        {
            case 1:
                this.owner.AddStatusEffect(pArrows);
                break;
            case 2:
                this.owner.AddStatusEffect(hArrows);
                break;
            case 3:
                this.owner.AddStatusEffect(sArrows);
                break;
            default:
                Console.WriteLine("Cancelling action.");
                return false;
        }
        return true;
    }
}