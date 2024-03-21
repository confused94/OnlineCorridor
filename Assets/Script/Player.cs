using UnityEngine;
using Photon.Realtime;
using Photon.Pun;


public class Player : MonoBehaviourPunCallbacks
{
    [SerializeField] Transform[] noktalar;
    GameManager gm;
    PhotonView pw;
    public bool hareketet,tahtakoy;
    //*********************Kodlar*************************
    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        pw = GetComponent<PhotonView>();
        //**********************************//
        /*Bu kısım oyunu kuranın ve misafir olanında kamera açısını düzenle
          Her iki kişide kendi perspektifinden görür*/
        if (PhotonNetwork.IsMasterClient)
            return;
        else
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 180);
        //*******************************//
    }
    private void FixedUpdate()
    {
        hareketet = gm.hareketet;//İki oyuncunun aynı anda hamle yapmasını engeller
        tahtakoy = gm.tahtakoy;  //Engel koydugumuzda hamle karşı tarafa geçer
        if (pw.IsMine&&hareketet&tahtakoy)
        {
            RaycastKontrol();
        }
    }
    void RaycastKontrol()//Playerın dört yönünden raycast gönderir karelere değme konrolu yapar
    {        
        RaycastHit2D hitSag = Physics2D.Raycast(noktalar[0].position, Vector2.right, 0.30f);
        RaycastHit2D hitSol = Physics2D.Raycast(noktalar[1].position, Vector2.left, 0.30f);
        RaycastHit2D hitYukari = Physics2D.Raycast(noktalar[2].position, Vector2.up, 0.30f);
        RaycastHit2D hitAsagi = Physics2D.Raycast(noktalar[3].position, Vector2.down, 0.30f);

        if (hitSag && hitSag.collider.tag == "kare")
            hitSag.collider.GetComponent<Kare>().degiyormu = tahtakoy;

        if (hitSol && hitSol.collider.tag == "kare")
            hitSol.collider.GetComponent<Kare>().degiyormu = tahtakoy;

        if (hitYukari && hitYukari.collider.tag == "kare")
            hitYukari.collider.GetComponent<Kare>().degiyormu = tahtakoy;

        if (hitAsagi && hitAsagi.collider.tag == "kare")
            hitAsagi.collider.GetComponent<Kare>().degiyormu = tahtakoy;
    }
}
