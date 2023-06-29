﻿namespace MoreBiomes;

public static class ClutterExtention
{
    public static MBClutter ToMBClutter(this ClutterSystem.Clutter clutter)
    {
        return new MBClutter(clutter);
    }

    public class MBClutter
    {
        public MBClutter(ClutterSystem.Clutter clutter)
        {
            this.clutter = clutter;
        }

        private ClutterSystem.Clutter clutter;

        public MBClutter SetMinAlt(float value)
        {
            clutter.m_minAlt = value;
            return this;
        }

        public MBClutter SetMaxAlt(float value)
        {
            clutter.m_maxAlt = value;
            return this;
        }

        public MBClutter SetMinScale(float value)
        {
            clutter.m_scaleMin = value;
            return this;
        }

        public MBClutter SetMaxScale(float value)
        {
            clutter.m_scaleMax = value;
            return this;
        }

        public MBClutter SetBiome(Heightmap.Biome value)
        {
            clutter.m_biome = value;
            return this;
        }
    }
}