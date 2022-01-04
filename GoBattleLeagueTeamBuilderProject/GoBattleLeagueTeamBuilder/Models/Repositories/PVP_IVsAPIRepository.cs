using GoBattleLeagueTeamBuilder.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoBattleLeagueTeamBuilder.Models.Repositories
{
    public class PVP_IVsAPIRepository : IPVP_IVsAPIRepository
    {
        public void printBestIV(int league, int baseAtk, int baseDef, int baseSta, float xLStatus)
        {
            List<IVPerformance> IVPerformanceList = getAllIVTokens(league, baseAtk, baseDef, baseSta, xLStatus);
            IVPerformanceList.OrderBy(o => o.statProduct);

            Console.WriteLine(IVPerformanceList.Take(25));
        }

        public IVPerformance getBestIV(int league, int baseAtk, int baseDef, int baseSta, float xLStatus)
        {
            List<IVPerformance> IVPerformanceList = getAllIVTokens(league, baseAtk, baseDef, baseSta, xLStatus);
            return IVPerformanceList.OrderByDescending(o => o.statProduct).FirstOrDefault();
          
        }

        private List<IVPerformance> getAllIVTokens(int league, int baseAtk, int baseDef, int baseSta, float xLStatus)
        {
            List<IVPerformance> IVPerformanceList = new();
           
            for (int atkIV = 0; atkIV <= 15; atkIV++)
            {               
                for (int defIV = 0; defIV <= 15; defIV++)
                {               
                    for (int staIV = 0; staIV <= 15; staIV++)
                    {
                        IVPerformanceList.Add(getPerformanceFromIV(league, baseAtk, atkIV, baseDef, defIV, baseSta, staIV, xLStatus));
                    }
                }
            }

            return IVPerformanceList;
        }

        private IVPerformance getPerformanceFromIV(int league, int baseAtk, int atkIV, int baseDef, int defIV, int baseSta, int staIV, float xLStatus)
        {
            IVPerformance bestPerformance = new();
            for (float level = 1f; level <= xLStatus; level += 0.5f)
            {
                float atkStat = getStat(level, baseAtk, atkIV);
                float defStat = getStat(level, baseDef, defIV);
                float staStat = getStat(level, baseSta, staIV);

                int cp = getCP(atkStat, defStat, staStat, level);
                if (league < cp) return bestPerformance;

                IVPerformance currentPerformance = new()
                {
                    iVS = new IV()
                    {
                        atkIV = atkIV,
                        defIV = defIV,
                        staIV = staIV
                    },
                    level = level,
                    cP = cp,
                    stats = new stat()
                    {
                        atkStat = atkStat,
                        defStat = defStat,
                        staStat = (float)Math.Floor(staStat)
                    },
                    statProduct = atkStat * defStat * (float)Math.Floor(staStat)
                };
                bestPerformance = currentPerformance;
            }
            return bestPerformance;
        }

        private int getCP(float atk, float def, float sta, float level)
        {
            return (int)(Math.Floor(atk * Math.Sqrt(def)* Math.Sqrt(sta))/10);
        }

        private float getStat(float level, int baseInt, int iv)
        {
            return (baseInt + iv) * CPMultiplyer[level];
        }

        private readonly Dictionary<float, float> CPMultiplyer = new()
        {
            {1f,    0.094f},
            {1.5f,  0.135137432f},
            {2f,    0.16639787f},
            {2.5f,  0.192650919f},
            {3f,    0.21573247f},
            {3.5f,  0.236572661f},
            {4f,    0.25572005f},
            {4.5f,  0.273530381f},
            {5f,    0.29024988f},
            {5.5f,  0.306057377f},
            {6f,    0.3210876f},
            {6.5f,  0.335445036f},
            {7f,    0.34921268f},
            {7.5f,  0.362457751f},
            {8f,    0.37523559f},
            {8.5f,  0.387592406f},
            {9f,    0.39956728f},
            {9.5f,  0.411193551f},
            {10f,   0.42250001f},
            {10.5f, 0.432926419f},
            {11f,   0.44310755f},
            {11.5f, 0.4530599578f},
            {12f,   0.46279839f},
            {12.5f, 0.472336083f},
            {13f,   0.48168495f},
            {13.5f, 0.4908558f},
            {14f,   0.49985844f},
            {14.5f, 0.508701765f},
            {15f,   0.51739395f},
            {15.5f, 0.525942511f},
            {16f,   0.53435433f},
            {16.5f, 0.542635767f},
            {17f,   0.55079269f},
            {17.5f, 0.558830576f},
            {18f,   0.56675452f},
            {18.5f, 0.574569153f},
            {19f,   0.58227891f},
            {19.5f, 0.589887917f},
            {20f,   0.59740001f},
            {20.5f, 0.604818814f},
            {21f,   0.61215729f},
            {21.5f, 0.619399365f},
            {22f,   0.62656713f},
            {22.5f, 0.633644533f},
            {23f,   0.64065295f},
            {23.5f, 0.647576426f},
            {24f,   0.65443563f},
            {24.5f, 0.661214806f},
            {25f,   0.667934f},
            {25.5f, 0.674577537f},
            {26f,   0.68116492f},
            {26.5f, 0.687680648f},
            {27f,   0.69414365f},
            {27.5f, 0.700538673f},
            {28f,   0.70688421f},
            {28.5f, 0.713164996f},
            {29f,   0.71939909f},
            {29.5f, 0.725571552f},
            {30f,   0.7317f},
            {30.5f, 0.734741009f},
            {31f,   0.73776948f},
            {31.5f, 0.740785574f},
            {32f,   0.74378943f},
            {32.5f, 0.746781211f},
            {33f,   0.74976104f},
            {33.5f, 0.752729087f},
            {34f,   0.75568551f},
            {34.5f, 0.758630378f},
            {35f,   0.76156384f},
            {35.5f, 0.764486065f},
            {36f,   0.76739717f},
            {36.5f, 0.770297266f},
            {37f,   0.7731865f},
            {37.5f, 0.776064962f},
            {38f,   0.77893275f},
            {38.5f, 0.781790055f},
            {39f,   0.78463697f},
            {39.5f, 0.787473578f},
            {40f,   0.79030001f},
            {40.5f, 0.792803968f},
            {41f,   0.79530001f},
            {41.5f, 0.797800015f},
            {42f,   0.8003f},
            {42.5f, 0.802799995f},
            {43f,   0.8053f},
            {43.5f, 0.8078f},
            {44f,   0.81029999f},
            {44.5f, 0.812799985f},
            {45f,   0.81529999f},
            {45.5f, 0.81779999f},
            {46f,   0.82029999f},
            {46.5f, 0.82279999f},
            {47f,   0.82529999f},
            {47.5f, 0.82779999f},
            {48f,   0.83029999f},
            {48.5f, 0.83279999f},
            {49f,   0.83529999f},
            {49.5f, 0.83779999f},
            {50f,   0.84029999f},
            {50.5f, 0.84279999f},
            {51f,   0.84529999f},
        };

    }
}

