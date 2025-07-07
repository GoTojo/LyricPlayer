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
public struct LyricData {
	public int measure;
	public uint msec;
	public string sentence;
	public List<ControlList> beats;
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
	public LyricData GetSentence(int track, int measure)
	{
		LyricData emptyData = new LyricData(measure, 0, "", 1);
		if (track < 1) return emptyData; // track0 is BeatTrack
		if (track > tracks.Count) return emptyData;
		Track trackData = tracks[track - 1];
		if (measure > trackData.lyrics.Count) return emptyData;
		return trackData.lyrics[measure];
	}
	private void GenerateTracks()
	{
		int numOfMeasure = eventMap.numOfMeasure;
		int numOfTrack = eventMap.numOfTrack;

		// Debug.Log($"numOfMeasure: {numOfMeasure}");
		// Debug.Log($"numOfTrack: {numOfTrack}");
		for (var track = 1; track < numOfTrack; track++) // track0 is BeatTrack
		{
			var trackData = new Track(track);
			for (var meas = 0; meas < numOfMeasure; meas++)
			{
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