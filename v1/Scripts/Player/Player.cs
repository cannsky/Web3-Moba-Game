using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public static Player Owner;

    public List<PlayerCoroutine> playerCoroutines = new List<PlayerCoroutine>() { new PlayerAwaitSetupCoroutine() };

    [SerializeField] public PlayerSettings playerSettings;
    [SerializeField] public NetworkVariable<PlayerData> playerData = new NetworkVariable<PlayerData>();
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
        if (IsClient) playerData.OnValueChanged += OnValueChanged;
    }

    private void Update()
    {
        if (!isReady) return;
        if (IsOwner) playerAttack.OnUpdate();
        if (IsOwner) playerCamera.OnUpdate();
        if (IsOwner) playerCursor.OnUpdate();
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

    public void GeneratePlayerData() => (playerData.Value = new PlayerData(ClassToStruct.ConvertChampion(1), PlayerData.PlayerTeam.TeamBlue)).UpdatePlayerData();
    public void DestroyGameObject(GameObject removedGameObject) => Destroy(removedGameObject);
    public void DestroyGameObject(Animator removedGameObject) => Destroy(removedGameObject);
    public GameObject InstantiateGameObject(GameObject addedGameObject, Vector3 position, Quaternion rotation) => Instantiate(addedGameObject, position, rotation);
    public GameObject InstantiateChampionPrefab() => Instantiate(((Champion) ResourceFinder.Find(ResourceFinder.Type.Champion, playerData.Value.playerChampion.ID)).characterPrefab, transform);

    //Remote procedure calls

    [ServerRpc]
    public void UpdatePlayerMovementServerRpc(Vector2 playerMovementDestination, float playerMovementTime) => playerData.Value = playerData.Value.GeneratePlayerData(playerMovementDestination, playerMovementTime);
    [ServerRpc] public void UpdatePlayerAnimationStateServerRpc(PlayerData.PlayerAnimationState playerAnimationState) => playerData.Value = playerData.Value.GeneratePlayerData(playerAnimationState);

    public void OnValueChanged(PlayerData a, PlayerData b)
    {
        Debug.Log("Veri deðiþti.");
        Debug.Log("Eski, " + a.playerAnimationState);
        Debug.Log("Yeni, " + b.playerAnimationState);
    }
}
