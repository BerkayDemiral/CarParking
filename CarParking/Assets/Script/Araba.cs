using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Araba : MonoBehaviour
{
    public bool ilerle;
    bool DurusNoktasiDurumu = false;
    float YukselmeDeger;
    bool PlatformYukselt;

    public GameObject[] Tekerizleri;
    public Transform parent;
    public GameManager _GameManager;
    public GameObject ParcPoint;
    



    void Update()
    {
        if (!DurusNoktasiDurumu)
            transform.Translate(7f * Time.deltaTime * transform.forward);

        if (ilerle)
            transform.Translate(15f * Time.deltaTime * transform.forward);

        if (PlatformYukselt)
        {
            if (YukselmeDeger > _GameManager.Platform_1.transform.position.y)
            {
                _GameManager.Platform_1.transform.position = Vector3.Lerp(_GameManager.Platform_1.transform.position, new Vector3(_GameManager.Platform_1.transform.position.x, _GameManager.Platform_1.transform.position.y + 1.3f, _GameManager.Platform_1.transform.position.z), .010f);
            }
            else
            {
                PlatformYukselt = false;
            }

        }

    }

    private void OnCollisionEnter(Collision collision)
    {

        
         if (collision.gameObject.CompareTag("Parking"))
        {
            ArabaTeknik›slemi();
            transform.SetParent(parent);

            if (_GameManager.YukselecekPlatformVarmi)
            {
                YukselmeDeger = _GameManager.Platform_1.transform.position.y + 1.3f;
                PlatformYukselt = true;
            }
            _GameManager.YeniArabaGetir();
        }

        
        
        else if (collision.gameObject.CompareTag("Araba"))
        {
            _GameManager.CarpmaEfekti.transform.position = ParcPoint.transform.position;
            _GameManager.CarpmaEfekti.Play();
            ArabaTeknik›slemi();
            _GameManager.Kaybettin();
        }


    }

    void ArabaTeknik›slemi()
    {
        ilerle = false;
        Tekerizleri[0].SetActive(false);
        Tekerizleri[1].SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DurusNoktasi"))
        {
            DurusNoktasiDurumu = true;
        }
        else if (other.CompareTag("Elmas"))
        {
            other.gameObject.SetActive(false);
            _GameManager.ElmasSayisi++;
            _GameManager.Sesler[0].Play();
        }
        else if (other.CompareTag("OrtaGobek"))
        {
            _GameManager.CarpmaEfekti.transform.position = ParcPoint.transform.position;
            _GameManager.CarpmaEfekti.Play();
            ArabaTeknik›slemi();
            _GameManager.Kaybettin();
        }
        else if (other.CompareTag("On_Parking"))
        {
            other.gameObject.GetComponent<On_Parking>().Parking.SetActive(true);
        }
    }
}
