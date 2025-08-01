public class Connive : Action {

    public Connive() {
        this.name = "Connive";
        this.description = "Use another random action.";
        this.actionType = ActionType.SKILL;
        this.targetting = TargetCategory.NONE;
    }

    public override bool useOnce(Modifier? modifier) {
        if (this.owner == null) {
            Console.WriteLine("ERROR: null owner for action '"+this+"'.");
            return false;
        }
        Dictionary<Action, Entity?> possibleActions = new Dictionary<Action, Entity?>();
        Entity? chosenTarget = null;
        // Filter out actions that cannot be used:
        foreach (Action act in this.owner.ActionListMinusPassives.ToList())
        {
            // Some actions do not require a target, but still need to be checked for usability:
            if (!act.requiresTarget() && act.canUse(null, null))
            {
                possibleActions.Add(act, null);
                continue;
            }
            // For actions that do require a target, validate that we can find a valid target:
            if (!this.owner.playerControlled)
            {
                chosenTarget = this.owner.chooseNextTarget(act);
                if (chosenTarget != null)
                {
                    possibleActions.Add(act, chosenTarget);
                    continue;
                }
            }
            else
            { // Player chooses target anyway.
                possibleActions.Add(act, null);
                continue;
            }            
            // That action could not find a valid target. Move onto the next.
        }
        if (possibleActions.Count < 1) {
            Console.WriteLine("Connive could not find a valid action to use.");
            return false;
        }
        // Pick a random one:
        List<Action> keyList = new List<Action>(possibleActions.Keys);
        CurrentRun.Shuffle(keyList);
        Action chosenAction = keyList[0];
        Console.WriteLine(this.owner.name+" chose "+chosenAction.name+" with Connive.");
        if (!this.owner.playerControlled)
        {
            chosenAction.use(possibleActions[keyList[0]], modifier);
        }
        else
        {
            chosenAction.promptUse();
        }
        return true;
    }
}