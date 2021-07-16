using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class TestAudio : MonoBehaviour {
    public Button playBtn;
    public Toggle muteToggle;
    bool isOn = true;
    bool hasInit = false;
    private AudioClip bgClip;
    private AudioSource audioBG;

	// Use this for initialization
	void Start () {
        bgClip = Resources.Load<AudioClip>("Audio/bg");
        audioBG=GetComponent<AudioSource>();
        playBtn.onClick.AddListener(PlayBgMusic);
        audioBG.clip = bgClip;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    
    public void PlayBgMusic()
    {
        if (!audioBG.isPlaying)
        {
            audioBG.Play();
        }
        else
        {
            audioBG.Pause();
        }
    }
    public void MuteBG()
    {
        if (isOn)
            audioBG.mute = true;
        else
            audioBG.mute = false;
        isOn = !isOn;

    }
}
