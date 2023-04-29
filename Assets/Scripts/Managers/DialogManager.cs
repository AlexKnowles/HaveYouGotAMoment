using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment.Managers
{
    public class DialogManager : MonoBehaviour
    {
        public GameObject DialogPrefab;

		// Start is called before the first frame update
		void Start()
		{
			if (DialogPrefab == null)
			{
				throw new ArgumentException("Value has not been set", nameof(DialogPrefab));
			}

			DialogPrefab.SetActive(false);
		}

		public void ShowDialog()
		{
			DialogPrefab.SetActive(true);
		}
	}
}
