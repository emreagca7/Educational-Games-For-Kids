using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class puanManager : MonoBehaviour
{
    private int toplamPuan;
    private int puanArtisi;
    [SerializeField] private TextMeshProUGUI puanText;
    
    void Start()
    {
        puanText.text = toplamPuan.ToString();
    }
    public void PuaniArttir(string zorlukSeviyesi)
    {
        switch (zorlukSeviyesi)
        {
            case "kolay":
                puanArtisi = 5;
                break;
            case "orta":
                puanArtisi = 10;
                break;
            case "zor":
                puanArtisi = 15;
                break;
        }
        toplamPuan += puanArtisi;
        puanText.text = toplamPuan.ToString();
    }

   
}
