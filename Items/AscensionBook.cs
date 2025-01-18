public class AscensionBook : Item {


    public AscensionBook() {
        this.name = "Ascension Book";
        this.description = "This book shifts reality itself, allowing an individual to become a more powerful version of themself.";
    }

    // Levels up a character.
    // First step is to generate the possible options; offer them to the player, and select one.
    // Then, the character is removed from the party and replaced with the leveled-up version.
    // Their items are all returned to the inventory.
    public void use(PlayerCharacter hero) {
        // TODO: level up logic
    }
}