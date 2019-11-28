using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KorCharMaker
{
    class KorCharMake
    {
        const int BASE_CODE = 0xAC00;
        const int BASE_INIT = 0x1100;
        const int BASE_VOWEL = 0x1161;
        static string chostring = "rRseEfaqQtTdwWczxvg";
        static string chostring_k = "ㄱㄲㄴㄷㄸㄹㅁㅂㅃㅅㅆㅇㅈㅉㅊㅋㅌㅍㅎ";
        static string[] jungstring = new string[] 
        { "k", "o", "i", "O", "j", "p", "u", "P", "h", "hk", "ho",
            "hl", "y", "n", "nj", "np", "nl", "b", "m", "ml", "l" };
        static string[] jungstring_k = new string[] 
        { "ㅏ", "ㅐ", "ㅑ", "ㅒ", "ㅓ", "ㅔ", "ㅕ", "ㅖ", "ㅗ", "ㅘ", "ㅙ",
            "ㅚ", "ㅛ", "ㅜ", "ㅝ", "ㅞ", "ㅟ", "ㅠ", "ㅡ", "ㅢ", "ㅣ" };
        static string[] jongstring = new string[] 
        { string.Empty, "r", "R", "rt", "s", "sw", "sg", "e", "f", "fr", "fa", "fq", "ft",
            "fx", "fv", "fg", "a", "q", "qt", "t", "T", "d", "w", "c", "z", "x", "v", "g" };
        static string[] jongstring_k = new string[]
        { string.Empty, "ㄱ", "ㄲ", "ㄳ", "ㄴ", "ㄵ", "ㄶ", "ㄷ", "ㄹ", "ㄺ", "ㄻ", "ㄼ", "ㄽ",
            "ㄾ", "ㄿ", "ㅀ", "ㅁ", "ㅂ", "ㅄ", "ㅅ", "ㅆ", "ㅇ", "ㅈ", "ㅊ", "ㅋ", "ㅌ", "ㅍ", "ㅎ" };

        public static int GetInitSoundCode(char ch)
        {
            int index = chostring_k.IndexOf(ch);
            if (index != -1) return index;
            return chostring.IndexOf(ch);
        }

        public static int GetVowelCode(string str)
        {
            int strlen = jungstring.Length;
            for (int i = 0; i < strlen; i++)
            {
                if (jungstring[i] == str) return i;
            }
            for (int i = 0; i < strlen; i++) 
            {
                if (jungstring_k[i] == str) return i;
            }
            return -1;
        }

        public static int GetVowelCode(char ch)
        {
            return GetVowelCode(ch.ToString());
        }

        public static int GetFinalConsonantCode(string str)
        {
            int strlen = jungstring.Length;
            for (int i = 0; i < strlen; i++)
            {
                if (jongstring[i] == str) return i;
            }
            for (int i = 0; i < strlen; i++)
            {
                if (jongstring_k[i] == str) return i;
            }
            return -1;
        }
        
        public static int GetFinalConsonantCode(char ch)
        {
            return GetFinalConsonantCode(ch.ToString());
        }

        public static char GetSingleConsonant(int value)
        {
            byte[] bytes = BitConverter.GetBytes((short)(BASE_INIT + value));
            return Char.Parse(Encoding.Unicode.GetString(bytes));
        }

        public static char GetSingleVowel(int value)
        {
            byte[] bytes = BitConverter.GetBytes((short)(BASE_VOWEL + value));
            return Char.Parse(Encoding.Unicode.GetString(bytes));
        }

        public static char GetCompleteChar(int init_sound, int vowel, int final)
        {
            int FinalConsonant = 0;
            if (final >= 0)
            {
                FinalConsonant = final;
            }
            int jungcnt = jungstring.Length;
            int jongcnt = jongstring.Length;
            int completeChar =
                (init_sound * jungcnt * jongcnt) +
                (vowel * jongcnt) +
                (FinalConsonant) + BASE_CODE;
            byte[] bytes = BitConverter.GetBytes((short)(completeChar));
            return Char.Parse(Encoding.Unicode.GetString(bytes));
        }

        public static char GetHangle(char ic, char vc, char fc)
        {
            int init_sound = GetInitSoundCode(ic);
            int vowel = GetVowelCode(vc);
            int final = GetFinalConsonantCode(fc);
            return GetCompleteChar(init_sound, vowel, final);
        }
    }
}
