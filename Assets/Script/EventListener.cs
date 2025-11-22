using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventListener : MonoBehaviour {
	public LyricControl lyricControl;
	public TMP_Dropdown trackInput;
	private int currentMeasure = 0;
	private float beatInterval = 0.5f;
	private float measureInterval = 2f;

	void Awake() {
		MidiWatcher midiWatcher = MidiWatcher.Instance;
		midiWatcher.onMeasureIn += MeasureIn;
		midiWatcher.onBeatIn += BeatIn;
	}
	void Start() {

	}
	void Update() {
	}
	public void UpdateControl(int measure) {
		for (int meas = 0; meas < measure; meas++) {
			LyricData data = SentenceList.Instance.GetSentence(trackInput.value, meas);
			foreach (ControlList controlList in data.beats) {
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
		}
	}
	private ControlList GetControlList(int beat) {
		LyricData data = SentenceList.Instance.GetSentence(trackInput.value, currentMeasure);
		return (data.beats.Count <= beat) ? new ControlList() : data.beats[beat];
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
		beatInterval = (float)measureInterval / (float)Player.smfPlayer.beat.count / 1000f;
		this.measureInterval = (float)measureInterval / 1000f;
	}
	public void BeatIn(int numerator, int denominator, uint currentMsec) {
		ApplyControl(numerator);
	}
}
