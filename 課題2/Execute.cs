using System;
using System.Collections.Generic;
using System.Linq;

namespace Data_practice
{
    /// <summary>
    /// パラメータ計算および表示をするクラス
    /// </summary>
    public class Execute
    {
        /// <summary>
        /// 各パラメータの計算および表示
        /// </summary>
        /// <param name="People">全人物リスト</param>
        /// <param name="companyName">会社名</param>
        public static void OutputResult(List<Person> People, string companyName)
        {
            // 会社ごとのリスト生成
            var CompanyPeople = CreateCompanyPeople(People, companyName);
            ShowCompanyName(companyName);

            string str = "平均値";
            var value = CalcMoney(CompanyPeople, str);
            ShowMoney(value, str);

            string str1 = "最大値";
            var value1 = CalcMoney(CompanyPeople, str1);
            ShowMoney(value1, str1);
            ShowName(SetTargetPeople(CompanyPeople, value1), str1);

            string str2 = "最小値";
            var value2 = CalcMoney(CompanyPeople, str2);
            ShowMoney(value2, str2);
            ShowName(SetTargetPeople(CompanyPeople, value2), str2);
        }

        /// <summary>
        /// 会社ごとの人物リストを生成する
        /// </summary>
        /// <param name="People">全人物リスト</param>
        /// <param name="companyName">会社名</param>
        /// <returns>会社ごとの人物リスト</returns>
        private static List<Person> CreateCompanyPeople(List<Person> People, string companyName)
             => People.Where(p => p.company == companyName).ToList();


        /// <summary>
        /// 会社名をコンソールに出力する
        /// </summary>
        /// <param name="companyName">会社名</param>
        private static void ShowCompanyName(string companyName)
            => Console.WriteLine(companyName);

        /// <summary>
        /// 引数に応じたパラメータを計算する
        /// </summary>
        /// <param name="People">人物リスト</param>
        /// <param name="type">パラメータの種類</param>
        /// <returns>パラメータ計算結果</returns>
        private static int CalcMoney(List<Person> People, string type)
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
        /// <param name="resultValue">パラメータ計算結果</param>
        /// <param name="type">パラメータの種類</param>
        private static void ShowMoney(int resultValue, string type)
            => Console.WriteLine("お小遣いの" + type + "：" + resultValue);

        /// <summary>
        /// 計算結果に一致する人物情報をリストに格納
        /// </summary>
        /// <param name="People">人物リスト</param>
        /// <param name="resultValue">パラメータ計算結果</param>
        /// <returns>パラメータ計算結果に一致する人物リスト</returns>
        private static List<Person> SetTargetPeople(List<Person> People, int resultValue)
            => People.Where(p => p.money == resultValue).ToList();

        /// <summary>
        /// 名前をコンソールに出力する
        /// </summary>
        /// <param name="targetPeople">パラメータ結果に一致する人物リスト</param>
        /// <param name="type">パラメータの種類</param>
        private static void ShowName(List<Person> targetPeople, string type)
        {
            Console.WriteLine("お小遣いが" + type + "の人:");
            foreach (var p in targetPeople)
            {
                Console.WriteLine(p.name);
            }
        }

    }
}