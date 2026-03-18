using Fusion;
using UnityEngine;
using System.Threading.Tasks;

public class Menu_Fusion : MonoBehaviour
{
    private NetworkRunner runner;

    void Start()
    {
        // Tự động tìm hoặc tạo anh Giao Hàng (NetworkRunner)
        runner = FindObjectOfType<NetworkRunner>();
        if (runner == null)
        {
            runner = gameObject.AddComponent<NetworkRunner>();
        }
    }

    // Hàm lõi: Gọi cửa Server
    async void KetNoi(GameMode cheDo)
    {
        // 1. Gắn bộ quản lý Scene của Fusion vào
        if (gameObject.GetComponent<NetworkSceneManagerDefault>() == null)
        {
            gameObject.AddComponent<NetworkSceneManagerDefault>();
        }

        // 2. Ra lệnh kết nối VÀ KHAI BÁO SCENE TẠI ĐÂY!
        var ketQua = await runner.StartGame(new StartGameArgs()
        {
            GameMode = cheDo,
            SessionName = "phongcuacao", 
            Scene = SceneRef.FromIndex(1),
            SceneManager = gameObject.GetComponent<NetworkSceneManagerDefault>()
        });
        if (ketQua.Ok)
        {
            Debug.Log("<color=green>Đã vào mạng thành công!</color>");
            Canvas khungUI = GetComponent<Canvas>();
            if (khungUI != null)
            {
                khungUI.enabled = false; 
            }
        }
        else
        {
            Debug.LogError("Lỗi rồi Bò ơi: " + ketQua.ShutdownReason);
        }
    }

    // ==========================================
    // CÁC HÀM NÀY ĐỂ BÒ KÉO VÀO SỰ KIỆN "ON CLICK" CỦA BUTTON TRONG UNITY
    // ==========================================
    public void BamNut_ChoiDon()
    {
        KetNoi(GameMode.Single);
    }

    public void BamNut_TaoPhong()
    {
        KetNoi(GameMode.Host);
    }

    public void BamNut_VaoPhong()
    {
        KetNoi(GameMode.Client);
    }
}