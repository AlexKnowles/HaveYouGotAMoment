using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class TenantManager : MonoBehaviour
    {
        public GameObject[] PossibleTenants;

		private GameObject _tenantContainerInWorld;
		private GameObject[] _tenantsForThisGame;

		// Start is called before the first frame update
		void Start()
        {
			_tenantContainerInWorld = new GameObject("TenantContainer");
			OnBeginGame();
			OnBeginDay();
		}

        // Update is called once per frame
        void Update()
        {
			
        }

        private void OnBeginGame()
		{
			int randomIndex = Random.Range(0, PossibleTenants.Length);

			_tenantsForThisGame = new GameObject[]
			{
				PossibleTenants[randomIndex]
			};
		}

		private void OnBeginDay()
		{
			if (_tenantsForThisGame is null)
				return;

			// Load in tenats for the day
			foreach(GameObject tenant in _tenantsForThisGame)
			{
				Instantiate(tenant, _tenantContainerInWorld.transform);
			}
		}

		private void OnEndDay()
		{
			// Remove tenants?
		}
	}
}
