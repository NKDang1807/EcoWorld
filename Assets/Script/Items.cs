using UnityEngine;
using Fusion;

// File này gắn vào cục rác (chung chỗ với Network Object)
public class XuLyRac : NetworkBehaviour
{
    // Hàm này của Fusion tự động chạy trên mọi máy ngay khi rác bị Despawn
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        // Tự hủy cái xác 3D trên màn hình của tất cả mọi người
        Destroy(gameObject); 
    }
}