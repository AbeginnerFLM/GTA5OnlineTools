﻿using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Data;

namespace GTA5OnlineTools.Modules.Windows.ExternalMenu;

/// <summary>
/// EM10JobHelperView.xaml 的交互逻辑
/// </summary>
public partial class EM10JobHelperView : UserControl
{
    public EM10JobHelperView()
    {
        InitializeComponent();

        ExternalMenuView.ClosingDisposeEvent += ExternalMenuView_ClosingDisposeEvent;
    }

    private void ExternalMenuView_ClosingDisposeEvent()
    {
        
    }

    private void CheckBox_RemoveBunkerSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Globals.Remove_Bunker_Supply_Delay(CheckBox_RemoveBunkerSupplyDelay.IsChecked == true);
    }

    private void CheckBox_UnlockBunkerResearch_Click(object sender, RoutedEventArgs e)
    {
        Globals.Unlock_Bunker_Research(CheckBox_UnlockBunkerResearch.IsChecked == true);
    }

    private void Button_TriggerBunkerResearch_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int supplycount = Globals.GG<int>(Globals.Get_Business_Index(5) + 2);
        int progress = Globals.GG<int>(Globals.Get_Business_Index(5) + 12);

        if (progress == 60)
        {
            TextBox_Result.Text = $"地堡研究进度已完成，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"地堡原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Globals.SG<int>(Globals.Get_Business_Index(5) + 13, 0);
            TextBox_Result.Text = $"正在触发地堡研究，当前研究进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void Button_TriggerBunkerProduction_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        int supplycount = Globals.GG<int>(Globals.Get_Business_Index(5) + 2);
        int progress = Globals.GG<int>(Globals.Get_Business_Index(5) + 1);

        if (progress == 100)
        {
            TextBox_Result.Text = $"地堡库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"地堡原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Globals.SG<int>(Globals.Get_Business_Index(5) + 9, 0);
            TextBox_Result.Text = $"正在触发地堡生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    //private void Button_DeliverBunker_Click(object sender, RoutedEventArgs e)
    //{
    //    CoreUtils.ClickSound();

    //    Memory.Write<int>(Globals.GlobalPTR - 0x128, Offsets.BunkerDelivery_1, 1);

    //    Globals.SG<float>(262145 + 21045, 1.0f);
    //    Globals.SG<int>(262145 + 21228 + 171, 1);
    //    Globals.SG<int>(262145 + 21232 + 171, 1);
    //    Globals.SG<int>(262145 + 21222 + 171, 1);
    //    Globals.SG<int>(262145 + 21230 + 171, 1);
    //    Globals.SG<int>(262145 + 21235 + 171, 1);
    //    Globals.SG<int>(262145 + 21224 + 171, 1);
    //    Thread.Sleep(1000);
    //    Globals.SG<float>(262145 + 21045, 1.5f);
    //    Globals.SG<int>(262145 + 21228 + 171, 1800000);
    //    Globals.SG<int>(262145 + 21232 + 171, 900000);
    //    Globals.SG<int>(262145 + 21222 + 171, 900000);
    //    Globals.SG<int>(262145 + 21230 + 171, 900000);
    //    Globals.SG<int>(262145 + 21235 + 171, 900000);
    //    Globals.SG<int>(262145 + 2122 + 1714, 900000);

    //    Button_DeliverBunker.IsEnabled = false;
    //}

    //private void Button_ReadBunkerData_Click(object sender, RoutedEventArgs e)
    //{
    //    CoreUtils.ClickSound();

    //    TextBox_BunkerValue.Text = "0";
    //    TextBox_BunkerCargo.Text = $"{Memory.Read<int>(Globals.GlobalPTR - 0x128, Offsets.BunkerCargo)}";
    //    TextBlock_BunkerDeliveryCount.Text = $"{Memory.Read<int>(Globals.GlobalPTR - 0x128, Offsets.BunkerDelivery)}";
    //}

    //private void Button_WriteBunkerData_Click(object sender, RoutedEventArgs e)
    //{
    //    CoreUtils.ClickSound();

    //    if (TextBox_BunkerValue.Text != "" &&
    //        TextBox_BunkerCargo.Text != "" &&
    //        Convert.ToInt32(TextBox_BunkerValue.Text) > 0 &&
    //        Convert.ToInt32(TextBox_BunkerCargo.Text) < 100 &&
    //        (Convert.ToInt32(TextBox_BunkerValue.Text) / Convert.ToInt32(TextBox_BunkerCargo.Text)) != 0)
    //    {
    //        int BunkerMoney = 2100000;

    //        if (ComboBox_BunkerMoney.SelectedIndex == 0)
    //        {
    //            BunkerMoney = 2100000;
    //        }
    //        else if (ComboBox_BunkerMoney.SelectedIndex == 1)
    //        {
    //            BunkerMoney = 1700000;
    //        }
    //        else if (ComboBox_BunkerMoney.SelectedIndex == 2)
    //        {
    //            BunkerMoney = 1400000;
    //        }
    //        else if (ComboBox_BunkerMoney.SelectedIndex == 3)
    //        {
    //            BunkerMoney = 1050000;
    //        }
    //        else if (ComboBox_BunkerMoney.SelectedIndex == 4)
    //        {
    //            BunkerMoney = 700000;
    //        }

    //        Memory.Write<int>(Globals.GlobalPTR - 0x128, Offsets.BunkerDelivery, BunkerMoney / (Convert.ToInt32(TextBox_BunkerValue.Text) / Convert.ToInt32(TextBox_BunkerCargo.Text)));

    //        TextBlock_BunkerDeliveryCount.Text = $"{Memory.Read<int>(Globals.GlobalPTR - 0x128, Offsets.BunkerDelivery)}";

    //        MessageBox.Show("写入成功，请返回游戏正常出货或者摧毁货车",
    //            "提示", MessageBoxButton.OK, MessageBoxImage.Information);

    //        Button_DeliverBunker.IsEnabled = true;
    //    }
    //    else
    //    {
    //        MessageBox.Show("<地堡出货价值>或<地堡出货数量>值不合法，请检查后再重新写入",
    //            "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
    //    }
    //}

    private void CheckBox_RemoveBuyingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Globals.CEO_Buying_Crates_Cooldown(CheckBox_CooldownForBuyingCrates.IsChecked == true);
    }

    private void CheckBox_RemoveSellingCratesCooldown_Click(object sender, RoutedEventArgs e)
    {
        Globals.CEO_Selling_Crates_Cooldown(CheckBox_CooldownForSellingCrates.IsChecked == true);
    }

    private void CheckBox_PricePerCrateAtXCrates_Click(object sender, RoutedEventArgs e)
    {
        Globals.CEO_Price_Per_Crate_At_Crates(CheckBox_PricePerCrateAtXCrates.IsChecked == true);
    }

    private int CheckBusinessID(int index)
    {
        //int[] Meth = new int[4] { 1, 6, 11, 16 };
        //int[] Weed = new int[4] { 2, 7, 12, 17 };
        //int[] Cocaine = new int[4] { 3, 8, 13, 18 };
        //int[] Cash = new int[4] { 4, 9, 14, 19 };
        //int[] Documents = new int[4] { 5, 10, 15, 20 };

        int[] business = new int[5];

        for (int i = 0; i < 5; i++)
        {
            int temp = Globals.GG<int>(Globals.Get_Business_Index(i));

            if (temp <= 5)
            {
                switch (temp)
                {
                    case 0:
                        business[4] = i;
                        break;
                    case 1:
                        business[0] = i;
                        break;
                    case 2:
                        business[1] = i;
                        break;
                    case 3:
                        business[2] = i;
                        break;
                    case 4:
                        business[3] = i;
                        break;
                }
            }
            else
            {
                temp %= 5;

                switch (temp)
                {
                    case 0:
                        business[4] = i;
                        break;
                    case 1:
                        business[0] = i;
                        break;
                    case 2:
                        business[1] = i;
                        break;
                    case 3:
                        business[2] = i;
                        break;
                    case 4:
                        business[3] = i;
                        break;
                }
            }

        }

        return business[index];
    }

    private void CheckBox_TriggerMethProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(0);

        int supplycount = Globals.GG<int>(Globals.Get_Business_Index(id) + 2);
        int progress = Globals.GG<int>(Globals.Get_Business_Index(id) + 1);

        if (progress == 20)
        {
            TextBox_Result.Text = $"冰毒实验室库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"冰毒实验室原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Globals.SG<int>(Globals.Get_Business_Index(id) + 9, 0);
            TextBox_Result.Text = $"正在触发冰毒实验室生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerWeedProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(1);

        int supplycount = Globals.GG<int>(Globals.Get_Business_Index(id) + 2);
        int progress = Globals.GG<int>(Globals.Get_Business_Index(id) + 1);

        if (progress == 80)
        {
            TextBox_Result.Text = $"大麻种植场库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"大麻种植场原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Globals.SG<int>(Globals.Get_Business_Index(id) + 9, 0);
            TextBox_Result.Text = $"正在触发大麻种植场生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerCocaineProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(2);

        int supplycount = Globals.GG<int>(Globals.Get_Business_Index(id) + 2);
        int progress = Globals.GG<int>(Globals.Get_Business_Index(id) + 1);

        if (progress == 10)
        {
            TextBox_Result.Text = $"可卡因工厂库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"可卡因工厂原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Globals.SG<int>(Globals.Get_Business_Index(id) + 9, 0);
            TextBox_Result.Text = $"正在触发可卡因工厂生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerCashProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(3);

        int supplycount = Globals.GG<int>(Globals.Get_Business_Index(id) + 2);
        int progress = Globals.GG<int>(Globals.Get_Business_Index(id) + 1);

        if (progress == 40)
        {
            TextBox_Result.Text = $"假钞工厂库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"假钞工厂原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Globals.SG<int>(Globals.Get_Business_Index(id) + 9, 0);
            TextBox_Result.Text = $"正在触发假钞工厂生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_TriggerDocumentsProduction_Click(object sender, RoutedEventArgs e)
    {
        int id = CheckBusinessID(4);

        int supplycount = Globals.GG<int>(Globals.Get_Business_Index(id) + 2);
        int progress = Globals.GG<int>(Globals.Get_Business_Index(id) + 1);

        if (progress == 60)
        {
            TextBox_Result.Text = $"伪证件办公室库存已满，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else if (supplycount <= 0)
        {
            TextBox_Result.Text = $"伪证件办公室原材料已空，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
        else
        {
            Globals.SG<int>(Globals.Get_Business_Index(id) + 9, 0);
            TextBox_Result.Text = $"正在触发伪证件办公室生产，当前生产进度为 {progress}，原材料数量 {supplycount}";
        }
    }

    private void CheckBox_RemoveMCSupplyDelay_Click(object sender, RoutedEventArgs e)
    {
        Globals.Remove_MC_Supply_Delay(CheckBox_RemoveMCSupplyDelay.IsChecked == true);
    }

    private void Button_CEOCargos_Click(object sender, RoutedEventArgs e)
    {
        AudioUtil.ClickSound();

        var str = (e.OriginalSource as Button).Content.ToString();

        int index = MiscData.CEOCargos.FindIndex(t => t.Name == str);
        if (index != -1)
        {
            // They are in gb_contraband_buy at func_915, for future updates.
            Globals.CEO_Special_Cargo(false);
            Thread.Sleep(100);
            Globals.CEO_Special_Cargo(true);
            Thread.Sleep(100);
            Globals.CEO_Cargo_Type(MiscData.CEOCargos[index].ID);
        }
    }

    private void CheckBox_RemoveExportVehicleDelay_Click(object sender, RoutedEventArgs e)
    {
        Globals.Remove_Export_Vehicle_Delay(CheckBox_RemoveExportVehicleDelay.IsChecked == true);
    }

    private void CheckBox_RemoveSmugglerRunInDelay_Click(object sender, RoutedEventArgs e)
    {
        Globals.RemoveSmuggler_Run_In_Delay(CheckBox_RemoveSmugglerRunInDelay.IsChecked == true);
    }

    private void CheckBox_RemoveSmugglerRunOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Globals.Remove_Smuggler_Run_Out_Delay(CheckBox_RemoveSmugglerRunOutDelay.IsChecked == true);
    }

    private void CheckBox_SetBunkerProduceResearchTime_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_Bunker_Produce_Research_Time(Settings.ProduceTime, CheckBox_SetBunkerProduceResearchTime.IsChecked == true);
    }

    private void CheckBox_SetMCProduceTime_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_MC_Produce_Time(Settings.ProduceTime, CheckBox_SetMCProduceTime.IsChecked == true);
    }

    private void CheckBox_SetNightclubProduceTime_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_Nightclub_Produce_Time(Settings.ProduceTime, CheckBox_SetNightclubProduceTime.IsChecked == true);
    }

    private void CheckBox_SetBunkerResupplyCosts_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_Bunker_Resupply_Costs(CheckBox_SetBunkerResupplyCosts.IsChecked == true);
    }

    private void CheckBox_SetMCResupplyCosts_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_MC_Resupply_Costs(CheckBox_SetMCResupplyCosts.IsChecked == true);
    }

    private void CheckBox_SetBunkerSaleMultipliers_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_Bunker_Sale_Multipliers(CheckBox_SetBunkerSaleMultipliers.IsChecked == true);
    }

    private void CheckBox_SetMCSaleMultipliers_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_MC_Sale_Multipliers(CheckBox_SetMCSaleMultipliers.IsChecked == true);
    }

    private void CheckBox_SetBunkerSuppliesPerUnitProduced_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_Bunker_Supplies_Per_Unit_Produced(CheckBox_SetBunkerSuppliesPerUnitProduced.IsChecked == true);
    }

    private void CheckBox_SetMCSuppliesPerUnitProduced_Click(object sender, RoutedEventArgs e)
    {
        Globals.Set_MC_Supplies_Per_Unit_Produced(CheckBox_SetMCSuppliesPerUnitProduced.IsChecked == true);
    }

    private void CheckBox_RemoveNightclubOutDelay_Click(object sender, RoutedEventArgs e)
    {
        Globals.Remove_Nightclub_Out_Delay(CheckBox_RemoveNightclubOutDelay.IsChecked == true);
    }

    private void CheckBox_NightclubNoTonyLaunderingMoney_Click(object sender, RoutedEventArgs e)
    {
        Globals.Nightclub_No_Tony_Laundering_Money(CheckBox_NightclubNoTonyLaunderingMoney.IsChecked == true);
    }

    private void Slider_ProduceTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        Settings.ProduceTime = (int)Slider_ProduceTime.Value;
    }
}
