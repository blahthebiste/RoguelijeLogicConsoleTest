public class Environment : Action
{

    public Environment()
    {
        this.name = "Environment";
        this.description = "Part of the environment. No HP, and cannot be targeted.";
        this.actionType = ActionType.PASSIVE;
    }

    // All actual impact of this passive is hard coded elsewhere in the code.
    // 1. ConsoleGameLogicTest -> do not display HP for Environment enemies
    // 2. ConsoleGameLogicTest -> enviroments cannot be targeted by actions
    // 3. Entity -> Environments are always considered dead
    // 4. Battlefield -> do not count Environment enemies towards the enemy total; players win if only environments are left
    // 5. Battlefield -> do not kill Environment enemies regardless of their HP

    public override void startOfCombat()
    { // Tell the Battlefield tracker that I am an environment
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        Battlefield.numEnvironmentEntities += 1;
        this.owner.isEnvironment = true;
    }
}