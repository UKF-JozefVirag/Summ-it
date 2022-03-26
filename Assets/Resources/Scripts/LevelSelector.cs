using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private int numOfLevels;
    public GameObject levelButton;
    public RectTransform ParentPanel;
    int levelReached;

    private void Awake()
    {
        numOfLevels = PlayerPrefs.GetInt("LevelCompleted", 1) + 20;
        LevelButtons();
    }

    private void LevelButtons()
    {
        if (PlayerPrefs.HasKey("LevelCompleted"))
        {
            levelReached = PlayerPrefs.GetInt("LevelCompleted");
        }
        else
        {
            PlayerPrefs.SetInt("LevelCompleted", 1);
            levelReached = PlayerPrefs.GetInt("LevelCompleted");
        }

        for (int i = 0; i < numOfLevels; i++)
        {
            int x = new int();
            x = i + 1;
            GameObject lvlButton = Instantiate(levelButton);
            lvlButton.transform.SetParent(ParentPanel, false);
            TMP_Text buttonText = lvlButton.GetComponentInChildren<TMP_Text>();
            buttonText.text = (i + 1).ToString();

            lvlButton.gameObject.GetComponent<Button>().onClick.AddListener(delegate {
                LevelSelected(x);
            });

            if (i > levelReached)
            {
                lvlButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    void LevelSelected(int index)
    {
        PlayerPrefs.SetInt("CurrentLevel", index);
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
