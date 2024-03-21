using Photon.Pun;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    [SerializeField] GameObject gm;

    
    public void GelenDeger(string text)
    {
        gm.GetComponent<PhotonView>().RPC("OyunBitir", RpcTarget.All, text);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PhotonView>().Owner.IsMasterClient && gameObject.tag == "Finish1")
        {
            GelenDeger(collision.GetComponent<PhotonView>().Owner.NickName+" Kazandý");
        }
        else if(gameObject.tag == "Finish2")
        {
            GelenDeger(collision.GetComponent<PhotonView>().Owner.NickName + " Kazandý");

        }
    }

}
