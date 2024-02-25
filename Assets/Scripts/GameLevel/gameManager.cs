using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditorInternal;
using UnityEngine.UI;
using TMPro;
public class gameManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject karePrefab;
    [SerializeField] 
    private Transform karelerPaneli;
    [SerializeField] 
    private Transform soruPaneli;
    [SerializeField]
    private TextMeshProUGUI soruText;
    [SerializeField]
    private GameObject sonucPaneli;
    [SerializeField]
    private Sprite[] kareSprites;
    [SerializeField]
    AudioSource audioSource;

    public AudioClip butonSesi;
    

    private GameObject[] karelerDizisi = new GameObject[25];
    private GameObject gecerliObje;
    private bool butonaBasilsinMi;
    List<int> bolumDegerlerListesi = new List<int>();
    private int bolenSayi, bolunenSayi, kacinciSoru;
    private int butonDegeri;
    private int dogruSonuc;
    private int kalanHak;
    string zorlukDerecesi;
    kalanHaklarManager kalanHaklarManager;
    puanManager puanManager;


    private void Awake()
    {
        butonaBasilsinMi = true;
        sonucPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        kalanHak = 3;
        audioSource = GetComponent<AudioSource>();
        kalanHaklarManager = Object.FindObjectOfType<kalanHaklarManager>();
        puanManager = Object.FindObjectOfType<puanManager>();
        kalanHaklarManager.kalanHaklariKontrolu(kalanHak);
    }
    void Start()
    {
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;
        KareleriOlustur();
    }
    public void KareleriOlustur()
    {
        for (int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            kare.transform.GetChild(1).GetComponent<Image>().sprite = kareSprites[Random.Range(0,kareSprites.Length)];
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            karelerDizisi[i] = kare;
        }
        BolumDegerleriniTexteYazdir();
        StartCoroutine(doFadeRoutine());
        Invoke("soruPaneliniAc", 2f);
    }
    void ButonaBasildi()
    {
        if (butonaBasilsinMi == true)
        {
            audioSource.PlayOneShot(butonSesi);
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text);
            gecerliObje = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            SonucuKontrolEt();
        }
        
    }
    void SonucuKontrolEt()
    {
        if (butonDegeri == dogruSonuc)
        {
            gecerliObje.transform.GetChild(1).GetComponent<Image>().enabled = true;
            gecerliObje.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = " ";
            gecerliObje.transform.GetComponent<Button>().interactable = false;
            puanManager.PuaniArttir(zorlukDerecesi);
            bolumDegerlerListesi.RemoveAt(kacinciSoru);
            if (bolumDegerlerListesi.Count>0)
            {
                soruPaneliniAc();
            }
            else
            {
                OyunBitti();
            }
            
        }
        else
        {
            kalanHak--;
            kalanHaklarManager.kalanHaklariKontrolu(kalanHak);
        }
        if (kalanHak <= 0)
        {
            OyunBitti();
        }
    }

    void OyunBitti()
    {
        butonaBasilsinMi = false;
        sonucPaneli.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }
    IEnumerator doFadeRoutine()
    {
        foreach (var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, 0.2f);

            yield return new WaitForSeconds(0.1f);
        }
    }
    void BolumDegerleriniTexteYazdir()
    {
        foreach (var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);
            bolumDegerlerListesi.Add(rastgeleDeger);
            kare.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rastgeleDeger.ToString();
        }
    }
    void soruPaneliniAc()
    {
        SoruyuSor();
        soruPaneli.GetComponent<RectTransform>().DOScale(1,0.3f).SetEase(Ease.OutBack);
    }
    void SoruyuSor()
    {
        bolenSayi = Random.Range(2, 11);
        kacinciSoru = Random.Range(0, bolumDegerlerListesi.Count);
        dogruSonuc = bolumDegerlerListesi[kacinciSoru];
        bolunenSayi = bolenSayi * dogruSonuc;
        if (bolunenSayi <= 40)
        {
            zorlukDerecesi = "kolay";
        }
        else if (bolunenSayi > 40 && bolunenSayi <= 80)
        {
            zorlukDerecesi = "orta";
        }
        else
        {
            zorlukDerecesi = "zor";
        }
        soruText.text = bolunenSayi.ToString() + " : " + bolenSayi.ToString();
    }


}
