using TMPro;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ShopDialog : Dialog
{
    public TextMeshProUGUI[] valueTexts, priceTexts;
    public TextMeshProUGUI watchAdValueText;

    protected override void Start()
    {
        base.Start();
        Purchaser.instance.onItemPurchased += OnItemPurchased;
        for(int i = 0; i < valueTexts.Length; i++)
        {
            valueTexts[i].text = Purchaser.instance.iapItems[i].value.ToString();
            priceTexts[i].text = "$" + Purchaser.instance.iapItems[i].price.ToString();
        }

        watchAdValueText.text = ConfigController.Config.rewardedVideoAmount + " Free";
    }

    public void OnBuyProduct(int index)
	{
		Sound.instance.PlayButton();
        Purchaser.instance.BuyProduct(index);
    }

    private void OnItemPurchased(IAPItem item, int index)
    {
        // A consumable product has been purchased by this user.
        if (item.productType == ProductType.Consumable)
        {
            CurrencyController.CreditBalance(item.value);
            Toast.instance.ShowMessage("Your purchase is successful");
            CUtils.SetBuyItem();
        }
        // Or ... a non-consumable product has been purchased by this user.
        else if (item.productType == ProductType.NonConsumable)
        {
            // TODO: The non-consumable item has been successfully purchased, grant this item to the player.
        }
        // Or ... a subscription product has been purchased by this user.
        else if (item.productType == ProductType.Subscription)
        {
            // TODO: The subscription item has been successfully purchased, grant this to the player.
        }
    }
	
	private void OnDestroy()
	{
		Purchaser.instance.onItemPurchased -= OnItemPurchased;
	}
}
