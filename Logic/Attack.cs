// Used for handling attack logic; passes info from the attacker to the attackee
public class Attack {

    public int damage = 0;
    public bool hitsAbove = false;
    public bool hitsBelow = false;
    public Entity source;
    public Entity target;

    // Normal constructor
    public Attack(int damage, Entity source, Entity target) {
        this.damage = damage;
        this.source = source;
        this.target = target;
    }
    
    // Copy constructor
    public Attack(Attack atk, Entity newTarget) {
        this.damage = atk.damage;
        this.target = atk.target;
        this.source = newTarget;
    }
}