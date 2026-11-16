using BaseLib.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Other;

namespace Remielle.Other
{
    public static class ReVoiceManager
    {
        static readonly string voicePath = "res://Remielle/voice/";

        static readonly string bigSkill = "大招";

        static readonly int bigSkillN = 3;

        static readonly string attackVoice = "攻";

        static readonly int attackVoiceN = 3;

        static readonly string attackVfx = "攻击音效";

        static readonly string InVoice = "入场";

        static readonly int InVoiceN = 2;

        static readonly string PhaseVoice = "相变";

        static readonly int PhaseVoiceN = 3;

        static readonly string changeVoice = "切人";

        static readonly int changeVoiceN = 3;

        static readonly string jinghongVoice = "惊鸿";

        static readonly int jinghongVoiceN = 2;

        private static readonly Random _rng = new();

        public static int GetRandomN(int n)
        {
            return _rng.Next(n);
        }

        public static string GetLanguage()
        {
            return ZZZConfig.VoiceLanguage == ZZZConfig.Language.Chinese ? "中" : "日";
        }

        public static void PlayBigSkillVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;
            int n = GetRandomN(bigSkillN)+1;
            ModAudio.PlaySound(new ModSound(
                voicePath + bigSkill + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlayAttackVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(attackVoiceN+2)+1;
            if(n<=attackVoiceN)
            ModAudio.PlaySound(new ModSound(
                voicePath + attackVoice + n + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlayAttackVfx()
        {
            ModAudio.PlaySound(new ModSound(
                voicePath + attackVfx + ".mp3",
                ModAudio.SoundType.Sfx), 12);
        }

        public static void PlayInVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(InVoiceN)+1;
            ModAudio.PlaySound(new ModSound(
                voicePath + InVoice + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 10);
        }

        public static void PlayPhaseVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(PhaseVoiceN) + 1;
            ModAudio.PlaySound(new ModSound(
                voicePath + PhaseVoice +n+ GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlayChangeVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(changeVoiceN)+1;
            ModAudio.PlaySound(new ModSound(
                voicePath + changeVoice + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }

        public static void PlayJinghongVoice()
        {
            if (!ZZZConfig.CharacterVoice) return;

            int n = GetRandomN(jinghongVoiceN)+1;
            ModAudio.PlaySound(new ModSound(
                voicePath + jinghongVoice + n + GetLanguage() + ".mp3",
                ModAudio.SoundType.Sfx), 7);
        }
    }
}
