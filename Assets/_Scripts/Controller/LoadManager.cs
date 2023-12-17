using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class LoadManager : BaseController
{

    private PackData packDatad;
    private PackDatas PackDatas;
    public Slider progressBar;
    // Start is called before the first frame update

    public TextMeshProUGUI progressText;

    public float fakeLoadTime = 20f;
    private float currentTime = 0f;


    void Start()
    {
        base.Start();

        currentTime = 0f;
        string jsonFileName = "default"; // Exclude the file extension
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(jsonFileName);

        if (jsonTextAsset != null)
        {

            // Parse the JSON text into a RootData object
            PackDatas = JsonUtility.FromJson<PackDatas>(jsonTextAsset.text);
          
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                Toast.instance.ShowMessage("No internet connection");
            }
            else
            {
                // Now you can access the data
                foreach (PackData packData in PackDatas.packDatas)
                {
                    packDatad = packData;
                    Debug.Log($"Pack Name: {packData.name}, Price: {packData.price}");
                    DownloadPack(packData);
                   
                }
            
            }

        }
        else
        {
            Debug.LogError($"Failed to load JSON file: {jsonFileName}");
        }
       
    }
    private int numComplete = 0;
    private int numDownload = 0;

    // Update is called once per frame
    void Update()
    {
        if (currentTime < fakeLoadTime)
        {
            currentTime += Time.deltaTime;
            float progress = currentTime / fakeLoadTime;

            // Update the progress bar value
            progressBar.value = progress;

            // Display the progress percentage as text
            progressText.text = Mathf.Round(progress * 100f) + "%";
        }
        else
        {
            // Loading is complete, you can perform any necessary actions here

            // For example, load the next scene or enable a game object
            // SceneManager.LoadScene("NextScene");
            // gameObject.SetActive(false);
            CUtils.LoadScene(1, false);
            Debug.Log("Loading complete!");
        }
    }

   
    public void DownloadPack(PackData packData)
    {
        Prefs.BuyPack(packData);
        
        //downloadBtn.GetComponent<Button>().interactable = false;
        //loading.SetActive(true);
        //numDownload = numComplete = 0;
        //foreach (var photoData in packData.photoDatas)
        //{
        //    string imageName = Path.GetFileName(photoData.imageUrl);
        //    string iconName = Path.GetFileName(photoData.iconUrl);
        //    Debug.Log("photoData.iconUrl" + photoData.iconUrl);
        //    StartCoroutine(CUtils.LoadPicture(photoData.imageUrl, packData.name, imageName, OnCachePictureComplete, Const.imageSize, true));
        //    StartCoroutine(CUtils.LoadPicture(photoData.iconUrl, packData.name, iconName, null, Const.iconSize, true));
        //}
    }

    private void OnCachePictureComplete(Texture2D texture)
    {
        numDownload++;
        if (texture != null) numComplete++;


        Debug.Log("Num Complete: " + numComplete);
        if (numDownload == packDatad.photoDatas.Count)
        {
            if (numDownload == numComplete)
            {
               
                Toast.instance.ShowMessage("The pack is downloaded");
            }
            else
            {
                Toast.instance.ShowMessage(numComplete + "/" + packDatad.photoDatas.Count + " pictures are downloaded");
            }

            //downloadBtn.GetComponent<Button>().interactable = true;
            //loading.SetActive(false);
            //UpdateButtons();
        }
    }
}
