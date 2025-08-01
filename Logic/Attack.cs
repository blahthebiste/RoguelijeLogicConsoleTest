// Used for handling attack logic; passes info from the attacker to the attackee
public class Attack {

    public int damage = 0;
    public bool hitsAbove = false;
    public bool hitsBelow = false;
    public Entity source;
    public Entity target;

    // Normal constructor
    public Attack(int damage, Entity source, Entity target, bool hitsAbove = false, bool hitsBelow = false) {
        this.damage = damage;
        this.source = source;
        this.target = target;
        this.hitsAbove = hitsAbove;
        this.hitsBelow = hitsBelow;
    }
    
    // Copy constructor
    public Attack(Attack atk, Entity newTarget) {
        damage = atk.damage;
        target = atk.target;
        hitsAbove = atk.hitsAbove;
        hitsBelow = atk.hitsBelow;
        source = newTarget;
    }
}