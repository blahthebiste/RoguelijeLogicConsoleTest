public class StatusEffect {
    public bool isDebuff = false;
    public int amount = 0;

    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";

    public Entity? owner;


    // TODO: Add all events that would apply to an entity, virtual.
    public virtual void endOfTurn() {

    }

}