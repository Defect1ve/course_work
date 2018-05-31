using System;
using System.Drawing;

namespace k_means2
{
    public partial class Clusterizing
    {
        double[][] rawData;
        double[][] means;
        double[] mean;
        double R;
        int numClusters;
        int checkedNumClusters;
        int[] clustering;
        bool clustered = false;


        //bool meansComputed = false;
        //Brush[] brushes = { Brushes.LimeGreen, Brushes.Blue, Brushes.Orange, Brushes.Red, Brushes.Violet, Brushes.Turquoise };


        // задаем набор случайных даных
        public void InitializeTuples(int c, string create)
        {
            if (create == "static")
            {
                rawData = new double[20][];
                rawData[0] = new double[] { 422.0, 225.0 };
                rawData[1] = new double[] { 223.0, 160.0 };
                rawData[2] = new double[] { 59.0, 110.0 };
                rawData[3] = new double[] { 66.0, 120.0 };
                rawData[4] = new double[] { 235.0, 150.0 };
                rawData[5] = new double[] { 467.0, 240.0 };
                rawData[6] = new double[] { 468.0, 200.0 };
                rawData[7] = new double[] { 470.0, 250.0 };
                rawData[8] = new double[] { 45.0, 130.0 };
                rawData[9] = new double[] { 466.0, 210.0 };
                rawData[10] = new double[] { 217.0, 190.0 };
                rawData[11] = new double[] { 245.0, 180.0 };
                rawData[12] = new double[] { 234.0, 170.0 };
                rawData[13] = new double[] { 450.0, 214.0 };
                rawData[14] = new double[] { 61.0, 110.0 };
                rawData[15] = new double[] { 50.0, 100.0 };
                rawData[16] = new double[] { 466.0, 225.0 };
                rawData[17] = new double[] { 67.0, 120.0 };
                rawData[18] = new double[] { 498.0, 205.0 };
                rawData[19] = new double[] { 75.0, 130.0 };
            }
            else if (create == "random")
            {
                rawData = new double[c][];
                Random random = new Random();
                for (int i = 0; i < rawData.Length; i++)
                {
                    rawData[i] = new double[] { random.Next(0, 750), random.Next(0, 350) };
                }
            }
        }

     
        // возращает массив кластеров для исходного набора данных
        public int[] Cluster()
        {
            numClusters = checkedNumClusters;
            means = new double[numClusters][]; //массив для центроидов
            for (int k = 0; k < numClusters; ++k)
                means[k] = new double[rawData[0].Length];
            bool changed = true; // были изменения в принадлежности к кластеру
            bool success = true; // все точки приписаны к кластерам
            clustering = InitClustering(); // приписываем данным случайные кластеры
            clustered = true;
            while (changed == true && success == true)
            {
                System.Threading.Thread.Sleep(1500);
                success = UpdateMeans(); // вычисление нового центроида
                changed = UpdateClustering(); // вычисление ближайшего центроида для каждой точки
            }
            return clustering;
        }

        //приписываем каждую точку к случайному кластеру так, чтобы к каждому кластеру была приписана хотя бы одна точка
        private int[] InitClustering()
        {
            Random random = new Random();
            int[] clustering = new int[rawData.Length];
            for (int i = 0; i < numClusters; ++i)
                clustering[i] = i;
            for (int i = numClusters; i < clustering.Length; ++i)
                clustering[i] = random.Next(0, numClusters);
            return clustering;
        }

        // обновление значений центроидов
        private bool UpdateMeans()
        {
            int[] clusterCounts = new int[numClusters]; //количество точек по кластерам
            for (int i = 0; i < rawData.Length; ++i)
            {
                int cluster = clustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // проверка - к каждому кластеру должны быть приписаны точки

            for (int k = 0; k < means.Length; ++k) //обнуление центроидов
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] = 0.0;

            for (int i = 0; i < rawData.Length; ++i)
            {
                int cluster = clustering[i];
                for (int j = 0; j < rawData[i].Length; ++j)
                    means[cluster][j] += rawData[i][j]; // записывается сумма координат точек, приписанных кластеру
            }

            for (int k = 0; k < means.Length; ++k)
                for (int j = 0; j < means[k].Length; ++j)
                    means[k][j] /= clusterCounts[k]; // находим среднее 
            //meansComputed = true;
            return true;
        }

        // обновление распределений по кластерам
        private bool UpdateClustering()
        {
            bool changed = false;
            int[] newClustering = new int[clustering.Length]; // массив для нового результата
            Array.Copy(clustering, newClustering, clustering.Length);
            double[] distances = new double[numClusters]; // расстояние от точки до центроидов
            for (int i = 0; i < rawData.Length; ++i) // рассчитываем расстоение до цетроидов для всех точек
            {
                for (int k = 0; k < numClusters; ++k)
                    distances[k] = Distance(rawData[i], means[k]);

                int newClusterID = MinIndex(distances); // находим ближайший центроид
                if (newClusterID != newClustering[i])
                {
                    changed = true;
                    newClustering[i] = newClusterID;
                }
            }

            if (changed == false) // если изменений в распределении точек по кластерам нет - выход
                return false;

            int[] clusterCounts = new int[numClusters];
            for (int i = 0; i < rawData.Length; ++i)
            {
                int cluster = newClustering[i];
                ++clusterCounts[cluster];
            }

            for (int k = 0; k < numClusters; ++k)
                if (clusterCounts[k] == 0)
                    return false; // к каждому кластеру должны быть приписаны точки

            Array.Copy(newClustering, clustering, newClustering.Length);  // копируем полученный массив в исходный
            return true; // если внесены изменеия
        }

        //разница между векторами точки и центроида
        private double Distance(double[] tuple, double[] mean)
        {
            double sumSquaredDiffs = 0.0;
            for (int j = 0; j < tuple.Length; ++j)
                sumSquaredDiffs += Math.Pow((tuple[j] - mean[j]), 2);
            return Math.Sqrt(sumSquaredDiffs);
        }

        private int MinIndex(double[] distances)
        {
            int indexOfMin = 0;
            double smallDist = distances[0];
            for (int k = 0; k < distances.Length; ++k)
            {
                if (distances[k] < smallDist)
                {
                    smallDist = distances[k];
                    indexOfMin = k;
                }
            }
            return indexOfMin;
        }


        public int[] Cluster2()
        {
            numClusters = checkedNumClusters;
            int r;
            int help;
            int cluster;
            cluster = 1;
            clustering = InitClustering(); // приписываем данным случайные кластеры
            Random rand = new Random();
            for (int i = 0; i < rawData.Length; ++i)
                clustering[i] = 0;
            mean = new double[rawData[0].Length];
            clustered = true;
            help = 0;
            while (EmptyCluster())
            {
                r = 1;
                while (r != 0)
                    r = clustering[help = rand.Next(0, rawData.Length)];
                mean = rawData[help];
                while (true)
                {
                    clustering = InitCluster(cluster);
                    if (UpdateMean(clustering))
                        break ;
                }
                System.Threading.Thread.Sleep(1500);
            }
            return clustering;
        }

        private bool EmptyCluster()
        {
            for (int i = 0; i < rawData.Length; ++i)
                if (clustering[i] == 0)
                    return true;
            return false;
        }

        private bool UpdateMean(int[] clustering)
        {
            int sum;
            double[] temp;

            temp = new double[mean.Length];
            for (int k = 0; k < mean.Length; ++k)
                temp[k] = mean[k];
            sum = 0;
            for (int j = 0; j < rawData[0].Length; ++j)
                mean[j] = 0;
            for (int i = 0; i < rawData.Length; ++i)
            {
                if (clustering[i] != 0)
                {
                    for (int j = 0; j < rawData[i].Length; ++j)
                        mean[j] += rawData[i][j];
                    sum++;
                }
            }
            for (int i = 0; i < rawData[0].Length; ++i)
                mean[i] /= sum;
            for (int s = 0; s < mean.Length; ++s)
                if (mean[s] != temp[s])
                    return false;
            return true;
        }
      
        private int[] InitCluster(int cluster)
        {
            R = 5;
            int count;
            count = 0;
            int[] clustering = new int[rawData.Length];
            for (int i = 0; i < rawData.Length; ++i)
            {
                if (Distance(rawData[i], mean) < R)
                    clustering[i] = cluster;
                else
                    clustering[i] = 0;
            }
            for (int i = 0; i < rawData.Length; ++i)
            {
                if (clustering[i] != 0)
                    count++;
            }
            if (count == 0)
            {
                R += 5;
                clustering = InitCluster(cluster);
            }
            return clustering;
        }
    }
}

