﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Threading;
using GTA5OnlineTools.Common.Utils;
using GTA5OnlineTools.Features.SDK;
using GTA5OnlineTools.Features.Core;

namespace GTA5OnlineTools.Modules.Windows
{
    /// <summary>
    /// CasinoHackWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CasinoHackWindow : Window
    {
        public CasinoHackWindow()
        {
            InitializeComponent();

            {
                Dictionary<int, string> dicItem = new Dictionary<int, string>();
                for (int i = 0; i < 37; i++)
                {
                    dicItem.Add(i, i.ToString());
                }
                dicItem.Add(37, "00");
                cmb_Roulette.ItemsSource = dicItem;
                cmb_Roulette.SelectedIndex = 0;
            }
            {
                Dictionary<int, string> dicItem = new Dictionary<int, string>();
                dicItem.Add(0, "奖品一");
                dicItem.Add(1, "奖品二");
                dicItem.Add(2, "奖品三");
                dicItem.Add(3, "奖品四");
                dicItem.Add(4, "奖品五");
                dicItem.Add(5, "奖品六");
                dicItem.Add(6, "奖品七");
                dicItem.Add(7, "奖品八");
                cmb_SlotMachine.ItemsSource = dicItem;
                cmb_SlotMachine.SelectedIndex = 6;
            }
            {
                Dictionary<int, string> dicItem = new Dictionary<int, string>();
                dicItem.Add(0, "衣服(1)");
                dicItem.Add(1, "2500经验");
                dicItem.Add(2, "20000美元");
                dicItem.Add(3, "10000筹码");
                dicItem.Add(4, "折扣");
                dicItem.Add(5, "5000经验");
                dicItem.Add(6, "30000美元 ");
                dicItem.Add(7, "15000筹码");
                dicItem.Add(8, "衣服(2)");
                dicItem.Add(9, "7500经验");
                dicItem.Add(10, "20000筹码");
                dicItem.Add(11, "神秘奖品");
                dicItem.Add(12, "衣服(3)");
                dicItem.Add(13, "10000经验");
                dicItem.Add(14, "40000美元");
                dicItem.Add(15, "25000筹码");
                dicItem.Add(16, "衣服(4)");
                dicItem.Add(17, "15000经验");
                dicItem.Add(18, "载具奖品");
                dicItem.Add(19, "50000美元");
                cmb_LuckyWheel.ItemsSource = dicItem;
                cmb_LuckyWheel.SelectedIndex = 18;
            }

            var thread_main = new Thread(main_thread);
            thread_main.IsBackground = true;
            thread_main.Start();
        }
        private void Window_CasinoHack_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                Memory.Initialize(CoreUtil.TargetAppName);

                Globals.LocalScriptsPTR = Memory.FindPattern(Offsets.Mask.LocalScriptsMask);
                Globals.LocalScriptsPTR = Memory.Rip_37(Globals.LocalScriptsPTR);
            });
        }
        private void Window_CasinoHack_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void main_thread()
        {
            while (true)
            { 
                Dispatcher.BeginInvoke(new Action(delegate
                {
                    if (CheckBox_Blackjack.IsChecked == true)
                    {
                        long p = Locals.LocalAddress("blackjack");
                        if (p != 0)
                        {
                            p = Memory.Read<long>(p);
                            int i = Memory.Read<int>(p + 0x3F60);
                            string str = "";
                            if ((i - 1) / 13 == 0) str += "♣梅花" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 1) str += "♦方块" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 2) str += "♥红心" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 3) str += "♠黑桃" + ((i - 1) % 13 + 1).ToString();
                            TextBox_Blackjack.Text = str;
                        }                 
                    }
                    if (CheckBox_Poker.IsChecked == true)
                    {
                        long p = Locals.LocalAddress("three_card_poker");
                        if (p != 0)
                        {
                            p = Memory.Read<long>(p);
                            int i = Memory.Read<int>(p + 0x3948);
                            string str = "";
                            if ((i - 1) / 13 == 0) str += "♣梅花" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 1) str += "♦方块" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 2) str += "♥红心" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 3) str += "♠黑桃" + ((i - 1) % 13 + 1).ToString();
                            str += " ";
                            i = Memory.Read<int>(p + 0x3938);
                            if ((i - 1) / 13 == 0) str += "♣梅花" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 1) str += "♦方块" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 2) str += "♥红心" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 3) str += "♠黑桃" + ((i - 1) % 13 + 1).ToString();
                            str += " ";
                            i = Memory.Read<int>(p + 0x3940);
                            if ((i - 1) / 13 == 0) str += "♣梅花" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 1) str += "♦方块" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 2) str += "♥红心" + ((i - 1) % 13 + 1).ToString();
                            if ((i - 1) / 13 == 3) str += "♠黑桃" + ((i - 1) % 13 + 1).ToString();
                            TextBox_Poker.Text = str;
                        }                     
                    }
                    if (CheckBox_Roulette.IsChecked == true)
                    {
                        long p = Locals.LocalAddress("casinoroulette");
                        if (p != 0)
                        {
                            p = Memory.Read<long>(p);
                            for (int i = 0; i < 6; i++)
                            {
                                Memory.Write<int>(p + 0x32D0 + i * 0x8, cmb_Roulette.SelectedIndex);
                            }
                        }
                    }
                    if(CheckBox_SlotMachine.IsChecked == true)
                    {
                        long p = Locals.LocalAddress("casino_slots");
                        if (p != 0)
                        {
                            p = Memory.Read<long>(p);
                            for(int i = 0; i < 3; i++)
                                for(int j = 0; j < 64; j++)
                                {
                                    int index = 1339 + 1 + 1 + i * 65 + 1 + j;
                                    Memory.Write<int>(p + index * 8, cmb_SlotMachine.SelectedIndex);
                                }
                        }
                    }
                    if(CheckBox_LuckyWheel.IsChecked == true)
                    {
                        long p = Locals.LocalAddress("casino_lucky_wheel");//https://www.unknowncheats.me/forum/grand-theft-auto-v/483416-gtavcsmm.html
                        if (p != 0)
                        {
                            p = Memory.Read<long>(p);
                            int index = 271 + 14;
                            Memory.Write<int>(p + index * 8, cmb_LuckyWheel.SelectedIndex);
                        }
                    }
                }));
                Thread.Sleep(500);
            }
        }
        private void CheckBox_Blackjack_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CheckBox_Poker_Click(object sender, RoutedEventArgs e)
        {

        }
        private void CheckBox_Roulette_Click(object sender, RoutedEventArgs e)
        {

        }
        private void cmb_Roulette_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        private void CheckBox_SlotMachine_Click(object sender, RoutedEventArgs e)
        {

        }
        private void cmb_SlotMachine_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
        private void CheckBox_LuckyWheel_Click(object sender, RoutedEventArgs e)
        {

        }
        private void cmb_LuckyWheel_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
