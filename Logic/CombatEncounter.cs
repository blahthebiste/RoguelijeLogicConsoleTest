public class CombatEncounter {
    public static string name = "MISSING NAME";
    public static string description = "MISSING DESCRIPTION";
    public static int difficulty = 0;
    public List<Entity> EnemyTroupe;

    public CombatEncounter(string combatID) {
        switch(combatID.ToLower().Trim()) {
            case "pengoons":
                this.name = DataRegistry.EnemyTroupes.Pengoons.Name
                this.description = DataRegistry.EnemyTroupes.Pengoons.Description
                this.difficulty = DataRegistry.EnemyTroupes.Pengoons.Difficulty
                foreach(Entity enemy in DataRegistry.EnemyTroupes.Pengoons.EnemyList) {
                    this.EnemyTroupe.Add(enemy);
                }
                break;
            default:
                break;
        }
    }
}