## Important Information

**V1 is including a web2 server, web3 implementation is not added to v1.**

If you want to get web3 implementation, please wait v2 version of the project.

## How Does Headless Server Work? (Unity Netcode 2023)

You might be wondering where is the server (or validators on web3) files on that project. Well actually inside of the project with client codes. Let's give a brief explanation on that:

Right now both client and server use the same code base, which is called as Unity's headless server technology (or you can say Netcode), where it is also a game without graphic calculations included. Basically when a player enters to a game, an another game with the same logic welcomes them and makes calculations with the same logic in the game.
