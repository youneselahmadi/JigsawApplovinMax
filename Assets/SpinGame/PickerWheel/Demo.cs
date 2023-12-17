using UnityEngine ;
using EasyUI.PickerWheelUI ;
using UnityEngine.UI ;
using TMPro;

public class Demo : MonoBehaviour {

   [SerializeField] private Button uiSpinButton ;

   [SerializeField] private PickerWheel pickerWheel ;
   [SerializeField] private GameObject SpinGame;
   [SerializeField] Button CloseButton;

    public TextMeshProUGUI countdownText;


    public CountdownManager countdownManager;


    //private void OnEnable()
    //{
    //    AdmobController.instance.rewardBasedVideo.OnUserEarnedReward += HandleRewardBasedVideoRewarded;
    //}
    // Singleton instance
 //   private static Demo instance;

    void Awake()
    {
        // Ensure only one instance of Countdown exists
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    private void Start () {

        uiSpinButton.onClick.AddListener (WatchAd) ;

        // Retrieve countdown state from the CountdownManager scriptable object
        countdownManager.remainingTime = countdownManager.timerRunning ? countdownManager.remainingTime : countdownManager.countdownTime;
        uiSpinButton.interactable = !countdownManager.timerRunning;
        countdownText.text = "Spin!!";
        // Ensure that the countdown text is updated
        // UpdateCountdownText();

    }
    void Update()
    {
        if (countdownManager.timerRunning)
        {
            countdownManager.remainingTime -= Time.deltaTime;

            if (countdownManager.remainingTime <= 0f)
            {
                countdownManager.timerRunning = false;
                uiSpinButton.interactable = true;
                countdownManager.remainingTime = 0f;
                
            }

            UpdateCountdownText();
        }
        else
        {
            countdownText.text = "Spin!!";
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaa" + countdownManager.remainingTime);
        }
    }
    public void HandleRewardBasedVideoRewarded(int sender, string args)
    {
        Debug.Log("spin start !!!!!!!!!!!!!!!!!");
        pickerWheel.Spin();
        ApplovinMaxManager.Instance.OnCustomRewardedVideoCompleted -= HandleRewardBasedVideoRewarded;
    }
   
    //private void OnDisable()
    //{
    //    AdmobController.instance.rewardBasedVideo.OnUserEarnedReward -= HandleRewardBasedVideoRewarded;
    //}
    public void onCloseButtonClick()
    {
        SpinGame.active = false;
    }

    void WatchAd()
    {

        //uiSpinButton.interactable = false;
        CloseButton.interactable = false;

        ApplovinMaxManager.Instance.OnCustomRewardedVideoCompleted += HandleRewardBasedVideoRewarded;

        pickerWheel.OnSpinEnd(wheelPiece => {
            Superpow.Utils.SpinRewardVideoAd(wheelPiece.Amount);

            Debug.Log(
               @" <b>Index:</b> " + wheelPiece.Index
               + "\n <b>Amount:</b> " + wheelPiece.Amount + "      <b>Chance:</b> " + wheelPiece.Chance + "%"
            );
            //uiSpinButton.interactable = true;
            CloseButton.interactable = true;


        });
        //  pickerWheel.Spin();
        ApplovinMaxManager.Instance.ShowRewardAd();
        // Assuming the user watches the ad and is rewarded with 'rewardedTime' seconds

        countdownManager.remainingTime = countdownManager.countdownTime;
        countdownManager.timerRunning = true;
        uiSpinButton.interactable = false;
    }

    void UpdateCountdownText()
    {
        countdownText.text = Mathf.Round(countdownManager.remainingTime).ToString();
    }



}
