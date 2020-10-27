using UnityEngine;

public class NearbyManager
{
    AndroidCallback mAndroidCallback;
    public NearbyManager(AndroidCallback androidCallback)
    {
        mAndroidCallback = androidCallback;
    }

    public void startDiscovery(string ownName)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("startDiscovery", ownName, mAndroidCallback);
#endif
    }

    public void SendMessage(string endpointId, string msg)
    {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("SendMessage", endpointId, msg);
#endif
    }

    public void log(string info) {
#if UNITY_ANDROID
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("test", info);
#endif
    }

}

public class AndroidCallback : AndroidJavaProxy
{
    public AndroidCallback()
    : base("com.hms.nearbyjarforunity.IMyCallback")
    {
    }
    public virtual void onFoundPlayer(string endpointName, string endpointId)
    {
    }
    public virtual void onLostPlayer(string endpointId)
    {
    }
    public virtual void onReceiveMsg(string endpointName, string Msg)
    { 
    }
}