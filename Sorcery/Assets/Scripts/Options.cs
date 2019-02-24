using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {
	[SerializeField] private AudioClip sound;

	[SerializeField] private Slider cameraSlider;

	[SerializeField] private Button low;
	[SerializeField] private Button med;
	[SerializeField] private Button high;
	private Color lowColor;
	private Color medColor;
	private Color highColor;

	[SerializeField] private Button soundOn;
	[SerializeField] private Button soundOff;
	private Color soundOnColor;
	private Color soundOffColor;

	[SerializeField] private Button musicOn;
	[SerializeField] private Button musicOff;
	private Color musicOnColor;
	private Color musicOffColor;

	void Awake(){
//		PlayerPrefs.DeleteAll ();
		QualitySettings.SetQualityLevel (5, true);
	}

	void Start () {

		if (PlayerPrefs.HasKey ("CameraDistance")) {
			cameraSlider.value = PlayerPrefs.GetFloat ("CameraDistance");
		}
		lowColor = low.GetComponent<Image> ().color;
		medColor = med.GetComponent<Image> ().color;
		highColor = high.GetComponent<Image> ().color;
		switch(QualitySettings.GetQualityLevel()){
		case 0:
			low.GetComponent<Image> ().color = lowColor;
			med.GetComponent<Image> ().color = Color.black;
			high.GetComponent<Image> ().color = Color.black;
			break;
		case 2:
			low.GetComponent<Image> ().color = Color.black;
			med.GetComponent<Image> ().color = medColor;
			high.GetComponent<Image> ().color = Color.black;
			break;
		case 5:
			low.GetComponent<Image> ().color = Color.black;
			med.GetComponent<Image> ().color = Color.black;
			high.GetComponent<Image> ().color = highColor;
			break;
		default:
			break;
		}

		soundOnColor = soundOn.GetComponent<Image> ().color;
		soundOffColor = soundOff.GetComponent<Image> ().color;
		if (AudioController.Audio.soundMute) {
			soundOn.GetComponent<Image> ().color = Color.black;
			soundOff.GetComponent<Image> ().color = soundOffColor;
		} else {
			soundOn.GetComponent<Image> ().color = soundOnColor;
			soundOff.GetComponent<Image> ().color = Color.black;
		}

		musicOnColor = musicOn.GetComponent<Image> ().color;
		musicOffColor = musicOff.GetComponent<Image> ().color;
		if (AudioController.Audio.musicMute) {
			musicOn.GetComponent<Image> ().color = Color.black;
			musicOff.GetComponent<Image> ().color = musicOffColor;
		} else {
			musicOn.GetComponent<Image> ().color = musicOnColor;
			musicOff.GetComponent<Image> ().color = Color.black;
		}
	}

//	public void vibrate(){
//		Handheld.Vibrate ();
//	}

	public void soundFX(){
		AudioController.Audio.PlaySound(sound);
	}

	public void camDistance(float dist){
		PlayerPrefs.SetFloat ("CameraDistance", dist);
	}

	public void lowQuality(){
		QualitySettings.SetQualityLevel (0, true);
		low.GetComponent<Image> ().color = lowColor;
		med.GetComponent<Image> ().color = Color.black;
		high.GetComponent<Image> ().color = Color.black;
		AudioController.Audio.PlaySound(sound);
	}

	public void medQuality(){
		QualitySettings.SetQualityLevel (2, true);
		low.GetComponent<Image> ().color = Color.black;
		med.GetComponent<Image> ().color = medColor;
		high.GetComponent<Image> ().color = Color.black;
		AudioController.Audio.PlaySound(sound);
	}

	public void highQuality(){
		QualitySettings.SetQualityLevel (5, true);
		low.GetComponent<Image> ().color = Color.black;
		med.GetComponent<Image> ().color = Color.black;
		high.GetComponent<Image> ().color = highColor;
		AudioController.Audio.PlaySound(sound);
	}

	public void sfxOn() { // button
		if (AudioController.Audio.soundMute) {
			AudioController.Audio.soundMute = !AudioController.Audio.soundMute;
			soundOn.GetComponent<Image> ().color = soundOnColor;
			soundOff.GetComponent<Image> ().color = Color.black;
		}
		AudioController.Audio.PlaySound(sound);
	}

	public void sfxOff() { // button
		if (!AudioController.Audio.soundMute) {
			AudioController.Audio.soundMute = !AudioController.Audio.soundMute;
			soundOn.GetComponent<Image> ().color = Color.black;
			soundOff.GetComponent<Image> ().color = soundOffColor;
		}

		AudioController.Audio.PlaySound(sound);
	}
		
	public void OnSoundValue(float volume) { //slider
		AudioController.Audio.soundVolume = volume;
	}

	public void musicToggleOn(){
		if(AudioController.Audio.musicMute){
			AudioController.Audio.musicMute = !AudioController.Audio.musicMute;
			musicOn.GetComponent<Image> ().color = musicOnColor;
			musicOff.GetComponent<Image> ().color = Color.black;
		}
		AudioController.Audio.PlaySound(sound);
	}

	public void musicToggleOff(){
		if(!AudioController.Audio.musicMute){
			AudioController.Audio.musicMute = !AudioController.Audio.musicMute;
			musicOn.GetComponent<Image> ().color = Color.black;
			musicOff.GetComponent<Image> ().color = musicOffColor;
		}
		AudioController.Audio.PlaySound(sound);
	}

	public void OnMusicValue(float volume) { //slider
		AudioController.Audio.musicVolume = volume;
	}
	public void OnPlayMusic(int selector) {
		AudioController.Audio.PlaySound(sound);
		switch (selector) {
		case 1:
			AudioController.Audio.PlayIntroMusic();
			break;
		case 2:
			AudioController.Audio.PlayLevelMusic();
			break;
		default:
			AudioController.Audio.StopMusic();
			break;
		}
	}
}
