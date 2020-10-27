using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListCreaor : MonoBehaviour
{
    [SerializeField]
    private Transform SpawnPoint = null;

    [SerializeField]
    private Button item = null;

    [SerializeField]
    private RectTransform content = null;

    public Text messages = null;
    public Text nameLabel = null;

    private Dictionary<string, string> listData = new Dictionary<string, string>();
    private List<Button> ItemList = new List<Button>();
    private NearbyManager nm;
    private string nameStr;


    private int count = 0;
    // Use this for initialization
    void Start()
    {
        nameStr = randomName();
        nameLabel.text = nameStr;
#if UNITY_ANDROID
        AndroidMyCallback cb = new AndroidMyCallback(this);
        nm = new NearbyManager(cb);
        nm.startDiscovery(nameStr);
#endif
    }

    public void AddPlayerToList(string endpointName, string endpointId)
    {
        if (listData.ContainsKey(endpointId) == false) {
            listData.Add(endpointId, endpointName);
            refreshList();
        }
    }

    public void RemovePlayerFromList(string endpointId) {
        nm.log( "RemovePlayerFromList:" + endpointId);
        if (listData.ContainsKey(endpointId) == true) {
            nm.log("listData contains:" + endpointId );
            listData.Remove(endpointId);
            nm.log("After remove. listData now have :" + listData.Count + " Items");
            refreshList();
        }
    }

    private string randomName() {
        string[] names = new string[] { "alice", "bob", "clark", "Tom", "Mickey", "David", "Geodge", "zhang", "wang", "li", "zhao" };
        int RandKey = Random.Range(0, names.Length - 1);
        return names[RandKey];
    }

    private void refreshList() {
        foreach (var item in ItemList) {
            GameObject.DestroyImmediate(item);
        }
        ItemList.Clear();
        count = 0;
        content.sizeDelta = new Vector2(0, listData.Count * 60);
        foreach (var dic in listData) {
            showOneItem(dic.Key, dic.Value);
        }
    }

    private void showOneItem(string endpointId, string endpointName) {
        // 60 width of item
        float spawnY = count * 60;
        //newSpawn Position
        Vector3 pos = new Vector3(SpawnPoint.position.x, -spawnY, SpawnPoint.position.z);
        Debug.Log("pos:" + pos.x + "," + pos.y + "," + pos.z);
        //instantiate item
        Button SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
        Debug.Log("pos:" + SpawnedItem.transform.position.x + "," + SpawnedItem.transform.position.y + "," + SpawnedItem.transform.position.z);
        ItemList.Add(SpawnedItem);
        SpawnedItem.onClick.AddListener(delegate () { this.OnClick(endpointId); }) ;
        //setParent
        SpawnedItem.transform.SetParent(SpawnPoint, false);
        Debug.Log("pos:" + SpawnedItem.transform.position.x + "," + SpawnedItem.transform.position.y + "," + SpawnedItem.transform.position.z);
        SpawnedItem.GetComponentInChildren<Text>().text = endpointName;
        count++;
    }

    private void OnClick(string endpointId)
    {
        nm.log("OnClick. SendMessage to " + endpointId);
        nm.SendMessage(endpointId, "invites you to join a game.");
    }

    public void ReceiveMsg(string remoteName ,string msg) {
        nm.log("Receive msg from:" + remoteName + ":" + msg);
        messages.text = remoteName + ":" + msg;
    }
}

