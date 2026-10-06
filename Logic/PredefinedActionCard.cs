// All action cards which come prebaked with their own action extend this class.
// These perform the same action, regardless of which character is using them.
public class PredefinedActionCard : ActionCard
{
    public Action? predefinedAction; // This will be null by default, classes extending this MUST implement it.

    // TODO Action can always be used?
    public override int numberMatchingActions(Entity entityToUseAction)
    {
        Console.WriteLine("Predefined Action card always playable, return 1");
        return 1;
    }

    // TODO Assuming the user is alive, that is
    public override bool CanBeUsedBy(Entity entityToUseAction)
    {
        if (!entityToUseAction.isAlive())
        {
            Console.WriteLine("ERROR: only living entities can perform this action!");
            return false;
        }
        if (entityToUseAction is PlayerCharacter && Battlefield.PlayerSide.Contains(entityToUseAction))
        {
            return true;
        }
        else
        {
            Console.WriteLine("ERROR: only player characters can perform this action!");
            return false;
        }
    }

    // Slightly different from cards without predefined actions.
    // In this case, we do not need to figure out which action is being used. It is already defined.
    public override bool Play(Entity entityToUseAction, Entity? target)
    {
        // Step 1: ensure the predefined action has actually been defined
        if(predefinedAction == null)
        {
            Console.WriteLine("ERROR: Predefined action not defined for "+this.name);
            return false;
        }

        // Step 2: validate the entity who is using the action.
        if (!CanBeUsedBy(entityToUseAction))
        {
            // This case is for when canBeUsedBy returns false. Error message is handled by that function.
            return false;
        }

        // Step 3: validate the target of the action
        if(!ValidTarget(predefinedAction, target))
        { // ValidTarget handles error message.
            return false;
        }

        // Step 4: trigger pre-use effects if the card has any
        BeforeUse(entityToUseAction, target, predefinedAction);

        // Step 5: actually use the action
        if (predefinedAction.use(target, modifier))
        {
            // Action was used.
            CardManager.discardCard(this);
            // Step 6: trigger after-use effects if the card has any
            AfterUse(entityToUseAction, target, predefinedAction);
            Battlefield.TriggerOnActionResolved(predefinedAction); // Post-Resolve events for player actions
            return true;
        }
        else
        { // Action could not be used. Action.use() will handle the error message.
            return false;
        }
    } 
}
