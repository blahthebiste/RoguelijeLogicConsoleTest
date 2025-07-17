public class Enemy : Entity {
    
    // Tracks what the enemy is about to do each turn.
    // For most enemies, just increments by 1 until it hits a usable action each turn.
    public int nextActionIndex = 0;
    
    public int nextTargetPosition = 0; // 0 is the top player character, 2 is the bottom
    
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
            newAction.owner = this;
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
            if (chooseNextTarget())
            {
                break;
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
        Entity? nextTarget = getNextTarget();
        getNextAction().use(nextTarget, null); // Null modifier, enemies don't use cards
        nextActionIndex++;
        if (nextActionIndex >= ActionListMinusPassives.Count)
        {
            nextActionIndex = 0;
        }
    }

    // Figures out who the next action should target.
    // Selects a random target index from the appropriate side of combat.
    // If no valid target could be found, returns false.
    // If the entity has no actions, or their next action does not require a target, returns true.
    public bool chooseNextTarget()
    {
        if (ActionListMinusPassives.Count < 1)
        {
            Console.WriteLine("Entity has no actions.");
            return true;
        }
        List<int> InvalidActionIndices = new List<int>(); // Indices of Actions that cannot find a valid target
        // Get random order for targetting priority:
        List<int> targetPriorityList = new List<int>();
        switch (this.getNextAction().targetting)
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
                    if (getNextAction().canUse(Battlefield.PlayerSide[targetIndex], null))
                    {
                        Console.WriteLine("Action " + getNextAction() + " can be used on " + Battlefield.PlayerSide[targetIndex].name + "!");
                        setNextTarget(targetIndex, true);
                        return true;
                    }
                }
                Console.WriteLine("Action has no valid targets.");
                return false;
            case TargetCategory.SINGLE_ALLY:
                for (int i = 0; i < Battlefield.EnemySide.Count; i++)
                {
                    targetPriorityList.Add(i);
                }
                CurrentRun.Shuffle(targetPriorityList);
                foreach (int targetIndex in targetPriorityList)
                {
                    // Try each target in order.
                    if (getNextAction().canUse(Battlefield.EnemySide[targetIndex], null))
                    {
                        Console.WriteLine("Action " + getNextAction() + " can be used on " + Battlefield.EnemySide[targetIndex] + "!");
                        setNextTarget(targetIndex, false);
                        return true;
                    }
                }
                Console.WriteLine("Action has no valid targets.");
                return false;
            default:
                Console.WriteLine("Next action does not use targeting.");
                return true;
        }
    }

    // Uses the selected target position and side to set the nextTarget object.
    public void setNextTarget(int newTargetPosition, bool onPlayerSide = true)
    {
        // Check if there is a valid target for the next action at the new target position
        Entity? nextTarget;
        if (onPlayerSide)
        {
            nextTarget = Battlefield.PlayerSide[newTargetPosition];
        }
        else
        {
            nextTarget = Battlefield.EnemySide[newTargetPosition];
        }
        // Check if the next action can target them
        if (this.getNextAction().CanTarget(nextTarget))
        {
            this.nextTargetPosition = newTargetPosition;
        }
        else
        {
            Console.WriteLine("Entity at position " + newTargetPosition + " is not a valid target for " + this.getNextAction().name);
        }
    }


    // Returns the currently selected next target.
    public Entity? getNextTarget()
    {
        if (ActionListMinusPassives.Count < 1)
        {
            Console.WriteLine("Entity has no actions.");
            return null;
        }
        switch (this.getNextAction().targetting)
        {
            case TargetCategory.SINGLE_ENEMY:
                if (Battlefield.PlayerSide.Count <= nextTargetPosition)
                {
                    return null;
                }
                Console.WriteLine("Target for " + this.getNextAction().name + " is " + Battlefield.PlayerSide[nextTargetPosition].name);
                return Battlefield.PlayerSide[nextTargetPosition];
            case TargetCategory.SINGLE_ALLY:
                if (Battlefield.EnemySide.Count <= nextTargetPosition)
                {
                    return null;
                }
                Console.WriteLine("Target for " + this.getNextAction().name + " is " + Battlefield.EnemySide[nextTargetPosition].name);
                return Battlefield.EnemySide[nextTargetPosition];
            default:
                Console.WriteLine("Next action does not use targeting.");
                return null;
        }
    }
    
    // Returns the name of the currently selected next target.
    public string getNextTargetName()
    {
        if (ActionListMinusPassives.Count < 1)
        {
            return "None";
        }
        switch (this.getNextAction().targetting)
        {
            case TargetCategory.SINGLE_ENEMY:
                if (Battlefield.PlayerSide.Count <= nextTargetPosition)
                {
                    return "None";
                }
                return Battlefield.PlayerSide[nextTargetPosition].name;
            case TargetCategory.SINGLE_ALLY:
                if (Battlefield.EnemySide.Count <= nextTargetPosition)
                {
                    return "None";
                }
                return Battlefield.EnemySide[nextTargetPosition].name;
            default:
                return "None";
        }
    }
    
    public Action getNextAction() {
        if(ActionListMinusPassives.Count < 1) {
            Console.WriteLine("ERROR: action list of "+this.getNextTargetName()+" was empty! Returning Idle for next action");
            return new Idle();
        }
        return this.ActionListMinusPassives[this.nextActionIndex];
    }

    public override void startOfTurn(){
        base.startOfTurn();
        // Check heroes for Piety, leave peacefully if HP < max piety
        foreach(PlayerCharacter hero in Battlefield.PlayerSide) {
            if(hero != null && hero.HasStatusEffect("Piety") && hero.GetStatusEffect("Piety")!.amount >= this.currentHP) {
                Console.WriteLine(hero.name+" is too pious! "+this.name+" leaves combat peacefully.");
                this.fleeing = true;
                return;
            }
        }
        prepareTurn();
    }

    // Don't kill this entity, but do remove it from combat.
    public void flee() {
        // Does not trigger events for death
        Console.WriteLine(this.name+" has exited combat!");
        Battlefield.RemoveEntity(this);
    }
}