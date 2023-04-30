using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Managers
{
	public class DialogManager : MonoBehaviour
	{
		public GameObject Dialog;
		public GameObject DialogMessageContainer;
		public GameObject DialogMessagePrefab;

		public string[] PlayerPossibleHellos = new string[] { "Hi", "Hey", "Hello"};

		private List<DialogMessage> _dialogMessages = new List<DialogMessage>();
		private float _maxMessageWidth;

		// Start is called before the first frame update
		void Start()
		{
			if (Dialog == null)
			{
				throw new System.ArgumentException("Value has not been set", nameof(Dialog));
			}

			if (DialogMessageContainer == null)
			{
				throw new System.ArgumentException("Value has not been set", nameof(DialogMessageContainer));
			}

			if (DialogMessagePrefab == null)
			{
				throw new System.ArgumentException("Value has not been set", nameof(DialogMessagePrefab));
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

		public void HideDialog()
		{
			Dialog.SetActive(false);
		}

		public void AddMessage(DialogMessageSender sender, string message)
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

		internal void SendPlayerHello()
		{
			string playerHello = PlayerPossibleHellos[Random.Range(0, PlayerPossibleHellos.Length)];
			bool addThere = (Random.Range(0, 1) > 0);
			string messageStart = playerHello + (addThere ? " there" : "");

			AddMessage(DialogMessageSender.Player, $"{messageStart}, have you got a moment?");
		}

		internal void SendTenantMessage(string message)
		{
			AddMessage(DialogMessageSender.Tenant, message);
		}

		internal void ClearDialog()
		{
			for (int i = 0; i < _dialogMessages.Count; i++) 
			{
				DialogMessage selectedDialog = _dialogMessages[i];

				Destroy(selectedDialog.gameObject);

				_dialogMessages.Remove(selectedDialog);
			}
		}
	}
}
