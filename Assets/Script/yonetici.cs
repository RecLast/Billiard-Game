using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class yonetici : MonoBehaviour
{

    public LineRenderer cizgi;
    public Camera kamera;

    public Transform beyaztop;
    public Rigidbody beyaztop_guc;

    public Transform cubuk;

    public AudioSource ses_dosyasi;
    public AudioClip temas_sesi;
    public AudioClip sayi_sesi;

    float vurus_hizi = 0.0f;

    Vector3 cubugun_baslangic_koordinati;
    Vector3 beyaz_topun_baslangic_koordinati;

    int oyuncu1_skor = 0;
    int oyuncu2_skor = 0;

    bool oyuncu_degistir = false;

    public TMPro.TextMeshProUGUI oyuncu_txt;
    public TMPro.TextMeshProUGUI oyuncu_skorlari_txt;
    public TMPro.TextMeshProUGUI kazanan_txt;
    // Start is called before the first frame update
    void Start()
    {
        beyaz_topun_baslangic_koordinati = beyaztop.position;
        cubugun_baslangic_koordinati = cubuk.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        cizgi_ayari();
        fare_kontrol();
        gorunurluk();
    }

    void gorunurluk()
    {
        if (beyaztop_guc.velocity.magnitude <= 0.05f && cizgi.gameObject.activeSelf == false)
        {
            cizgi.gameObject.SetActive(true);
            cubuk.gameObject.SetActive(true);

            if (oyuncu_degistir == false)
            {
                oyuncu_txt.text = "1.Oyuncu";
            }
            else
            {
                oyuncu_txt.text = "2.Oyuncu";
            }
        }
    }

    void cizgi_ayari()
    {
        RaycastHit temas;
        Ray isik = kamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(isik, out temas))
        {
            Vector3 beyaz_topun_pozisyonu = beyaztop.position;
            Vector3 farenin_temas_yeri_pozisyonu = new Vector3(temas.point.x, temas.point.y, temas.point.z);

            cizgi.SetPosition(0, beyaz_topun_pozisyonu);
            cizgi.SetPosition(1, farenin_temas_yeri_pozisyonu);

            beyaztop.LookAt(farenin_temas_yeri_pozisyonu);
        }
    }

    void fare_kontrol()
    {
        if (Input.GetMouseButton(0) && cizgi.gameObject.activeSelf)
        {
            if (cubuk.localPosition.z >= -30.0f)
            {
                cubuk.Translate(0, 0, -3.0f * Time.deltaTime);
                vurus_hizi += 30.0f * Time.deltaTime;
            }
        }
        if (Input.GetMouseButtonUp(0) && cizgi.gameObject.activeSelf)
        {
            cubuk.localPosition = cubugun_baslangic_koordinati;
            Invoke("vur", 0.1f);
        }
    }

    void vur()
    {
        ses_dosyasi.PlayOneShot(temas_sesi);

        beyaztop_guc.velocity = beyaztop.forward * vurus_hizi;

        cizgi.gameObject.SetActive(false);
        cubuk.gameObject.SetActive(false);
        vurus_hizi = 0.0f;

        oyuncu_degistir = !oyuncu_degistir;
    }

    

    public void skor_arttir()
    {
        if (oyuncu_degistir== false)
        {
            oyuncu2_skor++;
            if(oyuncu2_skor == 8)
            {
                oyunu_bitir("2. Oyuncu Kazandý !");
            }
        }
        else
        {
            oyuncu1_skor++;
            if(oyuncu1_skor ==8)
            {
                oyunu_bitir("1. Oyuncu Kazandý !");
            }
        }
        oyuncu_skorlari_txt.text = "1. Oyuncu: " + oyuncu1_skor + " 2. Oyuncu: " + oyuncu2_skor;
    }

    void oyunu_bitir(string kazanan)
    {
        kazanan_txt.gameObject.transform.parent.gameObject.SetActive(true);
        kazanan_txt.text = kazanan;
    }

    public void tekrar_oyna()
    {
        SceneManager.LoadScene("Scenes/Level");
    }
    public void beyaz_topu_resetle()
    {
        beyaztop_guc.velocity = Vector3.zero;
        beyaztop.position = beyaz_topun_baslangic_koordinati;
    }
}
