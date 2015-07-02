using UnityEngine;
using System.Collections;
using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;

public class PushNotificationsManager : MonoBehaviour
{
    /// <summary>
    /// Static reference.
    /// </summary>
    public static PushNotificationsManager instance;

    /// <summary>
    /// Initializes static reference.
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Initializes GCM service.
    /// </summary>
    public void Initialize()
    {
        GCM.Initialize();
    }

    /// <summary>
    /// Registers device to receive remote notifications from GCM.
    /// </summary>
    public void Register()
    {
        GCM.SetRegisteredCallback(OnDeviceRegistered);
        GCM.SetMessageCallback(OnReceivedMessage);

        GCM.Register(new string[] { "1064222689329" });
    }

    /// <summary>
    /// Callback used when device is successfully registered with GCM. Sends data do PlayFab.
    /// </summary>
    /// <param name="registrationID">Registration ID.</param>
    void OnDeviceRegistered(string registrationID)
    {
        GCM.SetNotificationsEnabled(true);
        
        AndroidDevicePushNotificationRegistrationRequest request= new AndroidDevicePushNotificationRegistrationRequest();
        request.DeviceToken = registrationID;
        request.SendPushNotificationConfirmation = true;

        PlayFabClientAPI.AndroidDevicePushNotificationRegistration(request, OnRegisteredForPushNotification, OnRegisterForPushNotificationError);
    }

    /// <summary>
    /// Callback used to handle push notification registration success.
    /// </summary>
    /// <param name="result">Result info.</param>
    void OnRegisteredForPushNotification(AndroidDevicePushNotificationRegistrationResult result)
    {
        Debug.Log("Successfully registered for push notification!");
    }

    /// <summary>
    /// Callback used to handle push notification registration error.
    /// </summary>
    /// <param name="error">Error details.</param>
    void OnRegisterForPushNotificationError(PlayFabError error)
    {
        Debug.LogError("Error registering for push notification. Error: " + error.Error + " " + error.ErrorMessage);
    }

    /// <summary>
    /// Callback used when the game receives a remote message.
    /// </summary>
    /// <param name="messageParams">Message info.</param>
    void OnReceivedMessage(Dictionary<string, object> messageParams)
    {
        string _text = "Message: " + System.Environment.NewLine;
        foreach (var key in messageParams.Keys)
        {
            _text += key + "=" + messageParams[key] + System.Environment.NewLine;
        }
    }
}