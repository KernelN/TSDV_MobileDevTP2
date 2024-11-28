# TSDV_MobileDevTP2 / TheWasteland
A Vampire Survivors inspired game where the main gimmick is that the player moves rotating the phone.

# Design Patterns Used
Factory Pattern:
--Scripts/Enemy/Spawner/
EnemyFactory (and all it's children)
EnemyManager (as client, not factory)

Decorator Pattern:
--Scripts/Powers/
PowerComponent (and all it's children)
PowerDecorator (and all it's children)

Observer Pattern:
--Scripts/Backend/CustomEvents/
EventListener (and all it's children)
EventManager
--Scripts/Shop/
ShopManager (as client and EventListener child)
BuyableUI (and all it's children) (as client and EventListener child
