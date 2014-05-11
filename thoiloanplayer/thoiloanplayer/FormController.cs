﻿/*
 * Created by SharpDevelop.
 * Author: Dat - trandatnh@gmail.com
 * Date: 11/5/2014
 * Time: 16:21 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace thoiloanplayer
{
	/// <summary>
	/// Description of FormController.
	/// </summary>
	public class FormController
	{
		public FormController()
		{
		}
		private delegate bool EnumWindowProc(IntPtr hwnd, IntPtr lParam);
 
		[DllImport("user32")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr lParam);
 
		public List<IntPtr> GetAllChildHandles(IntPtr handle)
		{
			var childHandles = new List<IntPtr>();
 
			GCHandle gcChildhandlesList = GCHandle.Alloc(childHandles);
			IntPtr pointerChildHandlesList = GCHandle.ToIntPtr(gcChildhandlesList);
 
			try {
				var childProc = new EnumWindowProc(EnumWindow);
				EnumChildWindows(handle, childProc, pointerChildHandlesList);
			} finally {
				gcChildhandlesList.Free();
			}
 
			return childHandles;
		}
 
		private bool EnumWindow(IntPtr hWnd, IntPtr lParam)
		{
			GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);
 
			if (gcChildhandlesList == null || gcChildhandlesList.Target == null) {
				return false;
			}
 
			var childHandles = gcChildhandlesList.Target as List<IntPtr>;
			childHandles.Add(hWnd);
 
			return true;
		}
	}
}