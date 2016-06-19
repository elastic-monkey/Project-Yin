public class ShopMenu : GameMenu
{
    public ShopSellMenu SellMenu;

    public override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.GoToSell:
                SellMenu.UpdateTitle();
                return false;
            case Actions.LeaveShop:
                Close();
                return true;
        }

        return false;
    }
}
