Name:
Jake Roman-Barnes

Instructions:
WASD keys to move
LMB to attack

Description:
I created a top-down 2D arcade-style shooter. The players "weapon" rotates and fires
toward the mouse location and while they have not been implemented, the groundwork for
additional weapons is there and will make their addition simpler. As there is only one
weapon at the moment it isn't apparent, but the UI in the corner updates to show the
sprite of the players current weapon.
The enemies only become active once they are both on the screen(visible to the player)
and have a direct line-of-sight to the player. Once they collide with the player they
will start to attack with their weapon which damages the player when it collides with
them. The enemies also have a life bar which is only shown once they take damage.

Problems:
I created my own code to move the player which was helpful for understanding C# and
Unity. However, it did cause me a lot of frustration when I ended up needing to use
rigidbody anyway for wall collision due to time restraints. Enemies would apply force
to the players rigidbody which would mess up movement greatly because my method of
movement did not communicate with the rigidbody physics.

Plan:
In the future, I plan to work on additional weapons, the addition of abilities, more
enemy types, more and better maps, actual artwork/sprites, and overall polish.