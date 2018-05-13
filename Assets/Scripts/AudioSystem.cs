using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour
{
    public Button[] audioButtons;
    private Text[] audioButtonTexts;

    public AudioMixerSnapshot[] musicSnapshots;
    public AudioMixerSnapshot[] sfxSnapshots;
    public AudioMixerSnapshot[] rainSnapshots;

    private int music;
    private int sfx;
    private int rain;

	void Start ()
    {
        audioButtons[0].onClick.AddListener(() => MuteMusic());
        audioButtons[1].onClick.AddListener(() => MuteSFX());
        audioButtons[2].onClick.AddListener(() => MuteRain());

        audioButtonTexts = new Text[audioButtons.Length];
        for (int i = 0; i < audioButtons.Length; i++)
            audioButtonTexts[i] = audioButtons[i].GetComponentInChildren<Text>();
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

    void MuteRain()
    {
        rain = (rain + 1) % 2;
        rainSnapshots[rain].TransitionTo(0.0f);
        audioButtonTexts[2].text = string.Format("Rain: {0}", rain == 0 ? "On" : "Off");
    }
}
