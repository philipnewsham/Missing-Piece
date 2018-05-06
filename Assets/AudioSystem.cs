using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour
{
    public Button[] audioButtons;
    private Text[] audioButtonTexts = new Text[2];

    public AudioMixerSnapshot[] musicSnapshots;
    public AudioMixerSnapshot[] sfxSnapshots;

    private int music;
    private int sfx;

	void Start ()
    {
        audioButtons[0].onClick.AddListener(() => MuteMusic());
        audioButtons[1].onClick.AddListener(() => MuteSFX());
        audioButtonTexts[0] = audioButtons[0].GetComponentInChildren<Text>();
        audioButtonTexts[1] = audioButtons[1].GetComponentInChildren<Text>();
	}
	
	void MuteMusic()
    {
        music = (music + 1) % 2;
        musicSnapshots[music].TransitionTo(0.0f);
        audioButtonTexts[0].text = string.Format("Music: {0}", music == 0 ? "On" : "Off");
    }

    void MuteSFX()
    {
        sfx = (sfx + 1) % 2;
        sfxSnapshots[sfx].TransitionTo(0.0f);
        audioButtonTexts[1].text = string.Format("SFX: {0}", sfx == 0 ? "On" : "Off");
    }
}
