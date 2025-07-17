/* Base class containing all common events.
 * Utilized by entities, items, actions, and status effects.
 * Entities are the only ones who explicitly trigger events;
 * they are responsible for iterating through all of their own
 * actions, items, and status effects, and passing the event triggers on.
 * 
 * Note that each of the afforementioned clases may also have some
 * unique events of their own, handled seperately.
*/

public class Events
{

    public virtual void startOfCombat()
    {//

    }

    public virtual void endOfCombat()
    { // Not used by Enemies, since if combat is over, they are already dead.

    }

    public virtual void startOfTurn()
    {//

    }

    public virtual void endOfTurn()
    {//

    }

    // Triggered every time an entity acts
    public virtual Action onUseAction(Action actionBeingUsed)
    {//
        return actionBeingUsed;
    }

    public virtual Attack onAttack(Attack atk)
    {//
        return atk;
    }

    public virtual Attack onReceiveAttack(Attack atk)
    {//
        return atk;
    }

    public virtual int onGainBlock(int block)
    {//
        return block;
    }


    public virtual int onHPChange(int HPdelta)
    {//
        return HPdelta;
    }


    public virtual void onDeath()
    {//

    }
}