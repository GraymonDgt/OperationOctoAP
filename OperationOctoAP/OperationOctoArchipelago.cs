using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using MelonLoader;
using Newtonsoft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace OperationOctoAP
{

    public class OperationOctoAPMod : MelonMod
    {
        public GameObject canvaslevelselectGl;
        public GameObject gameManagerGl;
        public FeatureDatabase elixirUnlock = new FeatureDatabase()
        {
            myName = "Elixers",
            id = 0,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.ElixirBottle,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase shopUnlock = new FeatureDatabase()
        {
            myName = "Shop",
            id = 2,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.System,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase mapUnlock = new FeatureDatabase()
        {
            myName = "Map",
            id = 3,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.System,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase bucketUnlock = new FeatureDatabase()
        {
            myName = "Treat Bucket",
            id = 5,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.System,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase telescopeUnlock = new FeatureDatabase()
        {
            myName = "Telescope",
            id = 8,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.Upgrade,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase frostUnlock = new FeatureDatabase()
        {
            myName = "Frost Elixir",
            id = 100,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.ElixirBottle,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase mutantUnlock = new FeatureDatabase()
        {
            myName = "Mutant Elixir",
            id = 101,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.ElixirBottle,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase hallowUnlock = new FeatureDatabase()
        {
            myName = "Hallow Elixir",
            id = 102,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.ElixirBottle,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase sandywUnlock = new FeatureDatabase()
        {
            myName = "Sandy Shallows",
            id = 200,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.Biome,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase toxicUnlock = new FeatureDatabase()
        {
            myName = "Toxic Wasteland",
            id = 201,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.Biome,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase twilightUnlock = new FeatureDatabase()
        {
            myName = "Twilight Zone",
            id = 202,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.Biome,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };
        public FeatureDatabase volcanicUnlock = new FeatureDatabase()
        {
            myName = "Volcanic Ridge",
            id = 203,
            prerequisiteLevel = null,
            featureType = FeatureDatabase.FeatureType.Biome,
            desc = "Hopefully this doesn't matter",
            alsoUnlockGarageRegion = false,
            garageIdToUnlock = 0
        };

        public const int maxItems = 210;
        public const int maxLocations = 160;
        public static int currentPlayerSlot;
        public static bool[] unlockedArray = new bool[maxItems];
        public static bool[] checkedArray = new bool[maxLocations];


        public PlayerHelper playerInfo;//this line
        public static ArchipelagoSession session = null;
        int curretShopCheck = 0;

        public override void OnLateInitializeMelon()
        {
            string unity3dpath = System.IO.Path.Combine(UnityEngine.Application.dataPath, "data.unity3d");
            string modDir = this.MelonAssembly.Location;
            string modFolder = System.IO.Path.GetDirectoryName(modDir);
            string jsonPath = System.IO.Path.Combine(modFolder, "config.json");

            if (gameManagerGl == null)
            {
                gameManagerGl = GameObject.Find("GameManager");
                if (gameManagerGl == null)
                { return; }
            }
            var ProgHelper = gameManagerGl.GetComponent<PlayerProgression>();
            for (int i = 1; i < 37; i++)
            {
                if (ProgHelper.SaveDataExistsForTurret(i, out var saveData))
                {
                    saveData.unlocked = false;
                    saveData.revealed = false;

                }
            }
            SaveData_Feature featSaveData = null;


            if (ProgHelper.SaveDataExistsForFeature(0, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(2, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(3, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(5, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(8, out featSaveData))
            {
                featSaveData.unlocked = false;
            }


            if (ProgHelper.SaveDataExistsForFeature(100, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(101, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(102, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(201, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(202, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            if (ProgHelper.SaveDataExistsForFeature(203, out featSaveData))
            {
                featSaveData.unlocked = false;
            }
            //UniDatabaseRef.allTurrets


            if (System.IO.File.Exists(jsonPath))
            {
                string jsonContent = System.IO.File.ReadAllText(jsonPath);
                // Deserialize if needed
                APData data = JsonConvert.DeserializeObject<APData>(jsonContent);
                MelonLogger.Msg("Loaded JSON data");
                session = ArchipelagoSessionFactory.CreateSession(data.serverAddress, data.serverPort);
                Connect("Operation Octo", data.slotName, data.password, session);
            }
            else
            {
                MelonLogger.Warning("config.json not found, place it in the same folder as OperationOctoAP.dll");
            }
            CheckForNewItems();
        }

        //    public class FeatureDatabase : ScriptableObject
        //{
        //    public enum FeatureType
        //    {
        //        System,
        //        ElixirBottle,
        //        Upgrade,
        //        MenuOpt,
        //        Biome,
        //        GarageRegion,
        //        ShopSupplier
        //    }
        //
        //    public string myName;
        //
        //    public int id;
        //
        //    public LevelDatabase prerequisiteLevel;
        //
        //    public FeatureType featureType;
        //
        //    [Header("Texts")]
        //    [TextArea(1, 3)]
        //    public string desc;
        //
        //    [Header("Associated Unlocks")]
        //    public bool alsoUnlockGarageRegion;
        //
        //    public int garageIdToUnlock;
        //}







        public override void OnUpdate()
        {

            if (gameManagerGl == null)
            {
                gameManagerGl = GameObject.Find("GameManager");
                if (gameManagerGl == null)
                { return; }
            }




            var ProgHelper = gameManagerGl.GetComponent<PlayerProgression>();
            if (Input.GetKeyDown(KeyCode.L))
            {




                //UniDatabaseRef.allTurrets
            }

            
                



            CheckForNewItems();

            //todo remove all features/turrets that may have been unlocked normally

            //ModifySaveDataForTurret(0, delegate (SaveData_Turret d)
            //{
            //    d.unlocked = true;
            //    d.revealed = true;
            //});


        }



        private void OnErrorReceived(System.Exception e, string message)
        {
            // The message passed by the event
            MelonLogger.Msg(e);
            MelonLogger.Msg(message);
        }

        public void Connect(string game, string user, string pass, ArchipelagoSession session)
        {
            LoginResult result;
            session.Socket.ErrorReceived += OnErrorReceived;
            try
            {
                // handle TryConnectAndLogin attempt here and save the returned object to `result`
                result = session.TryConnectAndLogin(game, user, ItemsHandlingFlags.AllItems);
            }
            catch (System.Exception e)
            {
                result = new LoginFailure(e.GetBaseException().Message);

            }
            
            if (!result.Successful)
            {
                LoginFailure failure = (LoginFailure)result;
                string errorMessage = $"Failed to Connect to {game} as {user}:";
                foreach (string error in failure.Errors)
                {
                    errorMessage += $"\n    {error}";
                }
                foreach (var error in failure.ErrorCodes)
                {
                    errorMessage += $"\n    {error}";
                }
                MelonLogger.Msg(errorMessage);
                

                

                return; // Did not connect, show the user the contents of `errorMessage`
            }


            //Dictionary<string, object> slotData = new Dictionary<string, object>();

            // Successfully connected, `ArchipelagoSession` (assume statically defined as `session` from now on) can now be
            // used to interact with the server and the returned `LoginSuccessful` contains some useful information about the
            // initial connection (e.g. a copy of the slot data as `loginSuccess.SlotData`)
            MelonLogger.Msg("Successfully connected to archipelago");
            var loginSuccess = (LoginSuccessful)result;

            //ringLinkMode = session.DataStorage.GetSlotData();

            var slotData = session.DataStorage.GetSlotData();

            //session.Socket.PacketReceived += OnPacketReceived;



            ILocationCheckHelper locationHelper = session.Locations;

            var checkedLocations = locationHelper.AllLocationsChecked;

            var count = checkedLocations.Count;
            for (int i = 0; i < count; i++)
            {
                //MelonLogger.Msg(checkedLocations[i].ToString());
                checkedArray[checkedLocations[i]] = true;


            }

        }

        private void CheckForNewItems()
        {

            while (session.Items.Any())
            {
                var networkItem = session.Items.DequeueItem();

                //if (networkItem.ItemId == null)
                //{ continue; }
                if (networkItem.ItemId < maxItems)
                { unlockedArray[networkItem.ItemId] = true; }

                var ProgHelper = gameManagerGl.GetComponent<PlayerProgression>();


                if (networkItem.ItemId == 1)//pistol shrimp
                {ProgHelper.TurretUnlock(0);}
                if (networkItem.ItemId == 2)//clam
                {ProgHelper.TurretUnlock(1);}
                if (networkItem.ItemId == 3)//bumper
                { ProgHelper.TurretUnlock(2); }
                if (networkItem.ItemId == 4)//blobfish
                { ProgHelper.TurretUnlock(3); }
                if (networkItem.ItemId == 5)//bolt eel
                { ProgHelper.TurretUnlock(4); }
                if (networkItem.ItemId == 6)//turret clam
                { ProgHelper.TurretUnlock(5); }
                if (networkItem.ItemId == 7)//mussel
                { ProgHelper.TurretUnlock(6); }
                if (networkItem.ItemId == 8)//bubble parrot
                { ProgHelper.TurretUnlock(7); }
                if (networkItem.ItemId == 9)//pleco
                { ProgHelper.TurretUnlock(8); }
                if (networkItem.ItemId == 10)//argonaut
                { ProgHelper.TurretUnlock(31); }
                if (networkItem.ItemId == 11)//spiky urchin
                { ProgHelper.TurretUnlock(26); }
                if (networkItem.ItemId == 12)//red herring
                { ProgHelper.TurretUnlock(27); }
                if (networkItem.ItemId == 13)//imitation crab
                { ProgHelper.TurretUnlock(28); }
                if (networkItem.ItemId == 14)//lionfish
                { ProgHelper.TurretUnlock(29); }
                if (networkItem.ItemId == 15)//zappy jelly
                { ProgHelper.TurretUnlock(9); }
                if (networkItem.ItemId == 16)//heal snail
                { ProgHelper.TurretUnlock(10); }
                if (networkItem.ItemId == 17)//hermit holster
                { ProgHelper.TurretUnlock(11); }
                if (networkItem.ItemId == 18)//sniper turtle
                { ProgHelper.TurretUnlock(12); }
                if (networkItem.ItemId == 19)//crab trap
                { ProgHelper.TurretUnlock(13); }
                if (networkItem.ItemId == 20)//navy seal
                { ProgHelper.TurretUnlock(14); }
                if (networkItem.ItemId == 21)//pearl tree
                { ProgHelper.TurretUnlock(15); }
                if (networkItem.ItemId == 22)//everywhere eel
                { ProgHelper.TurretUnlock(34); }
                if (networkItem.ItemId == 23)//frogfish
                { ProgHelper.TurretUnlock(30); }
                if (networkItem.ItemId == 24)//lantern fish
                { ProgHelper.TurretUnlock(16); }
                if (networkItem.ItemId == 25)//death ray
                { ProgHelper.TurretUnlock(17); }
                if (networkItem.ItemId == 26)//hammer shark
                { ProgHelper.TurretUnlock(18); }
                if (networkItem.ItemId == 27)//lobby leviathan
                { ProgHelper.TurretUnlock(19); }
                if (networkItem.ItemId == 28)//charge station
                { ProgHelper.TurretUnlock(20); }
                if (networkItem.ItemId == 29)//siren
                { ProgHelper.TurretUnlock(21); }
                if (networkItem.ItemId == 30)//shooting star
                { ProgHelper.TurretUnlock(22); }
                if (networkItem.ItemId == 31)//watchdog
                { ProgHelper.TurretUnlock(23); }
                if (networkItem.ItemId == 32)//harvest barreleye
                { ProgHelper.TurretUnlock(25); }



                // zappy jelly 9
                // heal snail 10
                // hermit 11
                // sniper 12
                // crab 13
                // seal 14
                // pearltree 15
                // lanternfish - 16
                // death ray - 17
                // hammer -18
                // leviathan - 19
                // charger -20 probably
                // siren - 21
                // star - 22
                // watchdog 23
                // barreleye -25

                //spiky urchin - 26
                // red herring -27
                //lionfish - 29
                //frogfish -30
                //argonaut -31
                //eve eel -34


                if (networkItem.ItemId == 101)
                { ProgHelper.FeatureUnlock(elixirUnlock); }
                if (networkItem.ItemId == 102)
                { ProgHelper.FeatureUnlock(shopUnlock);
                    session.Locations.CompleteLocationChecks(150 + this.curretShopCheck);
                    session.Locations.CompleteLocationChecks(151 + this.curretShopCheck);
                    session.Locations.CompleteLocationChecks(152 + this.curretShopCheck);
                    this.curretShopCheck+=3;

                }
                if (networkItem.ItemId == 103)
                {
                    ProgHelper.FeatureUnlock(mapUnlock);
                    ProgHelper.FeatureUnlock(toxicUnlock);
                }
                if (networkItem.ItemId == 104)
                { ProgHelper.FeatureUnlock(bucketUnlock); }
                if (networkItem.ItemId == 105)
                { ProgHelper.FeatureUnlock(telescopeUnlock); }
                if (networkItem.ItemId == 150)
                { ProgHelper.FeatureUnlock(frostUnlock); }
                if (networkItem.ItemId == 151)
                { ProgHelper.FeatureUnlock(mutantUnlock); }
                if (networkItem.ItemId == 152)
                { ProgHelper.FeatureUnlock(hallowUnlock); }
                if (networkItem.ItemId == 202)
                { ProgHelper.FeatureUnlock(twilightUnlock); }
                if (networkItem.ItemId == 203)
                { ProgHelper.FeatureUnlock(volcanicUnlock); }

                //skip 32-33







            }
        }

        [HarmonyPatch(typeof(ShopWindow), "Awake")]

        public static class Patch0
        {
            private static void Postfix()
            {//remove all current items in shop
             //use CreateNewCardAtSlot(ShopItemDatabase database, BasicSlot targSlot, GameObject targSlotRow, out ShopItemCard cardScript, bool adjustPosImmediately = false)
             //create 'ap items' that use some variable to track which check id it is
             //add hook to purchase button thats sends checks
                MelonLogger.Msg("Shop awoken");

                    GameObject canvaslevelselect = GameObject.Find("Canvas_LevelSel");
                if (canvaslevelselect != null)
                {

                    //for loops to delete any existing shop items

                    var shopHelper = canvaslevelselect.transform.Find("window_shop").GetComponent<ShopWindow>();


                    CollectibleDatabase colData = new CollectibleDatabase
                    {
                        desc = "A magical cluster of pearls from a far off archipelago.\nWho knows what these could turn into.",
                        displayOnHoverInfo = true


                    };

                    ShopItemDatabase database = new ShopItemDatabase
                    {
                        desc = "A magical cluster of pearls from a far off archipelago.\nWho knows what these could turn into.",
                        name = "Archipelago pearl cluster",
                        price = 10,
                        currency = CurrencyType.Coin,
                        isVariableCost = false,
                        saleContentType = ShopItemDatabase.SaleContentType.Collectible,
                        isInfinite = false,
                        isDaily = false,
                        collectibleData = colData
                    };

                    var targSlotRow = canvaslevelselect.transform.Find("window_shop/options_mask/optionGrid/shopSlot_row(Clone)").gameObject;

                    shopHelper.CreateNewCardAtSlot(database, new BasicSlot { }, targSlotRow, out ShopItemCard cardScript);

                    //shopHelper.CreateNewCardAtSlot


            }

            }

        }





            [HarmonyPatch(typeof(RewardHandler), "HandleEndOfLevelReward")]
        public static class Patch
        {
            private static void Postfix()
            {

                var canvas = GameObject.Find("Canvas_Battle");
                var levelInfo = canvas.transform.Find("ProgBar/levelName").GetComponent<TextMeshProUGUI>();
                switch (levelInfo.text)
                {

                    case "1-1 Initial Test":
                        session.Locations.CompleteLocationChecks(1);
                        break;
                    case "1-2 Four-Way Traffic":
                        session.Locations.CompleteLocationChecks(2);
                        break;
                    case "1-3 Cannons Aweigh":
                        session.Locations.CompleteLocationChecks(3);
                        break;
                    case "1-4 Bumper Encounter":
                        session.Locations.CompleteLocationChecks(4);
                        break;
                    case "1-5 Bumper Maze":
                        session.Locations.CompleteLocationChecks(5);
                        break;
                    case "1-6 Undead Armory":
                        session.Locations.CompleteLocationChecks(6);
                        break;
                    case "1-7 Frosty Treat":
                        session.Locations.CompleteLocationChecks(7);
                        break;
                    case "1-8 Firework":
                        session.Locations.CompleteLocationChecks(8);
                        break;
                    case "1-9 Electro Dynamo":
                        session.Locations.CompleteLocationChecks(9);
                        break;
                    case "1-10 Free Shipment":
                        session.Locations.CompleteLocationChecks(10);
                        break;
                    case "1-11 Flying Scoundrels":
                        session.Locations.CompleteLocationChecks(11);
                        break;
                    case "1-12 Tunnel Hideout":
                        session.Locations.CompleteLocationChecks(12);
                        break;
                    case "1-13 Warhead Pursuit":
                        session.Locations.CompleteLocationChecks(13);
                        break;
                    case "1-14 Sandy Shoal-down":
                        session.Locations.CompleteLocationChecks(14);
                        break;
                    case "Crâne Exalté":
                        session.Locations.CompleteLocationChecks(15);
                        break;
                    case "2-1 Alley of Corrosion":
                        session.Locations.CompleteLocationChecks(16);
                        break;
                    case "2-2 Twirling Riverbank":
                        session.Locations.CompleteLocationChecks(17);
                        break;
                    case "2-3 Fields of Green":
                        session.Locations.CompleteLocationChecks(18);
                        break;
                    case "2-4 Gastro Therapy":
                        session.Locations.CompleteLocationChecks(19);
                        break;
                    case "2-5 Clam Farm":
                        session.Locations.CompleteLocationChecks(20);
                        break;
                    case "2-6 Scorched Earth":
                        session.Locations.CompleteLocationChecks(21);
                        break;
                    case "2-7 Rampant Mutation":
                        session.Locations.CompleteLocationChecks(22);
                        break;
                    case "2-8 Diver Cavern":
                        session.Locations.CompleteLocationChecks(23);
                        break;
                    case "2-9 Scuttling Trail":
                        session.Locations.CompleteLocationChecks(24);
                        break;
                    case "2-10 Sergeant's Plateau":
                        session.Locations.CompleteLocationChecks(25);
                        break;
                    case "2-11 Swampy Rockery":
                        session.Locations.CompleteLocationChecks(26);
                        break;
                    case "2-12 Graffiti Gallery":
                        session.Locations.CompleteLocationChecks(27);
                        break;
                    case "2-13 Hazard Treeyard":
                        session.Locations.CompleteLocationChecks(28);
                        break;
                    case "2-14 Lago Toxico":
                        session.Locations.CompleteLocationChecks(29);
                        break;
                    case "Dr. Kraxen":
                        session.Locations.CompleteLocationChecks(30);
                        break;
                    case "3-1 Nightfall":
                        session.Locations.CompleteLocationChecks(31);
                        break;
                    case "3-2 Light The Way":
                        session.Locations.CompleteLocationChecks(32);
                        break;
                    case "3-3 Phantasmic Apparition":
                        session.Locations.CompleteLocationChecks(33);
                        break;
                    case "3-4 Mesopelagic March":
                        session.Locations.CompleteLocationChecks(34);
                        break;
                    case "3-5 Graveyard Shift":
                        session.Locations.CompleteLocationChecks(35);
                        break;
                    case "3-6 Spectral Mausoleum":
                        session.Locations.CompleteLocationChecks(36);
                        break;
                    case "3-7 Watery Grave":
                        session.Locations.CompleteLocationChecks(37);
                        break;
                    case "3-8 Terminal Omega":
                        session.Locations.CompleteLocationChecks(38);
                        break;
                    case "3-9 Tangled Paths":
                        session.Locations.CompleteLocationChecks(39);
                        break;
                    case "3-10 Leviathan Trench":
                        session.Locations.CompleteLocationChecks(40);
                        break;
                    case "3-11 Abyssal Chorals":
                        session.Locations.CompleteLocationChecks(41);
                        break;
                    case "3-12 Eerie Distillery":
                        session.Locations.CompleteLocationChecks(42);
                        break;
                    case "3-13 Hazard Treeyard":
                        session.Locations.CompleteLocationChecks(43);
                        break;
                    case "3-14 Starry Midnight":
                        session.Locations.CompleteLocationChecks(44);
                        break;
                    case "Captain Dallons":
                        session.Locations.CompleteLocationChecks(45);
                        session.Socket.SendPacket(new StatusUpdatePacket() { Status = ArchipelagoClientState.ClientGoal });
                        break;



                    default:
                        MelonLogger.Msg("Level not implemented");
                        break;

                }
                MelonLogger.Msg(levelInfo.text);
                GameObject gameManagerGl = GameObject.Find("GameManager");
                var ProgHelper = gameManagerGl.GetComponent<PlayerProgression>();
                if (!unlockedArray[1] && ProgHelper.IsTurretUnlocked(0))//re lock any turrets that might have been unlocked
                {
                    ProgHelper.SaveDataExistsForTurret(0, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[2] && ProgHelper.IsTurretUnlocked(1))
                {
                    ProgHelper.SaveDataExistsForTurret(1, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[3] && ProgHelper.IsTurretUnlocked(2))
                {
                    ProgHelper.SaveDataExistsForTurret(2, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[4] && ProgHelper.IsTurretUnlocked(3))
                {
                    ProgHelper.SaveDataExistsForTurret(3, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[5] && ProgHelper.IsTurretUnlocked(4))
                {
                    ProgHelper.SaveDataExistsForTurret(4, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[6] && ProgHelper.IsTurretUnlocked(5))
                {
                    ProgHelper.SaveDataExistsForTurret(5, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[7] && ProgHelper.IsTurretUnlocked(6))
                {
                    ProgHelper.SaveDataExistsForTurret(6, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[8] && ProgHelper.IsTurretUnlocked(7))
                {
                    ProgHelper.SaveDataExistsForTurret(7, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[9] && ProgHelper.IsTurretUnlocked(8))
                {
                    ProgHelper.SaveDataExistsForTurret(8, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[10] && ProgHelper.IsTurretUnlocked(31))
                {
                    ProgHelper.SaveDataExistsForTurret(31, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[11] && ProgHelper.IsTurretUnlocked(26))
                {
                    ProgHelper.SaveDataExistsForTurret(26, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[12] && ProgHelper.IsTurretUnlocked(27))
                {
                    ProgHelper.SaveDataExistsForTurret(27, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[13] && ProgHelper.IsTurretUnlocked(28))
                {
                    ProgHelper.SaveDataExistsForTurret(28, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[14] && ProgHelper.IsTurretUnlocked(29))
                {
                    ProgHelper.SaveDataExistsForTurret(29, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[15] && ProgHelper.IsTurretUnlocked(9))
                {
                    ProgHelper.SaveDataExistsForTurret(9, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[16] && ProgHelper.IsTurretUnlocked(10))
                {
                    ProgHelper.SaveDataExistsForTurret(10, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[16] && ProgHelper.IsTurretUnlocked(10))
                {
                    ProgHelper.SaveDataExistsForTurret(10, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[17] && ProgHelper.IsTurretUnlocked(11))
                {
                    ProgHelper.SaveDataExistsForTurret(11, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[18] && ProgHelper.IsTurretUnlocked(12))
                {
                    ProgHelper.SaveDataExistsForTurret(12, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[19] && ProgHelper.IsTurretUnlocked(13))
                {
                    ProgHelper.SaveDataExistsForTurret(13, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[20] && ProgHelper.IsTurretUnlocked(14))
                {
                    ProgHelper.SaveDataExistsForTurret(14, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[21] && ProgHelper.IsTurretUnlocked(15))
                {
                    ProgHelper.SaveDataExistsForTurret(15, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[22] && ProgHelper.IsTurretUnlocked(34))
                {
                    ProgHelper.SaveDataExistsForTurret(34, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[23] && ProgHelper.IsTurretUnlocked(30))
                {
                    ProgHelper.SaveDataExistsForTurret(30, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[24] && ProgHelper.IsTurretUnlocked(16))
                {
                    ProgHelper.SaveDataExistsForTurret(16, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[25] && ProgHelper.IsTurretUnlocked(17))
                {
                    ProgHelper.SaveDataExistsForTurret(17, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[26] && ProgHelper.IsTurretUnlocked(18))
                {
                    ProgHelper.SaveDataExistsForTurret(18, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[27] && ProgHelper.IsTurretUnlocked(19))
                {
                    ProgHelper.SaveDataExistsForTurret(19, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[28] && ProgHelper.IsTurretUnlocked(20))
                {
                    ProgHelper.SaveDataExistsForTurret(20, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[29] && ProgHelper.IsTurretUnlocked(21))
                {
                    ProgHelper.SaveDataExistsForTurret(21, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[30] && ProgHelper.IsTurretUnlocked(22))
                {
                    ProgHelper.SaveDataExistsForTurret(22, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[31] && ProgHelper.IsTurretUnlocked(23))
                {
                    ProgHelper.SaveDataExistsForTurret(23, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
                if (!unlockedArray[32] && ProgHelper.IsTurretUnlocked(25))
                {
                    ProgHelper.SaveDataExistsForTurret(25, out var saveData);
                    saveData.unlocked = false; saveData.revealed = false;
                }
            }
        }



    }
    public class APData
    {
        public string serverAddress { get; set; }
        public int serverPort { get; set; }
        public string slotName { get; set; }
        public string password { get; set; }

    }
}
