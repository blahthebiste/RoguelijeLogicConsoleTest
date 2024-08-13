public class CombatEncounter {
    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";
    public int difficulty = 0;
    public List<Entity> EnemyTroupe = new List<Entity>();

    public CombatEncounter(string combatID) {
        switch(combatID.ToLower().Trim()) {
            case "pengoons":
                this.name = DataRegistry.EnemyTroupes.Pengoons.Name;
                this.description = DataRegistry.EnemyTroupes.Pengoons.Description;
                this.difficulty = DataRegistry.EnemyTroupes.Pengoons.Difficulty;
                foreach(Entity enemy in DataRegistry.EnemyTroupes.Pengoons.EnemyList) {
                    this.EnemyTroupe.Add(enemy);
                }
                break;
            default:
                break;
        }
    }
}