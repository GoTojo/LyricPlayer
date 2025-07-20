/// Title.cs
/// タイトルを表示する
using UnityEngine;
using TMPro;

public class TitleControl : LyricBase
{
	public TextMeshPro title;
	void Start() {
		title.text = SongInfo.GetTitle();
	}
	void Update() {
		title.enabled = active;
	}
}