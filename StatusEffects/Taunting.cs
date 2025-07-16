public class Taunting : StatusEffect {

    
    public Taunting(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Taunting";
        this.description = "Your allies cannot be targeted by enemies for that many turns.";
        this.owner = owner;
    }

    public override void onApplied() {
        // Add owner to Taunters list:
        Battlefield.Taunters.Add(owner!);
        // If this is on a player controlled entity, update enemy targets:
        if(owner!.playerControlled) {
            // Go through all enemies, and for those that target allies, change the target
            foreach(Enemy enemy in Battlefield.EnemySide) {
                // Skip enemies whose next action ignores taunt:
                if(enemy != null && !enemy.getNextAction().ignoresTaunt) {
                    enemy.setNextTarget(Battlefield.PlayerSide.FindIndex(a => a.name == owner!.name));
                }
            }
        }
    }

    public override void onRemoved() {
        // Remove owner from Taunters list:
        Battlefield.Taunters.Remove(owner!);
    }

    public override void onDeath() {
        // Remove owner from Taunters list:
        Battlefield.Taunters.Remove(owner!);
    }

    // Decrement every turn
    public override void endOfTurn() {
        this.Decrease(1);
    }
}