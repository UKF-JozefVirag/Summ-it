using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider timer;
    public TextMeshProUGUI timer_txt;
    public TextMeshProUGUI Sum_text;
    public TextMeshProUGUI Level_txt;
    public GameObject number_button;
    public GameObject grid;
    public GameObject LevelOverPanel;
    public GameObject PausePanel;
    private GameObject[] btns = new GameObject[16];
    private GameObject[] txt = new GameObject[16];
    [SerializeField] private GameObject[] hpIcon = new GameObject[3];
    private int[] values = new int[16];
    private int result;
    private ArrayList clickedValues = new ArrayList();
    private int countOfNewSum = 3;
    private int hp = 3;
    private AudioSource audioSource;
    [SerializeField] private AudioClip corrRes, wrongRes, levelCompleted, gameOverSound;

    private void Awake(){
        SetBtnValueZero();
    }

    private void Start(){
        audioSource = transform.GetComponent<AudioSource>();
        Time.timeScale = 1f;
        SpawnButtons(16);
        FillArrayWithNumbers();
        SetNewResult();
        timer.maxValue = 10 + Mathf.RoundToInt(PlayerPrefs.GetInt("CurrentLevel") / 5);
        Level_txt.text = ""+ PlayerPrefs.GetInt("CurrentLevel");
    }

    private void Update(){
        StartTimer();
        timer_txt.text = ""+ System.Math.Round((timer.maxValue - timer.value));
        for (int i = 0; i < 16; i++){
            if (PlayerPrefs.GetInt("btn_" + i) == 1){
                int n = System.Int32.Parse(GameObject.Find("btn_" + i).
                    transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text);
                clickedValues.Add(n);
                PlayerPrefs.SetInt("btn_" + i, 0);
            }
        }
        if (timer.value == timer.maxValue){
            GameOver();
            return;
        }
        
    }

    private void SetNewResult(){
        int firstbtn = Random.Range(0, btns.Length);
        int secondbtn = Random.Range(0, btns.Length);
        if (secondbtn == firstbtn){
            secondbtn = values[Random.Range(0, values.Length)];
        }
        int firstNum = values[firstbtn];
        int secondNum = values[secondbtn];
        result = firstNum + secondNum;
        Sum_text.text = result.ToString();
    }

    private void FillArrayWithNumbers(){
        for (int i = 0; i < 16; i++){
            btns[i] = grid.transform.GetChild(i).gameObject;
            txt[i] = btns[i].transform.GetChild(0).gameObject;
            values[i] = System.Int32.Parse(txt[i].GetComponent<TMP_Text>().text);
        }
    }

    private void SpawnButtons(int count){
        for (int i = 0; i < count; i++){
            GameObject x = Instantiate(number_button, grid.transform);
            x.name = "btn_" + i;
        }
    }

    void StartTimer(){
        timer.value += Time.deltaTime;
    }

    public void SubmitResult(){
        int total = 0;
        for (int i = 0; i < clickedValues.Count; i++){
            int o = (int)clickedValues[i];
            total += o;
        }
        if (total == result){
            audioSource.PlayOneShot(corrRes);
            countOfNewSum--;
            if (countOfNewSum != 0){
                SetNewResult();
                timer.value -= 3;
            }else{
                LevelCompleted();
            }
        }else{
            audioSource.PlayOneShot(wrongRes);
            StartCoroutine(HighlightResult());
            hp--;
            switch (hp){
                case 0: hpIcon[0].SetActive(false);
                        break;
                case 1: hpIcon[1].SetActive(false);
                        break;
                case 2: hpIcon[2].SetActive(false);
                        break;
                case 3: hpIcon[0].SetActive(true);
                        hpIcon[1].SetActive(true);
                        hpIcon[2].SetActive(true);
                        break;
            } if (hp == 0) GameOver();
        }
        for (int i = 0; i < 16; i++){
            clickedValues.Clear();
            grid.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    private void SetBtnValueZero(){
        for (int i = 0; i < 16; i++){
            PlayerPrefs.SetInt("btn_" + i, 0);
        }
    }

    private void GameOver(){
        audioSource.PlayOneShot(gameOverSound);
        LevelOverPanel.SetActive(true);
        LevelOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().color = new Color(255f, 0f, 0f, 0.9f);
        LevelOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Underline;
        LevelOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "GAME OVER";
        LevelOverPanel.transform.GetChild(1).gameObject.SetActive(false);
        LevelOverPanel.transform.GetChild(4).gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    private void LevelCompleted(){
        audioSource.PlayOneShot(levelCompleted);
        LevelOverPanel.SetActive(true);
        LevelOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().fontStyle = FontStyles.Underline;
        LevelOverPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "LEVEL " + PlayerPrefs.GetInt("CurrentLevel", 1) + " COMPLETED";
        LevelOverPanel.transform.GetChild(4).gameObject.SetActive(false);
        LevelOverPanel.transform.GetChild(3).gameObject.SetActive(true);
        Time.timeScale = 0f;
        if (PlayerPrefs.GetInt("LevelCompleted") < PlayerPrefs.GetInt("CurrentLevel")) PlayerPrefs.SetInt("LevelCompleted", PlayerPrefs.GetInt("CurrentLevel", 1));
        else return;
    }

    public void PauseGame(){
        PausePanel.SetActive(true);
        PausePanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "PAUSED";
        PausePanel.transform.GetChild(3).gameObject.SetActive(true);
        PausePanel.transform.GetChild(2).gameObject.SetActive(false);
        Time.timeScale = 0f;
    }


    IEnumerator HighlightResult(){
        Sum_text.color = new Color32(255, 0, 0, 255);
        yield return new WaitForSeconds(0.5f);
        Sum_text.color = new Color32(99, 93, 89, 255);
    }

    public void LoadMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevels(){
        SceneManager.LoadScene("LevelSelector");
    }

    public void Replay(){
        SceneManager.LoadScene("Game");
    }

    public void NextLevel(){
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        PlayerPrefs.GetInt("CurrentLevel");
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void Resume(){
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

}
