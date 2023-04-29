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
            Destroy(package);
            _tenantMovement.ContinueToWayPoint();
		}
	}
}
