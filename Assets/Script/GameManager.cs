
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Photon.Pun.UtilityScripts;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks,IPunTurnManagerCallbacks
{
    /***********************Referanslar**********************/
    [SerializeField] TextMeshProUGUI _finishText, tahtatext;
    GameObject panel;
    [SerializeField] AudioSource ses;
    public Transform[] _points,bpoints;
    public PunTurnManager pm;
    /************************Degiskenler**********************/
    public int _tahtaSayisi;
    public int _tahtaSayisi2;
    public bool hareketet,tahtakoy;
    string isim;
    //************************KODLAR******************/
    private void Start()
    {
        pm = GetComponent<PunTurnManager>();
        pm.TurnDuration = 1000;
        pm.TurnManagerListener = this;
        _finishText.text = "Oyuncu Bekleniyor";
        _tahtaSayisi = 10;
        _tahtaSayisi2 = 10;
        panel = GameObject.FindGameObjectWithTag("Panel");
       
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", _points[0].position, Quaternion.identity, 0, null).GetPhotonView().Owner.NickName = PlayerPrefs.GetString("isim");
            hareketet = true;
            tahtakoy = true;
            transform.GetChild(0).gameObject.SetActive(hareketet);
            transform.GetChild(1).gameObject.SetActive(hareketet);
            tahtatext.text = _tahtaSayisi.ToString();
           
        }
        else
        {
            PhotonNetwork.Instantiate("Player", _points[1].position, Quaternion.identity, 0, null).GetPhotonView().Owner.NickName = PlayerPrefs.GetString("isim");
            transform.GetChild(0).gameObject.SetActive(hareketet);
            transform.GetChild(1).gameObject.SetActive(hareketet);
            transform.GetChild(0).transform.position = bpoints[0].position;
            transform.GetChild(1).transform.position = bpoints[1].position;
            tahtatext.text = _tahtaSayisi2.ToString();
           

        }
        InvokeRepeating("OyuncuKontrol",1,0.1f);
    }
    public void TahtaAzalt()//Koyulacak engellerin adet sayılarını azaltma işlemi
    {
        if (PhotonNetwork.IsMasterClient)//Oyunu kuran ise onun engel sayısını azalt
        {
            ses.Play();
            _tahtaSayisi--;
            tahtatext.text = _tahtaSayisi.ToString();
           
            if (_tahtaSayisi <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
        else //oyunu kuran dışındaki diğer oyuncu için engel azaltma işlemi
        {
            ses.Play();
            _tahtaSayisi2--;
            tahtatext.text = _tahtaSayisi2.ToString();

          
            if (_tahtaSayisi2 <= 0)
            {
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
            }
        }
    }
    void OyuncuKontrol()//Oyuncu sayısımı kontrol eder ve 2 ye ulaştıgında tur işlemini başlatır
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            panel.SetActive(false);
            pm.BeginTurn();
            CancelInvoke();
        }
    }

    [PunRPC]
    public void OyunBitir(string player)//oyunu bitiren kişinin ismini panelde texte yazdırır
    {
        _finishText.text = player;
        panel.SetActive(true);
        StartCoroutine(OyuncularıCikar());
        
        
    }
    public void OnTurnBegins(int turn)
    {
        
       
    }

    public void OnTurnCompleted(int turn)
    {
       
        
    }

    public void OnPlayerMove(Photon.Realtime.Player player, int turn, object move)
    {
      
        
    }

    public void OnTurnTimeEnds(int turn)
    {
       
       
    }

    public void OnPlayerFinished(Photon.Realtime.Player player, int turn, object move)
    {
        if (turn >= 2)
            pm.BeginTurn();
        if (player.IsMasterClient)
        {
            hareketet = !hareketet;
            tahtakoy = !tahtakoy;
            transform.GetChild(0).gameObject.SetActive(hareketet);
            transform.GetChild(1).gameObject.SetActive(hareketet);
            tahtatext.enabled= hareketet;

        }
        else
        {
            tahtakoy = !tahtakoy;
            hareketet = !hareketet;
            transform.GetChild(0).gameObject.SetActive(hareketet);
            transform.GetChild(1).gameObject.SetActive(hareketet);
            tahtatext.enabled = hareketet;

        }
    }
    IEnumerator OyuncularıCikar()
    {
        yield return new WaitForSeconds(3);
        PhotonNetwork.LeaveRoom();
    }
    
}
