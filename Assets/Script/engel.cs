
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class engel : MonoBehaviour
{
    [SerializeField] float _radius;
    Vector3 pos;
    public bool isCreated;
    PhotonView pw;
    private void Start()
    {
        pw = GetComponent<PhotonView>();   
    }
    private void Update()
    {

        if (pw.IsMine)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, _radius);
            if (col != null)
            {


                pos = Vector3.zero;
                foreach (var item in col)
                {

                    if (item.tag == "kare" && col.Length == 4)
                    {
                        pos += item.transform.position;
                        isCreated = true;
                    }
                    else
                    {
                        isCreated = false;

                    }
                }

                if (isCreated)
                {
                    transform.position = pos / 4;
                    GetComponent<SpriteRenderer>().color = Color.green;
                 
                }
                else
                {
                    GetComponent<SpriteRenderer>().color = Color.yellow;
                }


            }
        }
    }
        
 }
 
 



