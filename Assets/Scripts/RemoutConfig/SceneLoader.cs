using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RemoutConfig
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private RemoutConfigLoader _loader;

        private void OnEnable()
        {
            _loader.ConfigLoadEnded += OnConfigLoadEnded;
        }

        private void OnDisable()
        {
            _loader.ConfigLoadEnded -= OnConfigLoadEnded;
        }

        private void OnConfigLoadEnded(bool showPrivacy)
        {
            if (showPrivacy)
            {
                if (PlayerPrefs.HasKey("OnboardingComplete"))
                {
                    SceneManager.LoadScene("MainScene");
                }
                else
                {
                    SceneManager.LoadScene("Onboarding");
                }
            }
            else
            {
                SceneManager.LoadScene("TestScene");
            }
        }
    }
}