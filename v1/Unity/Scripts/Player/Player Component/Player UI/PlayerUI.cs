using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI
{
    private Player player;

    private TMP_Text playerNameText, playerHealthText;

    public PlayerUI(Player player) => this.player = player;

    public void OnStart()
    {
        playerNameText = player.playerSettings.playerNameText;
        playerHealthText = player.playerSettings.playerHealthText;
    }

    public void OnUpdate()
    {
        SyncronizeTextWithCamera(playerNameText.transform);
        SyncronizeTextWithCamera(playerHealthText.transform);
    }

    public void UpdatePlayerUI()
    {
        playerNameText.text = player.playerData.Value.playerID.ToString();
        playerHealthText.text = player.playerData.Value.playerHealth.ToString() + " / " + player.playerData.Value.playerTotalHealth.ToString();
    }

    private void SyncronizeTextWithCamera(Transform transform) => transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
}
