using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carpisma : MonoBehaviour
{
    public AudioSource ses_dosyasi;
    public AudioClip temas_sesi;
    public AudioClip sayi_sesi;

    yonetici yonet;
    void Start()
    {
        yonet = GameObject.Find("yonetici").GetComponent<yonetici>();
    }

    private void OnCollisionEnter(Collision nesne)
    {
        if (nesne.gameObject.tag == "delikler" && gameObject.tag == "top")
        {
            ses_dosyasi.PlayOneShot(sayi_sesi);
            yonet.skor_arttir();
            Destroy(gameObject);
        }

        if (nesne.gameObject.tag == "delikler" && gameObject.tag == "Player")
        {
            ses_dosyasi.PlayOneShot(sayi_sesi);
            yonet.beyaz_topu_resetle();
        }

        if(nesne.gameObject.tag == "top")
        {
            ses_dosyasi.PlayOneShot(temas_sesi);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
