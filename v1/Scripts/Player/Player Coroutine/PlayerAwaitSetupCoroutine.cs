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
        GameObject championGameObject = player.InstantiateChampionPrefab();
        championGameObject.transform.localPosition = Vector3.zero;
        player.GetComponent<Animator>().runtimeAnimatorController = championGameObject.GetComponent<Animator>().runtimeAnimatorController;
        player.GetComponent<Animator>().avatar = championGameObject.GetComponent<Animator>().avatar;
        player.DestroyGameObject(championGameObject.GetComponent<Animator>());
        player.isReady = true;
        player.playerAnimator = new PlayerAnimator(player);
        player.playerAttack = new PlayerAttack(player);
        player.playerCamera = new PlayerCamera(player);
        player.playerCursor = new PlayerCursor(player);
        player.playerInput = new PlayerInput(player);
        player.playerMovement = new PlayerMovement(player);
        player.playerVFX = new PlayerVFX(player);
        player.playerAnimator.OnStart();
        player.playerMovement.OnStart();
        if (!player.IsOwner) return;
        Player.Owner = player;
        player.playerCamera.OnStart();
        player.playerInput.OnStart();
    }
}
