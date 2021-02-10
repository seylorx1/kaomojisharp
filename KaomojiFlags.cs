using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KaomojiSharp {
    public class KaomojiFlags : ICloneable {
        /// <summary>
        /// The Category enum represents the emotions shared by Kaomoji.
        /// (At least, the Kaomoji added to this library by default.)
        /// </summary>
        public enum Category {
            /// <summary>
            /// Note: this category is broad.
            /// </summary>
            Positive = 0,

            /// <summary>
            /// Note: this category is broad.
            /// </summary>
            Neutral = 1,

            /// <summary>
            /// Note: this category is broad.
            /// </summary>
            Negative = 2,

            Joy             = 3,
            Love            = 4,
            Embarrassment   = 5,
            Sympathy        = 6,

            Dissatisfaction = 7,
            Anger           = 8,
            Sadness         = 9,
            Pain            = 10,
            Fear            = 11,

            Indifference    = 12,
            Confusion       = 13,
            Doubt           = 14,
            Surprise        = 15
        }

        private BitArray bits;

        /// <summary>
        /// Flags used to filter Kaomoji.
        /// </summary>
        /// <param name="list">A parameter list of categories.</param>
        public KaomojiFlags(params Category[] list) {
            if(list == null) {
                throw new NullReferenceException("Parameter list cannot be null!");
            }

            bits = new BitArray(Enum.GetValues(typeof(Category)).Length, false);


            //Set the bits.
            foreach (Category category in list) {
                bits.Set((int)category, true);
            }
        }

        private KaomojiFlags(BitArray bits) {
            this.bits = new BitArray(bits);
        }

        public object Clone() {
            return new KaomojiFlags(bits);
        }

        /// <param name="category">Category to compare to.</param>
        /// <returns>Contains the category enum value.</returns>
        public bool ContainsFlag(Category category) {
            return bits.Get((int)category);
        }

        /// <summary>
        /// Checks to see if both KaomojiFlags share a flag.
        /// </summary>
        /// <param name="flags">Flags to compare to.</param>
        /// <returns>A flag is found to be true in both KaomojiFlags.</returns>
        public bool ContainsFlag(KaomojiFlags flags) {
            for (int i = 0; i < bits.Length; i++) {
                if (bits.Get(i) && flags.bits.Get(i)) {
                    return true;
                }
            }
            return false;
        }
    }
}
