using System.Collections;
using System.Collections.Generic;
using OneSignalSDK;
using UnityEngine;

public class OneSignalScript : MonoBehaviour
{
    void Start()
    {
        OneSignal.Default.Initialize("2ba6ba7d-88c7-4034-b7d0-fec62c0dc264");
        OneSignal.Default.SetExternalUserId("123456789");
    }
}
