using Fusion;
using UnityEngine;
using System.Threading.Tasks;
using TMPro;
using Fusion.Photon.Realtime;
public class Menu_Fusion : MonoBehaviour
{
    private NetworkRunner runner;
    public TMP_InputField inputSessionName;
    public GameObject joinPlayer;

    async void KetNoi(GameMode cheDo)
    {
        if (runner != null)
        {
            await runner.Shutdown();            
            Destroy(runner.gameObject);
        }
        Debug.Log("Bà mụ: Đang kết nối..." + inputSessionName.text.Trim());
        var idRieng = new AuthenticationValues(System.Guid.NewGuid().ToString());
        // 2. THUÊ ANH GIAO HÀNG MỚI TINH XƯỞNG
        GameObject runnerObject = new GameObject("TienTrinhFusion");
        runner = runnerObject.AddComponent<NetworkRunner>();

        // 3. RA LỆNH KẾT NỐI
        var ketQua = await runner.StartGame(new StartGameArgs()
        {
            GameMode = cheDo,
            SessionName = inputSessionName.text.Trim(), 
            AuthValues = idRieng,
            Scene = SceneRef.FromIndex(1), // Nhớ check Index trong Build Settings
            SceneManager = runnerObject.AddComponent<NetworkSceneManagerDefault>()
            
        });
    }

    public void BamNut_ChoiDon() { KetNoi(GameMode.Single); }
    public void BamNut_TaoPhong() { KetNoi(GameMode.Host); }
    public void BamNut_TenPhong() { joinPlayer.SetActive(true); }
    public void BamNut_NhapPhong() { joinPlayer.SetActive(true); }
    public void BamNut_VaoPhong() { KetNoi(GameMode.Client); }
}