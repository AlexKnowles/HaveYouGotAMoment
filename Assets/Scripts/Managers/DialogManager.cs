using HaveYouGotAMoment.Tenants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace HaveYouGotAMoment.Managers
{
	public class DialogManager : MonoBehaviour
	{
		public GameObject Dialog;
		public GameObject DialogMessageContainer;
		public GameObject DialogMessagePrefab;

		public string[] PlayerPossibleHellos = new string[] { "Hi", "Hey", "Hello"};

		private Camera _mainCamera;
		private Image _dialogBackgroundImage;
		private List<DialogMessage> _dialogMessages = new List<DialogMessage>();
		private float _maxMessageWidth;
		private GameObject _tenantToFollow;
		private TenantMovingDirection _tenantToFollowMovingDirection;
		private bool _fadeOut = false;
		private float _fadeDuration = 6;
		private float _dialogBackgroundImageOriginalAlpha;

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

			if(_dialogBackgroundImage  == null)
			{
				_dialogBackgroundImage = Dialog.GetComponent<Image>();
				_dialogBackgroundImageOriginalAlpha = _dialogBackgroundImage.color.a;
			}

			_maxMessageWidth = (DialogMessagePrefab.GetComponent<RectTransform>().rect.width * 0.8f);

			Dialog.SetActive(false);
		}

		void Update()
		{
			if(_fadeOut && _dialogBackgroundImage.color.a > 0)
			{
				float newImageAlpha = _dialogBackgroundImage.color.a - (Time.deltaTime / _fadeDuration);
				SetBackgroundImageAlpha(newImageAlpha);
			}

			if (_tenantToFollow is null)
			{
				return;
			}

			Vector3 tenantOnScreen = _mainCamera.WorldToScreenPoint(_tenantToFollow.transform.position);

			Dialog.transform.position = tenantOnScreen;
		}

		public void ShowDialog(GameObject tenantToFollow, TenantMovingDirection movingDirection)
		{
			ClearDialog();

			_fadeOut = false;
			SetBackgroundImageAlpha(_dialogBackgroundImageOriginalAlpha);

			if (movingDirection == TenantMovingDirection.Left)
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

				_dialogMessages[i] = null;
			}

			_dialogMessages = _dialogMessages.Where(w => w is not null).ToList();
		}

		internal void StartFadeOut()
		{
			_fadeOut = true;
		}

		private void SetBackgroundImageAlpha(float alpha)
		{
			_dialogBackgroundImage.color = new Color(_dialogBackgroundImage.color.r, _dialogBackgroundImage.color.g, _dialogBackgroundImage.color.b, alpha);
		}
	}
}
