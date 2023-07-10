using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public static Player Owner;

    public PlayerSettings playerSettings;

    public NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>();
    public NetworkVariable<PlayerAttackData> playerAttackData = new NetworkVariable<PlayerAttackData>();

    public PlayerAnimator playerAnimator;
    public PlayerAttack playerAttack;
    public PlayerCoroutine playerCoroutine;
    public PlayerCamera playerCamera;
    public PlayerCursor playerCursor;
    public PlayerInput playerInput;
    public PlayerMovement playerMovement;
    public PlayerUI playerUI;
    public PlayerVFX playerVFX;

    public bool isReady;

    public override void OnNetworkSpawn()
    {
        if (IsServer) GeneratePlayerData();
        if (IsClient) playerData.OnValueChanged += OnValueChanged;
        if (IsServer) ServerManager.Instance.players[(int)OwnerClientId] = this;
        StartCoroutine((playerCoroutine = new PlayerCoroutine(this)).playerAwaitSetupCoroutine.Coroutine());
    }

    private void Update()
    {
        if (!isReady) return;
        if (IsOwner || IsServer) playerAttack.OnUpdate();
        if (IsOwner) playerCamera.OnUpdate();
        if (IsOwner) playerCursor.OnUpdate();
        if (IsClient) playerUI.OnUpdate();
        playerMovement.OnUpdate();
    }

    private void LateUpdate()
    {
        if (!isReady) return;
        if (IsOwner) playerInput.OnLateUpdate();
        if (IsOwner) playerCamera.OnLateUpdate();
        if (IsOwner) playerCursor.OnLateUpdate();
    }

    public void GeneratePlayerData()
    {
        playerData.Value = new PlayerData((int)OwnerClientId, PlayerDataGenerator.GeneratePlayerChampionData(1), (OwnerClientId % 2 == 0) ? PlayerData.PlayerTeam.TeamBlue : PlayerData.PlayerTeam.TeamRed).UpdatePlayerData();
        playerAttackData.Value = new PlayerAttackData(true);
    }
    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);
    public void DestroyGameObject(Animator removedGameObject) => Destroy(removedGameObject);
    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public GameObject InstantiateChampionPrefab() => Instantiate(((Champion)PlayerResourceFinder.Find(PlayerResourceFinder.Type.Champion, playerData.Value.playerChampionData.ID)).characterPrefab, transform);
    public void OnValueChanged(PlayerData a, PlayerData b)
    {
        Debug.Log(a.playerID);
        Debug.Log("Old Data: " + a.playerHealth + " New Data: " + b.playerHealth);
        Debug.Log("Old Data: " + a.playerADAttackDamage + " New Data: " + b.playerADAttackDamage);
        Debug.Log("Old Data: " + a.playerADArmor + " New Data: " + b.playerADArmor);
        playerUI.UpdatePlayerUI();
    }

    //SERVER RPC'S DON'T CHECK ANYTHING!
    //THIS IS TOTALLY UNSAFE!

    [ClientRpc] public void PlayerAttackAnimationOrderClientRpc() => playerAnimator.PlayAttackAnimation("Normal Attack");
    [ServerRpc]
    public void PlayerMovementRequestServerRpc(Vector2 playerMovementDestination, float playerMovementTime)
    {
        playerData.Value = playerData.Value.GeneratePlayerData(PlayerData.PlayerDataType.Movement, new PlayerData() 
        { 
            isMoving = true,
            isMoveRequested = true,
            playerMovementDestination = playerMovementDestination, 
            playerMovementTime = playerMovementTime 
        });
        Debug.Log(playerData.Value.playerMovementDestination);
    }
    [ServerRpc] public void PlayerAnimationStateRequestServerRpc(PlayerData.PlayerAnimationState playerAnimationState) => playerData.Value = playerData.Value.GeneratePlayerData(PlayerData.PlayerDataType.Animation, new PlayerData() { playerAnimationState = playerAnimationState });
    [ServerRpc] public void PlayerAttackRequestServerRpc(int playerTargetID)
    {
        playerAttackData.Value = playerAttackData.Value.GeneratePlayerAttackData(playerTargetID: playerTargetID, isPlayerAttacking: false);
    }
}
