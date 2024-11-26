using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrivacyShower : MonoBehaviour
{
    [SerializeField] private UniWebView _uni;

    private void Start()
    {
        OpenPrivacy();
    }

    public void OpenPrivacy()
    {
        var reg = PlayerPrefs.GetString("link");
        _uni.Load(reg);
        _uni.Show();
    }
}