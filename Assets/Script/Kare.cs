
using UnityEngine;
using System.Collections;
using Photon.Pun;
public class Kare : MonoBehaviour
{
    GameObject player;
    PhotonView pw;
    GameManager gm;
    [SerializeField] AudioSource ses;

    public bool isClick,isColor,degiyormu;
    
    private void Start()
    {
        StartCoroutine(PlayerBul());
        pw = GetComponent<PhotonView>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    IEnumerator PlayerBul()
    {
        while (true)
        {
            yield return null;
            if (FindObjectOfType<Player>())
            {
                player = GameObject.FindGameObjectWithTag("Player");
                break;
            }
            else
            {
                continue;
            }
        }
    }
    private void OnMouseOver()
    {
        if (isClick && Input.GetMouseButtonDown(0))
        {
            ses.Play();
            player.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            gm.pm.SendMove(player.transform.position,true);
        }
    }
    private void Update()
    {
        if (degiyormu&&player.GetComponent<Player>().tahtakoy)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            isClick = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            isClick = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        isClick = false;
        degiyormu = false;
    }

}
