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

		private DialogManager _dialogManager;

		// Start is called before the first frame update
		void Start()
		{
			if(_dialogManager is null)
			{
				_dialogManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<DialogManager>();
			}
			
		}

		public void BeginDialog()
        {
			_dialogManager.ShowDialog();

		}
    }

	[Serializable]
	public struct DialogItem
	{
		public string MainText;
		public string[] Responses;
	}
}
