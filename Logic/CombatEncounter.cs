public class CombatEncounter {
    public string name = "MISSING NAME";
    public string description = "MISSING DESCRIPTION";
    public int difficulty = 0;
    public List<Entity> EnemyTroupe = new List<Entity>();

    public CombatEncounter(string combatID) {
        TroupeData? data = DataRegistry.EnemyTroupes.getTroupeDataByName(combatID);
        if(data == null) {
            Console.WriteLine("Could not generate enemy troupe; ID not found.");
            return;
        }
        foreach(string enemyName in data.EnemyList) {
            Entity? newEnemy = DataRegistry.EnemyData.getEnemyByName(enemyName);
            if(newEnemy == null) {
                Console.WriteLine("Could not generate enemy troupe; enemy not found.");
                return;
            }
            EnemyTroupe.Add(newEnemy);
        }
        name = data.Name;
        description = data.Description;
        difficulty = data.Difficulty;
    }
}