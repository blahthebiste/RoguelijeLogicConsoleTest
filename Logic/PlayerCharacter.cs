public class PlayerCharacter : Entity {
    
    // The card that is included in the master deck whenever this character is in the party.
    public ActionCard personalCard;
    
    // Default Constructor
    public PlayerCharacter() {
        playerControlled = true;
        hostile = false;
        personalCard = new BasicAttack();
    }
    // Constructor from data
    public PlayerCharacter(string characterID) {
        personalCard = new BasicAttack();
        DataRegistry.fillCharacterData(this, characterID);
        playerControlled = true;
        hostile = false;
        exhausted = false;
        currentHP = maxHP;
    }
    
}