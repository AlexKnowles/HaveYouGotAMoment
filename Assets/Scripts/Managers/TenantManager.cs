using System.Linq;
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

		public string[] GetTenantNames()
		{
			return PossibleTenants.Select(x => x.GetComponent<Tenants.TenantData>().TenantName).ToArray();
		}

		private void OnBeginGame()
		{
			//int randomIndex = Random.Range(0, PossibleTenants.Length);

			//_tenantsForThisGame = new GameObject[]
			//{
			//	PossibleTenants[randomIndex]
			//};
			_tenantsForThisGame = PossibleTenants;
		}

		private void OnBeginDay()
		{
			if (_tenantsForThisGame is null)
				return;

			// Load in tenats for the day
			foreach (GameObject tenant in _tenantsForThisGame)
			{
				Instantiate(tenant, _tenantContainerInWorld.transform);
			}
		}

		private void OnEndDay()
		{
			// Remove tenants?
		}

		internal void TryGivePackageToTenant(Vector2 mouseInWorldSpace, GameObject package)
		{
			RaycastHit2D[] hits = Physics2D.RaycastAll(mouseInWorldSpace, Vector2.zero);

			foreach(RaycastHit2D hit in hits)
			{
				if (!hit.collider.gameObject.CompareTag("Tenant"))
				{
					continue;
				}
				
				TenantPackageHandler tenantToTakePackage = hit.collider.gameObject.GetComponent<TenantPackageHandler>();
				tenantToTakePackage.RecievePackage(package);

				break;
			}
		}
	}
}
