using TMPro;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class PackageScanner : MonoBehaviour
    {
        public GameObject Display;

        private PackageScannerDisplay _display;

        // Start is called before the first frame update
        void Start()
        {
            _display = Display.GetComponent<PackageScannerDisplay>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Packages")
            {
                _display.UpdatePackage(other.gameObject);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Packages")
            {
                _display.UpdatePackage(null);
            }
        }
    }
}
