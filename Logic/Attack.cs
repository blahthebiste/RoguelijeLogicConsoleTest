// Used for handling attack logic; passes info from the attacker to the attackee
public class Attack {

    public int damage = 0;
    public bool hitsAbove = false;
    public bool hitsBelow = false;
    public bool ignoresToughness = false; // Logic is in Toughness
    public bool ignoresBlock = false; // Code is in Entity.onReceiveAttack block logic.
    public bool ignoresDodge = false; // Code is in Dodging and Vanished status effects.
    // No need to check whether it ignores taunt, as that is only relevant for targetting.
    public Entity source;
    public Entity target;

    // Normal constructor
    public Attack(int damage, Entity source, Entity target, bool hitsAbove = false, bool hitsBelow = false, bool ignoresBlock = false, bool ignoresDodge = false, bool ignoresToughness = false) {
        this.damage = damage;
        this.source = source;
        this.target = target;
        this.hitsAbove = hitsAbove;
        this.hitsBelow = hitsBelow;
        this.ignoresBlock = ignoresBlock;
        this.ignoresDodge = ignoresDodge;
        this.ignoresToughness = ignoresToughness;
    }
    
    // Copy constructor
    public Attack(Attack atk, Entity newTarget) {
        damage = atk.damage;
        target = atk.target;
        hitsAbove = atk.hitsAbove;
        hitsBelow = atk.hitsBelow;
        source = newTarget;
        ignoresBlock = atk.ignoresBlock;
        ignoresDodge = atk.ignoresDodge;
        ignoresToughness = atk.ignoresToughness;
    }

    // Constructor from ActionProperties
     public Attack(ActionProperties prop, Entity source, Entity target) {
        this.damage = (int)(prop.damage*prop.damageMult);
        this.source = source;
        this.target = target;
        this.hitsAbove = prop.hitsAbove;
        this.hitsBelow = prop.hitsBelow;
        this.ignoresBlock = prop.ignoresBlock;
        this.ignoresDodge = prop.ignoresDodge;
        this.ignoresToughness = prop.ignoresToughness;
    }
}