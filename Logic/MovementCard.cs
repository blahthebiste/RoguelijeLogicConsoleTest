// All action cards extend this class.
public class MovementCard : ActionCard
{

    // Action can always be used
    public override int numberMatchingActions(Entity entityToUseAction)
    {
        Console.WriteLine("Movement card always playable, return 1");
        return 1;
    }

    // Assuming the user is alive, that is
    public override bool canBeUsedBy(Entity entityToUseAction)
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


    public override void use(Entity entityToUseAction, Entity? target)
    {
        if (canBeUsedBy(entityToUseAction))
        {
            if (executeAction((PlayerCharacter)entityToUseAction))
            {
                CardManager.discardCard(this);
            }
        }
        else
        {
            Console.WriteLine("Failed to execute movement '" + this.name + "'.");
        }

    }

    // The meat and potatoes of the action, to be implemented by each card:
    public virtual bool executeAction(PlayerCharacter hero)
    {
        Console.WriteLine("ERROR: card " + this.name + " has not implemented its executeAction function!");
        return false;
    }

 
}
