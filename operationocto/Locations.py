from BaseClasses import Location

class OctoLocation(Location):
    game: str = "Operation Octo"

#Bob-omb Battlefield

SS_table = {
    "1-1 Initial Test": 1,
    "1-2 Four-Way Traffic": 2,
    "1-3 Cannons Aweigh": 3,
    "1-4 Bumper Encounter": 4,
    "1-5 Bumper Maze": 5,
    "1-6 Undead Armory": 6,
    "1-7 Frosty Treat": 7,
    "1-8 Firework": 8,
    "1-9 Electro Dynamo": 9,
    "1-10 Free Shipment": 10,
    "1-11 Flying Scoundrels": 11,
    "1-12 Tunnel Hideout": 12,
    "1-13 Warhead Pursuit": 13,
    "1-14 Sandy Shoal-down": 14,
    "1-15 Crane Exalte": 15,
}

TW_table = {
    "2-1 Alley of Corrosion": 16,
    "2-2 Twirling Riverbank": 17,
    "2-3 Fields of Green": 18,
    "2-4 Gastro Therapy": 19,
    "2-5 Clam Farm": 20,
    "2-6 Scorched Earth": 21,
    "2-7 Rampant Mutation": 22,
    "2-8 Diver Cavern": 23,
    "2-9 Scuttling Trail": 24,
    "2-10 Sergeant's Plateau": 25,
    "2-11 Swampy Rockery": 26,
    "2-12 Graffiti Gallery": 27,
    "2-13 Hazard Treeyard": 28,
    "2-14 Lago Toxico": 29,
    "2-15 Dr. Kraxen": 30,
}
TZ_table = {
    "3-1 Nightfall": 31,
    "3-2 Light The Way": 32,
    "3-3 Phantasmic Apparition": 33,
    "3-4 Mesopelagic March": 34,
    "3-5 Graveyard Shift": 35,
    "3-6 Spectral Mausoleum": 36,
    "3-7 Watery Grave": 37,
    "3-8 Terminal Omega": 38,
    "3-9 Tangled Paths": 39,
    "3-10 Leviathan Trench": 40,
    "3-11 Abyssal Chorals": 41,
    "3-12 Eerie Distillery": 42,
    "3-13 Starry Midnight": 43,
    "3-14 Operation Nocto": 44,
    "3-15 Captain Dallons": 45,
}
shop_table = {
    "Shop Item 1": 150,
    "Shop Item 2": 151,
    "Shop Item 3": 152,
    "Shop Item 4": 153,
    "Shop Item 5": 154,
    "Shop Item 6": 155,
    "Shop Item 7": 156,
    "Shop Item 8": 157,
    "Shop Item 9": 158,
}

bucket_quest_table = {
"Treat Bucket Quest 1": 200,
"Treat Bucket Quest 2": 201,
"Treat Bucket Quest 3": 202,
"Treat Bucket Quest 4": 203,
"Treat Bucket Quest 5": 204,
}

location_table = {**SS_table,**TW_table,**TZ_table,**shop_table,**bucket_quest_table}
