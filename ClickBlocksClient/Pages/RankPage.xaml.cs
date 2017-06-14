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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ClickBlocksClient.Statics;

namespace ClickBlocksClient
{
    /// <summary>
    /// RankPage.xaml 的交互逻辑
    /// </summary>
    public partial class RankPage : Page
    {
        public RankPage()
        {
            InitializeComponent();
            Loaded += RankPage_Loaded;
        }

        private void RankPage_Loaded(object sender, RoutedEventArgs e)
        {
            MWindow.Title = Title;
            RankModeCbBox.ItemsSource = R.GetString("RankModes").Split('#');
            RankRangeCbBox.ItemsSource = R.GetString("RankRange").Split('#');
            RankModeCbBox.SelectedIndex = RankRangeCbBox.SelectedIndex = 0;
        }

        private void RankModeCbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RankModeCbBox.SelectedIndex == -1) return;
            if (RankModeCbBox.SelectedValue as string == "总积分")
            {
                RankRangeCbBox.ItemsSource = new string[] { "全球" };
            }
            else
            {
                RankRangeCbBox.ItemsSource = R.GetString("RankRange").Split('#');
            }
            RankRangeCbBox.SelectedIndex = 0;
        }

        private void RankRangeCbBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RankRangeCbBox.SelectedIndex == -1) return;
            else
            {
                if (RankModeCbBox.SelectedValue as string == "总积分")
                {
                    var list = Client.GetPeopleList();
                    RankList.ItemsSource = list.OrderByDescending(x => x.Score).Take(100);
                }
                else
                {
                    if (RankRangeCbBox.SelectedValue as string == "个人")
                    {
                        var list = Client.GetRecordList(RankModeCbBox.SelectedValue as string, UserName);
                        RankList.ItemsSource = list.OrderByDescending(x => x.Score).Take(100);
                    }
                    else
                    {
                        var list = Client.GetRecordList(RankModeCbBox.SelectedValue as string, null);
                        RankList.ItemsSource = list.OrderByDescending(x => x.Score).Take(100);
                    }
                }
            }
        }
    }
}
