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
	private ControlList GetControlList(int beat) {
		LyricData data = SentenceList.Instance.GetSentence(trackInput.value, currentMeasure);
		return (data.beats.Count < beat) ? new ControlList() : data.beats[beat];
	}
	private void ApplyControl(int beat) {
		ControlList controlList = GetControlList(beat);
		foreach (string control in controlList.controls) {
			string[] args = control.Split("_");
			switch (args[0]) {
			case "Title":
			case "Line":
			case "Words":
			case "MultiL":
			case "MultiR":
			case "MultiVL":
			case "MultiVR":
			case "MultiWordL":
			case "MultiWordR":
			case "MultiWordVL":
			case "MultiWordVR":
				lyricControl.ApplyControl(args);
				break;
			default:
				break;
			}
		}
	}
	public void MeasureIn(int measure, int measureInterval, uint currentMsec) {
		currentMeasure = measure;
	}
	public void BeatIn(int numerator, int denominator, uint currentMsec) {
		ApplyControl(numerator);
	}
}
