namespace Data_practice
{
    /// <summary>
    /// 人物情報を格納するクラス
    /// </summary>
    public class Person
    {
        public int id { get; set; }
        public string name { get; set; }
        public int money { get; set; }
        public string company { get; set; }

        public Person(int id, string name, int money)
        {
            this.id = id;
            this.name = name;
            this.money = money;
            company = CheckCompany(id);
        }

        /// <summary>
        /// 社員番号の上3桁から会社名を判別する
        /// </summary>
        /// <param name="id">社員番号</param>
        /// <returns>会社名</returns>
        private string CheckCompany(int id)
        {
            int topThreeDigit = id / 100000;
            if (topThreeDigit == 128)
            {
                return "FFS";
            }
            else if (topThreeDigit == 100)
            {
                return "FF";
            }
            else
            {
                return "unknown";
            }
        }
    }
}
