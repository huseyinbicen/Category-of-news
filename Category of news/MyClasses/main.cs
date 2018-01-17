using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Category_of_news.MyClasses
{
    public class main
    {

        private float[,] matris; // new float[toolFile.MakaleSayisi(), 257];
        private string gelen_Metin;
        private int makaleAdet = 5;
        private int m_q = 5;
        private String kategori;
        List<String> ListKategori = new toolFile().getKatetori().ToList();

        public main(String str)
        {
            gelen_Metin = str;

            CreateMatrix();
            Min_Max_Normalizasyon();
            GettingDistance();
            methodResult2(GettingDistance());

        }

        private void CreateMatrix()
        {
            List<String> ListMakale = new toolFile().getMakale().ToList();
            List<String> ListKategori = new toolFile().getKatetori().ToList();
            int kategori = 0;
            matris = new float[ListMakale.Count + 1, 257];

            int count = 0;

            for (int i = 0; i < ListMakale.Count; i++)
            {
                int[] dizi = new toolText(ListMakale[i]).getList();
                for (int k = 0; k < dizi.Length; k++)
                {
                    matris[count, k] = dizi[k];
                }
                if (i % 10 == 0)
                {
                    kategori++;
                }
                matris[count, 256] = kategori;
                count++;
            }
            MessageBox.Show("Makaleler Matris'e Yazıldı.");


            int[] yeniDizi = new toolText(gelen_Metin).getList();
            for (int i = 0; i < yeniDizi.Length; i++)
            {
                matris[count, i] = yeniDizi[i];
            }

            //new toolFile().DosyayaYaz(matris);
        }

        private void Min_Max_Normalizasyon()
        {
            for (int i = 0; i < matris.GetLength(1)-1; i++)
            {
                List<float> gecici = new List<float>();
                for (int k = 0; k < matris.GetLength(0); k++)
                {
                    gecici.Add(matris[k, i]);
                }

                gecici = new min_max(gecici).GetList();

                for (int k = 0; k < matris.GetLength(0); k++)
                {
                    matris[k, i] = gecici[k];
                }
            }

            //new toolFile().DosyayaYaz(matris);
        }




        private Dictionary<int, double> GettingDistance()
        {
            double result = 0;
            Dictionary<int, double> dictionary = new Dictionary<int, double>();
            Dictionary<int, double> dictionary2 = new Dictionary<int, double>();
            List<int> listeNum = new List<int>();
            List<float> listGelen = new List<float>();

            for (int i = 0; i < matris.GetLength(1) - 1; i++)
            {
                listGelen.Add(matris[matris.GetLength(0) - 1, i]);
            }


            //with minkowski
            //for (int i = 0; i < matris.GetLength(0) - 1; i++)
            //{
            //    for (int k = 0; k < matris.GetLength(1) - 1; k++)
            //    {
            //        result += Math.Pow(Math.Abs(matris[i, k] - listGelen[k]), m_q);
            //    }
            //    result = Math.Pow(result, 1.0 / m_q);
            //    dictionary.Add(i, result);
            //    result = 0;
            //}



            //with öklit
            for (int i = 0; i < matris.GetLength(0) - 1; i++)
            {
                for (int k = 0; k < matris.GetLength(1) - 1; k++)
                {
                    result += Math.Pow(Math.Abs(matris[i, k] - listGelen[k]), 2.0);
                }
                result = Math.Sqrt(result);
                dictionary.Add(i, result);
                result = 0;
            }



            var items = from pair in dictionary
                        orderby pair.Value ascending
                        select pair;


            foreach (KeyValuePair<int, double> pair in items)
            {
                dictionary2.Add(pair.Key, pair.Value);
                listeNum.Add(pair.Key);
            }

            //return listeNum;
            return dictionary2;

        }

        private void methodResult2(Dictionary<int, double> dictionary)
        {
            Dictionary<String, double> dic = new Dictionary<String, double>();
            Dictionary<String, double> dic2 = new Dictionary<String, double>();

            for (int i = 0; i < ListKategori.Count; i++)
            {
                dic.Add(ListKategori[i],0);
            }

            for (int i = 0; i < makaleAdet; i++)
            {
                double skor = AgirlikliOylama(dictionary.Values.ElementAt(i));
                for (int k = 0; k < ListKategori.Count; k++)
                {
                    if (matris[dictionary.Keys.ElementAt(i), matris.GetLength(1) - 1] == k+1)
                    {
                        dic[ListKategori[k]] += skor;
                    }
                }

            }

            var items = from pair in dic
                        orderby pair.Value descending
                        select pair;

            foreach (KeyValuePair<String, double> pair in items)
            {
                dic2.Add(pair.Key, pair.Value);
                
            }

            kategori = dic2.Keys.ElementAt(0);
        }

        private double AgirlikliOylama(double x)
        {
            return 1.0 / Math.Pow(x, 2);
        }

        public String getKategori()
        {
            return kategori;
        }





    }
}
