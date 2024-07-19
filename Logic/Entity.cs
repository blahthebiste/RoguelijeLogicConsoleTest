// Both player characters and enemies extend from this class
public class Entity {
    public String name = "Missing Name!";
    public int maxHP = 1;
    public bool hostile = true;
    public bool playerControlled = false;
    public int currentHP = 1;
    public bool exhausted = false;
    public List<Action> ActionList = new List<Action>(); // Equipment is tied to actions.
    public List<StatusEffect> EffectList = new List<StatusEffect>(); // All status effects currently on the entity.
    
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

    public virtual void recieveAttack(int damage) {
        this.currentHP -= damage;
    }

    public virtual void recieveHealing(int healing) {
        if(this.currentHP + healing > this.maxHP) {
            this.currentHP = this.maxHP;
        }
        else {
            this.currentHP += healing;
        }
    }

    public virtual void recieveStatusEffect(StatusEffect newEffect) {
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
}