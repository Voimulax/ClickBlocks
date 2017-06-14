﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ClickBlocksClient
{
    /// <summary>
    /// R模块
    /// </summary>
    class R
    {
        /// <summary>
        /// 查找Resources里面的字符串数据，若找不到则返回空
        /// </summary>
        /// <param name="name">字符串的名称</param>
        /// <returns>查找结果</returns>
        public static string GetString(string name)
        {
            return Properties.Resources.ResourceManager.GetString(name);
        }
    }
    /// <summary>
    /// 禁止Frame的导航
    /// </summary>
    public static class DisableNavigation
    {
        public static bool GetDisable(DependencyObject o)
        {
            return (bool)o.GetValue(DisableProperty);
        }
        public static void SetDisable(DependencyObject o, bool value)
        {
            o.SetValue(DisableProperty, value);
        }

        public static readonly DependencyProperty DisableProperty =
            DependencyProperty.RegisterAttached("Disable", typeof(bool), typeof(DisableNavigation),
                                                new PropertyMetadata(false, DisableChanged));



        public static void DisableChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var frame = (Frame)sender;
            frame.Navigated += DontNavigate;
            frame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
        }

        public static void DontNavigate(object sender, NavigationEventArgs e)
        {
            ((Frame)sender).NavigationService.RemoveBackEntry();
        }
    }
    /// <summary>
    /// Security模块
    /// </summary>
    class Security
    {
        /// <summary>
        /// SHA512加密
        /// </summary>
        /// <param name="strData"></param>
        /// <returns></returns>
        public static string GetSHA512Hash(string strData)
        {
            byte[] bytValue = System.Text.Encoding.UTF8.GetBytes(strData);
            try
            {
                SHA512 sha512 = new SHA512CryptoServiceProvider();
                byte[] retVal = sha512.ComputeHash(bytValue);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x"));
                }
                sha512.Clear();
                return Convert.ToBase64String(retVal);
                //return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex; 
            }
        }
    }
}
