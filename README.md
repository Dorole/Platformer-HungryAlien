# Platformer-HungryAlien
Final course project. Unity/C#. Current project. 

****************************

STORY:
On a distant planet, Pinky and his friends are living peacefully, enjoying their favourite treats every day: the tastiest apples in the universe which can grow only in their planet's special climate. 
The apples must be consumed with clean hands, but one of Pinky's friends, Sandy, is too lazy to wash his hands.
Evil bacteria spreads through his body and consumes him: now he wants to make everyone and everything dirty.
He kidnaps Blue, Greeny and Yellow while they were in the orchard and imprisons them in specially constructed cages with no access to water.
Now Pinky, armed only with a water gun, has to save his friends and help Sandy become himself by washing him with the water from his water gun. 

****************************

GAMEPLAY:
The player plays as an alien who goes through three levels collecting gems which unlock chests with runes, which in turn unlock his imprisoned friends.
In the fourth level the player has to run away from a giant snowball and defeat the black alien.

****************************

MECHANICS:
++ Collect apples so as not to starve ++
The apple/health bar in the upper left corner shows the hunger status.
The player has to collect apples regularly, otherwise the alien will starve and die.

++ Jump on enemies to stun and kill them ++
Moving enemies (except metal saws) can be jumped on once to become stunned. 
To kill them, the player has to jump on them again while they are stunned.

++ Collect the water gun to kill enemies faster and activate moving platforms ++
Available from lvl 2.
If an enemy is shot with a water gun, he is automatically killed.
Some moving platforms have to be activated by shooting the specially marked switch.
The gun is available for a limited time: the bar at the upper right corner shows how much time is left before the gun expires.

++ Free friends by collecting gems and runes ++
The player has to collect gems of various colors.
The gems unlock the chests marked with an empty gem in the same color as the gem which unlocks it.
Each chest contains a rune.
All runes (between 3 and 4) have to be collected to free an alien friend and complete the level.

++ Defeat the boss in lvl 4 ++
The boss follows the player on the x-axis only.
The boss can shoot the player when the player is within its range. The bullets target the player.
The boss' health is indicated with a health bar over his head.
The player can shoot the boss which takes -10 of his health or jump on him which takes -5.
When the boss is shot or jumped on, he becomes invulnerable for 2 seconds during which period he becomes semi-transparent.
Once the boss' health bar is depleted, the boss is defeated.

++ Ways to die ++
Starving
Touching an enemy
Touching a harmful object (moving saws, spinners, spikes)
Falling on spikes or into the water
Falling behind the camera view (only while being chased by the snowball in lvl 4)
Being shot (by the black alien in lvl 4)

****************************

CONTROLS:
Walk: arrow keys or WASD
Jump: Space
Double jump (unlock in lvl 2): Space
Shoot (unlock in lvl 3): Ctrl
Bounce off walls (unlock in lvl 4): Space + left/right
Pause: Esc

****************************

KNOWN ERRORS (as of 2-12-2021): 
+ water bullets sometimes fire off, but don't travel forward in lvl 4 (depends on player local scale?)
+ spring activates even if the player isn't jumping, but just standing in front of it (find a different solution)


