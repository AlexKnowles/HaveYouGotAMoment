using System;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Managers
{
	public class DialogManager : MonoBehaviour
	{
		public GameObject Dialog;
		public GameObject DialogMessageContainer;
		public GameObject DialogMessagePrefab;

		private List<DialogMessage> _dialogMessages = new List<DialogMessage>();
		private float _maxMessageWidth;

		// Start is called before the first frame update
		void Start()
		{
			if (Dialog == null)
			{
				throw new ArgumentException("Value has not been set", nameof(Dialog));
			}

			if (DialogMessageContainer == null)
			{
				throw new ArgumentException("Value has not been set", nameof(DialogMessageContainer));
			}

			if (DialogMessagePrefab == null)
			{
				throw new ArgumentException("Value has not been set", nameof(DialogMessagePrefab));
			}

			_maxMessageWidth = (DialogMessagePrefab.GetComponent<RectTransform>().rect.width * 0.8f);

			Dialog.SetActive(false);
		}

		void Update()
		{
		}

		public void ShowDialog()
		{
			Dialog.SetActive(true);
		}

		private void AddMessage(DialogMessageSender sender, string message)
		{
			DialogMessage newMessageBubble = Instantiate(DialogMessagePrefab, DialogMessageContainer.transform).GetComponent<DialogMessage>();

			newMessageBubble.Setup(sender, message, _maxMessageWidth);

			MoveExistingMessagesUp(newMessageBubble.Height);

			_dialogMessages.Add(newMessageBubble);
		}

		private void MoveExistingMessagesUp(float newMessageHeight)
		{
			foreach (DialogMessage message in _dialogMessages)
			{
				message.MoveUpBy(newMessageHeight);
			}
		}
	}
}
