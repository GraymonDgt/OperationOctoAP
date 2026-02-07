import typing
from dataclasses import dataclass
from Options import DefaultOnToggle, Range, Toggle, DeathLink, Choice, PerGameCommonOptions, OptionSet, OptionGroup, OptionCounter


class TrapPercentage(Range):
    """Percentage of filler items to replace with traps"""
    display_name = "Trap Percentage"
    range_start = 0
    range_end = 100
    default = 20

class CompletionType(Choice):
    """Set goal for Victory Condition
    Dallons - defeat captain dallons"""
    display_name = "Completion Goal"
    option_dallons = 0

class RingLink(Choice):
    """Enable Ringlink for pearls (nonfunctional)"""
    option_off = 0
    option_pearls = 1


octo_options_groups = [
    OptionGroup("Meta Options", [
        TrapPercentage,
        CompletionType,
        RingLink

    ])
]

@dataclass
class OctoOptions(PerGameCommonOptions):
    trap_percentage:TrapPercentage
    ring_link: RingLink
    death_link: DeathLink
    completion_type: CompletionType
