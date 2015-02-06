using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Prime31;

public class InAppManager : MonoBehaviour 
{
	// In app purchases product list
	//private List<StoreKitProduct> _products;
	private string storeId = "857751871";

	void Start()
	{
		// put the name of the in app purchaess here...
		/*
		var productIdentifiers = new string[] { 
			"credits_004", 
			"credits_003", 
			"exp_003",
			"credits_002",
			"exp_002",
			"credits_001",
			"exp_001",
			};
		StoreKitBinding.requestProductData( productIdentifiers );
		
		// you cannot make any purchases until you have retrieved the products from the server with the requestProductData method
		// we will store the products locally so that we will know what is purchaseable and when we can purchase the products
		StoreKitManager.productListReceivedEvent += allProducts =>
		{
			Debug.Log( "received total products: " + allProducts.Count );
			_products = allProducts;
		};

*/
	}

	// change the names of the case statements here to what the productIdentifiers are...
	public void PurchaseEvent(string name)
	{
		switch (name) 
		{
		case "credits_001":
			Debug.Log("Bought 3000 credits");
			Variables.instance.IncreaseMoney(3000);
			Variables.instance.SaveInformationNow();
			break;
		case "credits_002":
			Debug.Log("Bought 9000 credits");
			Variables.instance.IncreaseMoney(9000);
			Variables.instance.SaveInformationNow();
			break;
		case "credits_003":
			Debug.Log("Bought 27000 credits");
			Variables.instance.IncreaseMoney(27000);
			Variables.instance.SaveInformationNow();
			break;
		case "credits_004":
			Debug.Log("Bought 50k, 10 xp credits");
			Variables.instance.IncreaseMoney(50000);
			Variables.instance.playerExperiencePoints+=10;
			Variables.instance.SaveInformationNow();
			break;
		case "exp_001":
			Debug.Log("Bought 1 xp");
			Variables.instance.playerExperiencePoints+=1;
			Variables.instance.SaveInformationNow();
			break;
		case "exp_002":
			Debug.Log("Bought 3 xp");
			Variables.instance.playerExperiencePoints+=3;
			Variables.instance.SaveInformationNow();
			break;
		case "exp_003":
			Debug.Log("Bought 7 xp");
			Variables.instance.playerExperiencePoints+=7;
			Variables.instance.SaveInformationNow();
			break;
		}
	}

	// use the same id as they are set in the productIdentifiers...
	public void BuyInAppPurchase(int productId)
	{
		// enforce the fact that we can't purchase products until we retrieve the product data
		/*if( _products != null && _products.Count > 0 )
		{
			var product = _products[productId];
			
			Debug.Log( "preparing to purchase product: " + product.productIdentifier );
			StoreKitBinding.purchaseProduct( product.productIdentifier, 1 );
		}*/
	}

	public void OpenStore()
	{
		//StoreKitBinding.displayStoreWithProductId( storeId );
	}
}
