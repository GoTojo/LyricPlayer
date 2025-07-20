///
/// LyricControl.cs
/// Lyricのエディットやイベントの反映などを行う
/// Copyright (c) 2025 gotojo
/// 
using UnityEngine;
using System.Collections.Generic;

public class LyricBase : MonoBehaviour
{
	protected bool active = false;
	void Awake() {
		LyricList.lyrics.Add(this);
	}
	public void SetActive(bool f) {
		active = f;
		OnParamChanged();
	}
	public virtual void OnParamChanged() {
	}
	public virtual void Clear() {
	}
}

public class LyricList {
	static public List<LyricBase> lyrics = new List<LyricBase>();
	static public void Reset() {
		foreach (LyricBase lyric in lyrics) {
			lyric.SetActive(false);
			lyric.Clear();
		}
	}
}