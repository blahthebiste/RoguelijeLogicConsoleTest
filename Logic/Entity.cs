// Both player characters and enemies extend from this class
public class Entity : Events
{
    public string name = "Missing entity name!";
    public string description = "Missing entity description!";
    public int maxHP = 1;
    public bool hostile = true;
    public bool playerControlled = false;
    public int currentHP = 1;
    public bool exhausted = false;
    public List<Action> ActionList = new List<Action>(); // Equipment is tied to actions.
    public List<Action> ActionListMinusPassives = new List<Action>(); // Used for enemies determining what action to use next
    public List<StatusEffect> EffectList = new List<StatusEffect>(); // All status effects currently on the entity.
    public Action? previousAction = null;

    // Default constuctor
    public Entity()
    {

    }

    // Full constructor
    public Entity(string name, int maxHP, bool hostile, bool playerControlled)
    {
        this.name = name;
        this.maxHP = maxHP;
        this.hostile = hostile;
        this.playerControlled = playerControlled;
        this.currentHP = maxHP;
        this.exhausted = false;
    }

    public bool isAlive()
    {
        return currentHP > 0;
    }


    // Fills in the 'owner' field for all actions in the ActionList to be this entity
    public void assignActionOwnership()
    {
        foreach (Action act in this.ActionList) {
            act.owner = this;
            if (act.actionType != ActionType.PASSIVE)
            { // Assign ActionListMinusPassives:
                ActionListMinusPassives.Add(act);
            }
        }
    }

    // Should always be used instead of direct HP operations
    public void changeHP(int delta)
    {
        delta = this.onHPChange(delta);
        this.currentHP += delta;
        if (this.currentHP > this.maxHP) this.currentHP = this.maxHP; // Cap healing
    }

    // Should always be used instead of direct max HP operations
    // No events associated with changes to max HP for now.
    public void changeMaxHP(int delta)
    {
        this.maxHP += delta;
        if (this.currentHP > this.maxHP) this.currentHP = this.maxHP; // Cap healing
    }

    public virtual void ReceiveHealing(int healing)
    {
        Console.WriteLine(this.name + " was healed for " + healing + " HP.");
        changeHP(healing);
    }

    public virtual void AddStatusEffect(StatusEffect newEffect)
    {
        if (this.hasItem(new Grog()) && newEffect.isDebuff && newEffect.amount > 0)
        {
            Console.WriteLine("Grog reduced incoming " + newEffect.name + "!");
            newEffect.amount -= 1;
        }
        string newEffectName = newEffect.name;
        Console.WriteLine(this.name + " gained new effect: " + newEffectName + " with value " + newEffect.amount + ".");
        foreach (StatusEffect existingEffect in EffectList)
        {
            string existingEffectName = existingEffect.name;
            //Console.WriteLine("Comparing to '"+existingEffectName+"'...");
            if (existingEffectName == newEffectName)
            {
                // If the entity already has the effect, just add to it
                existingEffect.amount += newEffect.amount;
                existingEffect.onAmountChanged(newEffect.amount);
                return;
            }
        }
        // Entity does not have this effect, add it
        this.EffectList.Add(newEffect);
        newEffect.onApplied();
    }

    // Bool value is whether the effect was successfully removed
    public virtual bool RemoveStatusEffectByName(string effectName)
    {
        Console.WriteLine("Removing effect named '" + effectName + "'.");
        foreach (StatusEffect existingEffect in EffectList.ToList())
        {
            string existingEffectName = existingEffect.name;
            //Console.WriteLine("Comparing to '"+existingEffectName+"'...");
            if (existingEffectName == effectName)
            {
                // If the entity has the effect, remove it
                existingEffect.onRemoved();
                EffectList.Remove(existingEffect);
                return true;
            }
        }
        // Entity does not have this effect, return false
        return false;
    }

    // Returns a reference to the status effect object, if it exists.
    // Otherwise returns null.
    public StatusEffect? GetStatusEffect(string effectName)
    {
        foreach (StatusEffect existingEffect in EffectList)
        {
            string existingEffectName = existingEffect.name;
            //Console.WriteLine("Comparing to '"+existingEffectName+"'...");
            if (existingEffectName == effectName)
            {
                return existingEffect;
            }
        }
        return null;
    }

    public bool HasStatusEffect(string effectName)
    {
        foreach (StatusEffect existingEffect in EffectList)
        {
            string existingEffectName = existingEffect.name;
            //Console.WriteLine("Comparing to '"+existingEffectName+"'...");
            if (existingEffectName == effectName)
            {
                return true;
            }
        }
        return false;
    }

    // Kill this entity and remove it from combat.
    public virtual void die()
    {
        // Trigger events for death
        this.onDeath();
        Console.WriteLine(this.name + " has been slain!");
        Battlefield.RemoveEntity(this);
        if (this is PlayerCharacter)
        {
            Battlefield.DeadHeroes.Add((PlayerCharacter)this);
        }
        else if (this is Enemy)
        {
            Battlefield.DeadEnemies.Add((Enemy)this);
        }
    }

    // Determines whether an entity has the given item equipped (slot irrelevant)
    public bool hasItem(EquipmentItem item)
    {
        foreach (Action act in this.ActionList)
        {
            if (act.equippedItem != null && act.equippedItem.name == item.name)
            {
                return true;
            }
        }
        return false;
    }

    //====================EVENTS====================
    // Entities are responsible for passing on events to their actions, items, and statuses.
    public override void startOfCombat()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.startOfCombat();
        }
        foreach (Action action in this.ActionList.ToList())
        {
            action.startOfCombat();
            if (action.equippedItem != null)
            {
                action.equippedItem.startOfCombat();
            }
        }
    }

    public override void endOfCombat()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.endOfCombat();
        }
        foreach (Action action in this.ActionList.ToList())
        {
            action.endOfCombat();
            if (action.equippedItem != null)
            {
                action.equippedItem.endOfCombat();
            }
        }

    }

    public override void startOfRound()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.startOfRound();
        }
        foreach (Action action in this.ActionList.ToList())
        {
            action.startOfRound();
            if (action.equippedItem != null)
            {
                action.equippedItem.startOfRound();
            }
        }
    }
    public override void startOfTurn()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.startOfTurn();
        }
        foreach (Action action in this.ActionList.ToList())
        {
            action.startOfTurn();
            if (action.equippedItem != null)
            {
                action.equippedItem.startOfTurn();
            }
        }
    }

    public override void endOfTurn()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.endOfTurn();
        }
        foreach (Action action in this.ActionList.ToList())
        {
            action.endOfTurn();
            if (action.equippedItem != null)
            {
                action.equippedItem.endOfTurn();
            }
        }
    }
    public override void endOfRound()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.endOfRound();
        }
        foreach (Action action in this.ActionList.ToList())
        {
            action.endOfRound();
            if (action.equippedItem != null)
            {
                action.equippedItem.endOfRound();
            }
        }
    }

    // Triggered every time an entity acts
    public override Action onUseAction(Action actionBeingUsed)
    {
        // "Used action" event for all status effects on the entity
        foreach (StatusEffect eff in this.EffectList.ToList())
        {
            actionBeingUsed = eff.onUseAction(actionBeingUsed);
        }
        // "Used action" event for all items on the entity
        foreach (Action act in this.ActionList.ToList())
        {
            act.onUseAction(actionBeingUsed);
            if (act.equippedItem != null)
            {
                actionBeingUsed = act.equippedItem.onUseAction(actionBeingUsed);
                if (act == actionBeingUsed)
                {
                    // "Used action" event for the item equipped to the action
                    act.equippedItem.onUseEquippedAction(actionBeingUsed);
                }
            }
        }
        return actionBeingUsed;
    }

    public override Attack onAttack(Attack atk)
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            atk = effect.onAttack(atk);
        }
        foreach (Action action in this.ActionList.ToList())
        {
            atk = action.onAttack(atk);
            if (action.equippedItem != null)
            {
                atk = action.equippedItem.onAttack(atk);
            }
        }
        int targetIndex;
        // Apply additional targets if applicable
        if (atk.target.playerControlled)
        {
            targetIndex = Battlefield.PlayerSide.IndexOf((PlayerCharacter)atk.target);
            if (atk.hitsAbove && targetIndex > 0)
            {
                Entity aboveTarget = Battlefield.PlayerSide[targetIndex - 1];
                Attack aboveAtk = new Attack(atk, aboveTarget);
                Console.WriteLine("Also hits target above.");
                aboveTarget.onReceiveAttack(aboveAtk);
            }
            if (atk.hitsBelow && targetIndex < (Battlefield.PlayerSide.Count - 1))
            {
                Entity belowTarget = Battlefield.PlayerSide[targetIndex + 1];
                Attack belowAtk = new Attack(atk, belowTarget);
                Console.WriteLine("Also hits target below.");
                belowTarget.onReceiveAttack(belowAtk);
            }
        }
        else
        {
            targetIndex = Battlefield.EnemySide.IndexOf((Enemy)atk.target);
            if (atk.hitsAbove && targetIndex > 0)
            {
                Entity aboveTarget = Battlefield.EnemySide[targetIndex - 1];
                Attack aboveAtk = new Attack(atk, aboveTarget);
                Console.WriteLine("Also hits target above.");
                aboveTarget.onReceiveAttack(aboveAtk);
            }
            if (atk.hitsBelow && targetIndex < (Battlefield.EnemySide.Count - 1))
            {
                Entity belowTarget = Battlefield.EnemySide[targetIndex + 1];
                Attack belowAtk = new Attack(atk, belowTarget);
                Console.WriteLine("Also hits target below.");
                belowTarget.onReceiveAttack(belowAtk);
            }
        }
        return atk;
    }

    public override Attack onReceiveAttack(Attack atk)
    {
        foreach (StatusEffect effect in EffectList.ToList().ToList())
        {
            atk = effect.onReceiveAttack(atk); // Handle events
        }
        foreach (Action action in this.ActionList.ToList().ToList())
        {
            atk = action.onReceiveAttack(atk);
            if (action.equippedItem != null)
            {
                atk = action.equippedItem.onReceiveAttack(atk);
            }
        }
        int blockedDamage = 0;
        if (this.playerControlled && Battlefield.playerBlock > 0)
        {
            blockedDamage = Math.Min(atk.damage, Battlefield.playerBlock);
            Battlefield.playerBlock -= blockedDamage;
        }
        else if (!this.playerControlled && Battlefield.enemyBlock > 0)
        {
            blockedDamage = Math.Min(atk.damage, Battlefield.enemyBlock);
            Battlefield.enemyBlock -= blockedDamage;
        }
        if (blockedDamage > 0)
        {
            Console.WriteLine(blockedDamage + " damage was blocked.");
            atk.damage -= blockedDamage;
        }
        Console.WriteLine(this.name + " was hit for " + atk.damage + " damage.");
        changeHP(-atk.damage);
        return atk;
    }

    public override int onGainBlock(int block)
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            block = effect.onGainBlock(block); // Handle events
        }
        foreach (Action action in this.ActionList.ToList())
        {
            block = action.onGainBlock(block);
            if (action.equippedItem != null)
            {
                block = action.equippedItem.onGainBlock(block);
            }
        }
        return block;
    }

    public override int onHPChange(int HPdelta)
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            HPdelta = effect.onHPChange(HPdelta); // Handle events
        }
        foreach (Action action in this.ActionList.ToList())
        {
            HPdelta = action.onHPChange(HPdelta);
            if (action.equippedItem != null)
            {
                HPdelta = action.equippedItem.onHPChange(HPdelta);
            }
        }
        return HPdelta;
    }

    public override void onDeath()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.onDeath();
        }
        foreach (Action action in this.ActionList.ToList())
        {
            action.onDeath();
            if (action.equippedItem != null)
            {
                action.equippedItem.onDeath();
            }
        }

    }


}