using Fusion;
using UnityEngine;

// Kế thừa NetworkBehaviour để xài được hàm Spawned siêu xịn
public class Player_Runner : NetworkBehaviour, IPlayerJoined
{
    [SerializeField] NetworkPrefabRef playerPrefab; 

    // =======================================================
    // 1. CHẠY KHI MAP 1 LOAD XONG (Đẻ cho Trưởng phòng)
    // =======================================================
    // Hàm này tự động chạy khi cái Object chứa script này xuất hiện trong mạng
    public override void Spawned()
    {
        if (Object.HasStateAuthority) 
        {
            Vector3 vitrispawn = new Vector3(0, 5, 0);  
            Runner.Spawn(playerPrefab, vitrispawn, Quaternion.identity, Runner.LocalPlayer);
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
                Vector3 vitrispawn = new Vector3(0, 5, 0);  
                Runner.Spawn(playerPrefab, vitrispawn, Quaternion.identity, player);
            }
        }
    }
}