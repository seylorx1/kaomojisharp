//KaomojiSharp. Version 0.1.
//Copyright 2021, Lyes Oussaiden (seylorx1)

/* MIT LICENSE
 * Permission is hereby granted, free of charge,
 * to any person obtaining a copy of this software and
 * associated documentation files (the "Software"),
 * to deal in the Software without restriction,
 * including without limitation the rights to
 * use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished
 * to do so, subject to the following conditions:
 
 * The above copyright notice and this permission notice
 * shall be included in all copies or substantial portions of the Software.
 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
 * DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using System;
using System.Collections.Generic;

namespace KaomojiSharp {

    public class Kaomoji : ICloneable {

        /// <summary>
        /// The kaomoji emoticon text.
        /// </summary>
        public string Emoticon { get; private set; }

        /// <summary>
        /// Identifier flags.
        /// </summary>
        public KaomojiFlags Flags { get; private set; }

        public Kaomoji(string emoticon, KaomojiFlags flags) {
            Emoticon = emoticon;
            Flags = flags;
        }
        public object Clone() {
            return new Kaomoji(Emoticon, (KaomojiFlags)Flags.Clone());
        }

        /// <returns>Emoticon. (Can also be accessed via Kaomoji.Emoticon)</returns>
        public override string ToString() {
            return Emoticon;
        }

        #region Kaomoji Registry
        private static List<Kaomoji> Registry = null;

        public enum RegistryFilter {
            AllowOnly,
            DenyOnly
        }

        public Kaomoji Register() {
            if (Registry == null) {
                Registry = new List<Kaomoji>();
            }

            Registry.Add(this);

            return this;
        }
        #endregion

        #region Get Kaomojis
        /// <summary>
        /// Returns a deep-copy list of all kaomojis.
        /// </summary>
        public static List<Kaomoji> GetKaomojis() {
            //Load data if not already loaded.
            if (!KaomojiDataHandler.IsLoaded) {
                KaomojiDataHandler.Load();
            }

            List<Kaomoji> regCopy = new List<Kaomoji>(Registry.Count);

            Registry.ForEach((item) => {
                regCopy.Add((Kaomoji)item.Clone());
            });
            return regCopy;
        }

        /// <summary>
        /// Returns a deep-copy list of filtered kaomojis from flags.
        /// </summary>
        public static List<Kaomoji> GetKaomojis(RegistryFilter filter, KaomojiFlags flags) {
            
            //Load data if not already loaded.
            if (!KaomojiDataHandler.IsLoaded) {
                KaomojiDataHandler.Load();
            }

            List<Kaomoji> filterCopy = new List<Kaomoji>();

            foreach (Kaomoji kaomoji in Registry) {

                if (filter == RegistryFilter.AllowOnly) {
                    if (!kaomoji.Flags.ContainsFlag(flags))
                        continue;
                }
                //if(filter == RegistryFilter.Exclude)
                //TODO change this if more filters are added!
                else {
                    if (kaomoji.Flags.ContainsFlag(flags))
                        continue;
                }
                filterCopy.Add((Kaomoji)kaomoji.Clone());
            }

            return filterCopy;
        }

        /// <summary>
        /// Returns a deep-copy list of filtered kaomojis from a single category.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static List<Kaomoji> GetKaomojis(RegistryFilter filter, KaomojiFlags.Category flag) {
            
            //Load data if not already loaded.
            if (!KaomojiDataHandler.IsLoaded) {
                KaomojiDataHandler.Load();
            }

            List<Kaomoji> filterCopy = new List<Kaomoji>();

            foreach (Kaomoji kaomoji in Registry) {

                if (filter == RegistryFilter.AllowOnly) {
                    if (!kaomoji.Flags.ContainsFlag(flag))
                        continue;
                }
                //if(filter == RegistryFilter.Exclude)
                //TODO change this if more filters are added!
                else {
                    if (kaomoji.Flags.ContainsFlag(flag))
                        continue;
                }
                filterCopy.Add((Kaomoji)kaomoji.Clone());
            }

            return filterCopy;
        }
        #endregion

        #region Get Random Kaomoji
        /// <summary>
        /// Clones a random kaomoji.
        /// </summary>
        public static Kaomoji GetRandom() {
            return GetRandom(true);
        }

        private static Kaomoji GetRandom(bool clone) {
            //Load data if not already loaded.
            if (!KaomojiDataHandler.IsLoaded) {
                KaomojiDataHandler.Load();
            }

            //Find a random value in the registry.
            Random rnd = new Random();
            Kaomoji k = Registry[rnd.Next(0, Registry.Count)];

            if (clone) {
                k = (Kaomoji)k.Clone();
            }

            return k;
        }

        /// <summary>
        /// Finds a random Kaomoji with the specified filter parameters.
        /// </summary>
        public static Kaomoji GetRandom(RegistryFilter filter, KaomojiFlags flags) {
            Kaomoji kaomoji = GetRandom(false);

            if (filter == RegistryFilter.AllowOnly) {
                while (!kaomoji.Flags.ContainsFlag(flags)) { kaomoji = GetRandom(false); }
            }
            //if(filter == RegistryFilter.Exclude)
            //TODO change this if more filters are added!
            else {
                while (kaomoji.Flags.ContainsFlag(flags)) { kaomoji = GetRandom(false); }
            }
            return (Kaomoji)kaomoji.Clone();
        }

        /// <summary>
        /// Finds a random Kaomoji with the specified filter parameters.
        /// </summary>
        public static Kaomoji GetRandom(RegistryFilter filter, KaomojiFlags.Category flag) {
            Kaomoji kaomoji = GetRandom(false);

            if (filter == RegistryFilter.AllowOnly) {
                while (!kaomoji.Flags.ContainsFlag(flag)) { kaomoji = GetRandom(false); }
            }
            //if(filter == RegistryFilter.Exclude)
            //TODO change this if more filters are added!
            else {
                while (kaomoji.Flags.ContainsFlag(flag)) { kaomoji = GetRandom(false); }
            }
            return (Kaomoji)kaomoji.Clone();
        }
        #endregion
    }
}
