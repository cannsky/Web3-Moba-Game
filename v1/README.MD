## Important Information

**V1 is including a web2 server, web3 implementation is not added to v1.**

If you want to get web3 implementation, please wait v2 version of the project.

## How Does Headless Server Work? (Unity Netcode 2023)

You might be wondering where is the server (or validators on web3) files on that project. Well they are inside of the project. If you look closely to the classes, you will realize that some classes uses **[IsServer, IsClient]** etc. That means that both client and server use the same code base. This is Unity's headless server technology, where it is also a game without graphic calculations included. Basically when a player enters to a game, an another game with the same logic welcomes them and makes calculations with the same logic in the game.
