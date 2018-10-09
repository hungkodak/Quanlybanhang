using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlybanhang.Scripts.Source.Defination
{
    public enum ProductSize
    {
        XS = 0,
        S = 1,
        M = 2,
        L = 3,
        XL = 4,
        XXL = 5,
        XXXL = 6
    }

    [Serializable]
    public class ProductSizeContract
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}