from typing import Callable, Union, Dict, Set

from BaseClasses import MultiWorld, CollectionState
from ..generic.Rules import add_rule, set_rule
from .Locations import location_table
from .Options import OctoOptions
from .Regions import connect_regions, OctoZones
from .Items import turret_item_data_table
from .Items import feature_item_data_table

def shuffle_dict_keys(world, dictionary: dict) -> dict:
    keys = list(dictionary.keys())
    values = list(dictionary.values())
    world.random.shuffle(keys)
    return dict(zip(keys, values))

def fix_reg(entrance_map: Dict[OctoZones, str], entrance: OctoZones, invalid_regions: Set[str],#scared to remove this
            swapdict: Dict[OctoZones, str], world):
    if entrance_map[entrance] in invalid_regions: # Unlucky :C
        replacement_regions = [(rand_entrance, rand_region) for rand_entrance, rand_region in swapdict.items()
                               if rand_region not in invalid_regions]
        rand_entrance, rand_region = world.random.choice(replacement_regions)
        old_dest = entrance_map[entrance]
        entrance_map[entrance], entrance_map[rand_entrance] = rand_region, old_dest
        swapdict[entrance], swapdict[rand_entrance] = rand_region, old_dest
    swapdict.pop(entrance)



def set_rules(world, options: OctoOptions, player: int, area_connections: dict, move_rando_bitvec: int):
    connect_regions(world, player, "Menu", "Sandy Shallows", lambda state: state.has("Sandy Shallows", player))
    connect_regions(world, player, "Menu", "Toxic Wasteland", lambda state: state.has("Nautical Chart", player))
    connect_regions(world, player, "Menu", "Twilight Zone", lambda state: state.has("Nautical Chart", player) and state.has("Titanium Plates", player))

    connect_regions(world, player, "Menu", "Shop",
                    lambda state: state.has("Business Permit", player))
    #                lambda state: state.count("Progressive Shop", player) > 0)
    connect_regions(world, player, "Menu", "Treat Bucket Quests", lambda state: state.has("Treat Bucket", player))





    #add_rule(world.get_location("Shop Item 4", player),
    #         lambda state: state.count("Progressive Shop", player) > 1)
    #add_rule(world.get_location("Shop Item 5", player),
    #         lambda state: state.count("Progressive Shop", player) > 1)
    #add_rule(world.get_location("Shop Item 6", player),
    #         lambda state: state.count("Progressive Shop", player) > 1)
#
    #add_rule(world.get_location("Shop Item 7", player),
    #         lambda state: state.count("Progressive Shop", player) > 2)
    #add_rule(world.get_location("Shop Item 8", player),
    #         lambda state: state.count("Progressive Shop", player) > 2)
    #add_rule(world.get_location("Shop Item 9", player),
    #         lambda state: state.count("Progressive Shop", player) > 2)


    add_rule(world.get_location("1-3 Cannons Aweigh", player),
             lambda state: state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player))#need a gadget to get past the tutorial
    add_rule(world.get_location("1-4 Bumper Encounter", player),
             lambda state: state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player))
    add_rule(world.get_location("1-5 Bumper Maze", player),
             lambda state: state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player))
    add_rule(world.get_location("1-6 Undead Armory", player),
             lambda state: state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player))
    add_rule(world.get_location("1-7 Frosty Treat", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-8 Firework", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-9 Electro Dynamo", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-10 Free Shipment", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-11 Flying Scoundrels", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-12 Tunnel Hideout", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-13 Warhead Pursuit", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-14 Sandy Shoal-down", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))
    add_rule(world.get_location("1-15 Crane Exalte", player),
             lambda state: (state.has("Spiky Urchin", player) or state.has("Red Herring", player) or state.has("Imitation Crab", player) or
             state.has("Lionfish", player) or state.has("Frogfish", player)) and state.has("Elixir Slots", player))


    add_rule(world.get_location("2-7 Rampant Mutation", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-8 Diver Cavern", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-9 Scuttling Trail", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-10 Sergeant's Plateau", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-11 Swampy Rockery", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-12 Graffiti Gallery", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-13 Hazard Treeyard", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-14 Lago Toxico", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("2-15 Dr. Kraxen", player),
             lambda state: state.has("Elixir Slots", player))


    add_rule(world.get_location("3-6 Spectral Mausoleum", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-7 Watery Grave", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-8 Terminal Omega", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-9 Tangled Paths", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-10 Leviathan Trench", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-11 Abyssal Chorals", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-12 Eerie Distillery", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-13 Starry Midnight", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-14 Operation Nocto", player),
             lambda state: state.has("Elixir Slots", player))
    add_rule(world.get_location("3-15 Captain Dallons", player),
             lambda state: state.has("Elixir Slots", player))






    #later levels need a 2nd combat turret for logic
    #most levels require 2 weak combat turrets (pistol shrimp is strong enough for most levels)
    #levels with a lot of clams should require a collection turret
    if options.completion_type == 0:
        world.completion_condition[player] = lambda state: state.can_reach_location("2-15 Dr. Kraxen", player)
    else:
        world.completion_condition[player] = lambda state: state.can_reach_location("3-15 Captain Dallons", player)

