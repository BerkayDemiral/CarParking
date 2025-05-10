using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
 

public class GameManager : MonoBehaviour
{
    [Header("ARABA AYARLAR")]
    public GameObject[] Arabalar;
    public int KacArabaOlsun;
    int KalanAracSayisiDegeri;
    int AktifAracIndex;

    [Header("Canvas AYARLAR")]
    public Sprite AracGeldiGorseli;
    public TextMeshProUGUI KalanAracSayisi;
    public GameObject[] ArabaCanvasGorselleri;
    public TextMeshProUGUI[] Textler;
    public GameObject[] Paneller;
    public GameObject[] TapToButonlar;

    [Header("PLATFORM AYARLAR")]
    public GameObject Platform_1;
    public GameObject Platform_2;
    public float[] DonusHizlari;
    public bool YukselecekPlatformVarmi;
    bool DonusVarmi;

    [Header("Level AYARLAR")]
    public int ElmasSayisi;
    public ParticleSystem CarpmaEfekti;
    public AudioSource[] Sesler;
    bool DokunmaKilidi;
    
    void Start()
    {
        DokunmaKilidi= true;
        DonusVarmi = true;
        VarSayilanDegerleriKontrolEt();

        KalanAracSayisiDegeri = KacArabaOlsun;

        //KalanAracSayisi.text = KalanAracSayisiDegeri.ToString();
        for (int i = 0; i < KacArabaOlsun; i++)
        {
            ArabaCanvasGorselleri[i].SetActive(true);
        }

    }

    public void YeniArabaGetir()
    {
        KalanAracSayisiDegeri--;
        if (AktifAracIndex < KacArabaOlsun)
        {
            Arabalar[AktifAracIndex].SetActive(true);
        }
        else
        {
            Kazandin();
        }


        ArabaCanvasGorselleri[AktifAracIndex-1].GetComponent<UnityEngine.UI.Image>().sprite = AracGeldiGorseli;
        //KalanAracSayisi.text = KalanAracSayisiDegeri.ToString();
    }


    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (DokunmaKilidi)
                {
                    Paneller[0].SetActive(false);
                    Paneller[3].SetActive(true);
                    DokunmaKilidi = false;
                }
                else
                {
                    Arabalar[AktifAracIndex].GetComponent<Araba>().ilerle = true;
                    AktifAracIndex++;
                }

            }

        }


        if (Input.GetKeyDown(KeyCode.G))
        {
            Arabalar[AktifAracIndex].GetComponent<Araba>().ilerle = true;
            AktifAracIndex++;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Paneller[0].SetActive(false);
            Paneller[3].SetActive(true);
        }

        if (DonusVarmi)
        {
            Platform_1.transform.Rotate(new Vector3(0, 0, -DonusHizlari[0]), Space.Self);
            if(Platform_2!=null)
            Platform_2.transform.Rotate(new Vector3(0, 0, DonusHizlari[1]), Space.Self);
        }
    }

    void Kazandin()
    {
        PlayerPrefs.SetInt("Elmas", PlayerPrefs.GetInt("Elmas") + ElmasSayisi);
        Textler[2].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[3].text = SceneManager.GetActiveScene().name;
        Textler[4].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[5].text = ElmasSayisi.ToString();
        Sesler[2].Play();

        Paneller[1].SetActive(true);
        Paneller[3].SetActive(false);
        Invoke("KazandinButonuOrtayaCikart", 2f);
    }

    void KazandinButonuOrtayaCikart()
    {
        TapToButonlar[0].SetActive(true);
    }
    void KaybettinButonuOrtayaCikart()
    {
        TapToButonlar[1].SetActive(true);
    }


    public void Kaybettin()
    {
        DonusVarmi = false;
        Textler[6].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[7].text = SceneManager.GetActiveScene().name;
        Textler[8].text = (KacArabaOlsun - KalanAracSayisiDegeri).ToString();
        Textler[9].text = ElmasSayisi.ToString();
        Sesler[1].Play();
        Sesler[3].Play();


        Paneller[2].SetActive(true);
        Paneller[3].SetActive(false);
        Invoke("KaybettinButonuOrtayaCikart", 2f);
    }


    public void izleVeDevamEt()
    {
        //önceki oyunda yapmýþtým.
    }

    public void izleVeDahaFazlaKazan()
    {
        //önceki oyunda yapmýþtým.
    }

    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SonrakiLevel()
    {
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // BELLEK YÖNETÝMÝ
    void VarSayilanDegerleriKontrolEt()
    {
        Textler[0].text = PlayerPrefs.GetInt("Elmas").ToString();
        Textler[1].text = SceneManager.GetActiveScene().name;
    }
}
