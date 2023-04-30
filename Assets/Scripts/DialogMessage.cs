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

		private RectTransform _rectTransform;
		private Image _messageBackgroundImage;
		private float _maxWidth;
		private DialogMessageSender _messageSender;

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

		internal void MoveUpBy(float value)
		{
			_rectTransform.localPosition += new Vector3(0f, value, 0f);
		}

		internal void Setup(DialogMessageSender sender, string message, float maxWidth)
		{
			_maxWidth = maxWidth;
			_messageSender = sender;
			TextMesh.SetText(message);

			StartCoroutine(DelayedUpdatingSizeForAFrame());
		}

		private IEnumerator DelayedUpdatingSizeForAFrame()
		{
			// Wait one frame for the layout calculations to complete
			yield return null;

			if (_messageSender == DialogMessageSender.Tenant)
			{
				TextMesh.alignment = TextAlignmentOptions.MidlineLeft;
				_rectTransform.anchorMin = new Vector2(0f, 0f);
				_rectTransform.anchorMax = new Vector2(0f, 0f);
				_rectTransform.localPosition += new Vector3(Width, 0f, 0f);
			}

			_rectTransform.sizeDelta = new Vector2(Width, Height);
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
