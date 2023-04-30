using HaveYouGotAMoment.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Tenants
{
    public class TenantDialog : MonoBehaviour
    {
		public DialogItem[] Dialog;
		public GameObject DialogSpawnPoint;

		private DialogManager _dialogManager;
		private TenantPackageHandler _tenantPackageHandler;
		private TenantMovement _tenantMovement;
		private bool _dialogStarted = false;
		private float _timeSinceLastMessage = 0;

		// Start is called before the first frame update
		void Start()
		{
			if(_dialogManager is null)
			{
				_dialogManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<DialogManager>();
			}

			if(_tenantPackageHandler is null)
			{
				_tenantPackageHandler = GetComponent<TenantPackageHandler>(); 
			}			

			if(_tenantMovement  is null)
			{
				_tenantMovement = GetComponent<TenantMovement>();
			}
		}

		// Update is called once per frame
		void Update()
		{
			if (!_dialogStarted)
			{
				return;
			}

			_timeSinceLastMessage += Time.deltaTime;

			if (_timeSinceLastMessage > 1.2f)
			{
				_timeSinceLastMessage = 0;
				_dialogManager.SendTenantMessage("okay");

				_tenantPackageHandler.ReviewGivenPackage();
				_dialogStarted = false;
			}
		}

		public void BeginDialog()
        {
			_dialogManager.ShowDialog(DialogSpawnPoint, _tenantMovement.MovingDirection);
			_dialogManager.SendPlayerHello();
			_dialogStarted = true;
		}

		public void EndDialog()
		{
			//_dialogManager.HideDialog();
			//_dialogManager.ClearDialog();
		}

		internal void ThankPlayer()
		{
		}
	}

	[Serializable]
	public struct DialogItem
	{
		public string MainText;
		public string[] Responses;
	}
}
