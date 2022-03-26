using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public int btn_value = 1;
    private GameObject btn_text;
    private Color32[] colors = { new Color32(255,0,0,255), new Color32(0, 0, 255, 255), 
        new Color32(0, 195, 195, 255), new Color32(0,255,0,180), new Color32(195, 195, 0, 255), 
        new Color32(185, 255, 100, 255), new Color32(108, 122, 137, 255) };

    private void Awake(){
        GetRandomBtnValue();
        SetButtonValue();
        SetButtonColor();
        gameObject.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    private void GetRandomBtnValue(){
        gameObject.tag = "Empty";
        int level = PlayerPrefs.GetInt("CurrentLevel");
        int tmp = 1;
        if (level % 5 == 0) tmp++;

        if (level < 11) 
            btn_value = Random.Range((level - level + 1) * tmp, (level + 10) * tmp);
        else
            btn_value = Random.Range((level - 10) * tmp, (level + 10) * tmp);
    }

    private void SetButtonValue(){
        btn_text = gameObject.transform.GetChild(0).gameObject;
        btn_text.GetComponent<TMP_Text>().text = btn_value.ToString();
    }

    private void SetButtonColor(){
        gameObject.GetComponent<Image>().color = colors[Random.Range(0, colors.Length)];
    }

    public void ButtonClicked(){
        int numClicked = System.Int32.Parse(gameObject.transform.GetChild(0).
            GetComponent<TMP_Text>().text);
        if (gameObject.tag != "Clicked") gameObject.tag = "Clicked";
        else gameObject.tag = "Unclicked";
        PlayerPrefs.SetInt(gameObject.name, 1);
        gameObject.GetComponent<Button>().interactable = false;
    }
}
