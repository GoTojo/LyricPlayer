using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventListener : MonoBehaviour {
	public LyricControl lyricControl;
	public TMP_Dropdown trackInput;
	private int currentMeasure = 0;
	void Awake() {
		MidiWatcher midiWatcher = MidiWatcher.Instance;
		midiWatcher.onMeasureIn += MeasureIn;
		midiWatcher.onBeatIn += BeatIn;
	}
	void Start() {

	}

	void Update() {

	}
	private void ApplyControl(string command) {
		string[] args = command.Split("_");
		switch (args[0]) {
		case "Title":
			lyricControl.ApplyControl(args);
			break;
		default:
			break;
		}
	}
	public void MeasureIn(int measure, int measureInterval, uint currentMsec) {
		currentMeasure = measure;
	}
	public void BeatIn(int numerator, int denominator, uint currentMsec) {
		LyricData data = SentenceList.Instance.GetSentence(trackInput.value, currentMeasure);
		if (data.beats.Count < numerator) return;
		foreach (string control in data.beats[numerator].controls) {
			ApplyControl(control);
		}
	}
}
