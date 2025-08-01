// Both player characters and enemies extend from this class
public class Entity : Events
{
    public string name = "Missing entity name!";
    public string description = "Missing entity description!";
    public int maxHP = 1;
    public bool hostile = true; // Whether the entity appears on the Enemy side of the battlefield, or player side
    public bool playerControlled = false; // Whether the entity requires the player to play action cards to get them to act
    public int currentHP = 1;
    public bool exhausted = false;
    public List<Action> ActionList = new List<Action>(); // Equipment is tied to actions.
    public List<Action> ActionListMinusPassives = new List<Action>(); // Used for enemies determining what action to use next
    public List<StatusEffect> EffectList = new List<StatusEffect>(); // All status effects currently on the entity.
    public Action? previousAction = null;

    // For non-player controlled entities only:
    // Tracks what the entity is about to do each turn.
    // For most enemies, just increments by 1 until it hits a usable action each turn.
    public int nextActionIndex = 0;

    // For non-player controlled entities only:
    public Entity? nextTarget; // The next entity that will be targeted. Can be null for actions that do not require a target

    public bool fleeing = false;

    public Action idle = new Idle();

    public string? master; // Used by minions

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
        currentHP = maxHP;
        exhausted = false;
    }

        // Constructor from data
    public Entity(string characterID)
    {
        EntityData? data = DataRegistry.CharacterData.getEntityDataByName(characterID);
        if (data == null)
        {
            Console.WriteLine("Could not generate entity; ID not found.");
            return;
        }
        foreach (string actionName in data.ActionList)
        {
            Action? newAction = DataRegistry.ActionData.getActionByName(actionName);
            if (newAction == null)
            {
                Console.WriteLine("Could not generate entity; action not found.");
                return;
            }
            ActionList.Add(newAction);
        }
        idle.owner = this;
        name = data.Name;
        description = data.Description;
        maxHP = data.HP;
        exhausted = false;
        currentHP = maxHP;
        master = data.Master;
        assignActionOwnership();
    }

    // Prints out full details about the entity
    public override string ToString()
    {
        string str = name;
        str += "\nHP: " + currentHP + "/" + maxHP;
        str = str + "\nActions:";
        for (int i = 0; i < ActionList.Count; i++)
        {
            Action action = ActionList[i];
            str += "\n\t";
            str += action.ToString();
        }
        return str;
    }

    public bool isAlive()
    {
        return currentHP > 0;
    }


    // Fills in the 'owner' field for all actions in the ActionList to be this entity.
    // Updates ActionListMinusPassives.
    public void assignActionOwnership()
    {
        ActionListMinusPassives = new List<Action>();
        foreach (Action act in ActionList)
        {
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
        delta = onHPChange(delta);
        currentHP += delta;
        if (currentHP > maxHP) currentHP = maxHP; // Cap healing
    }

    // Should always be used instead of direct max HP operations
    // No events associated with changes to max HP for now.
    public void changeMaxHP(int delta)
    {
        maxHP += delta;
        if (currentHP > maxHP) currentHP = maxHP; // Cap healing
    }

    public virtual void ReceiveHealing(int healing)
    {
        Console.WriteLine(name + " was healed for " + healing + " HP.");
        changeHP(healing);
    }


    // Determines whether an entity has the given item equipped (slot irrelevant)
    public bool hasItem(EquipmentItem item)
    {
        foreach (Action act in ActionList)
        {
            if (act.equippedItem != null && act.equippedItem.name == item.name)
            {
                return true;
            }
        }
        return false;
    }


    public virtual void AddStatusEffect(StatusEffect newEffect)
    {
        if (this.hasItem(new Grog()) && newEffect.isDebuff && newEffect.amount > 0)
        {
            Console.WriteLine("Grog reduced incoming " + newEffect.name + "!");
            newEffect.amount -= 1;
        }
        string newEffectName = newEffect.name;
        Console.WriteLine(name + " gained new effect: " + newEffectName + " with value " + newEffect.amount + ".");
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
        EffectList.Add(newEffect);
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
        onDeath();
        Console.WriteLine(name + " has been slain!");
        if (Battlefield.PlayerSide.Contains(this))
        {
            Battlefield.DeadHeroes.Add(this);
        }
        else if (Battlefield.EnemySide.Contains(this))
        {
            Battlefield.DeadEnemies.Add(this);
        }
        Battlefield.RemoveEntity(this);
    }

    // Don't kill this entity, but do remove it from combat.
    public void flee()
    {
        // Does not trigger events for death
        Console.WriteLine(this.name + " has exited combat!");
        Battlefield.RemoveEntity(this);
    }

    //=========================== AI ===========================
    //===
    //===
    //===

    // Selects the next action and target automatically.
    // Should never be called for a playerControlled character.
    public void prepareTurn()
    {
        if (playerControlled)
        {
            Console.WriteLine("ERROR: entity is under player control, and should not be preparing their turn!");
            return;
        }
        // Loop through actions until we find a usable one
        for (int failedActions = 0; failedActions < ActionListMinusPassives.Count; failedActions++)
        {
            // Some actions do not require a target, but still need to be checked for usability:
            if (!getNextAction().requiresTarget() && getNextAction().canUse(null, null))
            {
                setNextTarget(null);
                return;
            }
            // For actions that do require a target, validate that we can find a valid target:
            Entity? chosenTarget = chooseNextTarget(getNextAction());
            if (chosenTarget != null)
            {
                setNextTarget(chosenTarget);
                return;
            }
            // That action could not find a valid target. Move onto the next.
            nextActionIndex++;
            if (nextActionIndex >= ActionListMinusPassives.Count)
            {
                nextActionIndex = 0;
            }
        }
        // Every non-passive action was unable to find a valid target. SKip turn
    }

    // Entity takes its turn. This involves using their action, and then selecting the next one.
    // Many enemies/summons will override this
    // Should never be called for a playerControlled character.
    public void takeTurn()
    {
        if (playerControlled)
        {
            Console.WriteLine("ERROR: entity is under player control, and should not automatically take their turn!");
            return;
        }
        getNextAction().use(nextTarget, null); // Null modifier, entities outside of player control don't use cards
        nextActionIndex++;
        if (nextActionIndex >= ActionListMinusPassives.Count)
        {
            nextActionIndex = 0;
        }
    }

    // Figures out who the given action should target.
    // Selects a random target index from the appropriate side of combat.
    // If no valid target could be found, returns null.
    // If the entity has no actions, or their next action does not require a target, returns null.
    // Should never be called for a playerControlled character.
    public Entity? chooseNextTarget(Action act)
    {
        if (playerControlled)
        {
            Console.WriteLine("ERROR: entity is under player control, and should not be automatically selecting targets!");
            return null;
        }
        // Get random order for targetting priority:
        List<int> targetPriorityList = new List<int>();
        switch (act.targetting)
        {
            case TargetCategory.SINGLE_ENEMY:
                if (hostile)
                {
                    for (int i = 0; i < Battlefield.PlayerSide.Count; i++)
                    {
                        targetPriorityList.Add(i);
                    }
                    CurrentRun.Shuffle(targetPriorityList);
                    foreach (int targetIndex in targetPriorityList)
                    {
                        // Try each target in order.
                        if (act.canUse(Battlefield.PlayerSide[targetIndex], null))
                        {
                            Console.WriteLine("Action " + act + " can be used on " + Battlefield.PlayerSide[targetIndex].name + "!");
                            return Battlefield.PlayerSide[targetIndex];
                        }
                    }
                }
                else
                { // If the entity is on the player's side:
                    for (int i = 0; i < Battlefield.EnemySide.Count; i++)
                    {
                        targetPriorityList.Add(i);
                    }
                    CurrentRun.Shuffle(targetPriorityList);
                    foreach (int targetIndex in targetPriorityList)
                    {
                        // Try each target in order.
                        if (act.canUse(Battlefield.EnemySide[targetIndex], null))
                        {
                            Console.WriteLine("Action " + act + " can be used on " + Battlefield.EnemySide[targetIndex].name + "!");
                            return Battlefield.EnemySide[targetIndex];
                        }
                    }
                }

                Console.WriteLine("Action has no valid targets.");
                return null;
            case TargetCategory.SINGLE_ALLY:
                if (hostile)
                {
                    for (int i = 0; i < Battlefield.EnemySide.Count; i++)
                    {
                        targetPriorityList.Add(i);
                    }
                    CurrentRun.Shuffle(targetPriorityList);
                    foreach (int targetIndex in targetPriorityList)
                    {
                        // Try each target in order.
                        if (act.canUse(Battlefield.EnemySide[targetIndex], null))
                        {
                            Console.WriteLine("Action " + act + " can be used on " + Battlefield.EnemySide[targetIndex].name + "!");
                            return Battlefield.EnemySide[targetIndex];
                        }
                    }
                }
                else
                { // If the entity is on the player's side:
                    for (int i = 0; i < Battlefield.PlayerSide.Count; i++)
                    {
                        targetPriorityList.Add(i);
                    }
                    CurrentRun.Shuffle(targetPriorityList);
                    foreach (int targetIndex in targetPriorityList)
                    {
                        // Try each target in order.
                        if (act.canUse(Battlefield.PlayerSide[targetIndex], null))
                        {
                            Console.WriteLine("Action " + act + " can be used on " + Battlefield.PlayerSide[targetIndex].name + "!");
                            return Battlefield.PlayerSide[targetIndex];
                        }
                    }
                }

                Console.WriteLine("Action has no valid targets.");
                return null;
            default:
                Console.WriteLine("Next action does not use targeting.");
                return null;
        }
    }

    // Set the nextTarget object.
    // Can be null for actions that do not require a target.
    public void setNextTarget(Entity? target)
    {
        nextTarget = target;
    }


    // Returns the currently selected next target.
    public Entity? getNextTarget()
    {
        return nextTarget;
    }

    // Returns the name of the currently selected next target,
    // or 'None' for actions that do not have a target.
    public string getNextTargetName()
    {
        if (nextTarget == null)
        {
            return "None";
        }
        return nextTarget.name;
    }

    public Action getNextAction()
    {
        if (ActionListMinusPassives.Count < 1)
        {
            //Console.WriteLine("ERROR: action list of "+getNextTargetName()+" was empty! Returning Idle for next action");
            return idle;
        }
        return ActionListMinusPassives[nextActionIndex];
    }

    //===
    //===
    //===
    //=========================== END AI ===========================

    //============================ EVENTS ============================
    //===
    //===
    //===

    // Entities are responsible for passing on events to their actions, items, and statuses.
    public override void startOfCombat()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.startOfCombat();
        }
        foreach (Action action in ActionList.ToList())
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
        foreach (Action action in ActionList.ToList())
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
        foreach (Action action in ActionList.ToList())
        {
            action.startOfRound();
            if (action.equippedItem != null)
            {
                action.equippedItem.startOfRound();
            }
        }
        exhausted = false;
        if (!playerControlled)
        {
            prepareTurn();
        }
    }
    public override void startOfTurn()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.startOfTurn();
        }
        assignActionOwnership(); // Update action list
        foreach (Action action in ActionList.ToList())
        {
            action.startOfTurn();
            if (action.equippedItem != null)
            {
                action.equippedItem.startOfTurn();
            }
        }
        if (hostile)
        {
            // Check hero side for Piety, leave peacefully if HP < max piety
            foreach (Entity hero in Battlefield.PlayerSide)
            {
                if (hero != null && hero.HasStatusEffect("Piety") && hero.GetStatusEffect("Piety")!.amount >= currentHP)
                {
                    Console.WriteLine(hero.name + " is too pious! " + name + " leaves combat peacefully.");
                    fleeing = true;
                    return;
                }
            }
        }
        else
        {
            // Check enemy side for Piety, leave peacefully if HP < max piety
            foreach (Entity enemy in Battlefield.EnemySide)
            {
                if (enemy != null && enemy.HasStatusEffect("Piety") && enemy.GetStatusEffect("Piety")!.amount >= currentHP)
                {
                    Console.WriteLine(enemy.name + " is too pious! " + name + " leaves combat peacefully.");
                    fleeing = true;
                    return;
                }
            }
        }
    }

    public override void endOfTurn()
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            effect.endOfTurn();
        }
        foreach (Action action in ActionList.ToList())
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
        foreach (Action action in ActionList.ToList())
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
        foreach (StatusEffect eff in EffectList.ToList())
        {
            actionBeingUsed = eff.onUseAction(actionBeingUsed);
        }
        // "Used action" event for all items on the entity
        foreach (Action act in ActionList.ToList())
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
        foreach (Action action in ActionList.ToList())
        {
            atk = action.onAttack(atk);
            if (action.equippedItem != null)
            {
                atk = action.equippedItem.onAttack(atk);
            }
        }
        int targetIndex;
        // Apply additional targets if applicable
        if (!atk.target.hostile)
        {
            targetIndex = Battlefield.PlayerSide.IndexOf(atk.target);
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
            targetIndex = Battlefield.EnemySide.IndexOf(atk.target);
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
        foreach (Action action in ActionList.ToList().ToList())
        {
            atk = action.onReceiveAttack(atk);
            if (action.equippedItem != null)
            {
                atk = action.equippedItem.onReceiveAttack(atk);
            }
        }
        int blockedDamage = 0;
        if (!hostile && Battlefield.playerBlock > 0)
        {
            blockedDamage = Math.Min(atk.damage, Battlefield.playerBlock);
            Battlefield.playerBlock -= blockedDamage;
        }
        else if (hostile && Battlefield.enemyBlock > 0)
        {
            blockedDamage = Math.Min(atk.damage, Battlefield.enemyBlock);
            Battlefield.enemyBlock -= blockedDamage;
        }
        if (blockedDamage > 0)
        {
            Console.WriteLine(blockedDamage + " damage was blocked.");
            atk.damage -= blockedDamage;
        }
        Console.WriteLine(name + " was hit for " + atk.damage + " damage.");
        changeHP(-atk.damage);
        return atk;
    }

    public override int onGainBlock(int block)
    {
        foreach (StatusEffect effect in EffectList.ToList())
        {
            block = effect.onGainBlock(block); // Handle events
        }
        foreach (Action action in ActionList.ToList())
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
        foreach (Action action in ActionList.ToList())
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
        foreach (Action action in ActionList.ToList())
        {
            action.onDeath();
            if (action.equippedItem != null)
            {
                action.equippedItem.onDeath();
            }
        }

    }

    //===
    //===
    //===
    //=========================== END EVENTS ===========================
}