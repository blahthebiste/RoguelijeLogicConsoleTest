public class Modifier : Item {
    
    public Modifier()
    {
        this.type = "Modifier";
    }

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

    // The meat and potatoes of the modifier.
    // This one always runs once before the action takes place.
    // It changes properties of the action, such as damage, block, healing, etc.
    public virtual void modifyProperties(ActionProperties prop, Entity? actionOwner)
    {
    }
}