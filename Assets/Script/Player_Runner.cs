using Fusion;
using UnityEngine;

public class Player_Runner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField] GameObject playerPrefab;

    public void PlayerJoined(PlayerRef player)
    {
        Vector3 vitrispawn = new Vector3(0, 5, 0);  
        if (Runner.GameMode == GameMode.Shared)
        {
            
            if (player == Runner.LocalPlayer)
            {
                Runner.Spawn(playerPrefab, vitrispawn, Quaternion.identity, player);
            }
        }
        else 
        {
            if (Runner.IsServer)
            {
                 Runner.Spawn(playerPrefab, vitrispawn, Quaternion.identity, player);
            }
        }
    }
}