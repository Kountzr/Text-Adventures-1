using System.Collections.Generic;

namespace Engine
{
    public class World
    {
       public static readonly List<Item> Items = new List<Item>(); 
         public static readonly List<Monster> Monsters = new List<Monster>(); 
         public static readonly List<Quest> Quests = new List<Quest>(); 
         public static readonly List<Location> Locations = new List<Location>();

         public const int UNSELLABLE_ITEM_PRICE = -1;

         public const int ITEM_ID_RUSTY_SWORD = 1; 
         public const int ITEM_ID_RAT_TAIL = 2; 
         public const int ITEM_ID_PIECE_OF_FUR = 3; 
         public const int ITEM_ID_SNAKE_FANG = 4; 
         public const int ITEM_ID_SNAKESKIN = 5; 
         public const int ITEM_ID_CLUB = 6; 
         public const int ITEM_ID_HEALING_POTION = 7; 
         public const int ITEM_ID_SPIDER_FANG = 8; 
         public const int ITEM_ID_SPIDER_SILK = 9; 
         public const int ITEM_ID_ADVENTURER_PASS = 10; 
 
 
         public const int MONSTER_ID_RAT = 1; 
         public const int MONSTER_ID_SNAKE = 2; 
         public const int MONSTER_ID_GIANT_SPIDER = 3; 
 
 
         public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1; 
         public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2; 
 
 
         public const int LOCATION_ID_HOME = 1; 
         public const int LOCATION_ID_TOWN_SQUARE = 2; 
         public const int LOCATION_ID_GUARD_POST = 3; 
         public const int LOCATION_ID_ALCHEMIST_HUT = 4; 
         public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5; 
         public const int LOCATION_ID_FARMHOUSE = 6; 
         public const int LOCATION_ID_FARM_FIELD = 7; 
         public const int LOCATION_ID_BRIDGE = 8; 
         public const int LOCATION_ID_SPIDER_FIELD = 9;
         public const int LOCATION_ID_IKOLA_VILLAGE_GATE = 10;
         public const int LOCATION_ID_IKOLA_TOWN_SQUARE = 11;
         public const int LOCATION_ID_FURS_AND_FINS = 12;
         public const int LOCATION_ID_BAKERY = 13;
         public const int LOCATION_ID_BLACKSMITH = 14;
         public const int LOCATION_ID_OUTPOST = 15;
         public const int LOCATION_ID_APOTHECARY = 16;
         public const int LOCATION_ID_INN = 17;
         public const int LOCATION_ID_SAFE_HOUSE = 18;
         public const int LOCATION_ID_MAYOR_HOUSE = 19;
         public const int LOCATION_ID_HOUSE1 = 20;
         public const int LOCATION_ID_HOUSE2 = 21;
         public const int LOCATION_ID_HOUSE3 = 22;
         public const int LOCATION_ID_HOUSE4 = 23;
         public const int LOCATION_ID_HOUSE5 = 24;
         public const int LOCATION_ID_HOUSE6 = 25;
         public const int LOCATION_ID_CAPTAINS_HOUSE = 26;
         public const int LOCATION_ID_BEACH_ROAD = 27;
         public const int LOCATION_ID_YAMI_PIER = 28;
         public const int LOCATION_ID_THE_DOCKS = 29;
         public const int LOCATION_ID_OLD_SHIP = 30;
         public const int LOCATION_ID_OS_CABIN = 31;
         public const int LOCATION_ID_THE_GARRISON = 32;
         public const int LOCATION_ID_FISHING_VESSEL = 33;
         public const int LOCATION_ID_BUNGULO = 34;
         public const int LOCATION_ID_LIGHTHOUSE = 35;
         public const int LOCATION_ID_NORTH_MARSH_ROAD = 36;
         public const int LOCATION_ID_THE_MARSHES = 37;
         public const int LOCATION_ID_OLD_HAGS_HUT = 38;
         public const int LOCATION_ID_IKOLA_CEMETARY = 39;
         public const int LOCATION_ID_INFINITY_CAVE = 40;
         public const int LOCATION_ID_THE_TOMB = 41;
         public const int LOCATION_ID_MARSH_TOWN_ROAD = 42;
         public const int LOCATION_ID_The_Hilltop = 43;
         public const int LOCATION_ID_IKOLA_TEMPLE = 44;
         public const int LOCATION_ID_FARM_ROAD = 45;
         public const int LOCATION_ID_OLD_FARMHOUSE = 46;
         public const int LOCATION_ID_THE_BARN = 47;
         public const int LOCATION_ID_FREMAS_FARM = 48;
         public const int LOCATION_ID_YAMI_WOODS = 49;
         public const int LOCATION_ID_ZERMOS_DEN = 50;
         public const int LOCATION_ID_CAVE_OF_THE_OLD_ONE = 51;
         public const int LOCATION_ID_THE_GROVE = 52;
 
         static World() 
         { 
             PopulateItems(); 
             PopulateMonsters(); 
             PopulateQuests(); 
             PopulateLocations(); 
         } 
 
 
         private static void PopulateItems() 
         { 
             Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, 5, 5)); 
             Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rat tail", "Rat tails", 1)); 
             Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Piece of fur", "Pieces of fur", 1)); 
             Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Snake fang", "Snake fangs", 1)); 
             Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snakeskin", "Snakeskins", 2)); 
             Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 3, 10, 8)); 
             Items.Add(new HealingPotion(ITEM_ID_HEALING_POTION, "Healing potion", "Healing potions", 5, 3)); 
             Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider fang", "Spider fangs", 1)); 
             Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Spider silk", "Spider silks", 1)); 
             Items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Adventurer pass", "Adventurer passes", UNSELLABLE_ITEM_PRICE)); 
         } 
 
 
         private static void PopulateMonsters() 
         { 
             Monster rat = new Monster(MONSTER_ID_RAT, "Rat", 5, 3, 10, 3, 3); 
             rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false)); 
             rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true)); 
 
 
             Monster snake = new Monster(MONSTER_ID_SNAKE, "Snake", 5, 3, 10, 3, 3); 
             snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false)); 
             snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true)); 
 
 
             Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER, "Giant spider", 20, 5, 40, 10, 10); 
             giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG), 75, true)); 
             giantSpider.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 25, false)); 
 
 
             Monsters.Add(rat); 
             Monsters.Add(snake); 
             Monsters.Add(giantSpider); 
         } 
 
 
         private static void PopulateQuests() 
         { 
             Quest clearAlchemistGarden = 
                 new Quest( 
                     QUEST_ID_CLEAR_ALCHEMIST_GARDEN, 
                     "Clear the alchemist's garden", 
                     "Kill rats in the alchemist's garden and bring back 3 rat tails. You will receive a healing potion and 10 gold pieces.", 20, 10); 
 
 
             clearAlchemistGarden.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL), 3)); 
 
 
             clearAlchemistGarden.RewardItem = ItemByID(ITEM_ID_HEALING_POTION); 
 
 
             Quest clearFarmersField = 
                 new Quest( 
                     QUEST_ID_CLEAR_FARMERS_FIELD, 
                     "Clear the farmer's field", 
                     "Kill snakes in the farmer's field and bring back 3 snake fangs. You will receive an adventurer's pass and 20 gold pieces.", 20, 20); 
 
 
             clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG), 3)); 
 
 
             clearFarmersField.RewardItem = ItemByID(ITEM_ID_ADVENTURER_PASS); 
 
 
             Quests.Add(clearAlchemistGarden); 
             Quests.Add(clearFarmersField); 
         } 
 
 
         private static void PopulateLocations() 
         { 
             // Create each location 
             Location home = new Location(LOCATION_ID_HOME, "Home", "Your house. You really need to clean up the place."); 
 
 
             Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town square", "You see a fountain.");

             Vendor bobTheRatCatcher = new Vendor("Bob the Rat-Catcher");
             bobTheRatCatcher.AddItemToInventory(ItemByID(ITEM_ID_PIECE_OF_FUR), 5);
             bobTheRatCatcher.AddItemToInventory(ItemByID(ITEM_ID_RAT_TAIL), 3);

             townSquare.VendorWorkingHere = bobTheRatCatcher;
 
             Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves."); 
             alchemistHut.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN); 
 
 
             Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "Many plants are growing here."); 
             alchemistsGarden.MonsterLivingHere = MonsterByID(MONSTER_ID_RAT); 
 
 
             Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "There is a small farmhouse, with a farmer in front."); 
             farmhouse.QuestAvailableHere = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD); 
 
 
             Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here."); 
             farmersField.MonsterLivingHere = MonsterByID(MONSTER_ID_SNAKE); 
 
 
             Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here.", ItemByID(ITEM_ID_ADVENTURER_PASS)); 
 
 
             Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river."); 
 
 
             Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering covering the trees in this forest."); 
             spiderField.MonsterLivingHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);

             Location ikolaVillage = new Location(LOCATION_ID_IKOLA_VILLAGE_GATE, "Ikola Village Gate", "You arrive at the neighboring town of Ikola.");

             Location ikolaSquare = new Location(LOCATION_ID_IKOLA_TOWN_SQUARE, "Ikola Town Square", "You arrived in Ikola Town square. It is filled with merchant shops and busy residents.");

             Location finsAndFur = new Location(LOCATION_ID_FURS_AND_FINS, "Furs and Fins Trade Post", "You entered a trading post the specializes in clothing and gear. A burly man with a mighty beard stares at you as you enter.");

             Location bakery = new Location(LOCATION_ID_BAKERY, "Ikola Bakery", "You entered the local bakery. The smell of freshly baked goods is pleasing.");

             Location blackSmith = new Location(LOCATION_ID_BLACKSMITH, "Ikola Blacksmith", "The blacksmith's shop is fiercely hot and filled with the sound of clanging metal.");
 
             // Link the locations together 
             home.LocationToNorth = townSquare; 
 
 
             townSquare.LocationToNorth = alchemistHut; 
             townSquare.LocationToSouth = home; 
             townSquare.LocationToEast = guardPost; 
             townSquare.LocationToWest = farmhouse; 
 
 
             farmhouse.LocationToEast = townSquare; 
             farmhouse.LocationToWest = farmersField; 
 
 
             farmersField.LocationToEast = farmhouse; 
 
 
             alchemistHut.LocationToSouth = townSquare; 
             alchemistHut.LocationToNorth = alchemistsGarden; 
 
 
             alchemistsGarden.LocationToSouth = alchemistHut; 
 
 
             guardPost.LocationToEast = bridge; 
             guardPost.LocationToWest = townSquare; 
 
 
             bridge.LocationToWest = guardPost; 
             bridge.LocationToEast = spiderField; 
 
 
             spiderField.LocationToWest = bridge;
             spiderField.LocationToEast = ikolaVillage;

             ikolaVillage.LocationToWest = spiderField;
             ikolaVillage.LocationToEast = ikolaSquare;
             
             ikolaSquare.LocationToNorth = finsAndFur;
             ikolaSquare.LocationToSouth = bakery;

             bakery.LocationToNorth = ikolaSquare;
             finsAndFur.LocationToSouth = ikolaSquare;
 
             // Add the locations to the static list 
             Locations.Add(home); 
             Locations.Add(townSquare); 
             Locations.Add(guardPost); 
             Locations.Add(alchemistHut); 
             Locations.Add(alchemistsGarden); 
             Locations.Add(farmhouse); 
             Locations.Add(farmersField); 
             Locations.Add(bridge); 
             Locations.Add(spiderField);
             Locations.Add(ikolaVillage);
             Locations.Add(ikolaSquare);
             Locations.Add(finsAndFur);
             Locations.Add(bakery);
         } 
 
 
         public static Item ItemByID(int id) 
         { 
             foreach(Item item in Items) 
             { 
                 if(item.ID == id) 
                 { 
                     return item; 
                 } 
             } 
 
 
             return null; 
         } 
 
 
         public static Monster MonsterByID(int id) 
         { 
             foreach(Monster monster in Monsters) 
             { 
                 if(monster.ID == id) 
                 { 
                     return monster; 
                 } 
             } 
 
 
             return null; 
         } 
 
 
         public static Quest QuestByID(int id) 
         { 
             foreach(Quest quest in Quests) 
             { 
                 if(quest.ID == id) 
                 { 
                     return quest; 
                 } 
             } 
 
 
             return null; 
         } 

        public static Location LocationByID(int id)
         {
             foreach (Location location in Locations)
             {
                 if (location.ID == id)
                 {
                     return location;
                 }
             }
             return null;
         }
    }
}
