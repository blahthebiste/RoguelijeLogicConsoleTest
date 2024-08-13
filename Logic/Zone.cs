public class Zone {
    public List<CombatEncounter> CombatEncounters = new List<CombatEncounter>();

    public Zone(ZoneID zoneID) {
        // TODO: add combat encounters
        switch(zoneID) {
            case ZoneID.HUB:
                break;
            case ZoneID.ZONE1:
                CombatEncounters.Add(new CombatEncounter("pengoons"));
                break;
            default:
                break;
        }
    }
}