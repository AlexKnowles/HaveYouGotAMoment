using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HaveYouGotAMoment
{
    public class SceneChanger : MonoBehaviour
    {
        public void LoadReceptionScene()
        {
            SceneManager.LoadScene("Reception");
        }
    }
}
