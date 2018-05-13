using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSystem : MonoBehaviour
{
    public Button[] audioButtons;
    private Image[] audioButtonImages;

    public AudioMixerSnapshot[] musicSnapshots;
    public AudioMixerSnapshot[] sfxSnapshots;
    public AudioMixerSnapshot[] rainSnapshots;

    private int music;
    private int sfx;
    private int rain;

    public Sprite[] musicSprites;
    public Sprite[] sfxSprites;
    public Sprite[] rainSprites;

	void Start ()
    {
        audioButtons[0].onClick.AddListener(() => MuteMusic());
        audioButtons[1].onClick.AddListener(() => MuteSFX());
        audioButtons[2].onClick.AddListener(() => MuteRain());

        audioButtonImages = new Image[audioButtons.Length];
        for (int i = 0; i < audioButtons.Length; i++)
        {
            Image[] images = audioButtons[i].GetComponentsInChildren<Image>();
            audioButtonImages[i] = images[1];
        }
	}
	
	void MuteMusic()
    {
        music = (music + 1) % 2;
        musicSnapshots[music].TransitionTo(0.0f);
        audioButtonImages[0].sprite = musicSprites[music];
    }

    void MuteSFX()
    {
        sfx = (sfx + 1) % 2;
        sfxSnapshots[sfx].TransitionTo(0.0f);
        audioButtonImages[1].sprite = sfxSprites[sfx];
    }

    void MuteRain()
    {
        rain = (rain + 1) % 2;
        rainSnapshots[rain].TransitionTo(0.0f);
        audioButtonImages[2].sprite = rainSprites[rain];
    }
}
