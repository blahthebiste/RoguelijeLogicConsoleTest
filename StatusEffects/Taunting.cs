public class Taunting : StatusEffect {

    
    public Taunting(int amount, Entity owner) {
        this.amount = amount;
        this.name = "Taunting";
        this.description = "Your allies cannot be targeted by enemies for that many turns.";
        this.owner = owner;
    }

    public override void onApplied()
    {
        // Add owner to Taunters list:
        Battlefield.Taunters.Add(owner!);
        // If this is on a player side entity, update enemy targets:
        if (!owner!.hostile)
        {
            // Go through all enemies, and for those that target allies, change the target
            foreach (Entity enemy in Battlefield.EnemySide)
            {
                // Skip enemies whose next action ignores taunt:
                if (enemy.nextTarget != null && Battlefield.PlayerSide.Contains(enemy.nextTarget) && !enemy.getNextAction().IgnoresTaunt())
                {
                    enemy.setNextTarget(owner!);
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
    public override void startOfTurn() {
        this.Decrease(1);
    }
}