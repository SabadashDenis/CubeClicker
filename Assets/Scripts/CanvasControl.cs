using UnityEngine;
using UnityEngine.UI;

public class CanvasControl : MonoBehaviour
{
    [SerializeField]
    private Sprite _musicOn;
    [SerializeField]
    private Sprite _musicOff;
    [SerializeField]
    private Image _musicImage;
    [SerializeField]
    private GameObject _cubeClickSound;

    void Start()
    {
        MusicOnOff();
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MusicOnOff()
    {
        if(PlayerPrefs.GetString("Music") == "On")
        {
            PlayerPrefs.SetString("Music", "Off");
            GetComponent<AudioSource>().volume = 0;
            _cubeClickSound.gameObject.GetComponent<AudioSource>().volume = 0;
            _musicImage.GetComponent<Image>().sprite = _musicOff;
        } else
        {
            PlayerPrefs.SetString("Music", "On");
            GetComponent<AudioSource>().volume = 0.2f;
            _cubeClickSound.gameObject.GetComponent<AudioSource>().volume = 1;
            _musicImage.GetComponent<Image>().sprite = _musicOn;
        }
    }
}
