// Both player characters and enemies extend from this class
public class Entity {
    public String name = "Missing entity name!";
    public String description = "Missing entity description!";
    public int maxHP = 1;
    public bool hostile = true;
    public bool playerControlled = false;
    public int currentHP = 1;
    public bool exhausted = false;
    public List<Action> ActionList = new List<Action>(); // Equipment is tied to actions.
    public List<StatusEffect> EffectList = new List<StatusEffect>(); // All status effects currently on the entity.
    public Action previousAction = new Idle();
    
    // Default constuctor
    public Entity() {

    }

    // Full constructor
    public Entity(string name, int maxHP, bool hostile, bool playerControlled) {
        this.name = name;
        this.maxHP = maxHP;
        this.hostile = hostile;
        this.playerControlled = playerControlled;
        this.currentHP = maxHP;
        this.exhausted = false;
    }

    public bool isAlive() {
        return currentHP != 0;
    }

    // Fills in the 'owner' field for all actions in the ActionList to be this entity
    public void assignActionOwnership() {
        foreach(Action action in ActionList) {
            action.owner = this;
        }
    }

    // Should always be used instead of direct HP operations
    public void changeHP(int delta){
        if(delta < 0) {
            foreach(StatusEffect effect in EffectList) {
                delta = effect.onLoseHP(delta);  // Handle events for status effects
            }
            // TODO: events for items, passives, modifiers...
            delta = onLoseHP(delta); // Handle events for the entity
        }
        this.currentHP += delta;
        if(this.currentHP > this.maxHP) this.currentHP = this.maxHP; // Cap healing
        if(this.currentHP <= 0) this.die(); // Trigger death
    }



    public virtual void ReceiveHealing(int healing) {
        Console.WriteLine(this.name+" regained "+healing+" HP.");
        changeHP(healing);
    }

    public virtual void ReceiveStatusEffect(StatusEffect newEffect) {
        string newEffectName = newEffect.name;
        Console.WriteLine("New effect is '"+newEffectName+"'.");
        foreach(StatusEffect existingEffect in EffectList) {
            string existingEffectName = existingEffect.name;
            Console.WriteLine("Comparing to '"+existingEffectName+"'...");
            if(existingEffectName == newEffectName) {
                // If the entity already has the effect, just add to it
                existingEffect.amount += newEffect.amount;
                return;
            }
        }
        // Entity does not have this effect, add it
        this.EffectList.Add(newEffect);
    }

    // Kill this entity and remove it from combat.
    public virtual void die() {
        Console.WriteLine(this.name+" has been slain!");
        Battlefield.RemoveEntity(this);
    }

    //====================EVENTS====================
    public virtual void startOfTurn() {
        foreach(StatusEffect effect in EffectList) {
            effect.startOfTurn(); // Handle events for status effects
        }
        EffectList.RemoveAll(element => element.amount == 0);
    }

    public virtual void endOfTurn() {
        foreach(StatusEffect effect in EffectList) {
            effect.endOfTurn(); // Handle events for status effects
        }
        EffectList.RemoveAll(element => element.amount == 0);
    }
    
    public virtual Attack onAttack(Attack atk) {
        foreach(StatusEffect effect in EffectList) {
            atk = effect.onAttack(atk); // Handle events for status effects
        }
        EffectList.RemoveAll(element => element.amount == 0);
        int targetIndex;
        if(atk.target.playerControlled) {
            targetIndex = Battlefield.PlayerSide.IndexOf((PlayerCharacter)atk.target);
            if(atk.hitsAbove && targetIndex > 0) {
                Entity aboveTarget = Battlefield.PlayerSide[targetIndex-1];
                Attack aboveAtk = new Attack(atk, aboveTarget);
                Console.WriteLine("Also hits target above.");
                aboveTarget.onReceiveAttack(aboveAtk);
            }
            if(atk.hitsBelow && targetIndex < (Battlefield.PlayerSide.Count-1)) {
                Entity belowTarget = Battlefield.PlayerSide[targetIndex+1];
                Attack belowAtk = new Attack(atk, belowTarget);
                Console.WriteLine("Also hits target below.");
                belowTarget.onReceiveAttack(belowAtk);
            }
        }
        else {
            targetIndex = Battlefield.EnemySide.IndexOf((Enemy)atk.target);
            if(atk.hitsAbove && targetIndex > 0) {
                Entity aboveTarget = Battlefield.EnemySide[targetIndex-1];
                Attack aboveAtk = new Attack(atk, aboveTarget);
                Console.WriteLine("Also hits target above.");
                aboveTarget.onReceiveAttack(aboveAtk);
            }
            if(atk.hitsBelow && targetIndex < (Battlefield.EnemySide.Count-1)) {
                Entity belowTarget = Battlefield.EnemySide[targetIndex+1];
                Attack belowAtk = new Attack(atk, belowTarget);
                Console.WriteLine("Also hits target below.");
                belowTarget.onReceiveAttack(belowAtk);
            }
        }
        return atk;
    }
    
    public virtual Attack onReceiveAttack(Attack atk) {
        foreach(StatusEffect effect in EffectList) {
            atk = effect.onReceiveAttack(atk); // Handle events for status effects
        }
        EffectList.RemoveAll(element => element.amount == 0);
        int blockedDamage = 0;
        if(this.playerControlled && Battlefield.playerBlock > 0) {
            blockedDamage = Math.Min(atk.damage, Battlefield.playerBlock);
            Battlefield.playerBlock -= blockedDamage;
        }
        else if(!this.playerControlled && Battlefield.enemyBlock > 0) {
            blockedDamage = Math.Min(atk.damage, Battlefield.enemyBlock);
            Battlefield.enemyBlock -= blockedDamage;
        }
        Console.WriteLine(blockedDamage+" damage was blocked.");
        atk.damage -= blockedDamage;
        Console.WriteLine(this.name+" was hit for "+atk.damage+" damage.");
        changeHP(-atk.damage);
        return atk;
    }

    public virtual int onLoseHP(int HPloss) {
        return HPloss;
    }

    public virtual int onGainBlock(int block) {
        foreach(StatusEffect effect in EffectList) {
            block = effect.onGainBlock(block); // Handle events for status effects
        }
        EffectList.RemoveAll(element => element.amount == 0);
        return block;
    }
}