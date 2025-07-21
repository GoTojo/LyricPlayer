/// SentenceList
/// Copyright (c) gotojo, All Rights Reserved.
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;
using Unity.VisualScripting;

[Serializable]
public class ControlList {
	public List<string> controls = new List<string>();
}

[Serializable]
public class LyricData {
	public int measure;
	public uint msec;
	public string sentence;
	public List<ControlList> beats;
	public void SetSentence(string text) {
		sentence = text;
	}
	public LyricData(int measure, uint msec, string sentence, int numofbeat) {
		this.measure = measure;
		this.msec = msec;
		this.sentence = sentence;
		this.beats = new List<ControlList>();
		for (var i = 0; i < numofbeat; i++) {
			ControlList controlList = new ControlList();
			this.beats.Add(controlList);
		}
	}
}

[Serializable]
public class Track {
	public int id = 0;
	public bool active = true;
	public List<LyricData> lyrics = new List<LyricData>();
	public Track(int id)
	{
		this.id = id;
	}
}

[Serializable]
public class TrackListWrapper
{
	public List<Track> tracks = new List<Track>();
}

public class SentenceList
{
	private static SentenceList _instance;  // singleton
	public static SentenceList Instance {
		get {
			if (_instance == null) {
				_instance = new SentenceList();
			}
			return _instance;
		}
	}
	private SentenceList() {}
	public List<Track> tracks = new List<Track>();
	private MIDIEventMap eventMap;

	public void Init(SMFPlayer player)
	{
		eventMap = new MIDIEventMap();
		eventMap.Init(player);
		string path = SongInfo.GetInfoPath();
		if (File.Exists(path)) {
			Load(path);
		} else {
			GenerateTracks();
			Save(path);
		}
	}
	public bool IsValid(int track, int measure) {
		if (track > tracks.Count) return false;
		if (track < 1) return false;
		Track trackData = tracks[track - 1];
		if (measure > trackData.lyrics.Count) return false;
		return true;
	}
	public LyricData GetSentence(int track, int measure) {
		if (track < 1) track = 1; // track0 is BeatTrack
		if (!IsValid(track, measure)) {
			LyricData emptyData = new LyricData(measure, 0, "", 1);
			return emptyData;
		} else {
			return tracks[track - 1].lyrics[measure];
		}
	}
	public void SetSentence(int track, int measure, string sentence) {
		if (track < 1) track = 1; // track0 is BeatTrack
		if (IsValid(track, measure)) {
			tracks[track - 1].lyrics[measure].sentence = sentence;
		}
	}
	public bool SetControl(int track, int measure, int beat, int num, string control) {
		if (!IsValid(track, measure)) return false;
		LyricData data = GetSentence(track, measure);
		if (beat >= data.beats.Count) return false;
		if (num < data.beats[beat].controls.Count) {
			if (control.Length == 0) {
				data.beats[beat].controls.RemoveAt(num);
			} else {
				data.beats[beat].controls[num] = control;
			}
		} else if (num == data.beats[beat].controls.Count) {
			if (control.Length == 0) return false;
			data.beats[beat].controls.Add(control);
		} else {
			return false;
		}
		return true;
	}
	private void GenerateTracks() {
		int numOfMeasure = eventMap.numOfMeasure;
		int numOfTrack = eventMap.numOfTrack;

		// Debug.Log($"numOfMeasure: {numOfMeasure}");
		// Debug.Log($"numOfTrack: {numOfTrack}");
		for (var track = 1; track < numOfTrack; track++) // track0 is BeatTrack
		{
			var trackData = new Track(track);
			for (var meas = 0; meas < numOfMeasure; meas++) {
				uint msec = eventMap.GetMsec(meas);
				string sentence = eventMap.GetSentence(meas, track);
				SMFPlayer.Beat beat = eventMap.GetBeat(meas);
				trackData.lyrics.Add(new LyricData(meas, msec, sentence, beat.unit));
				// Debug.Log($"meas:{meas} {msec}:{sentence}");
			}
			tracks.Add(trackData);
		}
	}
	private void Save(string path)
	{
		var wrapper = new TrackListWrapper { tracks = tracks };
		string json = JsonUtility.ToJson(wrapper, true);
		File.WriteAllText(path, json, new UTF8Encoding(false));
	}
	private void Load(string path)
	{
		string json = File.ReadAllText(path, new UTF8Encoding(false));
		var wrapper = JsonUtility.FromJson<TrackListWrapper>(json);
		tracks = wrapper.tracks;
	}
}