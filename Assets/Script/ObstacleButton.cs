using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ObstacleButton : MonoBehaviourPunCallbacks
{
    PhotonView pw;
    GameObject clone;
    [SerializeField] string _obstacle;
    Kare kare;
    public bool tiklandi,iscolor;
    int tahta1, tahta2;
    //********************KODLAR****************************//
    private void Start()
    {
        pw = GetComponent<PhotonView>();
        kare = FindObjectOfType<Kare>();
    }
    private void OnMouseDown()
    {
        tahta1 = GetComponentInParent<GameManager>()._tahtaSayisi;
        tahta2 = GetComponentInParent<GameManager>()._tahtaSayisi2;  
        if (PhotonNetwork.IsMasterClient)
        {  //E�er engeli master olu�turuyorsa
            if (!tiklandi&&tahta1>0)
            {
                clone = PhotonNetwork.Instantiate(_obstacle, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), Quaternion.identity, 0, null);
                clone.GetComponent<BoxCollider2D>().enabled = false;
                tiklandi = true;
                iscolor = true;
            }
        }
        else
        { //E�er engeli kurucu haricindeki di�er oyuncu olu�turuyorsa
            if (tahta2 <= 0)
                gameObject.SetActive(false);
            if (!tiklandi&&tahta2>0)
            {
                clone = PhotonNetwork.Instantiate(_obstacle, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), Quaternion.identity, 0, null);
                clone.GetComponent<BoxCollider2D>().enabled = false;
                tiklandi = true;
                iscolor = true;

            }
        }
    }
    private void OnMouseDrag()
    { //Olu�turulan obje clone mouse pozisyonunu takip eder bas�l� tutuldu�u taktirde
        //10 degeri cameran�n z posiyonunu ald��� i�in g�z�kmemesinden yaz�lm��t�r.
        if (tiklandi)
        {
            clone.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
        }
    }
    private void OnMouseUp()
    {  /*E�er tahtay� olu�turabilecek durumlar sa�land�ysa t�klanma b�rak�ld���nda tahta azaltma metodu ile
        oyuncunun hamle yapma de�eri gnderilir*/
        if (clone != null && clone.GetComponent<engel>().isCreated)
        {
            
            clone.GetComponent<BoxCollider2D>().enabled = true;
            tiklandi = false;
            GetComponentInParent<GameManager>().TahtaAzalt();
            GetComponentInParent<GameManager>().pm.SendMove(tiklandi,true);
            iscolor = false;
        }
        else
        {
            PhotonNetwork.Destroy(clone);
            tiklandi = false;
        }
    }

  
}
