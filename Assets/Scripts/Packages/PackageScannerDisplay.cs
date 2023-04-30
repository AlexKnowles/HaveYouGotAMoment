using System.Collections;
using System.Collections.Generic;
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
        private GameObject _tenantPrefab;

        // Start is called before the first frame update
        void Start()
        {
            _displayText = DisplayText.GetComponent<TextMeshPro>();
            _text = DefaultText;
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
            if (_package != null)
            {
                // Get name
                _text = _package.GetComponent<Packages.PackageData>().Tenant;
                // Get tenant prefab
            }
            else
            {
                _text = DefaultText;
                _tenantPrefab = null;
            }
        }
    }
}
