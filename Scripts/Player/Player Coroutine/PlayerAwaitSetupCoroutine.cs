using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAwaitSetupCoroutine : PlayerCoroutine
{
    public override IEnumerator Coroutine(Player player)
    {
        while (true)
        {
            if (player.playerData != null && player.playerData.Value.isSet)
            {
                Setup(player);
                break;
            }
            else yield return null;
        }
    }

    public void Setup(Player player)
    {
        player.InstantiateChampionPrefab().transform.localPosition = Vector3.zero;
        player.isReady = true;
        if (!player.IsOwner) return;
        Player.Owner = player;
        player.playerAnimator = new PlayerAnimator(player);
        player.playerAttack = new PlayerAttack(player);
        player.playerCamera = new PlayerCamera(player);
        player.playerCursor = new PlayerCursor(player);
        player.playerInput = new PlayerInput(player);
        player.playerMovement = new PlayerMovement(player);
        player.playerVFX = new PlayerVFX(player);
        player.playerAnimator.OnStart();
        player.playerCamera.OnStart();
        player.playerInput.OnStart();
        player.playerMovement.OnStart();
    }
}
