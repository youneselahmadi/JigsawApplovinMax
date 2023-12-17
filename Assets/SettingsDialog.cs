using UnityEngine.Purchasing;
using UnityEngine.UI;

public class SettingsDialog : Dialog
{
  

    protected override void Start()
    {
        base.Start();
       
    }

    public void RateUs() {
        CUtils.OpenStore();
    } 
    public void GoToPivacyPolcy() {
        CUtils.PrivacyPolicy(ConfigController.Config.privacyPolicy);
    }
   
}
