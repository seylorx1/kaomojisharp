using System;
using KaomojiSharp;

namespace KaomojiSharpExample {
    class Program {
        static void Main(string[] args) {
            //You will need a console font which can display unicode for this to work.
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            while (Console.ReadLine() != "exit") {

                KaomojiFlags flags = new KaomojiFlags(
                            KaomojiFlags.Category.Joy,
                            KaomojiFlags.Category.Love);

                Kaomoji happyKaomoji = Kaomoji.GetRandom(Kaomoji.RegistryFilter.AllowOnly, flags);

                Kaomoji negativeKaomoji = Kaomoji.GetRandom(Kaomoji.RegistryFilter.AllowOnly, KaomojiFlags.Category.Negative);

                Console.WriteLine($"Happy: {happyKaomoji.Emoticon} Negative: {negativeKaomoji.Emoticon}");
            }
        }
    }
}
