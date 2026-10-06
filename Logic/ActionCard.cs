// All action cards extend this class.
using System.Reflection.Metadata;

public class ActionCard
{
    public ActionType? actionType;
    public String originalName = "MISSING NAME";
    public String name = "MISSING NAME";
    public String originalDescription = "MISSING DESCRIPTION";
    public String description = "MISSING DESCRIPTION";
    public Modifier? modifier; // Each card can have 1 modifier.
    public Entity? owner; // Some cards are tied to specific characters.

    public bool bypassExhausted = false; // Whether this card can be played even on an exhausted entity
    public bool bypassOutOfUses = false; // Whether this card can be played on an action even when out of uses

    // Default constructor
    public ActionCard()
    {
        actionType = null;
        modifier = null;
        owner = null;
    }

    // Constructor from data
    public ActionCard(int actionCardID, Entity? owner = null)
    {
        // TODO: get info from data
    }

    public override string ToString()
    {
        string str = name + " (" + actionType.ToString() + "): " + description;
        if (modifier != null)
        {
            str += " Modifier: " + modifier.ToString();
        }
        return str;
    }

    // Returns an exact copy of this card
    public ActionCard makeCopy()
    {

        ActionCard? newCopy = DataRegistry.CardData.getCardByName(originalName);
        if (newCopy == null)
        {
            Console.WriteLine("ERROR: could not make copy of card; DataRegistry did not find card by name '" + name + "'!");
            return new BasicAttack();
        }
        newCopy.modifier = modifier;
        newCopy.UpdateName();
        newCopy.owner = owner;
        return newCopy;
    }


    // Possible cases where a card would be played:
    // 1. In the real, graphical game, the player would select actions and targets through a menu.
    //      CARD.play(HERO, TARGET, ACTION)
    //      ...we could ignore this case for now, but it is the final form once all info is gathered.
    // 2. In the console test, the "playcard" function gets the card, action-user, and no target.
    //      CARD.play(HERO, null)
    //  *       validateHero(HERO)
    //  *       ACTION = getAction(HERO)
    //  *       validateAction(ACTION)
    //          validateTarget(null)
    //  >       ACTION.use(HERO, TARGET)
    // 3. Same, but with a target index (int).
    //      CARD.play(HERO, INDEX)
    //  *       validateHero(HERO)
    //  *       ACTION = getAction(HERO)
    //  *       validateAction(ACTION)
    //          TARGET = getTarget(INDEX, ACTION)
    //          validateTarget(TARGET)
    //  >       ACTION.use(HERO, TARGET)
    // 4. Same, but with a target already selected by name (Entity).
    //      CARD.play(HERO, TARGET)
    //  *       validateEntity(HERO)
    //  *       ACTION = getAction(HERO)
    //  *       validateAction(ACTION)
    //          validateTarget(TARGET)
    //  >       ACTION.use(HERO, TARGET)
    // ...this logic can be identical to case 2, as long as validateTarget handles nulls well
    //
    // In cases 3 and 4, we do not know if the selected target is valid.
    // In cases 2-4, we do not know which action the player will choose, if there are multiple matches.
    // In cases 2-4, we cannot determine whether the action is valid for that user until the user chooses the action.
    // In cases 2-4, we do not know whether the action requires a target until the user chooses the action.
    //
    // So, steps:
    // 1. Validate hero
    // 2. Knowing the card and hero using it, retrieve the action. (We don't need to know the target for this.)
    // 3. Validate the action
    // 4. Validate the target or lack thereof.
    //      a. Verify that one was not provided if the action does not need one.
    //      b. Verify that one WAS provided if the action requires one.
    //      c. Verify that the target is valid for ACTION.
    // Finally, use the action.


    // Use the action; this version always uses autoselection.
    public virtual bool Play(Entity entityToUseAction, Entity? target)
    {
        // Step 1: validate the entity who is using the action.
        if (!CanBeUsedBy(entityToUseAction))
        {
            // This case is for when canBeUsedBy returns false. Error message is handled by that function.
            return false;
        }

        // Step 2: figure out which action the player wants to use.
        Action? actionToUse = GetAction(entityToUseAction);
        // Player has cancelled, or no valid action found.
        if(actionToUse == null)
        {
            Console.WriteLine("Cancelling card use.");
            return false;
        }

        // Step 3: validate the target of the action
        if(!ValidTarget(actionToUse, target))
        { // ValidTarget handles error message.
            return false;
        }

        // Step 4: trigger pre-use effects if the card has any
        BeforeUse(entityToUseAction, target, actionToUse);

        // Step 5: actually use the action
        if (actionToUse.use(target, modifier))
        {
            // Action was used.
            CardManager.discardCard(this);
            // Step 6: trigger after-use effects if the card has any
            AfterUse(entityToUseAction, target, actionToUse);
            Battlefield.TriggerOnActionResolved(actionToUse); // Post-Resolve events for player actions
            return true;
        }
        else
        { // Action could not be used. Action.use() will handle the error message.
            return false;
        }
    }

    // Overload using target index. TODO!
    public virtual bool Play(Entity entityToUseAction, int targetIndex)
    {
        // Step 1: validate the entity who is using the action.
        if (!CanBeUsedBy(entityToUseAction))
        {
            // This case is for when canBeUsedBy returns false. Error message is handled by that function.
            return false;
        }

        // Step 2: figure out which action the player wants to use.
        Action? actionToUse = GetAction(entityToUseAction);
        // Player has cancelled, or no valid action found.
        if(actionToUse == null)
        {
            Console.WriteLine("Cancelling card use.");
            return false;
        }

        // Step 3: Error if we were passed a target, and the player chose a non-targeted action
        if(!actionToUse.requiresTarget())
        {
            Console.WriteLine("ERROR: This action does not require a target.");
            Console.WriteLine("Example:    > 3 defender");
            return false;
        }

        // Step 4: get a target if we need one
        Entity? target = null;
        if(actionToUse.requiresTarget()) // Still need a target
        {
            // HAVE TO PARSE TARGET FROM INDEX.
            target = DeduceActionTargetFromIndex(actionToUse, targetIndex);
            if(target == null)
            { // Could not deduce target from index. Error message already handled in deduceActionTargetFromIndex.
                return false;
            }                
        }

        // Step 5: trigger pre-use effects if the card has any
        BeforeUse(entityToUseAction, target, actionToUse);

        // Step 6: actually use the action
        if (actionToUse.use(target, modifier))
        {
            // Action was used.
            CardManager.discardCard(this);
            // Step 6: trigger after-use effects if the card has any
            AfterUse(entityToUseAction, target, actionToUse);
            Battlefield.TriggerOnActionResolved(actionToUse); // Post-Resolve events for player actions
            return true;
        }
        else
        { // Action could not be used. Action.use() will handle the error message.
            return false;
        }
    }

    // Whether this card can be played on that action.
    public virtual bool MatchesAction(Action hoveredAction)
    {        
        if(actionType == ActionType.ANY) return true;

        // Otherwise, need to check that the main type matches:
        return hoveredAction.actionType == actionType;
    }

    // Whether this card can be played on that entity. Does not check for matching actions.
    public virtual bool CanBeUsedBy(Entity entityToUseAction)
    {
        if (!bypassExhausted && entityToUseAction.exhausted)
        {
            Console.WriteLine("Unit is exhausted and cannot act.");
            return false;
        }
        if (!entityToUseAction.playerControlled)
        {
            Console.WriteLine("Unit is not under player control.");
            return false;
        }
        return true;
    }

    // Only checks if the target
    public bool ValidTarget(Action act, Entity? target) {
        // Error if we were passed a target, and the player chose a non-targetted action:
        if(!act.requiresTarget() && target != null)
        {
            Console.WriteLine("ERROR: This action does not require a target.");
            Console.WriteLine("Example:    > 3 defender");
            return false;
        }
        // Different error if we were NOT passed a target, but the action requires one:
        if(act.requiresTarget() && target == null)
        {
            Console.WriteLine("ERROR: You must include the name or index of the target of this action as the last argument.");
            Console.WriteLine("Example:    > 2 fighter pengoon");
            return false;
        }
        return true;
    }

    // Attempts to smooth out selection. If there is more than 1 valid action, returns null.
    // Otherwise, returns the usable action that matches this card type.
    public virtual Action? autoSelectAction(Entity entityToUseAction)
    {
        List<Action> matchingActions = new List<Action>();
        foreach (Action action in entityToUseAction.ActionList)
        {
            if (MatchesAction(action))
            {
                matchingActions.Add(action);
            }
        }
        if (matchingActions.Count == 1) return matchingActions[0];
        return null; // More or less than 1 option, return null for no valid auto target
    }

    // Counts how many usable actions on the specified entity match this action card
    public virtual int numberMatchingActions(Entity entityToUseAction)
    {
        int matches = 0;
        foreach (Action action in entityToUseAction.ActionList)
        {
            if (MatchesAction(action))
            {
                matches++;
            }
        }
        return matches;
    }

    // Reshake the displayed name based on modifier
    public void UpdateName()
    {
        if (modifier == null)
        {
            name = originalName;
        }
        else
        {
            name = modifier.name + " " + originalName;
        }   
    }

    

    // Helper function to get the action about to be used by a hero + card.
    // Prompt the player to pick if there are multiple valid options.
    // Returns null if no valid action was found.
    public Action? GetAction(Entity hero)
    {
        int numMatchingActions = numberMatchingActions(hero);
        // If the hero has no matching actions, print an error:
        if(numMatchingActions == 0) {
            Console.WriteLine("Card '"+name+"' cannot be used by "+hero.name+".");
            return null;
        }
        // If the hero has multiple matching actions, prompt the player to choose one:
        if(numMatchingActions > 1) {
            Console.WriteLine(hero.name+" has multiple actions that this card can have them perform.");
            Console.WriteLine("Select one from the following by entering its number, or type something else to go back:");
            Console.WriteLine("");
            List<Action> matchingActions = new List<Action>();
            // Find all matching actions from the hero's action list:
            foreach(Action action in hero.ActionList) {
                if(MatchesAction(action)) {
                    matchingActions.Add(action);
                }
            }
            // Print them out and await selection:
            for(int i = 0; i < matchingActions.Count; i++) {
                Console.Write("["+(i+1)+" - "+matchingActions[i].name+"]\t");
            }
            Console.Write("\n> ");
            string? cmd2 = Console.ReadLine();
            if(cmd2 == null) return null;
            if(int.TryParse(cmd2.ToLower().Trim(), out int actionSelection)) {
                // If they entered a valid number for action selection, return it:
                if(actionSelection <= matchingActions.Count && actionSelection > 0) {
                    return matchingActions[actionSelection - 1];
                }
            }
            // No valid action selection.
            return null;
        }
        // If the hero has exactly 1 matching action, return it:
        if(numMatchingActions == 1) {
            return autoSelectAction(hero)!;
        }
        // numMatchingActions is negative, should never happen.
        if(numMatchingActions < 0)
        {
            Console.WriteLine("ERROR: negative matching actions!");
            return null;
        }
        Console.WriteLine("ERROR: WTF is even happening here... Super impossible");
        return null;
    }
    

    // Figure out who the player is trying to target based on the index and action targetting.
    public Entity? DeduceActionTargetFromIndex(Action actionToUse, int targetIndex) {
        switch(actionToUse.targetting)
        {
            case TargetCategory.NONE:
            case TargetCategory.SELF:
            case TargetCategory.ALL_ENEMIES:
            case TargetCategory.ALL_ALLIES:
            case TargetCategory.EVERYONE:
            case TargetCategory.OPPOSING:
                // In these cases, the player should not have included a target, since the action selects a target automatically.
                // We could error out, or we could ignore their selection. Lets error out to avoid confusion.
                Console.WriteLine("ERROR: Action does not require a target.");
                return null;
            case TargetCategory.SINGLE_ENEMY:
                if(targetIndex > 0 && targetIndex <= Battlefield.EnemySide.Count) // Check if index is valid for this group
                {
                    return Battlefield.EnemySide[targetIndex-1]; // Valid target
                }
                else {
                    // invalid index.
                    Console.WriteLine("Enemy index must be between 1-"+Battlefield.EnemySide.Count);
                    return null;
                }
            case TargetCategory.DEAD_ENEMY:
                if(targetIndex > 0 && targetIndex <= Battlefield.DeadEnemies.Count) // Check if index is valid for this group
                {
                    return Battlefield.DeadEnemies[targetIndex-1]; // Valid target
                }
                else {
                    // invalid index.
                    Console.WriteLine("Dead enemy index must be between 1-"+Battlefield.DeadEnemies.Count);
                    return null;
                }
            case TargetCategory.SINGLE_ALLY:
                if(targetIndex > 0 && targetIndex <= Battlefield.PlayerSide.Count) // Check if index is valid for this group
                {
                    return Battlefield.PlayerSide[targetIndex-1]; // Valid target
                }
                else {
                    // invalid index.
                    Console.WriteLine("Hero index must be between 1-"+Battlefield.PlayerSide.Count);
                    return null;
                }
            case TargetCategory.DEAD_ALLY:
                if(targetIndex > 0 && targetIndex <= Battlefield.DeadHeroes.Count) // Check if index is valid for this group
                {
                    return Battlefield.DeadHeroes[targetIndex-1]; // Valid target
                }
                else {
                    // invalid index.
                    Console.WriteLine("Dead hero index must be between 1-"+Battlefield.DeadHeroes.Count);
                    return null;
                }
            case TargetCategory.SINGLE_ANY:
                // Check if index is only valid for 1 side. If not, prompt the player to specify their target.
                bool indexValidForPlayerSide = false;
                bool indexValidForEnemySide = false;
                if(targetIndex > 0 && targetIndex <= Battlefield.PlayerSide.Count) // Check if index is valid for this group
                { // Valid for Heroes.
                    indexValidForPlayerSide = true;
                }
                if(targetIndex > 0 && targetIndex <= Battlefield.EnemySide.Count) // Check if index is valid for this group
                { // Valid for Enemies.
                    indexValidForEnemySide = true;
                }
                // If both are valid, prompt player to choose target
                if(indexValidForEnemySide && indexValidForPlayerSide)
                {
                    Console.WriteLine("Which character did you intend to target with "+actionToUse.name+"?");
                    Console.WriteLine("Select one from the following by entering its number, or type something else to cancel:");
                    Console.WriteLine("");
                    // Print them out and await selection:
                    Console.WriteLine("[1 - "+Battlefield.PlayerSide[targetIndex].name+"]\t[2 - "+Battlefield.EnemySide[targetIndex].name+"]");
                    string? cmd2 = Console.ReadLine();
                    if(cmd2 == null) return null;
                    if(int.TryParse(cmd2.ToLower().Trim(), out int targetSelection)) {
                        // If they entered a valid number for target selection, return that entity
                        if(targetSelection == 1) {
                            return Battlefield.PlayerSide[targetIndex];
                        }
                        if(targetSelection == 2) {
                            return Battlefield.EnemySide[targetIndex];
                        }
                    }
                    // No valid action selection.
                    return null;
                }
                // If only 1 is valid, use it
                else if(indexValidForPlayerSide)
                {
                    return Battlefield.PlayerSide[targetIndex];
                }
                else if(indexValidForEnemySide)
                {
                    return Battlefield.EnemySide[targetIndex];
                }
                else // At this point, neither are valid
                {
                    // invalid index.
                    Console.WriteLine("No hero or enemy found at index "+targetIndex);
                    return null;
                }
            case TargetCategory.DEAD_ANY:
                // Check if index is only valid for 1 side. If not, prompt the player to specify their target.
                bool indexValidForDeadHeroes = false;
                bool indexValidForDeadEnemies = false;
                if(targetIndex > 0 && targetIndex <= Battlefield.DeadHeroes.Count) // Check if index is valid for this group
                { // Valid for DeadHeroes.
                    indexValidForDeadHeroes = true;
                }
                if(targetIndex > 0 && targetIndex <= Battlefield.DeadEnemies.Count) // Check if index is valid for this group
                { // Valid for DeadEnemies.
                    indexValidForDeadEnemies = true;
                }
                // If both are valid, prompt player to choose target
                if(indexValidForDeadEnemies && indexValidForDeadHeroes)
                {
                    Console.WriteLine("Which character did you intend to target with "+actionToUse.name+"?");
                    Console.WriteLine("Select one from the following by entering its number, or type something else to cancel:");
                    Console.WriteLine("");
                    // Print them out and await selection:
                    Console.WriteLine("[1 - "+Battlefield.DeadHeroes[targetIndex].name+"]\t[2 - "+Battlefield.DeadEnemies[targetIndex].name+"]");
                    string? cmd2 = Console.ReadLine();
                    if(cmd2 == null) return null;
                    if(int.TryParse(cmd2.ToLower().Trim(), out int targetSelection)) {
                        // If they entered a valid number for target selection, return that entity
                        if(targetSelection == 1) {
                            return Battlefield.DeadHeroes[targetIndex];
                        }
                        if(targetSelection == 2) {
                            return Battlefield.DeadEnemies[targetIndex];
                        }
                    }
                    // No valid action selection.
                    return null;
                }
                // If only 1 is valid, use it
                else if(indexValidForDeadHeroes)
                {
                    return Battlefield.DeadHeroes[targetIndex];
                }
                else if(indexValidForDeadEnemies)
                {
                    return Battlefield.DeadEnemies[targetIndex];
                }
                else // At this point, neither are valid
                {
                    // invalid index.
                    Console.WriteLine("No dead hero or enemy found at index "+targetIndex);
                    return null;
                }
            default:
                Console.WriteLine("ERROR: Action has invalid targetting!");
                return null;
        }
    }

    // Card-only event to trigger before taking an action.
    public virtual bool BeforeUse(Entity entityToUseAction, Entity? target, Action hoveredAction) {
        return true;
    }

    // Card-only event to trigger before taking an action.
    public virtual bool AfterUse(Entity entityToUseAction, Entity? target, Action hoveredAction) {
        return true;
    }


}
