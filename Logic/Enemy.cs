public class Enemy : Entity {
    
    // Tracks what the enemy is about to do each turn.
    // For most enemies, just increments by 1 until it hits a usable action each turn.
    public int nextActionIndex = 0;
    
    public int nextTargetPosition = 0; // 0 is the top player character, 2 is the bottom
    
    public bool fleeing = false;
    
    // Default Constructor
    public Enemy() {
        playerControlled = false;
        hostile = true;
    }

    public void enterCombat() {
        chooseNextTarget();
    }

    // Prints out full details about the enemy
    public override string ToString(){
        string str = this.name;
        str += "\nHP: "+this.currentHP+"/"+this.maxHP;
        str = str +"\nActions:";
        for(int i = 0; i < this.ActionList.Count; i++) {
            Action action = ActionList[i];
            str += "\n\t";
            str += action.ToString();
        }
        return str;
    }

    // Many enemies will override this
    public void takeTurn() {
        Entity? nextTarget = getNextTarget();
        getNextAction().use(nextTarget, null); // Null modifier, enemies don't use cards
        nextActionIndex++;
        if(nextActionIndex >= ActionList.Count) {
            nextActionIndex = 0;
        }
    }

    // Selects a random target index from the appropriate side of combat.
    public void chooseNextTarget(){
        if(ActionList.Count < 1) {
            Console.WriteLine("Entity has no actions.");
            return;
        }
        int newTargetIndex;
        switch(this.getNextAction().targetting){
            case TargetCategory.SINGLE_ENEMY:
                while(true) { // Possibly laggy logic for invisibility
                    newTargetIndex = CurrentRun.rng.Next(0, Battlefield.PlayerSide.Count);
                    if(Battlefield.PlayerSide.Count > 1 && Battlefield.PlayerSide[newTargetIndex].HasStatusEffect("Invisibility")) {
                        Console.WriteLine(Battlefield.PlayerSide[newTargetIndex]+" was invisible; rerolling target");
                    }
                    else {
                        Console.WriteLine("Selected random target.");
                        break;
                    }
                }
                setNextTarget(newTargetIndex, true);
                break;
            case TargetCategory.SINGLE_ALLY:
                newTargetIndex = CurrentRun.rng.Next(0, Battlefield.EnemySide.Count);
                setNextTarget(newTargetIndex, false);
                break;
            default:
                Console.WriteLine("Next action does not use targeting.");
                return;
        }
    }

    public void setNextTarget(int newTargetPosition, bool onPlayerSide = true) {
        // Check if there is a valid target for the next action at the new target position
        Entity? nextTarget;
        if(onPlayerSide) {
            nextTarget = Battlefield.PlayerSide[newTargetPosition];
        }
        else {
            nextTarget = Battlefield.EnemySide[newTargetPosition];
        }
        // Check if the next action can target them
        if(this.getNextAction().CanTarget(nextTarget)) {
            this.nextTargetPosition = newTargetPosition;
        }
        else {
            Console.WriteLine("Entity at position "+newTargetPosition+" is not a valid target for "+this.getNextAction().name);
        }
    }

    public Entity? getNextTarget() {
        if(ActionList.Count < 1) {
            Console.WriteLine("Entity has no actions.");
            return null;
        }
        switch(this.getNextAction().targetting){
            case TargetCategory.SINGLE_ENEMY:
                if(Battlefield.PlayerSide.Count <= nextTargetPosition) {
                    return null;
                }
                Console.WriteLine("Target for "+this.getNextAction().name+" is "+Battlefield.PlayerSide[nextTargetPosition].name);
                return Battlefield.PlayerSide[nextTargetPosition];
            case TargetCategory.SINGLE_ALLY:
                if(Battlefield.EnemySide.Count <= nextTargetPosition) {
                    return null;
                }
                Console.WriteLine("Target for "+this.getNextAction().name+" is "+Battlefield.EnemySide[nextTargetPosition].name);
                return Battlefield.EnemySide[nextTargetPosition];
            default:
                Console.WriteLine("Next action does not use targeting.");
                return null;
        }
    }
    
    public string getNextTargetName() {
        if(ActionList.Count < 1) {
            return "None";
        }
        switch(this.getNextAction().targetting){
            case TargetCategory.SINGLE_ENEMY:
                if(Battlefield.PlayerSide.Count <= nextTargetPosition) {
                    return "None";
                }
                return Battlefield.PlayerSide[nextTargetPosition].name;
            case TargetCategory.SINGLE_ALLY:
                if(Battlefield.EnemySide.Count <= nextTargetPosition) {
                    return "None";
                }
                return Battlefield.EnemySide[nextTargetPosition].name;
            default:
                return "None";
        }
    }
    
    public Action getNextAction() {
        if(ActionList.Count < 1) {
            Console.WriteLine("ERROR: action list of "+this.getNextTargetName()+" was empty! Returning Idle for next action");
            return new Idle();
        }
        return this.ActionList[this.nextActionIndex];
    }

    public override void startOfTurn(){
        base.startOfTurn();
        // Check heroes for Piety, leave peacefully if HP < max piety
        foreach(PlayerCharacter hero in Battlefield.PlayerSide) {
            if(hero != null && hero.HasStatusEffect("Piety") && hero.GetStatusEffect("Piety").amount >= this.currentHP) {
                Console.WriteLine(hero.name+" is too pious!");
                this.fleeing = true;
                return;
            }
        }
        chooseNextTarget();
    }

    // Don't kill this entity, but do remove it from combat.
    public void flee() {
        // Does not trigger events for death
        Console.WriteLine(this.name+" has exited combat!");
        Battlefield.RemoveEntity(this);
    }
}