public class Enemy : Entity {
    
    // Tracks what the enemy is about to do each turn.
    // For most enemies, just increments by 1 until it hits a usable action each turn.
    public int nextActionIndex = 0;
    
    public int nextTargetPosition = 0; // 0 is the top player character, 2 is the bottom
    
    // Default Constructor
    public Enemy() {
        playerControlled = false;
        hostile = true;
    }

    // Constructor from data
    public Enemy(string enemyID) {
        DataRegistry.fillEnemyData(this, enemyID);
        playerControlled = false;
        hostile = true;
        exhausted = false;
        currentHP = maxHP;
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
        ActionList[nextActionIndex].use(getNextTarget(), null); // Null modifier for now
        nextActionIndex++;
        if(nextActionIndex >= ActionList.Count) {
            nextActionIndex = 0;
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
        if(this.ActionList[nextActionIndex].CanTarget(nextTarget)) {
            this.nextTargetPosition = newTargetPosition;
        }
        else {
            Console.WriteLine("Entity at position "+newTargetPosition+" is not a valid target for "+this.ActionList[nextActionIndex].name);
        }
    }

    public Entity? getNextTarget() {
        if(ActionList.Count < 1) {
            return null;
        }
        switch(this.ActionList[nextActionIndex].targetting){
            case TargetCategory.SINGLE_ENEMY:
                if(Battlefield.PlayerSide.Count <= nextTargetPosition) {
                    return null;
                }
                return Battlefield.PlayerSide[nextTargetPosition];
            case TargetCategory.SINGLE_ALLY:
                if(Battlefield.EnemySide.Count <= nextTargetPosition) {
                    return null;
                }
                return Battlefield.EnemySide[nextTargetPosition];
            default:
                return null;
        }
    }
    public string getNextTargetName() {
        if(ActionList.Count < 1) {
            return "None";
        }
        switch(this.ActionList[nextActionIndex].targetting){
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
    
}