using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class PackageScannerDisplay : MonoBehaviour
    {
        private TextMeshPro _displayText;

        public GameObject DisplayText;

        public string DefaultText = "-- Ready --";

        private string _text;
        private GameObject _package;
        private GameObject _displayTenant;

        private TenantManager _tenantManager;

        // Start is called before the first frame update
        void Start()
        {
            _displayText = DisplayText.GetComponent<TextMeshPro>();
            _text = DefaultText;

            if (_tenantManager == null)
            {
                _tenantManager = GameObject.FindGameObjectWithTag("Managers").GetComponent<TenantManager>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            _displayText.text = _text;
        }

        public void UpdatePackage(GameObject package)
        {
            _package = package;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            if (_displayTenant != null)
            {
                Destroy(_displayTenant);
                _displayTenant = null;
            }
            if (_package != null)
            {
                var tenantName = _package.GetComponent<Packages.PackageData>().Tenant;
                // Get name
                _text = tenantName;
                // Get tenant prefab
                var tenant = _tenantManager.PossibleTenants.First(x => x.GetComponent<Tenants.TenantData>().TenantName == tenantName);
                var displayTenant = Instantiate(tenant, transform);
                Destroy(displayTenant.GetComponent<Scheduler>());
                Destroy(displayTenant.GetComponent<Tenants.TenantDialog>());
                Destroy(displayTenant.GetComponent<Tenants.TenantClick>());
                Destroy(displayTenant.GetComponent<Tenants.TenantMovement>());
                Destroy(displayTenant.GetComponent<TenantPackageHandler>());
                displayTenant.transform.position = new Vector3(transform.position.x, transform.position.y, -0.6f);
                displayTenant.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
                _displayTenant = displayTenant;
            }
            else
            {
                _text = DefaultText;
            }
        }
    }
}
