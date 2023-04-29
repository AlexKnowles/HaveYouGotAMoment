using HaveYouGotAMoment.Packages;
using HaveYouGotAMoment.Tenants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class TenantPackageHandler : MonoBehaviour
    {
        private TenantManager _tenantManager;
		private TenantMovement _tenantMovement;
        private TenantData _tenantData;

		// Start is called before the first frame update
		void Start()
        {
            if(_tenantManager is null)
            {
                _tenantManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<TenantManager>();
			}
			if (_tenantMovement is null)
			{
				_tenantMovement = GetComponent<TenantMovement>();
			}
			if (_tenantData is null)
			{
				_tenantData = GetComponent<TenantData>();
			}
		}

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ReadyToTakePackage()
        {
            _tenantManager.SetTenantToTakePackage(gameObject);
		}
        public void RecievePackage(GameObject package)
        {
            PackageData packageData = package.GetComponent<PackageData>();

            if (packageData.Tenant == _tenantData.TenantName)
            {
                Destroy(package.gameObject);
			}

			_tenantMovement.ContinueToWayPoint();
		}
	}
}
