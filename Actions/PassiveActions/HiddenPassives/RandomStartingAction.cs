public class RandomStartingAction : Action {

    public RandomStartingAction() {
        this.name = "RandomStartingAction";
        this.actionType = ActionType.PASSIVE;
        this.description = "This entity chooses its first action randomly.";
        this.hiddenAction = true;
    }

    public override void startOfCombat()
    {
        if(this.owner == null) {
            Console.WriteLine("ERROR: "+this.name+" has null owner!");
            return;
        }
        int numPossibleActions = 0;
        Entity? chosenTarget = null;
        // Filter out actions that cannot be used:
        foreach (Action act in this.owner.ActionListMinusPassives.ToList())
        {
            // Some actions do not require a target, but still need to be checked for usability:
            if (!act.requiresTarget() && act.canUse(null))
            {
                numPossibleActions++;
                continue;
            }
            // For actions that do require a target, validate that we can find a valid target:
            if (!this.owner.playerControlled)
            {
                chosenTarget = this.owner.chooseNextTarget(act);
                if (chosenTarget != null)
                {
                    numPossibleActions++;
                    continue;
                }
            }
            else
            { // Player chooses target anyway.
                numPossibleActions++;
                continue;
            }            
            // That action could not find a valid target. Move onto the next.
        }
        if (numPossibleActions < 1) {
            Console.WriteLine("RandomStartingAction could not find a valid action to use.");
            return;
        }
        // Pick a random one:
        owner.nextActionIndex = CurrentRun.rng.Next(0,numPossibleActions);
    }
}