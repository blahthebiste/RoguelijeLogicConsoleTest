public class PlayerCharacter : Entity {
    
    // The card that is included in the master deck whenever this character is in the party.
    public ActionCard personalCard;
    
    // Default Constructor
    public PlayerCharacter() {
        playerControlled = true;
        hostile = false;
        exhausted = false;
        personalCard = new BasicAttack();
    }

    // Constructor from data
    public PlayerCharacter(string characterID) {
        personalCard = new BasicAttack();
        PlayerData? data = DataRegistry.CharacterData.getPlayerDataByName(characterID);
        if(data == null) {
            Console.WriteLine("Could not generate player character; ID not found.");
            return;
        }
        ActionCard? potentialPersonalCard = DataRegistry.CardData.getCardByName(data.PersonalCard);
        if(potentialPersonalCard == null) {
            Console.WriteLine("Could not generate player character; personal card not found.");
            return;
        }
        personalCard = potentialPersonalCard;
        foreach(string actionName in data.ActionList) {
            Action? newAction = DataRegistry.ActionData.getActionByName(actionName);
            if(newAction == null) {
                Console.WriteLine("Could not generate player character; action not found.");
                return;
            }
            ActionList.Add(newAction);
        }
        name = data.Name;
        description = data.Description;
        maxHP = data.HP;
        playerControlled = true;
        hostile = false;
        exhausted = false;
        currentHP = maxHP;
    }
    
}