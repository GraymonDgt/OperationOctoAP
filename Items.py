from typing import NamedTuple

from BaseClasses import Item, ItemClassification


class OctoItem(Item):
    game: str = "Operation Octo"

class OctoItemData(NamedTuple):
    code: int | None = None
    classification: ItemClassification = ItemClassification.progression


generic_item_data_table: dict[str, OctoItemData] = {
    "Treasure Chest": OctoItemData(100, ItemClassification.filler),
    "Ruby Chest": OctoItemData(109, ItemClassification.useful),
}
traps_item_data_table:dict[str, OctoItemData] = {
"Free Steam Achievement": OctoItemData(130, ItemClassification.trap),#apply a bunch of status effects


}

turret_item_data_table: dict[str, OctoItemData] = {
    "Pistol Shrimp": OctoItemData(1),
    "C.L.A.M.": OctoItemData(2),
    "Bumper Fish": OctoItemData(3),
    "Blobfish": OctoItemData(4),
    "Bolt Eel": OctoItemData(5),
    #"Turret C.L.A.M.": OctoItemData(6),
    "Mussel Beest": OctoItemData(7),
    "Bubble Parrot": OctoItemData(8),
    "Pleco-llector": OctoItemData(9),
    "Argonaut": OctoItemData(10),
    "Spiky Urchin": OctoItemData(11),
    "Red Herring": OctoItemData(12),
    "Imitation Crab": OctoItemData(13),
    "Lionfish": OctoItemData(14),
    "Zappy Jelly": OctoItemData(15),
    "Heal Snail": OctoItemData(16),
    "Hermit Holster": OctoItemData(17),
    "Sniper Turtle": OctoItemData(18),
    "Crab Trap": OctoItemData(19),
    "Navy Seal": OctoItemData(20),
    "Pearl Tree": OctoItemData(21),
    "Everywhere Eel": OctoItemData(22),
    "Frogfish": OctoItemData(23),
    "Lantern Fish": OctoItemData(24),
    "Death Ray": OctoItemData(25),
    "Hammer Shark": OctoItemData(26),
    #"Lobby Leviathan": OctoItemData(27),
    #"Charge Station": OctoItemData(28),
    "Siren": OctoItemData(29),
    "Shooting Star": OctoItemData(30),
    "Watchdog": OctoItemData(31),
    "Harvest Barreleye": OctoItemData(32),

}
feature_item_data_table: dict[str, OctoItemData] = {
    "Elixir Slots": OctoItemData(101),
    "Business Permit": OctoItemData(102),
    "Nautical Chart": OctoItemData(103),
    "Treat Bucket": OctoItemData(104),
    "Sailor's Telescope": OctoItemData(105, ItemClassification.useful),
    "Graveyard": OctoItemData(106),
    "Graveyard Toxic Ridge Upgrade": OctoItemData(107),
    "Graveyard Twilight Zone Upgrade": OctoItemData(108),

    "Starting Frost Elixir": OctoItemData(150, ItemClassification.useful),
    "Starting Mutant Elixir": OctoItemData(151, ItemClassification.useful),
    "Starting Hallow Elixir": OctoItemData(152, ItemClassification.useful),
    "Sandy Shallows":OctoItemData(200),
    "Titanium Plates":OctoItemData(202),
    "Ice Cream Cache":OctoItemData(203, ItemClassification.useful),


}



item_data_table = {
    **generic_item_data_table,
    **turret_item_data_table,
    **feature_item_data_table,
    **traps_item_data_table,
}

item_table = {name: data.code for name, data in item_data_table.items() if data.code is not None}
