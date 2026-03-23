using Fusion;
using UnityEngine;

// Kế thừa NetworkBehaviour để xài được hàm Spawned siêu xịn
public class Player_Runner : NetworkBehaviour, IPlayerJoined
{
    [SerializeField] 
    NetworkPrefabRef playerPrefab; 
    NetworkPrefabRef enemyPrefab; 
    public GameObject spawn;
    public GameObject spawnenemy;
    
    public override void Spawned()
    {
        if (Object.HasStateAuthority) 
        {
            Vector3 vitrispawn = spawn.transform.position;
            Vector3 viTriSpawnQuai = spawnenemy.transform.position;
            Runner.Spawn(playerPrefab, vitrispawn, Quaternion.identity, Runner.LocalPlayer);
            Runner.Spawn(enemyPrefab, viTriSpawnQuai, Quaternion.identity, null);
        }
    }

    // =======================================================
    // 2. CHẠY KHI CÓ KHÁCH VÀO PHÒNG SAU (Đẻ cho thằng bạn)
    // =======================================================
    public void PlayerJoined(PlayerRef player)
    {
        if (Object.HasStateAuthority) 
        {
            if (player != Runner.LocalPlayer) 
            {
                Vector3 vitrispawn = spawn.transform.position;  
                Runner.Spawn(playerPrefab, vitrispawn, Quaternion.identity, player);
            }
        }
    }
}