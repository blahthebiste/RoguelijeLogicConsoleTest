public class Enemy : Entity {
    
    // Tracks what the enemy is about to do each turn.
    // For most enemies, just increments by 1 until it hits a usable action each turn.
    int nextActionIndex = 0;
    
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

    // Many enemies will override this
    public void takeTurn() {
        nextActionIndex++;
        if(nextActionIndex >= ActionList.Count) {
            nextActionIndex = 0;
        }
    }
    
}