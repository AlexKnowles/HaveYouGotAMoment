using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HaveYouGotAMoment
{
	public class DialogMessage : MonoBehaviour
	{
		public TextMeshProUGUI TextMesh;


		public float Height
		{
			get
			{
				return TextMesh.preferredHeight + TextMesh.margin.y + TextMesh.margin.w;
			}
		}
		public float Width
		{
			get
			{
				return MathF.Min(_maxWidth, TextMesh.preferredWidth);
			}
		}

		public bool IsVisible
		{
			get
			{
				return !(_messageBackgroundImage.color.a <= 0 && TextMesh.color.a <= 0);				
			}
		}

		private float _fadeDuration = 5;
		private RectTransform _rectTransform;
		private Image _messageBackgroundImage;
		private float _maxWidth;
		private DialogMessageSide _displaySide;
		private Color _messageColour;

		private void Start()
		{
			if (_rectTransform is null)
			{
				_rectTransform = GetComponent<RectTransform>();
			}

			if (_messageBackgroundImage is null)
			{
				_messageBackgroundImage = GetComponent<Image>();
			}

			HideMessage();
		}
		private void Update()
		{
			if (_messageBackgroundImage.color.a > 0)
			{
				float newImageAlpha = _messageBackgroundImage.color.a - (Time.deltaTime / _fadeDuration);
				_messageBackgroundImage.color = new Color(_messageBackgroundImage.color.r, _messageBackgroundImage.color.g, _messageBackgroundImage.color.b, newImageAlpha);
			}

			if (TextMesh.color.a > 0)
			{
				float newTextAlpha = TextMesh.color.a - (Time.deltaTime / _fadeDuration);
				TextMesh.color = new Color(TextMesh.color.r, TextMesh.color.g, TextMesh.color.b, newTextAlpha);
			}
		}

		internal void MoveUpBy(float value)
		{
			_rectTransform.localPosition += new Vector3(0f, value, 0f);
		}


		internal void Setup(DialogMessageSide displaySide, string message, float maxWidth, Color messageColour)
		{
			_maxWidth = maxWidth;
			_displaySide = displaySide;

			_messageColour = messageColour;
			TextMesh.SetText(message);

			StartCoroutine(DelayedUpdatingSizeForAFrame());
		}

		private IEnumerator DelayedUpdatingSizeForAFrame()
		{
			// Wait one frame for the layout calculations to complete
			yield return null;

			if (_displaySide == DialogMessageSide.Left)
			{
				TextMesh.alignment = TextAlignmentOptions.MidlineLeft;
				_rectTransform.anchorMin = new Vector2(0f, 0f);
				_rectTransform.anchorMax = new Vector2(0f, 0f);
				_rectTransform.localPosition += new Vector3(Width, 0f, 0f);
			}

			_rectTransform.sizeDelta = new Vector2(Width, Height);
			_messageBackgroundImage.color = _messageColour;
			ShowMessage();
		}

		private void HideMessage()
		{
			_messageBackgroundImage.enabled = false;
			TextMesh.enabled = false;
		}

		private void ShowMessage()
		{
			_messageBackgroundImage.enabled = true;
			TextMesh.enabled = true;
		}
	}
}
