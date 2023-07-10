using System.Collections;
using System.Collections.Generic;

public class PlayerCoroutine
{
    public Player player;

    public PlayerAwaitSetupCoroutine playerAwaitSetupCoroutine;

    public PlayerCoroutine(Player player)
    {
        this.player = player;
        playerAwaitSetupCoroutine = new PlayerAwaitSetupCoroutine(player);
    }
}