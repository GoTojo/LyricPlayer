using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {
	private static SMFPlayer smfPlayer;
	private AudioSource audioSource;
	public int songnum = 0;
	public int measure = 0;
	private uint currentMsec = 0;

	// Start is called before the first frame update
	void Awake() {
		MidiWatcher midiWatcher = MidiWatcher.Instance;
		midiWatcher.onMidiIn += MIDIIn;
		midiWatcher.onLyricIn += LyricIn;
		midiWatcher.onTempoIn += TempoIn;
		midiWatcher.onBeatIn += BeatIn;
		midiWatcher.onMeasureIn += MeasureIn;

		SongInfo.SetCurSongnum(songnum);
		smfPlayer = new SMFPlayer(SongInfo.GetSMFPath(), SongInfo.GetNumOfMeasure());
		smfPlayer.midiHandler = MidiWatcher.Instance;
		FontResource.Instance.LoadFont();
		SentenceList.Instance.Init(smfPlayer);
		string clipname = SongInfo.GetAudioClipName();
		// Debug.Log($"clipname = {clipname}");
		audioSource = GetComponent<AudioSource>();
		AudioClip clip = Resources.Load<AudioClip>(clipname);
		audioSource.clip = clip;
	}
	void Start() {
	}

	// Update is called once per frame
	void Update() {
		smfPlayer.Update();
	}
	public void MIDIIn(int track, byte[] midiEvent, float position, uint currentMsec) {
	}
	public void LyricIn(int track, string lyric, float position, uint currentMsec) {
	}
	public void TempoIn(float msecPerQuaterNote, uint tempo, uint currentMsec) {
	}
	public void BeatIn(int numerator, int denominator, uint currentMsec) {
	}
	public void MeasureIn(int measure, int measureInterval, uint currentMsec) {
	}
	public void EventIn(MIDIHandler.Event playerEvent) {
		Debug.Log(playerEvent.ToString());
	}
	public void OnPlayClicked() {
		if (audioSource.isPlaying) {
			audioSource.Stop();
			this.currentMsec = smfPlayer.currentMsec;
			this.measure = smfPlayer.currentMeasure;
			smfPlayer.Stop();
			LyricGenList.Clear();
		} else {
			LyricData data = SentenceList.Instance.GetSentence(0, measure);
			currentMsec = data.msec;
			smfPlayer.Start(currentMsec);
			audioSource.time = currentMsec / 1000f;
			LyricGenList.Start(measure);
			audioSource.Play();
		}
	}
	public void OnRepeatClicked() {
		Debug.Log("Repeatボタンが押されたよ！！"); // コンソールに表示
	}
	public void OnCurPosChanged() {
		Debug.Log("スライダ変わったよ"); // コンソールに表示
	}
	public void OnInPosChanged() {
		Debug.Log("スライダ変わったよ"); // コンソールに表示
	}
	public void OnOutPosChanged() {
		Debug.Log("スライダ変わったよ"); // コンソールに表示
	}
}
