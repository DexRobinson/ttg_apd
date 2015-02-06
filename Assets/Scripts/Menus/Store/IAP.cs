using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Prime31;

public class IAP : MonoBehaviour{
	private bool isFullVersion;
	
	#if UNITY_IPHONE

	public static List<StoreKitProduct> _products;
	
	private static bool canMakePayment;
	
	void Start()
	{
		
		// you cannot make any purchases until you have retrieved the products from the server with the requestProductData method
		// we will store the products locally so that we will know what is purchaseable and when we can purchase the products
		StoreKitManager.productListReceivedEvent += allProducts =>
		{
			Debug.Log( "received total products: " + allProducts.Count );
			_products = allProducts;
		};
		
		canMakePayment = StoreKitBinding.canMakePayments();
		
		var productIdentifiers = new string[] { "small_credits", "unlock_game", "dev_support", "large_experience", "large_credits", "medium_experience", "medium_credits", "small_experience" };
		StoreKitBinding.requestProductData( productIdentifiers );
	}
	
	public static void PurchaseItem(int id)
	{
		if( _products != null && _products.Count > 0 )
		{
			Debug.Log("Buying: " + _products[id]);
			if(canMakePayment)
			{
				var productIndex = id;
				var product = _products[productIndex];
				
				Debug.Log( "preparing to purchase product: " + product.productIdentifier );
				StoreKitBinding.purchaseProduct( product.productIdentifier, 1 );
				
				
				MainMenuManager.instance.UpdateIAP();
			}
		}
	}

	#endif
}
