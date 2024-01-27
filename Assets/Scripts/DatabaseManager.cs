using UnityEngine;
using Firebase.Database;
using System.Collections;
using System;

public class DatabaseManager : SingletonMonoBehaviour<DatabaseManager>
{
    private DatabaseReference reference;

    public string sessionID;
    public int participantID;
    public float minPlatformHeight, maxPlatformHeight;
    public float minPlatformDistance, maxPlatformDistance;
    public float minPlatformSpeed, maxPlatformSpeed;
    public bool environment, skybox;

    void Start()
    {
        Firebase.AppOptions options = new Firebase.AppOptions();
        options.ApiKey = "AIzaSyALPN5i6MMC4-Y1OT-MH106CuDkvxjYSs4";
        options.AppId = "1:449889443394:android:045f8cdf2c9461f11b97db";
        options.DatabaseUrl = new Uri("https://motionsicknessteleportation-default-rtdb.firebaseio.com");
        options.ProjectId = "motionsicknessteleportation";
        options.StorageBucket = "motionsicknessteleportation.appspot.com";

        var app = Firebase.FirebaseApp.Create(options);
        reference = FirebaseDatabase.GetInstance(app).RootReference;
    }

    public IEnumerator GetSessionID(Action<string> onCallback)
    {
        var sessionIDData = reference.Child("currentSession").Child("sessionID").GetValueAsync();

        yield return new WaitUntil(predicate: () => sessionIDData.IsCompleted);

        DataSnapshot snapshot = sessionIDData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(value.ToString());
        }
        else
        {
            onCallback.Invoke(null);
        }
    }

    public IEnumerator GetParticipantID(Action<int> onCallback)
    {
        var participantIDData = reference.Child("currentSession").Child("participantID").GetValueAsync();

        yield return new WaitUntil(predicate: () => participantIDData.IsCompleted);

        DataSnapshot snapshot = participantIDData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(int.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(0);
        }
    }

    public IEnumerator GetMinPlatformHeight(Action<float> onCallback)
    {
        var heightData = reference.Child("currentSession").Child("minPlatformHeight").GetValueAsync();

        yield return new WaitUntil(predicate: () => heightData.IsCompleted);

        DataSnapshot snapshot = heightData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(float.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(0f);
        }
    }

    public IEnumerator GetMaxPlatformHeight(Action<float> onCallback)
    {
        var heightData = reference.Child("currentSession").Child("maxPlatformHeight").GetValueAsync();

        yield return new WaitUntil(predicate: () => heightData.IsCompleted);

        DataSnapshot snapshot = heightData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(float.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(0f);
        }
    }

    public IEnumerator GetMinPlatformDistance(Action<float> onCallback)
    {
        var distanceData = reference.Child("currentSession").Child("minPlatformDistance").GetValueAsync();

        yield return new WaitUntil(predicate: () => distanceData.IsCompleted);

        DataSnapshot snapshot = distanceData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(float.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(0f);
        }
    }

    public IEnumerator GetMaxPlatformDistance(Action<float> onCallback)
    {
        var distanceData = reference.Child("currentSession").Child("maxPlatformDistance").GetValueAsync();

        yield return new WaitUntil(predicate: () => distanceData.IsCompleted);

        DataSnapshot snapshot = distanceData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(float.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(0f);
        }
    }

    public IEnumerator GetMinPlatformSpeed(Action<float> onCallback)
    {
        var speedData = reference.Child("currentSession").Child("minPlatformSpeed").GetValueAsync();

        yield return new WaitUntil(predicate: () => speedData.IsCompleted);

        DataSnapshot snapshot = speedData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(float.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(0f);
        }
    }

    public IEnumerator GetMaxPlatformSpeed(Action<float> onCallback)
    {
        var speedData = reference.Child("currentSession").Child("maxPlatformSpeed").GetValueAsync();

        yield return new WaitUntil(predicate: () => speedData.IsCompleted);

        DataSnapshot snapshot = speedData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(float.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(1f);
        }
    }

    public IEnumerator GetEnvironment(Action<bool> onCallback)
    {
        var environmentData = reference.Child("currentSession").Child("environment").GetValueAsync();

        yield return new WaitUntil(predicate: () => environmentData.IsCompleted);

        DataSnapshot snapshot = environmentData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(bool.Parse(value.ToString())); 
        }
        else
        {
            onCallback.Invoke(true);
        }
    }

    public IEnumerator GetSkybox(Action<bool> onCallback)
    {
        var skyboxData = reference.Child("currentSession").Child("skybox").GetValueAsync();

        yield return new WaitUntil(predicate: () => skyboxData.IsCompleted);

        DataSnapshot snapshot = skyboxData.Result;

        var value = snapshot.Value;

        if (value != null)
        {
            onCallback.Invoke(bool.Parse(value.ToString()));
        }
        else
        {
            onCallback.Invoke(true);
        }
    }

    public void GetSessionInfo()
    {
        StartCoroutine(GetSessionID((string sessionID) =>
        {
            this.sessionID = sessionID;
        }));

        StartCoroutine(GetParticipantID((int participantID) =>
        {
            this.participantID = participantID;
        }));

        StartCoroutine(GetMinPlatformHeight((float height) =>
        {
            this.minPlatformHeight = height;
        }));

        StartCoroutine(GetMaxPlatformHeight((float height) =>
        {
            this.maxPlatformHeight = height;
        }));

        StartCoroutine(GetMinPlatformDistance((float distance) =>
        {
            this.minPlatformDistance = distance;
        }));

        StartCoroutine(GetMaxPlatformDistance((float distance) =>
        {
            this.maxPlatformDistance = distance;
        }));

        StartCoroutine(GetMinPlatformSpeed((float speed) =>
        {
            this.minPlatformSpeed = speed;
        }));

        StartCoroutine(GetMaxPlatformSpeed((float speed) =>
        {
            this.maxPlatformSpeed = speed;
        }));

        StartCoroutine(GetEnvironment((bool environment) =>
        {
            this.environment = environment;
        }));

        StartCoroutine(GetSkybox((bool skybox) =>
        {
            this.skybox = skybox;
        }));

        if (sessionID != null && participantID >= 0 && minPlatformHeight >= 0f && maxPlatformHeight >= minPlatformHeight && minPlatformDistance >= 0f && maxPlatformDistance >= minPlatformDistance && minPlatformSpeed >= 0f && maxPlatformSpeed >= minPlatformSpeed)
        {
            GameManager.Instance.isSessionStarted = true;
        }
    }

    /*    public void CreateSession()
    {
        StartCoroutine(GetSessionID((string sessionID) =>
        {
            if (sessionID != null)
            {
                Debug.Log(sessionID);
            }
            else
            {
                //Session newSession = new Session(intParticipantID, fHeight);
                //string json = JsonUtility.ToJson(newSession);

                //reference.Child("currentSession").SetRawJsonValueAsync(json);
            }
        }));
    }*/

    /*public IEnumerator GetSessionInfo(Action<string> onCallback)
    {
        var sessionData = reference.Child("currentSession").GetValueAsync();

        yield return new WaitUntil(predicate: () => sessionData.IsCompleted);

        DataSnapshot snapshot = sessionData.Result;

        if (snapshot != null)
        {
            onCallback.Invoke(snapshot.ToString());
            snapshot.getRef().removeValue();
        }
        else
        {
            onCallback.Invoke(null);
        }
    }*/

    /*public void FinishSession()
    {
        StartCoroutine(GetSessionID((string sessionID) =>
        {
            if (sessionID != null)
            {
                this.sessionID = sessionID;
            }
        }));

        if (sessionID != null && participantID != 0 && height != 0f)
        {
            GameManager.Instance.isSessionStarted = true;
            platformManager.GeneratePlatforms();
        }
    }*/
}