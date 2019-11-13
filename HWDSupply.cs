﻿using System;
using ff14bot;
using ff14bot.Managers;
using ff14bot.RemoteWindows;

namespace Ishgard
{
    public static class HWDSupply
    {
        
        private static readonly string WindowName = "HWDSupply";
        private static readonly int offset0 = 0x1CA;
        private static readonly int offset2 = 0x160;

        public static bool IsOpen => RaptureAtkUnitManager.GetWindowByName(WindowName) != null;

        public static int ClassSelected
        {
            get => ___Elements()[29].TrimmedData;
            set
            {
                var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
                if (windowByName != null && ___Elements()[29].TrimmedData != value)
                    windowByName.SendAction(2,0,1,1,(ulong) value);
            }
        }

        public static void ClickItem(int index)
        {
            var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
            if (windowByName != null)
                windowByName.SendAction(2,3,1,3,(ulong) index);
        }
        
        private static TwoInt[] ___Elements()
        {
            var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
            if (windowByName == null) return null;
            var elementCount = ElementCount();
            var addr = Core.Memory.Read<IntPtr>(windowByName.Pointer + offset2);
            return Core.Memory.ReadArray<TwoInt>(addr, elementCount);
        }

        private static ushort ElementCount()
        {
            var windowByName = RaptureAtkUnitManager.GetWindowByName(WindowName);
            return windowByName != null ? Core.Memory.Read<ushort>(windowByName.Pointer + offset0) : (ushort) 0;
        }
    }
}