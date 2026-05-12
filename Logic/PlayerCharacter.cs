public class PlayerCharacter : Entity {
    
    // The card that is included in the master deck whenever this character is in the party.
    public ActionCard personalCard;

    public int Level;

    public List<string>? UpgradeList;
    
    // Default Constructor
    public PlayerCharacter() {
        playerControlled = true;
        hostile = false;
        exhausted = false;
        personalCard = new BasicAttack();
        Level = 1;
    }

    // Constructor from data
    public PlayerCharacter(string characterID)
    {
        personalCard = new BasicAttack();
        PlayerData? data = DataRegistry.CharacterData.getPlayerDataByName(characterID);
        if (data == null)
        {
            Console.WriteLine("Could not generate player character; ID not found.");
            return;
        }
        ActionCard? potentialPersonalCard = DataRegistry.CardData.getCardByName(data.PersonalCard);
        if (potentialPersonalCard == null)
        {
            Console.WriteLine("Could not generate player character; personal card not found.");
            return;
        }
        personalCard = potentialPersonalCard;
        foreach (string actionName in data.ActionList)
        {
            Action? newAction = DataRegistry.ActionData.getActionByName(actionName);
            if (newAction == null)
            {
                Console.WriteLine("Could not generate player character; action not found.");
                return;
            }
            ActionList.Add(newAction);
        }
        name = data.Name;
        description = data.Description;
        maxHP = data.HP;
        Level = data.Level;
        UpgradeList = data.UpgradeList;
        playerControlled = true;
        hostile = false;
        exhausted = false;
        currentHP = maxHP;
        assignActionOwnership();
    }

    // Uses an action of the specified type.
    // If only 1 matching usable action is found, it is used automatically.
    // If multiple are found, asks the player which one to use.
    // If none are found, returns false.
    public bool PromptForTypedAction(ActionType type)
    {
        List<Action> validActions = new List<Action>();
        // Search for actions:
        foreach(Action act in this.ActionList)
        {
            if(act.actionType == type)
            {
                validActions.Add(act);
            }
        }
        if(validActions.Count == 0)
        {
            Console.WriteLine("DEBUG: no matching actions found.");
            return false;    
        }
        if(validActions.Count == 1)
        {
            Console.WriteLine("DEBUG: 1 matching action found, using...");
            return validActions[0].promptUse();
        }
        // At this point, there must be more than 1 valid action. Ask the player which one to use.
        while(true) {
            Console.WriteLine("Which action to use? (or 'skip' to skip using the action)");
            for(int i = 1; i <= validActions.Count; i++){
                Console.WriteLine("["+i+"] "+validActions[i-1].ToString());                  
            }
            Console.Write("\n> ");
            string? feedback = Console.ReadLine();
            if (feedback == null) continue;
            if(feedback.ToLower().Trim() == "skip") {
                Console.WriteLine("Skipping action.");
                return false;
            }
            if (int.TryParse(feedback.ToLower().Trim(), out int actionSelection))
            {
                if(actionSelection > 0 && actionSelection <= validActions.Count)
                {
                    // Check if the action can be used:
                    if(validActions[actionSelection-1].promptUse()) {
                        return true;    
                    }
                }
                else
                {
                    Console.WriteLine("Must enter a number between 1 and " + validActions.Count + ".\n");
                }
            }
            else
            {
                Console.WriteLine("Must enter a number between 1 and " + validActions.Count + ".\n");
            }
            // Otherwise just loop again
        }
    }
    
}