public class Zone {

    // Each zone is divided into 3 phases.
    // Each phase has 2 normal combat encounters, and 1 miniboss encounter.
    // After phase 3, the player fights the Boss of the zone.
    public List<CombatEncounter> Phase1CombatEncounters = new List<CombatEncounter>();
    public List<CombatEncounter> Phase2CombatEncounters = new List<CombatEncounter>();
    public List<CombatEncounter> Phase3CombatEncounters = new List<CombatEncounter>();
    public List<CombatEncounter> Miniboss1Encounters = new List<CombatEncounter>();
    public List<CombatEncounter> Miniboss2Encounters = new List<CombatEncounter>();
    public List<CombatEncounter> Miniboss3Encounters = new List<CombatEncounter>();
    public List<CombatEncounter> BossEncounters = new List<CombatEncounter>();

    public Zone(ZoneID zoneID) {
        // TODO: add combat encounters
        switch(zoneID) {
            case ZoneID.HUB:
                break;
            case ZoneID.ZONE1:
                // Phase 1
                Phase1CombatEncounters.Add(new CombatEncounter("pengoons"));
                Phase1CombatEncounters.Add(new CombatEncounter("mist wolf"));
                Phase1CombatEncounters.Add(new CombatEncounter("blackwood bandits"));
                Miniboss1Encounters.Add(new CombatEncounter("the witch of blackwood"));
                // Phase 2
                Phase2CombatEncounters.Add(new CombatEncounter("plagued peasants"));
                Phase2CombatEncounters.Add(new CombatEncounter("rats"));
                Miniboss2Encounters.Add(new CombatEncounter("the dark one"));
                // Phase 3
                Phase3CombatEncounters.Add(new CombatEncounter("castle gate"));
                Phase3CombatEncounters.Add(new CombatEncounter("courtyard sorcerers"));
                Miniboss3Encounters.Add(new CombatEncounter("the king's court"));
                // Boss
                BossEncounters.Add(new CombatEncounter("true winter king"));
                break;
            default:
                Console.WriteLine("ERROR: ZoneID not found");
                break;
        }
    }

    // Removes the given encounter from all pools.
    public void RemoveEncounter(CombatEncounter encounterToRemove) {
        Phase1CombatEncounters.Remove(encounterToRemove);
        Phase2CombatEncounters.Remove(encounterToRemove);
        Phase3CombatEncounters.Remove(encounterToRemove);
        Miniboss1Encounters.Remove(encounterToRemove);
        Miniboss2Encounters.Remove(encounterToRemove);
        Miniboss3Encounters.Remove(encounterToRemove);
        BossEncounters.Remove(encounterToRemove);
        Console.WriteLine("Removed combat '"+encounterToRemove.name+"' from all encounter pools.");
    }
}