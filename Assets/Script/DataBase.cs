using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DataBase : MonoBehaviour
{
    [SerializeField] TMP_InputField isim, odaisim;
    [SerializeField] GameObject[] paneller;
    [SerializeField] AudioSource ses;
    [SerializeField] Image sesCarpi;
    float sesDuzeyi;
    bool carpi;

    void Start()
    {
        sesDuzeyi = 0.5f;
        odaisim.text = PlayerPrefs.GetString("odaisim");
        isim.text = PlayerPrefs.GetString("isim");
        sesCarpi.enabled = carpi;
        if (PlayerPrefs.HasKey("ses")) ses.volume = PlayerPrefs.GetFloat("ses");
        else PlayerPrefs.SetFloat("ses", sesDuzeyi);
        if (PlayerPrefs.HasKey("carpi")) sesCarpi.enabled = Convert.ToBoolean(PlayerPrefs.GetInt("carpi"));
        else PlayerPrefs.SetInt("carpi", 0);
    }
    public void OdaKur()
    {
        if (isim.text != string.Empty && odaisim.text != string.Empty)
        {
            PlayerPrefs.SetString("isim", isim.text);
            PlayerPrefs.SetString("odaisim", odaisim.text);
            ServerManager.instance.GetComponent<ServerManager>().OdayaBaglan();  
        }
        else
        {
            paneller[2].SetActive(true);
            paneller[3].SetActive(true);
        }
    }
    public void Rastgele()
    {
        PlayerPrefs.SetString("isim", isim.text);
        ServerManager.instance.GetComponent<ServerManager>().RastGeleOda();
    }
    public void Baslat()
    {
        paneller[0].SetActive(false);
        paneller[1].SetActive(true); 
    }
    public void VerileriSil()
    {
        PlayerPrefs.DeleteAll();
        isim.text = string.Empty;
        odaisim.text = string.Empty;
    }
    public void SesKapa()
    {
        
        if (ses.volume>=0.5f)
        {
            ses.volume = 0;
            PlayerPrefs.SetFloat("ses", ses.volume);
            PlayerPrefs.SetInt("carpi", 1);
            carpi = Convert.ToBoolean(PlayerPrefs.GetInt("carpi"));
            sesCarpi.enabled = carpi;
            
        }
           
        else
        {
            ses.volume = 0.5f;
            PlayerPrefs.SetFloat("ses", ses.volume);
            PlayerPrefs.SetInt("carpi", 0);
            carpi = Convert.ToBoolean(PlayerPrefs.GetInt("carpi"));
            sesCarpi.enabled = carpi;

            
            
        }

    }
    
}