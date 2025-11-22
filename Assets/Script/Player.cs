using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour {
	public static SMFPlayer smfPlayer;
	private AudioSource audioSource;
	public int songnum = 0;
	public int measure = 0;
	private uint currentMsec = 0;
	public EventListener eventListener;
	public GameObject editPanel;
	public GameObject transportPanel;
	public GameObject settingPanel;
	public Button playButton;
	public Button repeatButton;
	public Slider curPos;
	public TextMeshProUGUI textPos;
	public Slider pointA;
	private TextMeshProUGUI textA;
	public Slider pointB;
	private TextMeshProUGUI textB;
	private bool fRepeat = false;
	private Image playButtonImage;
	private Image repeatButtonImage;
	private float endTimer = 0;
	private int numOfMeas;

	// Start is called before the first frame update
	void Awake() {
		PlayerPrefs.SetInt("Song", songnum);
		MidiWatcher midiWatcher = MidiWatcher.Instance;
		midiWatcher.onMidiIn += MIDIIn;
		midiWatcher.onLyricIn += LyricIn;
		midiWatcher.onTempoIn += TempoIn;
		midiWatcher.onBeatIn += BeatIn;
		midiWatcher.onMeasureIn += MeasureIn;
		midiWatcher.onEventIn += EventIn;

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
		textPos = curPos.handleRect.GetComponentInChildren<TextMeshProUGUI>();
		textPos.text = curPos.value.ToString();
		textA = pointA.handleRect.GetComponentInChildren<TextMeshProUGUI>();
		textA.text = pointA.value.ToString();
		textB = pointB.handleRect.GetComponentInChildren<TextMeshProUGUI>();
		textB.text = pointB.value.ToString();
		numOfMeas = SongInfo.GetNumOfMeasure();
		if (numOfMeas < 0) {
			numOfMeas = SentenceList.Instance.tracks[0].lyrics.Count;
		}
		curPos.minValue = 0;
		curPos.maxValue = numOfMeas;
		pointA.minValue = 0;
		pointA.maxValue = numOfMeas - 1;
		pointA.value = 0;
		pointB.minValue = 1;
		pointB.maxValue = numOfMeas;
		pointB.value = numOfMeas;
		playButtonImage = playButton.GetComponent<Image>();
		repeatButtonImage = repeatButton.GetComponent<Image>();
	}
	void Start() {
	}

	void Update() {
		smfPlayer.Update();
		if (smfPlayer.isPlaying()) {
			if (!audioSource.isPlaying) {
				endTimer -= Time.deltaTime;
				if (endTimer <= 0) {
					PlayStop();
				}
			}
			measure = smfPlayer.currentMeasure;
			if (fRepeat && measure >= pointB.value) {
				PlayStop();
				LyricGenList.Clear();
				measure = (int)pointA.value;
				PlayStart();
			} else {
				curPos.value = measure;
				textPos.text = curPos.value.ToString();
			}
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			settingPanel.SetActive(!settingPanel.activeSelf);
		}
		if (!settingPanel.activeSelf) {
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (Input.GetKey(KeyCode.LeftShift)) {
					measure = 0;
					LyricGenList.Clear();
				}
				OnPlayClicked();
			}
			if (Input.GetKeyDown(KeyCode.T)) {
				transportPanel.SetActive(!transportPanel.activeSelf);
			}
			if (Input.GetKeyDown(KeyCode.E)) {
				editPanel.SetActive(!editPanel.activeSelf);
			}
		}
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
		// Debug.Log(playerEvent.ToString());
	}
	private void PlayStop() {
		audioSource.Stop();
		this.currentMsec = smfPlayer.currentMsec;
		this.measure = smfPlayer.currentMeasure;
		smfPlayer.Stop();
	}
	private void PlayStart() {
		if (measure >= numOfMeas - 1) {
			measure = 0;
		}
		LyricData data = SentenceList.Instance.GetSentence(0, measure);
		Lyrics.Reset();
		eventListener.UpdateControl(measure);
		LyricGenList.Start(measure);
		currentMsec = data.msec;
		smfPlayer.Start(currentMsec);
		audioSource.time = currentMsec / 1000f;
		audioSource.Play();
	}
	public void OnPlayClicked() {
		if (audioSource.isPlaying) {
			PlayStop();
		} else {
			endTimer = 1f;
			PlayStart();
		}
		UpdatePlayButtonImage();
	}
	public void OnRepeatClicked() {
		fRepeat = !fRepeat;
		repeatButtonImage.color = fRepeat ? Color.green : Color.gray;
	}
	public void OnCurPosChanged() {
		measure = (int)curPos.value;
		if (textPos) textPos.text = curPos.value.ToString();
	}
	public void OnInPosChanged() {
		if (pointA.value >= pointB.value) {
			pointA.value = pointB.value - 1;
		}
		if (textA) textA.text = pointA.value.ToString();
	}
	public void OnOutPosChanged() {
		if (pointB.value <= pointA.value) {
			pointB.value = pointA.value + 1;
		}
		if (textB) textB.text = pointB.value.ToString();
	}
	public void UpdatePlayButtonImage() {
		playButtonImage.color = smfPlayer.isPlaying() ? Color.green : Color.gray;
	}
}
