using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Course_Work_Vikhlyayeva_Kotvitskiy.Models
{
    public class Result
    {
        public int[] coordinates;
        public int[] clasters;

        public Result(int[] c, int[]cl)
        {
            coordinates = c;
            clasters = cl;
        }
    }
}