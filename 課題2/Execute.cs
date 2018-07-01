using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_practice
{
    public class Execute
    {
        public static void OutputResult(List<Person> People, string companyName)
        {   
            // 会社ごとのリスト生成
            var targetPeople = makePerCompany(People, companyName);
            // 会社名を表示する
            ShowCompanyName(companyName);

            // 平均値を計算する
            string str = "平均値";
            // 平均値を表示する
            var value = CalcMoney(targetPeople, str);
            ShowMoney(value, str);

            // 最大値を計算する
            string str1 = "最大値";
            // 最大値を表示する
            var value1 = CalcMoney(targetPeople, str1);
            ShowMoney(value1, str1);
            ShowName(SetTargetPeople(targetPeople, value1), str1);
            
            // 最小値を計算する
            string str2 = "最小値";
            // 最小値を表示する
            var value2 = CalcMoney(targetPeople, str2);
            ShowMoney(value2, str2);
            ShowName(SetTargetPeople(targetPeople, value2), str2);
        }

        /// <summary>
        /// 会社ごとのリストを生成する
        /// </summary>
        /// <param name="People"></param>
        /// <param name="companyName"></param>
        /// <returns></returns>
        private static List<Person> makePerCompany(List<Person> People, string companyName)
             => People.Where(p => p.company == companyName).ToList();
             
        
        // 会社名をコンソールに出力する
        private static void ShowCompanyName(string companyName)
            => Console.WriteLine(companyName);

        /// <summary>
        /// 引数に応じた値を計算する
        /// </summary>
        /// <param name="People"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static int CalcMoney(List<Person> People, string type)
        {
            switch (type)
            {
                case "平均値":
                    return (int)People.Average(x => x.money);

                case "最大値":
                    return People.Max(x => x.money);

                case "最小値":
                    return People.Min(x => x.money);
                default:
                    throw new ArgumentException("型指定が正しくありません");
            }
        }

        /// <summary>
        /// 格納した値をコンソールに出力する
        /// </summary>
        /// <param name="targetPeople"></param>
        /// <param name="type"></param>
        public static void ShowMoney(int value, string type)
            => Console.WriteLine("お小遣いの" + type + "：" + value);

        /// <summary>
        /// お小遣いの金額に一致する人物情報をリストに格納
        /// </summary>
        /// <param name="People"></param>
        /// <param name="targetValue"></param>
        /// <returns></returns>
        public static List<Person> SetTargetPeople(List<Person> People, int targetValue)
            => People.Where(p => p.money == targetValue).ToList();

        /// <summary>
        /// 名前をコンソールに出力する
        /// </summary>
        /// <param name="targetPeople"></param>
        /// <param name="type"></param>
        public static void ShowName(List<Person> targetPeople, string type)
        {
            Console.WriteLine("お小遣いが" + type + "の人:");
            foreach (var p in targetPeople)
            {
                Console.WriteLine(p.name);
            }
        }

    }
}