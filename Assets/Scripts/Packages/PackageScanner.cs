using TMPro;
using UnityEngine;

namespace HaveYouGotAMoment
{
    public class PackageScanner : MonoBehaviour
    {
        private TextMeshPro _displayText;
        private BoxCollider2D _collider;

        public GameObject DisplayText;
        public string DefaultText = "-- Ready --";

        private string _text;
        
        // Start is called before the first frame update
        void Start()
        {
            _displayText = DisplayText.GetComponent<TextMeshPro>();
            _collider = GetComponent<BoxCollider2D>();
            _text = DefaultText;
        }

        // Update is called once per frame
        void Update()
        {
            _displayText.text = _text;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Collision");
            if (other.gameObject.tag == "Packages")
            {
                _text = "$: " + other.gameObject.GetComponent<Packages.PackageData>().Tenant;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            Debug.Log("Exit Collision");
            if (other.gameObject.tag == "Packages")
            {
                _text = DefaultText;
            }
        }
    }
}
