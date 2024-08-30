
        public class PlayerData {
            public string Name { get; set; }
            public string Description { get; set; }
            public int HP { get; set; }
            public string PersonalCard { get; set; }
            public List<string> ActionList { get; set; }

            public PlayerData() {
                Name = "MISSING NAME";
                Description = "MISSING DESCRIPTION";
                PersonalCard = "Basic Attack";
                ActionList = new List<string>();
            }
        }