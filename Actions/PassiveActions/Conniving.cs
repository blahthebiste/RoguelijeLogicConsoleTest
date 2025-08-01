public class Conniving : Action {

    SubtlePoison subtlepoisonInstance;
    public Conniving()
    {
        this.name = "Conniving";
        this.description = "Uses Subtle Poison before turn 1.";
        this.actionType = ActionType.PASSIVE;
        subtlepoisonInstance = new SubtlePoison();
    }

    public override void startOfCombat()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        subtlepoisonInstance.owner = this.owner;
        // Select target for subtle poison:
        if (this.owner.playerControlled)
        {
            // Let the player choose their target:
            subtlepoisonInstance.promptUse();
        }
        else
        {
            // Enemy selects a valid random target:
            Entity? target = this.owner.chooseNextTarget(subtlepoisonInstance);
            if (target == null)
            {
                Console.WriteLine("ERROR: " + this.name + " has null target!");
                return;
            }
            subtlepoisonInstance.use(target!, null);
        }
    }
}