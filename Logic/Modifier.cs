public class Modifier : Item {

    public int damageMod = 0;
    public int blockMod = 0;
    public int healMod = 0;

    public override string ToString()
    {
        return name + ": " + description;
    }

    // The meat and potatoes of the modifier.
    // This one only runs if the action SUCCESSFULLY worked on the target.
    public virtual void useOnTarget(Action act, Entity? target)
    {
    }

    // The meat and potatoes of the modifier.
    // This one always runs once if the action succeeded in any capacity.
    public virtual void useOnce(Action act)
    {
    }
}