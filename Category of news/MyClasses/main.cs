using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Category_of_news.MyClasses
{
    public class main
    {

        private float[,] matris; // new float[toolFile.MakaleSayisi(), 257];
        private string gelen_Metin;

        public main(String str)
        {
            gelen_Metin = str;

            CreateMatrix();
            Min_Max_Normalizasyon();
            methodResult2(GettingDistance());

        }

        private void methodResult2(object v)
        {
            throw new NotImplementedException();
        }

        private object GettingDistance()
        {
            throw new NotImplementedException();
        }

        private void Min_Max_Normalizasyon()
        {
            throw new NotImplementedException();
        }

        private void CreateMatrix()
        {
            throw new NotImplementedException();
        }
    }
}
