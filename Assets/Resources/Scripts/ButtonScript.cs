using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public int btn_value = 1;
    private GameObject btn_text;
    private Color[] colors = { Color.red, Color.blue, Color.cyan, Color.green, Color.yellow, Color.magenta, Color.gray };
    private Color32[] colorss = { new Color32(255,0,0,255), new Color32(0, 0, 255, 255), new Color32(0, 195, 195, 255), new Color32(0,255,0,180), new Color32(195, 195, 0, 255), new Color32(185, 255, 100, 255), new Color32(108, 122, 137, 255) };

    private void Awake()
    {
        PlayerPrefs.SetInt("Level", 1);
        GetRandomBtnValue();
        SetButtonValue();
        SetButtonColor();
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void GetRandomBtnValue()
    {
        gameObject.tag = "Empty";
        if ((PlayerPrefs.GetInt("Level")) <= 5)
        {
            btn_value = Random.Range(1, 5);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 5 && (PlayerPrefs.GetInt("Level")) <= 10)
        {
            btn_value = Random.Range(1, 10);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 10 && (PlayerPrefs.GetInt("Level")) <= 20)
        {
            btn_value = Random.Range(5, 20);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 20 && (PlayerPrefs.GetInt("Level")) <= 30)
        {
            btn_value = Random.Range(10, 30);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 30 && (PlayerPrefs.GetInt("Level")) <= 40)
        {
            btn_value = Random.Range(15, 40);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 40 && (PlayerPrefs.GetInt("Level")) <= 50)
        {
            btn_value = Random.Range(20, 50);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 50 && (PlayerPrefs.GetInt("Level")) <= 60)
        {
            btn_value = Random.Range(25, 60);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 60 && (PlayerPrefs.GetInt("Level")) <= 70)
        {
            btn_value = Random.Range(30, 70);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 70 && (PlayerPrefs.GetInt("Level")) <= 80)
        {
            btn_value = Random.Range(35, 80);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 80 && (PlayerPrefs.GetInt("Level")) <= 90)
        {
            btn_value = Random.Range(40, 90);
        }

        else if ((PlayerPrefs.GetInt("Level")) > 90 && (PlayerPrefs.GetInt("Level")) <= 100)
        {
            btn_value = Random.Range(45, 100);
        }
    }

    private void SetButtonValue()
    {
        btn_text = gameObject.transform.GetChild(0).gameObject;
        btn_text.GetComponent<TMP_Text>().text = btn_value.ToString();
    }

    private void SetButtonColor()
    {
        gameObject.GetComponent<Image>().color = colorss[Random.Range(0, colors.Length)];
    }

    public void ButtonClicked()
    {
        int numClicked = System.Int32.Parse(gameObject.transform.GetChild(0).GetComponent<TMP_Text>().text);
        if (gameObject.tag != "Clicked")
        {
            gameObject.tag = "Clicked";
        }
        else 
        { 
            gameObject.tag = "Unclicked";
        }

        PlayerPrefs.SetInt(gameObject.name, 1);

        gameObject.GetComponent<Button>().interactable = false;
    }


}
