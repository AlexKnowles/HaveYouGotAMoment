using HaveYouGotAMoment.Packages;
using HaveYouGotAMoment.Tenants;
using System;
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
		private TenantDialog _tenantDialog;

		private GameObject _givenPackage;

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
			if (_tenantDialog is null)
			{
				_tenantDialog = GetComponent<TenantDialog>();
			}
		}

        // Update is called once per frame
        void Update()
        {
        
        }

        public void RecievePackage(GameObject package)
        {
			if(_givenPackage is not null)
			{
				return;
			}

			_givenPackage = package;

			_tenantMovement.Stop();
			_tenantDialog.BeginDialog();
		}

		public bool ReviewGivenPackage()
		{
			PackageData packageData = _givenPackage.GetComponent<PackageData>();

			return (packageData.Tenant == _tenantData.TenantName);
		}

		public void TakeGivenPackage()
		{
			Destroy(_givenPackage);
			_givenPackage = null;
		}

		internal void RejectPackage()
		{
			_givenPackage = null;
		}
	}
}
