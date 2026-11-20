using UnityEngine;

public class EmptyCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player)
    {
       if (!HasKitchenObject())
        {
            // nothing there
            if (player.HasKitchenObject())
            {
                // is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else
            {
                // not carrying anything
            }
        } else
        {
            // is something there
            if (player.HasKitchenObject())
            {
                // is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    // is holding plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                    GetKitchenObject().DestroySelf();
                    }
                } else
                {
                    // not carrying plate but is carrying food
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject))
                    {
                        // counter has plate
                         if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
                
            } else
            {
                // not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
