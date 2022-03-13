using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Slider timer;
    public TextMeshProUGUI timer_txt;
    public TextMeshProUGUI Sum_text;
    public GameObject number_button;
    public GameObject grid;
    public GameObject LevelOverPanel;
    public GameObject PausePanel;
    private GameObject[] btns = new GameObject[16];
    private GameObject[] txt = new GameObject[16];
    private int[] values = new int[16];
    private int result;
    private ArrayList clickedValues = new ArrayList();
    private int countOfNewSum = 3;
    private int hp = 3;

    private void Awake()
    {
        SetBtnValueZero();
        PlayerPrefs.Save();
    }

    private void Start()
    {
        SpawnButtons(16);
        FillArrayWithNumbers();
        SetNewResult();
        timer.maxValue = 10;
    }

    private void Update()
    {
        StartTimer();
        timer_txt.text = ""+ System.Math.Round((timer.maxValue - timer.value));
        for (int i = 0; i < 16; i++)
        {
            if (PlayerPrefs.GetInt("btn_" + i) == 1)
            {
                int n = System.Int32.Parse(GameObject.Find("btn_" + i).transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                clickedValues.Add(n);
                PlayerPrefs.SetInt("btn_" + i, 0);
            }
        }
        
    }

    private void SetBtnValueZero()
    {
        for (int i = 0; i < 16; i++)
        {
            PlayerPrefs.SetInt("btn_" + i, 0);
        }
    }

    private void SetNewResult()
    {
        int firstbtn = Random.Range(0, btns.Length);
        int secondbtn = Random.Range(0, btns.Length);
        if (secondbtn == firstbtn)
        {
            secondbtn = values[Random.Range(0, values.Length)];
        }

        int firstNum = values[firstbtn];
        int secondNum = values[secondbtn];

        result = firstNum + secondNum;
        Sum_text.text = result.ToString();
    }

    private void FillArrayWithNumbers()
    {
        for (int i = 0; i < 16; i++)
        {
            btns[i] = grid.transform.GetChild(i).gameObject;
            txt[i] = btns[i].transform.GetChild(0).gameObject;
            values[i] = System.Int32.Parse(txt[i].GetComponent<TMP_Text>().text);
        }
    }

    private void SpawnButtons(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject x = Instantiate(number_button, grid.transform);
            x.name = "btn_" + i;
        }
    }

    void StartTimer()
    {
        timer.value += Time.deltaTime;
    }

    public void PauseGame()
    {
        PausePanel.SetActive(true);
        PausePanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "PAUSED";
        PausePanel.transform.GetChild(3).gameObject.SetActive(true);
        PausePanel.transform.GetChild(2).gameObject.SetActive(false);
        Time.timeScale = 0f;
    }

    public void SubmitResult()
    {
        int total = 0;

        for (int i = 0; i < clickedValues.Count; i++)
        {
            int o = (int)clickedValues[i];
            total += o;
        }

        // ak hrac tukne na spravne cislice a sucet sa zhoduje, vypise sa novy sucet a casomiera sa predlzi o 3 sekundy
        if (total == result)
        {
            countOfNewSum--;
            if (countOfNewSum != 0)
            {
                SetNewResult();
                timer.value -= 3;
            }
            else
            {
                LevelCompleted();
            }
        }
        else
        {
            // zvyraznenie vysledku v pripade zlej odpovede, odobratie hp, v pripade hp==0 koniec hry
            StartCoroutine(HighlightResult());
            hp--;
            if (hp == 0) GameOver();
        }
        for (int i = 0; i < 16; i++)
        {
            clickedValues.Clear();
            grid.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    private void GameOver()
    {
        LevelOverPanel.SetActive(true);
        LevelOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "GAME OVER";
        PausePanel.transform.GetChild(3).gameObject.SetActive(false);
        PausePanel.transform.GetChild(2).gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void LevelCompleted()
    {
        LevelOverPanel.SetActive(true);
        LevelOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "LEVEL " + PlayerPrefs.GetInt("CurrentLevel", 1) + " COMPLETED";
        PausePanel.transform.GetChild(3).gameObject.SetActive(false);
        PausePanel.transform.GetChild(2).gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    
    IEnumerator HighlightResult()
    {
        Sum_text.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.5f);
        Sum_text.color = new Color32(99, 93, 89, 255);
    }

}
