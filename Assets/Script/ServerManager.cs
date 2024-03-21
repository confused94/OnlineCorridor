using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;

public class ServerManager : MonoBehaviourPunCallbacks
{
    /*******************///Server işlemlerini yürüttüğümüz script//**********************/

    TextMeshProUGUI bilgi;
    public static GameObject instance;
    /****Kodlar********/
    private void Awake()
    {
        bilgi = GameObject.FindGameObjectWithTag("bilgi").GetComponent<TextMeshProUGUI>();
    }
    private void Start()
    {  
        if (instance == null||SceneManager.GetActiveScene().buildIndex==0)
        {
            instance = this.gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        PhotonNetwork.ConnectUsingSettings();
        bilgi.text = "Servera Bağlanılıyor";
    }
    public void OdayaBaglan()
    {
        PhotonNetwork.JoinLobby();
        bilgi.text = "Odaya Bağlanılıyor";
    }
    public override void OnConnectedToMaster()
    {
        bilgi.text = "Servera Bağlandı";
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        bilgi.text = "İnternet Bağlantınızı kontrol edin";
    }
    public void RastGeleOda()
    {
        PhotonNetwork.JoinRandomRoom();
        bilgi.text = "Odaya Bağlanılıyor";
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        bilgi.text = "Oda bulunamadı";
    }
    public override void OnJoinedLobby()
    {
        bilgi.text = "Odaya Bağlandı";
        PhotonNetwork.JoinOrCreateRoom(PlayerPrefs.GetString("odaisim"), new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
     PhotonNetwork.LoadLevel(1);   
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }


}
