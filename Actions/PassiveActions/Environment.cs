public class Inanimate : Action
{

    public Inanimate()
    {
        this.name = "Inanimate";
        this.description = "Part of the environment. No HP, and cannot be targeted.";
        this.actionType = ActionType.PASSIVE;
    }

    // All actual impact of this passive is hard coded elsewhere in the code.
    // 1. ConsoleGameLogicTest -> do not display HP for Inanimate enemies
    // 2. ConsoleGameLogicTest -> inanimate entities cannot be targeted by actions
    // 3. Entity -> Inanimate entities are always considered dead
    // 4. Battlefield -> do not count Inanimate enemies towards the enemy total; players win if only Inanimate enemies are left
    // 5. Battlefield -> do not kill Inanimate enemies regardless of their HP

    public override void startOfCombat()
    { // Tell the Battlefield tracker that I am Inanimate
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        Battlefield.numInanimateEntities += 1;
        this.owner.isInanimate = true;
    }
}