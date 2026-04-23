
        public class PlayerData : EntityData {
            public string PersonalCard { get; set; }

            public int level { get; set; }
            public PlayerData() : base() {
                PersonalCard = "Basic Attack";
            }
        }