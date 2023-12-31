using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAwaitSetupCoroutine
{
    private Player player;

    public PlayerAwaitSetupCoroutine(Player player) => this.player = player;

    public IEnumerator Coroutine()
    {
        while (true)
        {
            if (player.playerData != null && player.playerData.Value.isSet)
            {
                Setup();
                break;
            }
            else yield return null;
        }
    }

    public void Setup()
    {
        if (player.IsClient) ClientSetup();
        if (player.IsServer) ServerSetup();
        if (player.IsOwner) OwnerSetup();
        player.isReady = true;
    }

    public void ClientSetup()
    {
        Debug.Log("test");
        GameObject championGameObject = player.InstantiateChampionPrefab();
        championGameObject.transform.localPosition = Vector3.zero;
        player.GetComponent<Animator>().runtimeAnimatorController = championGameObject.GetComponent<Animator>().runtimeAnimatorController;
        player.GetComponent<Animator>().avatar = championGameObject.GetComponent<Animator>().avatar;
        player.DestroyGameObject(championGameObject.GetComponent<Animator>());
        player.playerAnimator = new PlayerAnimator(player);
        player.playerAttack = new PlayerAttack(player);
        player.playerMovement = new PlayerMovement(player);
        player.playerUI = new PlayerUI(player);
        player.playerVFX = new PlayerVFX(player);
        player.playerAnimator.OnStart();
        player.playerMovement.OnStart();
        player.playerUI.OnStart();
    }

    public void ServerSetup()
    {
        player.playerAttack = new PlayerAttack(player);
        player.playerEvent = new PlayerEvent(player);
        player.playerMovement = new PlayerMovement(player);
        player.playerMovement.OnStart();
    }

    public void OwnerSetup()
    {
        Player.Owner = player;
        player.playerCamera = new PlayerCamera(player);
        player.playerCursor = new PlayerCursor(player);
        player.playerInput = new PlayerInput(player);
        player.playerCamera.OnStart();
        player.playerInput.OnStart();
    }
}
