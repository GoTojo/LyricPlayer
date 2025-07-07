using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static SMFPlayer smfPlayer;
    private AudioSource audioSource;
	public int songnum = 0;

    // Start is called before the first frame update
	void Awake()
    {
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
	void Start()
	{
        audioSource.Play();
        smfPlayer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        smfPlayer.Update();
    }

    public void MIDIIn(int track, byte[] midiEvent, float position, uint currentMsec)
    {
    }
    public void LyricIn(int track, string lyric, float position, uint currentMsec)
    {
    }
    public void TempoIn(float msecPerQuaterNote, uint tempo, uint currentMsec)
    {
    }
    public void BeatIn(int numerator, int denominator, uint currentMsec)
    {
    }
    public void MeasureIn(int measure, int measureInterval, uint currentMsec)
    {
    }
    public void EventIn(MIDIHandler.Event playerEvent)
    {
        Debug.Log(playerEvent.ToString());
    }
}
