using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using Engine;

namespace SuperAdventure
{
    public partial class SuperAdventure : Form
    {
        private Player _player;
        private Monster _currentMonster;
        private const string PLAYER_DATA_FILE_NAME = "PlayerData.xml";

        public SuperAdventure()
        {
            InitializeComponent();

            if (File.Exists(PLAYER_DATA_FILE_NAME))
            {
                _player = Player.CreatePlayerFromXmlString(File.ReadAllText(PLAYER_DATA_FILE_NAME));
            }
            else
            {
                _player = Player.CreateDefaultPlayer();
            }

            lblHitPoints.DataBindings.Add("Text", _player, "CurrentHitPoints");
            lblGold.DataBindings.Add("Text", _player, "Gold");
            lblExperience.DataBindings.Add("Text", _player, "ExperiencePoints");
            lblLevel.DataBindings.Add("Text", _player, "Level");

            dgvInventory.RowHeadersVisible = false;
            dgvInventory.AutoGenerateColumns = false;

            dgvInventory.DataSource = _player.Inventory;

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 197,
                DataPropertyName = "Description"
            });

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quantity",
                DataPropertyName = "Quantity"
            });

             dgvQuests.RowHeadersVisible = false; 
             dgvQuests.AutoGenerateColumns = false; 
 
 
             dgvQuests.DataSource = _player.Quests; 
 
 
             dgvQuests.Columns.Add(new DataGridViewTextBoxColumn 
             { 
                 HeaderText = "Name", 
                 Width = 197, 
                 DataPropertyName = "Name" 
             }); 
 
 
             dgvQuests.Columns.Add(new DataGridViewTextBoxColumn 
             { 
                 HeaderText = "Done?", 
                 DataPropertyName = "IsCompleted" 
             });

             cboWeapons.DataSource = _player.Weapons;
             cboWeapons.DisplayMember = "Name";
             cboWeapons.ValueMember = "Id";

             if (_player.CurrentWeapon != null)
             {
                 cboWeapons.SelectedItem = _player.CurrentWeapon;
             }

             cboWeapons.SelectedIndexChanged += cboWeapons_SelectedIndexChanged;

             cboPotions.DataSource = _player.Potions;
             cboPotions.DisplayMember = "Name";
             cboPotions.ValueMember = "Id";

             _player.PropertyChanged += PlayerOnPropertyChanged;
             _player.OnMessage += DisplayMessage;

             _player.MoveTo(_player.CurrentLocation);
        }

        private void DisplayMessage(object sender, MessageEventArgs messageEventArgs)
        {
            rtbMessages.Text += messageEventArgs.Message + Environment.NewLine;

            if (messageEventArgs.AddExtraNewLine)
            {
                rtbMessages.Text += Environment.NewLine;
            }

            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void SuperAdventure_Load(object sender, EventArgs e)
        {

        }

        private void cboWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            _player.CurrentWeapon = (Weapon)cboWeapons.SelectedItem;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            _player.MoveNorth();

        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            _player.MoveEast();
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            _player.MoveSouth();
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            _player.MoveWest();
        }

        private void PlayerOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "Weapons")
            {
                cboWeapons.DataSource = _player.Weapons;

                if (!_player.Weapons.Any())
                {
                    cboWeapons.Visible = false;
                    btnUseWeapon.Visible = false;
                }
            }

            if (propertyChangedEventArgs.PropertyName == "Potions")
            {
                cboPotions.DataSource = _player.Potions;

                if (!_player.Potions.Any())
                {
                    cboPotions.Visible = false;
                    btnUsePotion.Visible = false;
                }
            }

            if (propertyChangedEventArgs.PropertyName == "CurrentLocation")
            {
                // Show/hide available movement buttons
                btnNorth.Visible = (_player.CurrentLocation.LocationToNorth != null);
                btnEast.Visible = (_player.CurrentLocation.LocationToEast != null);
                btnSouth.Visible = (_player.CurrentLocation.LocationToSouth != null);
                btnWest.Visible = (_player.CurrentLocation.LocationToWest != null);

                // Display current location name and description
                rtbLocation.Text = _player.CurrentLocation.Name + Environment.NewLine;
                rtbLocation.Text += _player.CurrentLocation.Description + Environment.NewLine;

                if (_player.CurrentLocation.MonsterLivingHere == null)
                {
                    cboWeapons.Visible = false;
                    cboPotions.Visible = false;
                    btnUseWeapon.Visible = false;
                    btnUsePotion.Visible = false;
                }
                else
                {
                    cboWeapons.Visible = _player.Weapons.Any();
                    cboPotions.Visible = _player.Potions.Any();
                    btnUseWeapon.Visible = _player.Weapons.Any();
                    btnUsePotion.Visible = _player.Potions.Any();
                }
            }


        }


        private void MoveTo(Location newLocation)
        {
            //Does the Location have any required items
           if(!_player.HasRequiredItemToEnterThisLocation(newLocation))
           {
               rtbMessages.Text += "You must have a " + newLocation.ItemRequiredToEnter.Name + " to enter this location. " + Environment.NewLine;
               return;
           }

            // Update the player's current location
            _player.CurrentLocation = newLocation;


            // Show/hide available movement buttons
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            //Display location name and description
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            // Completely Heal the Player
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            //Does the location have a quest?
            if(newLocation.QuestAvailableHere != null)
            {
                //See if the player already has the quest and if they completed it
                bool playerAlreadyHasQuest = _player.HasThisQuest(newLocation.QuestAvailableHere);
                bool playerAlreadyCompletedQuest = _player.CompletedThisQuest(newLocation.QuestAvailableHere);

            //See if the player already has the quest
            if(playerAlreadyHasQuest)
            {
                // If player has not completed the quest yet
                if(!playerAlreadyCompletedQuest)
                {
                    //See if the player has all the items needed to complete the quest
                    bool playerHasAllItemsToCompleteQuest = _player.HasAllQuestCompletionItems(newLocation.QuestAvailableHere);

                        //player has all the items needed to complete the quest
                        if(playerHasAllItemsToCompleteQuest)
                        {
                            //Display Message
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + newLocation.QuestAvailableHere.Name + "' quest." + Environment.NewLine;

                            //Remove quest items from inventory
                            _player.RemoveQuestCompletionItems(newLocation.QuestAvailableHere);
                            
                            // Give quest rewards
                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardGold.ToString() + " gold" + Environment.NewLine;
                            rtbMessages.Text += newLocation.QuestAvailableHere.RewardItem.Name + Environment.NewLine;
                            rtbMessages.Text += Environment.NewLine;

                           _player.AddExperiencePoints(newLocation.QuestAvailableHere.RewardExperiencePoints);
                           _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                            //add the reward item to player's inventory
                           _player.AddItemToInventory(newLocation.QuestAvailableHere.RewardItem);

                        //Mark the quest complete
                        //Find the quest in the player's quest list
                         _player.MarkQuestCompleted(newLocation.QuestAvailableHere);
                    }
                }
            }
            else
            {
                //The Player does not already have the quest

                //Display the messages
                rtbMessages.Text += "You receive the " + newLocation.QuestAvailableHere.Name + "quest." + Environment.NewLine;
                rtbMessages.Text += newLocation.QuestAvailableHere.Description + Environment.NewLine;
                rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                foreach(QuestCompletionItem qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                {
                    if (qci.Quantity == 1)
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.Name + Environment.NewLine;

                    }
                    else
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " + qci.Details.NamePlural + Environment.NewLine;
                    }
                }
                rtbMessages.Text += Environment.NewLine;

                //Add the quest to the player's quest list
                _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
            }
        }
        //Does this location have a monster?
        if(newLocation.MonsterLivingHere != null)
         {
            rtbMessages.Text += "You see a " + newLocation.MonsterLivingHere.Name + Environment.NewLine;

            //Make a new Monster, using the values from the standard monster in the World.Monster list
            Monster standardMonster = World.MonsterByID(newLocation.MonsterLivingHere.ID);

             _currentMonster = new Monster(standardMonster.ID, standardMonster.Name, standardMonster.MaximumDamage,
             standardMonster.RewardExperiencePoints, standardMonster.RewardGold, standardMonster.CurrentHitPoints, standardMonster.MaximumHitPoints);

             foreach(LootItem lootItem in standardMonster.LootTable)
             {
                _currentMonster.LootTable.Add(lootItem);
             }
                cboWeapons.Visible = _player.Weapons.Any();
                cboPotions.Visible = _player.Potions.Any();
                btnUseWeapon.Visible = _player.Weapons.Any();
                btnUsePotion.Visible = _player.Potions.Any();
           }
           else
        {
    
            _currentMonster = null;
    
            cboWeapons.Visible = false;
            cboPotions.Visible = false;
            btnUseWeapon.Visible = false;
            btnUsePotion.Visible = false;

        }
          
    
         }

     
        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            // Get the currently selected weapon from the cboWeapons ComboBox
            Weapon currentWeapon = (Weapon)cboWeapons.SelectedItem;

            _player.UseWeapon(currentWeapon);

        }

        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            // Get the currently selected potion from the combobox
            HealingPotion potion = (HealingPotion)cboPotions.SelectedItem;

            _player.UsePotion(potion);

        }

        private void rtbMessages_TextChanged(object sender, EventArgs e)
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void SuperAdventure_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(PLAYER_DATA_FILE_NAME, _player.ToXmlString());
        }


     }
}
