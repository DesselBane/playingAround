using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Company.Mixin
{
    public class ArraySequenceWithMixin : ArraySequence, IMixinRequires
    {
        public IMixin IMixin
        {
            get => default(IMixin);
            set
            {
            }
        }
    }
}