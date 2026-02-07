import typing
import os
import json
from .Items import (item_table, item_data_table, turret_item_data_table, feature_item_data_table, generic_item_data_table, OctoItem)

from .Locations import location_table, OctoLocation
from .Options import octo_options_groups, OctoOptions
from .Rules import set_rules
from .Regions import create_regions
from BaseClasses import Item, Tutorial, ItemClassification, Region
from ..AutoWorld import World, WebWorld
import random
from multiprocessing import Process
from worlds.LauncherComponents import Component, components, Type, launch_subprocess, icon_paths






#class SM64Web(WebWorld):
#    tutorials = [Tutorial(
#        "Multiworld Setup Guide",
#        "A guide to setting up SM64EX for MultiWorld.",
#        "English",
#        "setup_en.md",
#        "setup/en",
#        ["N00byKing"]
#    )]

option_groups = octo_options_groups

class OctoWorld(World):
    """ 
    Like PVZ if Odin could move
    """
    game: str = "Operation Octo"
    topology_present = False

    item_name_to_id = item_table
    location_name_to_id = location_table

    required_client_version = (0, 3, 5)

    area_connections: typing.Dict[int, int]

    options_dataclass = OctoOptions

    number_of_locations: int
    filler_count: int


    def generate_early(self):

        max_locations = 59#TODO up this once i have enough locations

        #if self.options.legally_distinct and not self.options.pirated:#im going insane
        #    max_locations +=1
        self.number_of_locations = max_locations
        self.move_rando_bitvec = 0



    def create_regions(self):
        create_regions(self.multiworld, self.options, self.player)

    def set_rules(self):
        self.area_connections = {}
        set_rules(self.multiworld, self.options, self.player, self.area_connections, self.move_rando_bitvec)


    def create_item(self, name: str) -> Item:
        data = item_data_table[name]
        item = OctoItem(name, data.classification, data.code, self.player)

        return item

    def create_items(self):
        slots_to_fill = self.number_of_locations

        self.multiworld.push_precollected(self.create_item("Sandy Shallows"))

        for turret in turret_item_data_table.keys():


            if turret == "Pistol Shrimp":
                self.multiworld.push_precollected(self.create_item("Pistol Shrimp"))
                continue
            self.multiworld.itempool += [self.create_item(turret)]
            slots_to_fill-=1

        self.multiworld.itempool += [self.create_item("Ruby Chest")]
        self.multiworld.itempool += [self.create_item("Elixir Slots")]
        self.multiworld.itempool += [self.create_item("Nautical Chart")]
        self.multiworld.itempool += [self.create_item("Treat Bucket")]
        self.multiworld.itempool += [self.create_item("Sailor's Telescope")]
        self.multiworld.itempool += [self.create_item("Starting Frost Elixir")]
        self.multiworld.itempool += [self.create_item("Starting Mutant Elixir")]
        self.multiworld.itempool += [self.create_item("Starting Hallow Elixir")]
        self.multiworld.itempool += [self.create_item("Titanium Plates")]
        self.multiworld.itempool += [self.create_item("Ice Cream Cache")]
        self.multiworld.itempool += [self.create_item("Business Permit")]
        slots_to_fill -= 11

        #self.multiworld.itempool += [self.create_item("Progressive Shop")]
        #self.multiworld.itempool += [self.create_item("Progressive Shop")]
        #self.multiworld.itempool += [self.create_item("Progressive Shop")]
        #slots_to_fill -= 3

        trap_slots = slots_to_fill*(self.options.trap_percentage/100)
        for i in range(int(trap_slots)):
            self.multiworld.itempool += [self.create_item("Free Steam Achievement")]
            slots_to_fill -= 1




        while slots_to_fill > 0:
            self.multiworld.itempool += [self.create_item("Treasure Chest")]
            slots_to_fill -= 1


    def generate_basic(self): #use to force items in a specific location
        #self.multiworld.get_location()
        return
           #self.multiworld.get_location("BoB: Bob-omb Buddy", self.player).place_locked_item(self.create_item("Cannon Unlock BoB"))


    def get_filler_item_name(self) -> str:
        return "Treasure Chest"

    def fill_slot_data(self):
        return {
            "RingLink": self.options.ring_link.value,
            "DeathLink": self.options.death_link.value,
            "CompletionType": self.options.completion_type.value,
        }

    def generate_output(self, output_directory: str):
        if self.multiworld.players != 1:
            return
        data = {
            "slot_data": self.fill_slot_data(),
            "location_to_item": {self.location_name_to_id[i.name] : item_table[i.item.name] for i in self.multiworld.get_locations()},
            "data_package": {
                "data": {
                    "games": {
                        self.game: {
                            "item_name_to_id": self.item_name_to_id,
                            "location_name_to_id": self.location_name_to_id
                        }
                    }
                }
            }
        }
        filename = f"{self.multiworld.get_out_file_name_base(self.player)}.apocto"
        with open(os.path.join(output_directory, filename), 'w') as f:
            json.dump(data, f)

    def extend_hint_information(self, hint_data: typing.Dict[int, typing.Dict[int, str]]):
        return

    def write_spoiler(self, spoiler_handle: typing.TextIO) -> None:
        # Write calculated star costs to spoiler.
        star_cost_spoiler_header = '\n\n' + self.player_name + ' line 159, TODO find out what this does:\n\n'
        spoiler_handle.write(self.player_name)
        # - Reformat star costs dictionary in spoiler to be a bit more readable.


