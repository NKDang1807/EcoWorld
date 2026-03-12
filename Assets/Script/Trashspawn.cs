using UnityEngine;
using Fusion;

public class TrashSpawner : NetworkBehaviour
{
    public NetworkPrefabRef racPrefab; // Kéo cục Prefab rác vào đây
    public int soLuongRac = 5;

    // Khi Host vào game, nó sẽ tự động rải rác khắp nơi
    public override void Spawned()
    {
        if (HasStateAuthority) // Chỉ Host mới được quyền đẻ
        {
            for (int i = 0; i < soLuongRac; i++)
            {
                // Rải ngẫu nhiên xung quanh
                Vector3 toaDoDe = transform.position + new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
                
                // Lệnh sinh ra rác chuẩn Online!
                Runner.Spawn(racPrefab, toaDoDe, Quaternion.identity);
            }
        }
    }
}