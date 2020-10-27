using UnityEngine.Networking;
class AndroidMyCallback : AndroidCallback
{
    ListCreaor mListController;
    public AndroidMyCallback(ListCreaor lc)
   : base()
    {
        mListController = lc;
    }
    public override void onFoundPlayer(string endpointName, string endpointId)
    {
        mListController.AddPlayerToList(endpointName, endpointId);
    }

    public override void onLostPlayer(string endpointId)
    {
        mListController.RemovePlayerFromList(endpointId);
    }
    public override void onReceiveMsg(string endpointName, string Msg)
    {
        mListController.ReceiveMsg(endpointName, Msg);
    }

}
