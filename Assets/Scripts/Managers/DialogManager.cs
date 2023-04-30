using HaveYouGotAMoment.Tenants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HaveYouGotAMoment.Managers
{
	public class DialogManager : MonoBehaviour
	{
		public GameObject Dialog;
		public GameObject DialogMessageContainer;
		public GameObject DialogMessagePrefab;

		public string[] PlayerPossibleHellos = new string[] { "Hi", "Hey", "Hello"};

		private Camera _mainCamera;
		private List<DialogMessage> _dialogMessages = new List<DialogMessage>();
		private float _maxMessageWidth;
		private GameObject _tenantToFollow;
		private TenantMovingDirection _tenantToFollowMovingDirection;

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

			if(_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
			}

			_maxMessageWidth = (DialogMessagePrefab.GetComponent<RectTransform>().rect.width * 0.8f);

			Dialog.SetActive(false);
		}

		void Update()
		{
			if(_tenantToFollow is null)
			{
				return;
			}

			Vector3 tenantOnScreen = _mainCamera.WorldToScreenPoint(_tenantToFollow.transform.position);

			Dialog.transform.position = tenantOnScreen;
		}

		public void ShowDialog(GameObject tenantToFollow, TenantMovingDirection movingDirection)
		{
			if(movingDirection == TenantMovingDirection.Left)
			{
				Dialog.GetComponent<RectTransform>().pivot = new Vector2(1, 0);
			}
			else if(movingDirection == TenantMovingDirection.Right)
			{
				Dialog.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
			}

			_tenantToFollow = tenantToFollow;
			_tenantToFollowMovingDirection = movingDirection;

			Dialog.SetActive(true);
		}

		public void HideDialog()
		{
			Dialog.SetActive(false);
		}

		public void AddMessage(DialogMessageSender sender, string message)
		{
			DialogMessage newMessageBubble = Instantiate(DialogMessagePrefab, DialogMessageContainer.transform).GetComponent<DialogMessage>();

			DialogMessageSide messageSide = DialogMessageSide.Right;

			if(sender == DialogMessageSender.Tenant && _tenantToFollowMovingDirection == TenantMovingDirection.Right
				|| sender == DialogMessageSender.Player && _tenantToFollowMovingDirection == TenantMovingDirection.Left)
			{
				messageSide = DialogMessageSide.Left;
			}

			newMessageBubble.Setup(messageSide, message, _maxMessageWidth);

			MoveExistingMessagesUp(newMessageBubble.Height);

			_dialogMessages.Add(newMessageBubble);
		}

		private void MoveExistingMessagesUp(float newMessageHeight)
		{
			for (int i = 0; i < _dialogMessages.Count; i++)
			{
				DialogMessage message = _dialogMessages[i];

				if (message.IsVisible)
				{
					message.MoveUpBy(newMessageHeight + 10);
				}
				else
				{
					Debug.Log("Removing Message");
					Destroy(message.gameObject);
					_dialogMessages[i] = null;
				}
			}

			_dialogMessages = _dialogMessages.Where(w => w is not null).ToList();
		}

		internal void SendPlayerHello()
		{
			string playerHello = PlayerPossibleHellos[Random.Range(0, PlayerPossibleHellos.Length)];
			bool addThere = (Random.Range(0, 2) > 0);
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
