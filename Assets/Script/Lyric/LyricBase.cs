///
/// LyricControl.cs
/// Lyricのエディットやイベントの反映などを行う
/// Copyright (c) 2025 gotojo
/// 
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class LyricBase : MonoBehaviour
{
	public TMP_FontAsset font;
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
	public virtual void SetPosX(float x) {

		// this.transform.position.x = x;
	}
	public virtual void SetPosY(float y) {
		// this.transform.position.y = y;
	}
	public virtual float GetPosX() {
		return this.transform.position.x;
	}
	public virtual float GetPosY() {
		return this.transform.position.y;
	}
	public void SetFont(Parameter.Font font) {
	}
	public Parameter.Font GetFont() {
		return Parameter.Font.JKMaruGothic;
	}
	public void SetSampleText(string [] text) {
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