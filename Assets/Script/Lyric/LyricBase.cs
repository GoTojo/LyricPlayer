///
/// LyricControl.cs
/// Lyricのエディットやイベントの反映などを行う
/// Copyright (c) 2025 gotojo
/// 
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public abstract class LyricBase : MonoBehaviour
{
	public TMP_FontAsset font;
	public bool active = false;
	void Awake() {
		LyricList.lyrics.Add(this);
	}
	public void SetActive(bool f) {
		active = f;
		OnParamChanged();
	}
	public virtual void SetPosX(float x) {
	}
	public virtual void SetPosY(float y) {
	}
	public virtual float GetPosX() {
		return this.transform.position.x;
	}
	public virtual float GetPosY() {
		return this.transform.position.y;
	}
	public void SetFont(TMP_FontAsset font) {
		this.font = font;
		OnParamChanged();
	}
	public TMP_FontAsset GetFont() {
		return font;
	}
	public virtual void Show() {
		active = true;
		OnParamChanged();
	}
	public virtual void Hide() {
		active = false;
		OnParamChanged();
	}
	public abstract void ShowSampleText(string [] text);
	public abstract void OnParamChanged();
	public abstract void Clear();
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