namespace App.Presentation.Helpers
{
    public class SideMenuItemsCollection
    {

        public List<SideMenuItem> SideMenuItems = new List<SideMenuItem>()
        {   
            new SideMenuItem()
            {
                ItemTextLocalizationResource="BasicDataMenuName", Controler="",Action="",Data_Ds_Target="",Icon="fa fa-store",ItemType=SideMenuItemType.MainMenu,
                ChaildsMenuItems=new List<SideMenuItem>()
                {
                    new SideMenuItem()
                    {
                        ItemTextLocalizationResource="ClinicData", Controler="Clinic",Action="List",Data_Ds_Target="",Icon="k-icon k-i-collapse",ItemType=SideMenuItemType.SubSubMenu
                    },
                    new SideMenuItem()
                    {
                        ItemTextLocalizationResource="Users", Controler="Account",Action="UserList",Data_Ds_Target="",Icon="k-icon k-i-collapse",ItemType=SideMenuItemType.SubSubMenu
                    },
                  new SideMenuItem()
                    {
                        ItemTextLocalizationResource="Patients", Controler="Patient",Action="List",Data_Ds_Target="",Icon="k-icon k-i-collapse",ItemType=SideMenuItemType.SubSubMenu
                    },

                }
            },


             new SideMenuItem()
            {
                ItemTextLocalizationResource="TransDataMenuName", Controler="",Action="",Data_Ds_Target="",Icon="fa fa-store",ItemType=SideMenuItemType.MainMenu,
                ChaildsMenuItems=new List<SideMenuItem>()
                {
                    //new SideMenuItem()
                    //{

                    //},
                 
                }
            },


        };
    }

    public class SideMenuItem
    {
        public string ItemTextLocalizationResource { get; set; }
        public string Controler { get; set; }
        public string Action { get; set; }
        public string Data_Ds_Target { get; set; }
        public string Icon { get; set; }
        public SideMenuItemType ItemType { get; set; }
        public List<SideMenuItem> ChaildsMenuItems { get; set; }
        //public AspNetClaims Permission { get; set; }


    }

    public enum SideMenuItemType
    {
        MainMenu,
        SubMenu,
        SubSubMenu
    }
}
