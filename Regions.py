import typing
from enum import Enum

from BaseClasses import MultiWorld, Region, Entrance, Location
from .Options import OctoOptions
from .Locations import OctoLocation, location_table, SS_table,TW_table,TZ_table,shop_table


class OctoZones(int, Enum):#scared to delete this
    SOMETHING = 1

class OctoRegion(Region):
    subregions: typing.List[Region] = []


def create_regions(world: MultiWorld, options: OctoOptions, player: int):
    regMM = Region("Menu", player, world, "Level Select")
    #create_default_locs(regMM, locSS_table)#TODO this might break something
    world.regions.append(regMM)


    regSS = create_region("Sandy Shallows", player, world)
    create_locs(regSS,"1-1 Initial Test","1-2 Four-Way Traffic","1-3 Cannons Aweigh","1-4 Bumper Encounter",
    "1-5 Bumper Maze","1-6 Undead Armory","1-7 Frosty Treat","1-8 Firework","1-9 Electro Dynamo","1-10 Free Shipment",
    "1-11 Flying Scoundrels","1-12 Tunnel Hideout","1-13 Warhead Pursuit",
    "1-14 Sandy Shoal-down","1-15 Crane Exalte")
    regTW = create_region("Toxic Wasteland", player, world)
    create_locs(regTW,"2-1 Alley of Corrosion","2-2 Twirling Riverbank","2-3 Fields of Green",
    "2-4 Gastro Therapy","2-5 Clam Farm","2-6 Scorched Earth","2-7 Rampant Mutation",
    "2-8 Diver Cavern","2-9 Scuttling Trail","2-10 Sergeant's Plateau","2-11 Swampy Rockery",
    "2-12 Graffiti Gallery","2-13 Hazard Treeyard","2-14 Lago Toxico","2-15 Dr. Kraxen")
    regTZ = create_region("Twilight Zone", player, world)
    create_locs(regTZ, "3-1 Nightfall","3-2 Light The Way","3-3 Phantasmic Apparition",
    "3-4 Mesopelagic March","3-5 Graveyard Shift","3-6 Spectral Mausoleum",
    "3-7 Watery Grave","3-8 Terminal Omega","3-9 Tangled Paths","3-10 Leviathan Trench",
    "3-11 Abyssal Chorals","3-12 Eerie Distillery","3-13 Starry Midnight","3-14 Operation Nocto",
    "3-15 Captain Dallons")
    regSH = create_region("Shop", player, world)
    create_locs(regSH,"Shop Item 1","Shop Item 2","Shop Item 3",
    "Shop Item 4","Shop Item 5","Shop Item 6",
    "Shop Item 7","Shop Item 8","Shop Item 9")
    regTB = create_region("Treat Bucket Quests", player, world)
    create_locs(regTB,"Treat Bucket Quest 1","Treat Bucket Quest 2",
    "Treat Bucket Quest 3","Treat Bucket Quest 4","Treat Bucket Quest 5")

def connect_regions(world: MultiWorld, player: int, source: str, target: str, rule=None) -> Entrance:
    sourceRegion = world.get_region(source, player)
    targetRegion = world.get_region(target, player)
    return sourceRegion.connect(targetRegion, rule=rule)


def create_region(name: str, player: int, world: MultiWorld) -> OctoRegion:
    region = OctoRegion(name, player, world)
    world.regions.append(region)
    return region


def create_subregion(source_region: Region, name: str, *locs: str) -> OctoRegion:
    region = OctoRegion(name, source_region.player, source_region.multiworld)
    connection = Entrance(source_region.player, name, source_region)
    source_region.exits.append(connection)
    connection.connect(region)
    source_region.multiworld.regions.append(region)
    create_locs(region, *locs)
    return region


def set_subregion_access_rule(world, player, region_name: str, rule):
    world.get_entrance(world, player, region_name).access_rule = rule


def create_default_locs(reg: Region, default_locs: dict):
    create_locs(reg, *default_locs.keys())


def create_locs(reg: Region, *locs: str):
    reg.locations += [OctoLocation(reg.player, loc_name, location_table[loc_name], reg) for loc_name in locs]
