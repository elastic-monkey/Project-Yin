public class ShopMenu : GameMenu
{
    public override bool OnNavItemAction(NavItem item, object actionObj, string[] data)
    {
        var action = (Actions)actionObj;

        switch (action)
        {
            case Actions.LeaveShop:
                Close();
                return true;
        }

        return false;
    }
}
