using UnityEngine;
using Fusion;

public class TrashSpawner : NetworkBehaviour
{
    public NetworkPrefabRef racPrefab; // Kéo Prefab rác vào đây
    public int soLuongRac = 5;

    public override void Spawned()
    {
        // Khi game bắt đầu, chỉ có Host mới được quyền đẻ rác ra sàn
        if (HasStateAuthority) 
        {
            for (int i = 0; i < soLuongRac; i++)
            {
                // Rải rác ngẫu nhiên xung quanh máy đẻ rác
                Vector3 toaDoDe = transform.position + new Vector3(Random.Range(20, 15f), 7f, Random.Range(60f, 65f));
                
                // Đây là rác ĐẺ BẰNG MẠNG, nên lúc Despawn nó sẽ bốc hơi hoàn toàn!
                Runner.Spawn(racPrefab, toaDoDe, Quaternion.identity);
            }
        }
    }
}