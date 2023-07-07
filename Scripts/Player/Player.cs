using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public static Player Owner;

    public List<PlayerCoroutine> playerCoroutines = new List<PlayerCoroutine>() { new PlayerAwaitSetupCoroutine() };

    [SerializeField] public PlayerSettings playerSettings;
    [SerializeField] public NetworkVariable<PlayerData> playerData;
    [SerializeField] public PlayerAnimator playerAnimator;
    [SerializeField] public PlayerAttack playerAttack;
    [SerializeField] public PlayerCamera playerCamera;
    [SerializeField] public PlayerCursor playerCursor;
    [SerializeField] public PlayerInput playerInput;
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public PlayerVFX playerVFX;

    public bool isReady;

    public override void OnNetworkSpawn()
    {
        if (IsClient) StartCoroutine(playerCoroutines[0].Coroutine(this));
        if (IsServer) GeneratePlayerData();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if (!isReady) return;
        playerAttack.OnUpdate();
        playerCamera.OnUpdate();
        playerCursor.OnUpdate();
        playerMovement.OnUpdate();
    }

    private void LateUpdate()
    {
        if (!IsOwner) return;
        if (!isReady) return;
        playerInput.OnLateUpdate();
        playerCamera.OnLateUpdate();
        playerCursor.OnLateUpdate();
    }

    public void GeneratePlayerData()
    {
        playerData = new NetworkVariable<PlayerData>(new PlayerData(ClassToStruct.ConvertChampion(1), PlayerData.Team.TeamBlue));
        var temp = playerData.Value;
        temp.UpdatePlayerData();
        playerData.Value = temp;
    }

    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);
    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public GameObject InstantiateChampionPrefab() => Instantiate(((Champion) ResourceFinder.Find(ResourceFinder.Type.Champion, playerData.Value.playerChampion.ID)).characterPrefab, transform);

    [ServerRpc]
    public void PlayerMovementServerRpc(float playerMovementDestionation, float playerMovementTime)
    {

    }
}
