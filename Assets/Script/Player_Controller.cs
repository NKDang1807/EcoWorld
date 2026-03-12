using UnityEngine;
using Fusion;
using System.Collections.Generic;
using UnityEngine.InputSystem;



public struct DuLieuInput : INetworkInput 
{
    public Vector2 moveInput;
}

public class Player_Controller : NetworkBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] [Header("Di chuyển")] CharacterController character;
    public float speed = 5f;
    private Vector2 moveInputLocal;
    
    [Header("Nhặt vật phẩm")]
    public float banKinhNhat = 5f;
    private bool NutE = false;
    private bool _daNhatRac = false; 

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            Runner.AddCallbacks(this); 
        }
        else
        {
            Camera cam = GetComponentInChildren<Camera>();
            if (cam != null) cam.enabled = false;
            
            AudioListener listener = GetComponentInChildren<AudioListener>();
            if (listener != null) listener.enabled = false;
        }
    }

    
    void Update()
    {
        if (HasInputAuthority && Keyboard.current != null)
        {
            if (Keyboard.current.eKey.wasPressedThisFrame)
            {
                RPC_YeuCauNhatRac();
            }
        }
    }
    
    public override void FixedUpdateNetwork()
    {
        if (GetInput(out DuLieuInput data))
        {
            // Bây giờ hàm này chỉ lo mỗi việc di chuyển
            Vector3 move = data.moveInput.x * transform.right + data.moveInput.y * transform.forward;
            character.Move(move * speed * Runner.DeltaTime);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {   
        var data = new DuLieuInput();
        data.moveInput = moveInputLocal;
        
        input.Set(data);
    }

    public void OnMove(InputValue value)
    {
        moveInputLocal = value.Get<Vector2>();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_YeuCauNhatRac()
    {
        Collider[] ketQuaQuet = Physics.OverlapSphere(transform.position, banKinhNhat);
        foreach (var Obj in ketQuaQuet)
        {
            if (Obj.CompareTag("Rac"))
            {
                NetworkObject nObj = Obj.GetComponent<NetworkObject>();
                
                if (nObj != null && nObj.IsValid)
                {
                    Runner.Despawn(nObj);
                    break; 
                }
            }
        }
    }

    // ==========================================================
    // BƯỚC 2: HOST PHÁT LỆNH CHO CẢ LÀNG (Tất cả máy đều chạy hàm này)
    // ==========================================================
    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_AnRacTrenMoiMay(NetworkObject rac)
    {
        if (rac != null)
        {
            rac.gameObject.SetActive(false); 

            if (HasStateAuthority)
            {
                Runner.Despawn(rac);
            }
        }
    }

    // ==========================================================
    // GIẤU ĐỐNG NÀY ĐI BẰNG DẤU [-] CHO ĐỠ RÁC MẮT NHÉ BÒ
    #region Hàm trống bắt buộc của Interface
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnDisconnectedFromServer(NetworkRunner runner, Fusion.Sockets.NetDisconnectReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnConnectFailed(NetworkRunner runner, Fusion.Sockets.NetAddress remoteAddress, Fusion.Sockets.NetConnectFailedReason reason) { }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnSceneLoadDone(NetworkRunner runner) { }
    public void OnSceneLoadStart(NetworkRunner runner) { }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, Fusion.Sockets.ReliableKey key, System.ArraySegment<byte> data) { }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, Fusion.Sockets.ReliableKey key, float progress) { }
    #endregion
}