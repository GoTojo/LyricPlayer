/// LyricGenUnder1Line.cs
/// 表示エリア一番下に1Lineの歌詞を表示する
/// Copyright (c) 2025 gotojo

using UnityEngine;
using TMPro;

public class LyricGenUnder1Line : LyricBase {
	public Vector3 position = new Vector3(0, -6.5f, 0);
	class LyricGenControl : LyricGenBase {
		public TextMeshPro text;
		private int waitCount = 3;
		private int waitClear = 0;
		public LyricGenControl(Vector3 position, Transform transform) {
			TMP_FontAsset font = FontResource.Instance.GetFont();
			Color color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
			float scale = 1f;
			float rotate = 0;
			Vector2 size = new Vector2(20, 2);
			this.active = true;
			GameObject simpleLyric = CreateText("", font, color, TextAlignmentOptions.Center, size, position, scale, rotate);
			this.text = simpleLyric.GetComponent<TextMeshPro>();
			simpleLyric.transform.SetParent(transform);
		}
		protected override void OnTextChanged(string sentence) {
			text.font = FontResource.Instance.GetFont();
			text.text = sentence;
			waitClear = waitCount;
		}
		protected override void OnEventIn(MIDIHandler.Event playerEvent) { }
		protected override void OnMeasureIn(int measure, int measureInterval, uint currentMsec) {
			if (waitClear > 0) {
				waitClear--;
				if (waitClear <= 0) {
					// sentence = "";
					text.text = "";
					// sentenceLength = 0;
				}
			}
		}
		public override void Clear() {
			text.text = "";
		}
	};
	LyricGenControl control;

	void Start() {
		control = new LyricGenControl(position, this.transform);
	}
	public override void OnParamChanged() {
		control.text.transform.position = position;
	}
}
