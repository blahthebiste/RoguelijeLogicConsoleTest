public class Rattified : Action {

    public Rattified() {
        this.name = "Rattified";
        this.description = "On death, transform a random Hench-Rat into a Giant Rat.";
        this.actionType = ActionType.PASSIVE;
    }

    public override void onDeath()
    {
        if (this.owner == null)
        {
            Console.WriteLine("ERROR: " + this.name + " has null owner!");
            return;
        }
        // Check for allied hench-rats:
        List<Entity> matchingRats = new List<Entity>();
        foreach (Entity enemy in Battlefield.EnemySide)
        {
            if (enemy.name == "Hench-Rat")
            {
                matchingRats.Add(enemy);
            }
        }
        // Pick a random one:
        if (matchingRats.Count > 0)
        {
            CurrentRun.Shuffle(matchingRats);
            int index = Battlefield.EnemySide.IndexOf(matchingRats[0]); // Nice to have the giant rat in the same position as the one that transformed.
            Battlefield.EnemySide.Remove(matchingRats[0]);
            Battlefield.SummonEntity("Giant Rat", this.owner.hostile, index);
        }
        base.onDeath();
    }
}