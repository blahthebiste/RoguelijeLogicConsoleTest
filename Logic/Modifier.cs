public class Modifier : Item {

    public int damageMod = 0;
    public int blockMod = 0;
    public int healMod = 0;
    
    // The meat and potatoes of the modifier
    public virtual void useOnTarget(Action act, Entity target)
    {
    }
    
    public virtual void useOnce(Action act)
    {
    }
}