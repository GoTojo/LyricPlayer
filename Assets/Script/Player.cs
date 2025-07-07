using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static SMFPlayer smfPlayer;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        MidiWatcher midiWatcher = MidiWatcher.Instance;
        midiWatcher.onMidiIn += MIDIIn;
        midiWatcher.onLyricIn += LyricIn;
        midiWatcher.onTempoIn += TempoIn;
        midiWatcher.onBeatIn += BeatIn;
        midiWatcher.onMeasureIn += MeasureIn;
        smfPlayer = new SMFPlayer($"{Application.streamingAssetsPath}/約束の場所へ.mid", 119);
        smfPlayer.midiHandler = MidiWatcher.Instance;
        FontResource.Instance.LoadFont();
        SentenceList.Instance.Init(smfPlayer);
        audioSource = GetComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("Audio/約束の場所へ");
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
