using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Camera camera;
    private AudioSource audioSource;
    [SerializeField]private Button button;

    private void Start()
    {
        audioSource = button.GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetInt("Sound", 1);
        }
    }

    public void LevelSelector()
    {
        audioSource.pitch = 1.5f;
        audioSource.Play();
        SceneManager.LoadScene("LevelSelector");
    }



}
