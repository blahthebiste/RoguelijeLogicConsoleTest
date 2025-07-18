public class Enemy : Entity {
    
    // Tracks what the enemy is about to do each turn.
    // For most enemies, just increments by 1 until it hits a usable action each turn.
    public int nextActionIndex = 0;
    
    public Entity? nextTarget; // The next entity that will be targeted. Can be null for actions that do not require a target
    
    public bool fleeing = false;
    

    // Default Constructor
    public Enemy()
    {
        playerControlled = false;
        hostile = true;
    }

    // Constructor from data
    public Enemy(string characterID)
    {
        EnemyData? data = DataRegistry.CharacterData.getEnemyDataByName(characterID);
        if (data == null)
        {
            Console.WriteLine("Could not generate enemy; ID not found.");
            return;
        }
        foreach (string actionName in data.ActionList)
        {
            Action? newAction = DataRegistry.ActionData.getActionByName(actionName);
            if (newAction == null)
            {
                Console.WriteLine("Could not generate enemy; action not found.");
                return;
            }
            ActionList.Add(newAction);
        }
        name = data.Name;
        description = data.Description;
        maxHP = data.HP;
        playerControlled = false;
        hostile = true;
        exhausted = false;
        currentHP = maxHP;
        this.assignActionOwnership();
    }

    // Prints out full details about the enemy
    public override string ToString()
    {
        string str = this.name;
        str += "\nHP: " + this.currentHP + "/" + this.maxHP;
        str = str + "\nActions:";
        for (int i = 0; i < this.ActionList.Count; i++)
        {
            Action action = ActionList[i];
            str += "\n\t";
            str += action.ToString();
        }
        return str;
    }

    // Selects the next action and target.
    public void prepareTurn()
    {
        this.exhausted = false;
        // Loop through actions until we find a usable one
        for (int failedActions = 0; failedActions < ActionListMinusPassives.Count; failedActions++)
        {
            // Some actions do not require a target, but still need to be checked for usability:
            if (!getNextAction().requiresTarget() && getNextAction().canUse(null, null))
            {
                setNextTarget(null);
                return;
            }
            // For actions that do require a target, validate that we can find a valid target:
            Entity? chosenTarget = chooseNextTarget(getNextAction());
            if (chosenTarget != null)
            {
                setNextTarget(chosenTarget);
                return;
            }
            // That action could not find a valid target. Move onto the next.
            nextActionIndex++;
            if (nextActionIndex >= ActionListMinusPassives.Count)
            {
                nextActionIndex = 0;
            }
        }
        // Every non-passive action was unable to find a valid target. SKip turn
    }

    // Enemy takes its turn. This involves using their action, and then selecting the next one.
    // Many enemies will override this
    public void takeTurn()
    {
        getNextAction().use(nextTarget, null); // Null modifier, enemies don't use cards
        nextActionIndex++;
        if (nextActionIndex >= ActionListMinusPassives.Count)
        {
            nextActionIndex = 0;
        }
    }

    // Figures out who the given action should target.
    // Selects a random target index from the appropriate side of combat.
    // If no valid target could be found, returns null.
    // If the entity has no actions, or their next action does not require a target, returns null.
    public Entity? chooseNextTarget(Action act)
    {
        // Get random order for targetting priority:
        List<int> targetPriorityList = new List<int>();
        switch (act.targetting)
        {
            case TargetCategory.SINGLE_ENEMY:
                for (int i = 0; i < Battlefield.PlayerSide.Count; i++)
                {
                    targetPriorityList.Add(i);
                }
                CurrentRun.Shuffle(targetPriorityList);
                foreach (int targetIndex in targetPriorityList)
                {
                    // Try each target in order.
                    if (act.canUse(Battlefield.PlayerSide[targetIndex], null))
                    {
                        Console.WriteLine("Action " + act + " can be used on " + Battlefield.PlayerSide[targetIndex].name + "!");
                        return Battlefield.PlayerSide[targetIndex];
                    }
                }
                Console.WriteLine("Action has no valid targets.");
                return null;
            case TargetCategory.SINGLE_ALLY:
                for (int i = 0; i < Battlefield.EnemySide.Count; i++)
                {
                    targetPriorityList.Add(i);
                }
                CurrentRun.Shuffle(targetPriorityList);
                foreach (int targetIndex in targetPriorityList)
                {
                    // Try each target in order.
                    if (act.canUse(Battlefield.EnemySide[targetIndex], null))
                    {
                        Console.WriteLine("Action " + act + " can be used on " + Battlefield.EnemySide[targetIndex] + "!");
                        return Battlefield.EnemySide[targetIndex];
                    }
                }
                Console.WriteLine("Action has no valid targets.");
                return null;
            default:
                Console.WriteLine("Next action does not use targeting.");
                return null;
        }
    }

    // Set the nextTarget object.
    // Can be null for actions that do not require a target.
    public void setNextTarget(Entity? target)
    {
        nextTarget = target;
    }


    // Returns the currently selected next target.
    public Entity? getNextTarget()
    {
        return nextTarget;
    }
    
    // Returns the name of the currently selected next target,
    // or 'None' for actions that do not have a target.
    public string getNextTargetName()
    {
        if (nextTarget == null)
        {
            return "None";
        }
        return nextTarget.name;
    }
    
    public Action getNextAction() {
        if(ActionListMinusPassives.Count < 1) {
            Console.WriteLine("ERROR: action list of "+getNextTargetName()+" was empty! Returning Idle for next action");
            return new Idle();
        }
        return this.ActionListMinusPassives[nextActionIndex];
    }

    public override void startOfRound()
    {
        base.startOfRound();
        prepareTurn();
    }
    
    public override void startOfTurn()
    {
        base.startOfTurn();
        // Check heroes for Piety, leave peacefully if HP < max piety
        foreach (PlayerCharacter hero in Battlefield.PlayerSide)
        {
            if (hero != null && hero.HasStatusEffect("Piety") && hero.GetStatusEffect("Piety")!.amount >= this.currentHP)
            {
                Console.WriteLine(hero.name + " is too pious! " + this.name + " leaves combat peacefully.");
                fleeing = true;
                return;
            }
        }
    }

    // Don't kill this entity, but do remove it from combat.
    public void flee() {
        // Does not trigger events for death
        Console.WriteLine(this.name+" has exited combat!");
        Battlefield.RemoveEntity(this);
    }
}