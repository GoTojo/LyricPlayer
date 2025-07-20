///
/// LyricControl.cs
/// Lyricのエディットやイベントの反映などを行う
/// Copyright (c) 2025 gotojo
/// 
using UnityEngine;

public class LyricBase : MonoBehaviour
{
	protected bool active = false;
	public void SetActive(bool f) {
		if (active == f) return;
		active = f;
		OnParamChanged();
	}
	public virtual void OnParamChanged() {
	}
}